using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using Zamba.ThreadPool;
using Zamba.ThreadPool.Core;

namespace Zamba.Insert.Components
{
   public class ThreadPoolService
    {

        private SmartThreadPool _smartThreadPool;

        public SmartThreadPool TPSSmartThreadPool
        {
            get { return _smartThreadPool; }
            set { _smartThreadPool = value; }
        }
        private IWorkItemsGroup _workItemsGroup;

        public IWorkItemsGroup TPSWorkItemsGroup
        {
            get { return _workItemsGroup; }
            set { _workItemsGroup = value; }
        }
        private bool running = false;

        public bool Running
        {
            get { return running; }
            set { running = value; }
        }
        private long workItemsGenerated;

        public long WorkItemsGenerated
        {
            get { return workItemsGenerated; }
            set { workItemsGenerated = value; }
        }
        private long workItemsCompleted;

        public long WorkItemsCompleted
        {
            get { return workItemsCompleted; }
            set { workItemsCompleted = value; }
        }
        private Thread workItemsProducerThread;
        private Decimal Interval;

        public void StartThreadService(Decimal MaxThreads, Decimal MinThreads, Decimal pInterval)
        {
            this.Interval = pInterval;
            STPStartInfo stpStartInfo = new STPStartInfo();
            
            stpStartInfo.MaxWorkerThreads = Convert.ToInt32(MaxThreads);
            stpStartInfo.MinWorkerThreads = Convert.ToInt32(MinThreads);
            stpStartInfo.PerformanceCounterInstanceName = "Test Zamba.ThreadPool";

            _smartThreadPool = new SmartThreadPool(stpStartInfo);

            //_workItemsGroup = _smartThreadPool.CreateWorkItemsGroup(1);
            _workItemsGroup = _smartThreadPool;

            workItemsProducerThread = new Thread(new ThreadStart(this.WorkItemsProducer));
            workItemsProducerThread.IsBackground = true;
            workItemsProducerThread.Start();
        }

        public void StopThreadService()
        {
            Running = false;
            workItemsProducerThread.Join();

            _smartThreadPool.Shutdown();
            _smartThreadPool.Dispose();
            _smartThreadPool = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
          
        }
        private void WorkItemsProducer()
        {
            WorkItemCallback workItemCallback = new WorkItemCallback(this.DoWork);
            Zamba.Insert.Components.InsertComponent IC = new Zamba.Insert.Components.InsertComponent();
            IC.OnStart();
            while (running)
            {
                IWorkItemsGroup workItemsGroup = _workItemsGroup;
                if (null == workItemsGroup)
                {
                    return;
                }

                try
                {
                    if (IC.HandleDocumentsToInsert(workItemsGenerated + 1) > 0)
                    {
                        workItemCallback = new WorkItemCallback(this.DoWork);
                        workItemsGroup.QueueWorkItem(workItemCallback, workItemsGenerated + 1);
                        workItemsGenerated++;
                    }
                }
                catch (ObjectDisposedException e)
                {
                    e.GetHashCode();
                    break;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }

                Thread.Sleep(Convert.ToInt32(Interval));
            }
        }

        private object DoWork(object WorkItem)
        {
            try
            {
                Zamba.Insert.Components.InsertComponent IC = new Zamba.Insert.Components.InsertComponent();
                IC.InsertDocuments(Int64.Parse(WorkItem.ToString()));
                Interlocked.Increment(ref workItemsCompleted);
                return null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }


    }
}
