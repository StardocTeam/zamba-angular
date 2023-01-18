using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Tesseract;
using Zamba;
using static Tesseract.ZambaOCR;
using static WindowsFormsApplication2.Main;

namespace WindowsFormsApplication2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            string status = null;
            DBBusiness.InitializeSystem(ObjectTypes.Services, null, false, ref status, null);

        }


        ListViewGroup OkGroup = new ListViewGroup("OK");
        ListViewGroup ErrorGroup = new ListViewGroup("Error");

        List<DirectoryInfo> Directories = new List<DirectoryInfo>();


        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.RootFolder = Environment.SpecialFolder.MyComputer;

            if (fb.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = fb.SelectedPath;
            }

        }

        List<string> newFiles = new List<string>();

        private void ConvertDirectory(DirectoryInfo dir)
        {
            var files = dir.GetFiles("*.mag", SearchOption.AllDirectories);
            foreach (FileInfo fi in files)
            {
                try
                {
                    newFiles.Add(fi.Directory.FullName + "\\" + fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + ".pdf");
                    if (File.Exists(fi.Directory.FullName + "\\" + fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + ".pdf") == false)
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                        process.StartInfo.FileName = Application.StartupPath + "\\nconvert.exe";
                        process.StartInfo.Arguments = string.Format(" -multi -c 1 -o \"{0}\" -out pdf \"{1}\"", fi.Directory.FullName + "\\" + fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + ".pdf", fi.FullName);
                        process.Start();
                        ListViewItem li = new ListViewItem(fi.FullName, OkGroup);
                        this.listView1.Items.Add(li);
                        Update();
                    }
                }
                catch (Exception ex)
                {
                    ListViewItem li = new ListViewItem(fi.FullName, ErrorGroup);
                    li.ToolTipText = ex.Message;
                    this.listView1.Items.Add(li);
                    Update();
                }

            }


            foreach (string file in newFiles)
            {
                try
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Exists == true)
                    {
                        SpireOCR.ExtractImages(fi.FullName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            MessageBox.Show("Conversion Finalizada");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.listView1.Groups.Add(OkGroup);
            this.listView1.Groups.Add(ErrorGroup);


            if (looks.Count == 0)
            {
                LookUp AseguradoLookUp = new LookUp("Asegurado", "ASEGURADO:", string.Empty, 1, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado"));
                AseguradoLookUp.Conditions.Add(new Condition(ConditionsTypes.Len, "10", ConditionsObjects.Before, ConditionsActions.TryNextMethod, ConditionsObjects.Before));
                looks.Add(AseguradoLookUp);
                looks.Add(new LookUp("Direccion", "ASEGURADO:", string.Empty, 2, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado")));

                LookUp LocalidadLookUp = new LookUp("Localidad", "ASEGURADO:", "CP:", 3, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado"));
                LocalidadLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "False", ConditionsObjects.After, ConditionsActions.TakeAll, ConditionsObjects.After));
                looks.Add(LocalidadLookUp);

                looks.Add(new LookUp("CodigoPostal", "CP:", " ", 0, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado")));
                looks.Add(new LookUp("Provincia", "LookUp.CodigoPostal", string.Empty, 0, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado")));

                LookUp IVALookUp = new LookUp("IVA", "ASEGURADO:", "DNI", 4, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado"));
                IVALookUp.Conditions.Add(new Condition(ConditionsTypes.NonExists, "CUI", ConditionsObjects.After, ConditionsActions.TryNextMethod, ConditionsObjects.After));
                IVALookUp.Conditions.Add(new Condition(ConditionsTypes.NonExists, "UNI", ConditionsObjects.After, ConditionsActions.TryNextMethod, ConditionsObjects.After));
                IVALookUp.Conditions.Add(new Condition(ConditionsTypes.NonExists, "CONSUMIDOR FINAL", ConditionsObjects.Before, ConditionsActions.TryNextMethod, ConditionsObjects.Value));
                IVALookUp.Conditions.Add(new Condition(ConditionsTypes.NonExists, "RESPONSABLE INSCRIPTO", ConditionsObjects.Before, ConditionsActions.TryNextMethod, ConditionsObjects.Value));
                IVALookUp.Conditions.Add(new Condition(ConditionsTypes.NonExists, "MONOTRIBUTO", ConditionsObjects.Before, ConditionsActions.TryNextMethod, ConditionsObjects.Value));
                IVALookUp.Conditions.Add(new Condition(ConditionsTypes.NonExists, "CONSUMIDOR", ConditionsObjects.Before, ConditionsActions.TryNextMethod, ConditionsObjects.Value));
                looks.Add(IVALookUp);

                LookUp TIPODOCLookUp = new LookUp("TIPODOC", string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado"));
                TIPODOCLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "DNI", ConditionsObjects.Value, ConditionsActions.Find, ConditionsObjects.Value));
                TIPODOCLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "CUIL", ConditionsObjects.Value, ConditionsActions.Find, ConditionsObjects.Value));
                TIPODOCLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "CUIT", ConditionsObjects.Value, ConditionsActions.Find, ConditionsObjects.Value));
                TIPODOCLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "DNT", ConditionsObjects.Value, ConditionsActions.Find, ConditionsObjects.Value));
                TIPODOCLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "UNI", ConditionsObjects.Value, ConditionsActions.Find, ConditionsObjects.Value));
                TIPODOCLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "UNI", "DNI"));
                TIPODOCLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "DNT", "DNI"));
                looks.Add(TIPODOCLookUp);



                LookUp DOCUMENTOLookUp = new LookUp("DOCUMENTO", "LookUp.TIPODOC", string.Empty, 0, string.Empty, 0, string.Empty, new Rectangular(new Point(129, 657), new Point(1725, 931), "Datos de asegurado"));
                DOCUMENTOLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "CUIT:"));
                DOCUMENTOLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "CUIL:"));
                DOCUMENTOLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "DNI:"));
                DOCUMENTOLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "DNT:"));
                DOCUMENTOLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "UNI:"));

                DOCUMENTOLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, ".", string.Empty));
                DOCUMENTOLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, ",", string.Empty));
                DOCUMENTOLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, "*", string.Empty));
                DOCUMENTOLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, " ", string.Empty));
                DOCUMENTOLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "A", "4"));

                looks.Add(DOCUMENTOLookUp);


                //looks.Add(new LookUp("Matricula", "Matr.", "A.", 0, string.Empty, 2, string.Empty, new Rectangular(new Point(129,667), new Point(1725,931), "Datos de asegurado")));
                // looks.Add(new LookUp("FechaEmision", string.Empty, " ", 0, "/", -2, string.Empty));

                LookUp VigenciaDesdeLookUp = new LookUp("VigenciaDesde", string.Empty, " ", 0, string.Empty, 0, string.Empty, new Rectangular(new Point(957, 978), new Point(1627, 1127), "Vigencia"));
                VigenciaDesdeLookUp.Conditions.Add(new Condition(ConditionsTypes.DataType, "/", ConditionsObjects.Value, ConditionsActions.OnlyWordWithCharacter, ConditionsObjects.Value));
                VigenciaDesdeLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "u", "4"));
                VigenciaDesdeLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "ú", "4"));
                looks.Add(VigenciaDesdeLookUp);

                LookUp VigenciaHastaLookUp = new LookUp("VigenciaHasta", "LookUp.VigenciaDesde", " ", 0, string.Empty, 0, string.Empty, new Rectangular(new Point(957, 978), new Point(1627, 1127), "Vigencia"));
                VigenciaHastaLookUp.Conditions.Add(new Condition(ConditionsTypes.Always, string.Empty, ConditionsObjects.Line, ConditionsActions.Concat, ConditionsObjects.Line));
                VigenciaHastaLookUp.Conditions.Add(new Condition(ConditionsTypes.Exists, "False", ConditionsObjects.After, ConditionsActions.TakeAll, ConditionsObjects.After));
                VigenciaHastaLookUp.Conditions.Add(new Condition(ConditionsTypes.DataType, string.Empty, ConditionsObjects.Value, ConditionsActions.OnlyWordWithNumbers, ConditionsObjects.Value));
                VigenciaHastaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "u", "4"));
                VigenciaHastaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "ú", "4"));
                looks.Add(VigenciaHastaLookUp);

                LookUp PatenteHastaLookUp = new LookUp("Patente", "Patente:", string.Empty, 0, string.Empty, 0, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Patenle:"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Patenta:"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Patents:"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Patents;"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Patante:"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Paten:e:"));
                PatenteHastaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Patanc:"));


                looks.Add(PatenteHastaLookUp);


                looks.Add(new LookUp("TipoVehiculo", "Vehiculo:", " ", 0, string.Empty, 0, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion")));
                looks.Add(new LookUp("Marca", "Marca", string.Empty, 0, string.Empty, 1, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion")));


                LookUp CarroceriaLookUp = new LookUp("Carroceria", "Carroceria:", "Model", 0, string.Empty, 0, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion"));
                CarroceriaLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Carrocexía"));
                looks.Add(CarroceriaLookUp);


                LookUp ModeloLookUp = new LookUp("Modelo", "Modelo", string.Empty, 0, string.Empty, 6, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion"));
                ModeloLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "delo A"));
                looks.Add(ModeloLookUp);


                LookUp MotorLookUp = new LookUp("Motor", "otor: ", " ", 0, string.Empty, 0, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion"));
                MotorLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Motar:"));
                looks.Add(MotorLookUp);

                LookUp ChasisLookUp = new LookUp("Chasis", "Chasis: ", string.Empty, 0, string.Empty, 0, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion"));
                ChasisLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "Chasia:"));
                looks.Add(ChasisLookUp);

                LookUp UsoLookUp = new LookUp("Uso", "uso:", string.Empty, 0, string.Empty, 1, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion"));
                UsoLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "use:"));
                UsoLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "usa:"));
                UsoLookUp.Synonymous.Add(new Synonym(ConditionsObjects.Before, "uso"));
                looks.Add(UsoLookUp);

                looks.Add(new LookUp("Cobertura", "Cubiert", string.Empty, 0, string.Empty, 3, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion")));

                looks.Add(new LookUp("SumaAsegurada", "Asegurada Casco:", "Ajuste", 0, string.Empty, 1, string.Empty, new Rectangular(new Point(133, 1134), new Point(1705, 2081), "Descripcion")));

                LookUp PolizaLookUp = new LookUp("Poliza", string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, new Rectangular(new Point(1750, 770), new Point(2051, 931), "Nro Poliza"));
                PolizaLookUp.Conditions.Add(new Condition(ConditionsTypes.DataType, string.Empty, ConditionsObjects.Value, ConditionsActions.OnlyWordWithNumbers, ConditionsObjects.Value));
                PolizaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, ".", string.Empty));
                PolizaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, ",", string.Empty));
                PolizaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, "*", string.Empty));
                PolizaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Replace, "A", "4"));
                PolizaLookUp.PostActions.Add(new PostAction(ConditionsObjects.Value, ConditionsActions.Remove, " ", string.Empty));
                looks.Add(PolizaLookUp);

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(this.textBox1.Text);

            ConvertDirectory(dir);

        }


        static class Conf
        {
            public static int numWorkers = 20;
            public static bool debugMode = true;
        }

        class Util
        {
            public static void DEBUG(string format, params object[] args)
            {
                if (Conf.debugMode)
                {
                    Console.WriteLine(format, args);
                }
            }
        }


        /**
   * ThreadPool which contains consumers and threads in the list.
   */
        class ThreadPool
        {
            List<Consumer> consumers = new List<Consumer>();
            List<Thread> threads = new List<Thread>();

            public ThreadPool(WorkQueue q)
            {
                for (int i = 0; i < Conf.numWorkers; ++i)
                {
                    Consumer c = new Consumer(q);
                    consumers.Add(c);
                    threads.Add(new Thread(new ThreadStart(c.Start)));
                }
            }

            public void AddWorker(WorkQueue q)
            {
                Consumer c = new Consumer(q);
                consumers.Add(c);
                Thread t = new Thread(new ThreadStart(c.Start));
                threads.Add(t);
                t.Start();
                //  t.Join();
                Conf.numWorkers++;

            }
            public void Start()
            {
                for (int i = 0; i < Conf.numWorkers; ++i)
                {
                    threads[i].Start();
                }
            }

            public void SetEndEvent()
            {
                for (int i = 0; i < Conf.numWorkers; ++i)
                {
                    consumers[i].EvEnd = true;
                }
            }

            public void Join()
            {
                for (int i = 0; i < Conf.numWorkers; ++i)
                {
                    //  threads[i].Join();
                }
            }
        }



        private static List<String> FilesToProcess = new List<string>();
        // Create a work queue.
        WorkQueue workQueue = new WorkQueue();
        ThreadPool tConsumers = null;
        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                FilesToProcess.AddRange(fd.FileNames);
            }


            this.radLabel1.Text = Conf.numWorkers.ToString();

            // Create a producer.
            Producer producer = new Producer(workQueue);
            Thread tProducer = new Thread(new ThreadStart(producer.Start));

            // Create consumers.
            tConsumers = new ThreadPool(workQueue);

            // Start producer and consumers
            tProducer.Start();
            tConsumers.Start();

            // If producing ends, set an end event signal.
            tProducer.Join();
            tConsumers.SetEndEvent();

            /**
             * TODO: Is there any method to wait all thread in a specific
             * list ended?
             */
            // Join consumers thread ended
            tConsumers.Join();


        }


        /**
   * Producer puts tasks into workQueue.
   */
        class Producer
        {
            private Random _random = new Random();
            private WorkQueue _queue;
            private const int _sleepInterval = 1;

            public Producer(WorkQueue q)
            {
                _queue = q;
            }

            public void Start()
            {
                Util.DEBUG("Producer started");

                lock (_queue.aLock)
                {

                    foreach (string filename in FilesToProcess)
                    {
                        List<System.Drawing.Image> ListImage = OCR.ExtractImages(filename);

                        for (int i = 0; i < ListImage.Count; i++)
                        {
                            try
                            {
                                lock (_queue.aLock)
                                {
                                    _queue.Enqueue(new object[] { filename, ListImage[i], i });
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }

                        Util.DEBUG("[Producer] Enqueued {0}", filename);

                    }
                }

                /**
                 * If you don't sleep at all, this function will occupy
                 * a core till it ends. If this happens in a single core
                 * machine, consumers can not start its routine until
                 * the producer ends its work.
                 */
                Thread.Sleep(_sleepInterval);


                Util.DEBUG("Producer ended");
            }
        }

        /**
   * Consumers check workQueue. If there's a task, consumer will
   * dequeue a task and will process with it.
   */
        class Consumer
        {
            private WorkQueue _queue;
            private int _taskLeft;
            public int NumTaskDone
            { get; set; }
            public bool EvEnd
            { get; set; }
            private const int _sleepInterval = 1;

            public Consumer(WorkQueue q)
            {
                _queue = q;
                _taskLeft = 0;
                EvEnd = false;
                NumTaskDone = 0;
            }

            public void Start()
            {
                Util.DEBUG("Consumer started");

                while (true)
                {

                    object param = null;
                    // Check if we have a task to do
                    lock (_queue.aLock)
                    {
                        _taskLeft = _queue.Length;
                        if (_taskLeft > 0)
                        {
                            param = _queue.Dequeue();
                        }
                    }
                    if (param != null)
                    {
                        String FileName = ((Object[])param)[0].ToString();
                        Image image = (Image)((Object[])param)[1];
                        int i = (int)((Object[])param)[2];

                        FileInfo fi = new FileInfo(FileName);

                        string WorkDirectory = fi.Directory.FullName + "\\Work";
                        if (System.IO.Directory.Exists(WorkDirectory) == false) System.IO.Directory.CreateDirectory(WorkDirectory);

                        image.Save(WorkDirectory + "\\" + fi.Name + "-Image" + i + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        ZambaOCR ZOCR = new ZambaOCR();
                        ZOCR.ExtractTextToTxt(WorkDirectory + "\\" + fi.Name + "-Image" + i + ".jpeg", WorkDirectory + "\\" + fi.Name + "-Image" + i + "Text.txt", null, true, string.Empty, WorkDirectory);
                        ZOCR = null;

                        // Process a task.
                        if (_taskLeft > 0)
                        {
                            Util.DEBUG("[Worker] Dequeued {0}", FileName);
                            NumTaskDone++;
                        }
                    }

                    /**
                     * Ending condition:
                     * No work left in the queue and ending event received.
                     */
                    if (EvEnd && _taskLeft == 0)
                    {
                        Thread.Sleep(_sleepInterval + 10000);
                    }

                    /**
                     * If consumer doesn't sleep here, it will hold a core.
                     * If consumers hold all cores so that the producer has
                     * no chance to get core to enqueue, deadlock occurs.
                     */
                    Thread.Sleep(_sleepInterval);
                }

            }
        }

        /**
   * WorkQueue has a lock so that only one thread at a time can
   * access to it.
   */
        class WorkQueue
        {
            public readonly object aLock = new object();
            private Queue<object> _queue;

            public WorkQueue()
            {
                _queue = new Queue<object>();
            }

            public void Enqueue(object item)
            {
                // TODO: exception handling for _queue.Length reaches Max value.
                _queue.Enqueue(item);
            }

            public object Dequeue()
            {
                // TODO: exception handling for _queue is empty
                return _queue.Dequeue();
            }

            public int Length
            { get { return _queue.Count; } }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fi = new OpenFileDialog();
            if (fi.ShowDialog() == DialogResult.OK)
            {
                ZambaOCR ZOCR = new ZambaOCR();
                ZOCR.ExtractTextToTxt(fi.FileName, "c:\\textoextraido.txt", null, true, string.Empty, "c:\\");
                ZOCR = null;
                // ImageOCR.ExtractText(fi.FileName,"c:\textoextraido.txt");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            tConsumers.AddWorker(workQueue);
            this.radLabel1.Text = Conf.numWorkers.ToString();
        }

        List<LookUp> looks = new List<LookUp>();

        int Total = 0;
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog fi = new OpenFileDialog();
            fi.Multiselect = true;

            if (fi.ShowDialog() == DialogResult.OK)
            {
                Total = fi.FileNames.Count();
                label1.Text = Total.ToString();
                Completed = 0;

                this.label2.Text = "Procesados: " + Completed.ToString();
                this.label3.Text = "Threads: " + Workers.ToString();


                List<string> FileNames = new List<string>();
                FileNames.AddRange(fi.FileNames);
                ParseOCR(FileNames, looks);
            }
        }



        internal class Results
        {

            public List<newFileslook> newFileslooks = new List<newFileslook>();

            public void AddResult(newFileslook NewFileslook, List<LookUp> looks)
            {
                lock (newFileslooks)
                {
                    newFileslooks.Add(NewFileslook);

                    string queryDef = string.Empty;

                    queryDef = "NumeroProductor,";
                    foreach (LookUp def in looks)
                    {
                        queryDef += def.Name + ",";
                    }
                    queryDef += "Archivo" + ",";
                    queryDef += "STATE" + ",";
                    queryDef += "Directory";

                    string query = string.Empty;

                    string NumeroProductor = NewFileslook.filename.Substring(NewFileslook.filename.LastIndexOf("\\") + 1, NewFileslook.filename.IndexOf(" ", NewFileslook.filename.LastIndexOf("\\")) - NewFileslook.filename.LastIndexOf("\\"));
                    string Archivo = NewFileslook.filename.Substring(NewFileslook.filename.LastIndexOf("\\") + 1, NewFileslook.filename.Length - NewFileslook.filename.LastIndexOf("\\") - 1);

                    object count = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("Select count(1) from BostonSolicitudes where archivo = '{0}' and directory = '{1}'", Archivo, new FileInfo(NewFileslook.filename).Directory.Name));

                    if (count == null || (int)count == 0)
                    {
                        query = "'" + NumeroProductor + "',";
                        foreach (LookUp def in looks)
                        {
                            bool found = false;
                            foreach (LookUp l in NewFileslook.newlooks)
                            {
                                if (l.Name == def.Name)
                                {
                                    query += "'" + l.value.Replace(";", " ") + "',";
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) query += "'',";
                        }
                        query += "'" + Archivo + "','Procesado " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "',";
                        query += "'" + new FileInfo(NewFileslook.filename).Directory.Name + "'";
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("Insert into BostonSolicitudes ({0}) values ({1})", queryDef, query));
                    }
                    else
                    {
                        query = "NumeroProductor = '" + NumeroProductor + "',";
                        foreach (LookUp def in looks)
                        {
                            bool found = false;
                            foreach (LookUp l in NewFileslook.newlooks)
                            {
                                if (l.Name == def.Name)
                                {
                                    query += def.Name + " = '" + l.value.Replace(";", " ") + "',";
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) query += def.Name + " = '',";
                        }
                        query += "Archivo = '" + Archivo + "',STATE = 'Actualizado " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "' ";
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("update BostonSolicitudes set {0} where archivo = '{1}'and directory = '{2}'", query, Archivo, new FileInfo(NewFileslook.filename).Directory.Name));
                    }
                }
            }

        }



        int Workers = 0;
        int Completed = 0;
        Results results = new Results();

        public enum ConditionsActions
        {
            TakeAll,
            TakeToFirstSpace,
            TryNextMethod,
            Concat,
            Remove,
            OnlyNumbers,
            OnlyWordWithNumbers,
            Replace,
            Find,
            OnlyWordWithCharacter,
        }

        public enum ConditionsObjects
        {
            Before,
            After,
            Value,
            Line
        }

        public enum ConditionsTypes
        {
            Len,
            DataType,
            Exists,
            Always,
            NonExists
        }

        private void ParseOCR(List<string> fileNames, List<LookUp> looks)
        {
            Started = DateTime.Now;

            results.newFileslooks.Clear();

            List<string> onlythesesfiles = new List<string>();

            foreach (string filename in fileNames)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += Bw_DoWork;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                Workers++;
                this.label3.Text = "Threads: " + Workers.ToString();
                bw.RunWorkerAsync(filename);
                while (Workers > 15)
                {
                    Application.DoEvents();
                }
            }
        }

        private void ReParseOCR(List<FileToProcess> fileNames, List<LookUp> looks)
        {
            Started = DateTime.Now;

            results.newFileslooks.Clear();

            List<string> onlythesesfiles = new List<string>();

            foreach (FileToProcess filename in fileNames)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += Bw_DoReWork;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                Workers++;
                this.label3.Text = "Threads: " + Workers.ToString();
                bw.RunWorkerAsync(filename);
                while (Workers > 5)
                {
                    Application.DoEvents();
                }
            }
        }

        private void ForceParseOCR(List<string> fileNames, List<LookUp> looks)
        {
            Started = DateTime.Now;

            results.newFileslooks.Clear();

            List<string> onlythesesfiles = new List<string>();

            foreach (string filename in fileNames)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += Bw_DoForceWork;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                Workers++;
                this.label3.Text = "Threads: " + Workers.ToString();
                bw.RunWorkerAsync(filename);
                while (Workers > 10)
                {
                    Application.DoEvents();
                }
            }
        }
        private void Bw_DoReWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                FileToProcess fileToProcess = (FileToProcess)e.Argument;

                string Archivo = fileToProcess.Archivo;

                object count = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("Select count(1) from BostonSolicitudes where archivo = '{0}' and directory = '{1}' and (STATE like '%PENDIENTE%' OR STATE LIKE '%REPROCESAR%' OR STATE LIKE '%ACTUALIZAR%')", Archivo, fileToProcess.directory));

                if (count != null && (int)count == 1)
                {
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("update BostonSolicitudes set state = 'Procesando {0}' where archivo = '{1}'and directory = '{2}'", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), Archivo, fileToProcess.directory));

                    string WorkDirectory = fileToProcess.DirectoryPath + "\\Work";
                    if (System.IO.Directory.Exists(WorkDirectory) == false) System.IO.Directory.CreateDirectory(WorkDirectory);

                    Boolean regenerate = false;
                    if (fileToProcess.state.ToLower().Contains("pendiente") || fileToProcess.state.ToLower().Contains("reprocesar"))
                        regenerate = true;

                    var newlooks = ParseFilename(fileToProcess.filename, looks, regenerate, WorkDirectory);
                    results.AddResult(new newFileslook(fileToProcess.filename, newlooks), looks);
                }
                else
                {
                    Completed++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Bw_DoForceWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string filename = e.Argument.ToString();
                string WorkDirectory = new FileInfo(filename).Directory.FullName + "\\Work";
                if (System.IO.Directory.Exists(WorkDirectory) == false) System.IO.Directory.CreateDirectory(WorkDirectory);

                var newlooks = ParseFilename(filename, looks, true, WorkDirectory);
                results.AddResult(new newFileslook(filename, newlooks), looks);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        DateTime Started = DateTime.Now;

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Completed++;
            this.label2.Text = "Procesados: " + Completed.ToString();
            Workers--;
            this.label3.Text = "Threads: " + Workers.ToString();

            try
            {
                TimeSpan DueTime = DateTime.Now - Started;
                Double Promedio = DueTime.TotalSeconds / Completed;
                this.label4.Text = "Promedio: " + Promedio.ToString();
                Double Estimado = Promedio * (Total - Completed) / 60;
                this.label5.Text = "Restante: " + Estimado.ToString() + " Minutos";
                this.label6.Text = "Hora de Finalizacion: " + DateTime.Now.AddMinutes(Estimado).ToString();
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }

            //if (Workers == 0)
            //{

            //    StreamWriter sw = new StreamWriter(ResultFileName);

            //    sw.Write("NumeroProductor" + ";");
            //    foreach (LookUp def in looks)
            //    {
            //        sw.Write(def.Name + ";");
            //    }
            //    sw.Write("Archivo" + ";");
            //    sw.WriteLine("");

            //    foreach (newFileslook newfileslooks in results.newFileslooks)
            //    {
            //        sw.Write(newfileslooks.filename.Substring(newfileslooks.filename.LastIndexOf("\\") + 1, newfileslooks.filename.IndexOf(" ", newfileslooks.filename.LastIndexOf("\\")) - newfileslooks.filename.LastIndexOf("\\")) + ";");

            //        foreach (LookUp def in looks)
            //        {
            //            bool found = false;
            //            foreach (LookUp l in newfileslooks.newlooks)
            //            {
            //                if (l.Name == def.Name)
            //                {
            //                    sw.Write(l.value.Replace(";", " ") + ";");
            //                    found = true;
            //                    break;
            //                }
            //            }
            //            if (!found) sw.Write(";");

            //        }

            //        sw.Write(newfileslooks.filename.Substring(newfileslooks.filename.LastIndexOf("\\") + 1, newfileslooks.filename.Length - newfileslooks.filename.LastIndexOf("\\") - 1) + ";");

            //        sw.WriteLine("");
            //    }
            //    sw.Flush();
            //    sw.Close();
            //    sw.Dispose();
            //}

        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string filename = e.Argument.ToString();

                string Archivo = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);

                object count = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("Select count(1) from BostonSolicitudes where archivo = '{0}' and directory = '{1}' and (STATE <> 'PENDIENTE' AND STATE <> 'REPROCESAR')", Archivo, new FileInfo(filename).Directory.Name));

                if (count == null || (int)count == 0)
                {
                    string WorkDirectory = new FileInfo(filename).Directory.FullName + "\\Work";
                    if (System.IO.Directory.Exists(WorkDirectory) == false) System.IO.Directory.CreateDirectory(WorkDirectory);

                    var newlooks = ParseFilename(filename, looks, false, WorkDirectory);
                    results.AddResult(new newFileslook(filename, newlooks), looks);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private List<LookUp> ParseFilename(string filename, List<LookUp> looks, bool regenerate, string WorkDirectory)
        {

            List<Line> AllTextLines = new List<Line>();

            List<LookUp> newlooks = new List<LookUp>();

            ZambaOCR ZOCR = new ZambaOCR();
            List<Line> TextLines;


            foreach (LookUp lookdef in looks)
            {

                if (lookdef.rectangular != null)
                {
                    string OCRSectorFile = WorkDirectory + "\\" + new FileInfo(filename).Name.Replace(".jpg", " " + lookdef.rectangular.SectorName + " OCR.TXT");
                    ZOCR.ExtractTextToTxt(filename, OCRSectorFile, lookdef.rectangular, regenerate, lookdef.Name, WorkDirectory);

                    StreamReader srSector = new StreamReader(OCRSectorFile);
                    List<Line> TextSectorLines = new List<Line>();
                    int countSector = 0;
                    while (srSector.Peek() > -1)
                    {
                        string line = srSector.ReadLine();
                        if (line.Length > 0)
                        {

                            line = line.Trim();
                            line = RemoveAccents(line);
                            if (line.Length > 0)
                            {
                                TextSectorLines.Add(new Line(line, countSector));
                                countSector++;
                            }
                        }
                    }
                    srSector.Close();
                    srSector.Dispose();
                    TextLines = TextSectorLines;
                }
                else
                {
                    ReadAllText(filename, regenerate, WorkDirectory, ref AllTextLines, ref ZOCR);
                    TextLines = AllTextLines;
                }

                LookUp newLookUp = new LookUp(lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value, lookdef.rectangular);
                newLookUp.Conditions = lookdef.Conditions;
                newLookUp.Synonymous = lookdef.Synonymous;
                newLookUp.PostActions = lookdef.PostActions;

                Boolean flagretry = false;
                TryAlltext:

                try
                {



                    if (newLookUp.Before.ToLower().Contains("lookup"))
                    {
                        string lookuptofind = newLookUp.Before.Replace("LookUp.", "");
                        LookUp ReferencedLookUpBefore = newlooks.Find(x => x.Name.ToLower().Contains(lookuptofind.ToLower()));
                        if (ReferencedLookUpBefore != null && ReferencedLookUpBefore.value.Trim().Length > 0)
                        {
                            newLookUp.Before = ReferencedLookUpBefore.value;
                        }
                    }
                    if (newLookUp.After.ToLower().Contains("lookup"))
                    {
                        string lookuptofind = newLookUp.After.Replace("LookUp.", "");
                        LookUp ReferencedLookUpAfter = newlooks.Find(x => x.Name.ToLower().Contains(lookuptofind.ToLower()));
                        if (ReferencedLookUpAfter != null && ReferencedLookUpAfter.value.Trim().Length > 0)
                        {
                            newLookUp.After = ReferencedLookUpAfter.value;
                        }
                    }


                    if (newLookUp.Before.Length > 0)
                    {

                        Line line = TextLines.Find(x => x.Value.ToLower().Contains(newLookUp.Before.ToLower()));


                        if (newLookUp.Conditions.Count > 0)
                        {
                            foreach (Condition cond in newLookUp.Conditions)
                            {
                                if (cond.AppliesTo == ConditionsObjects.Before && cond.Type == ConditionsTypes.Len)
                                {
                                    if (line != null && line.Value.Length != int.Parse(cond.Value))
                                    {
                                        line = null;
                                    }
                                }
                                if (cond.AppliesTo == ConditionsObjects.Line && cond.Type == ConditionsTypes.Always)
                                {
                                    if (line != null && cond.Action == ConditionsActions.Concat)
                                    {
                                        string concat = string.Empty;
                                        foreach (Line _line in TextLines)
                                        {
                                            concat += _line.Value + " ";
                                        }
                                        line.Value = concat;
                                        line.LineNumber = 0;
                                        TextLines.Clear();
                                        TextLines.Add(line);
                                    }
                                }
                            }
                        }

                        if (line == null)
                        {
                            if (newLookUp.Synonymous.Count > 0)
                            {
                                foreach (Synonym s in newLookUp.Synonymous)
                                {
                                    if (s.ObjectType == ConditionsObjects.Before)
                                    {
                                        line = TextLines.Find(x => x.Value.ToLower().Contains(s.Value.ToLower()));
                                        if (line != null)
                                        {
                                            newLookUp.Before = s.Value;
                                            break;
                                        }
                                    }
                                }
                            }


                            if (newLookUp.Conditions.Count > 0)
                            {
                                foreach (Condition cond in newLookUp.Conditions)
                                {
                                    if (cond.AppliesTo == ConditionsObjects.Before && cond.Type == ConditionsTypes.NonExists)
                                    {
                                        if (cond.Action == ConditionsActions.TryNextMethod && cond.ActionFor == ConditionsObjects.Value)
                                        {
                                            line = TextLines.Find(x => x.Value.ToLower().Contains(cond.Value.ToLower()));
                                            if (line != null)
                                            {
                                                newLookUp.value = cond.Value;
                                                goto ValueAsigned;
                                            }
                                        }
                                    }
                                }
                            }



                        }

                        if (line == null)
                        {
                            ReadAllText(filename, regenerate, WorkDirectory, ref AllTextLines, ref ZOCR);
                            line = AllTextLines.Find(x => x.Value.ToLower().Contains(newLookUp.Before.ToLower()));
                            TextLines = AllTextLines;
                        }

                        if (line != null)
                        {
                            if (newLookUp.LineBreaks != 0)
                            {
                                if (newLookUp.After.Length == 0)
                                {
                                    newLookUp.value = TextLines[line.LineNumber + newLookUp.LineBreaks].Value;
                                }
                                else
                                {
                                    newLookUp.value = TextLines[line.LineNumber + newLookUp.LineBreaks].Value;

                                    if (newLookUp.Conditions.Count > 0)
                                    {
                                        string TryState = string.Empty;
                                        foreach (Condition cond in newLookUp.Conditions)
                                        {
                                            if (cond.AppliesTo == ConditionsObjects.After && cond.Type == ConditionsTypes.NonExists)
                                            {
                                                if (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) != -1)
                                                {
                                                    newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                                    goto ValueAsigned;
                                                }
                                                else
                                                {
                                                    if (cond.Action == ConditionsActions.TakeAll)
                                                    {
                                                        newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.Length - newLookUp.Displacement));
                                                    }
                                                    else if (cond.Action == ConditionsActions.TryNextMethod)
                                                    {
                                                        TryState = "Trying";
                                                        if (newLookUp.value.IndexOf(cond.Value, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) >= 0)
                                                        {
                                                            newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(cond.Value, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                                            TryState = "Found";
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                            }
                                        }

                                        if (TryState == "Trying")
                                        {
                                            newLookUp.value = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                    }
                                }
                            }
                            else
                            {
                                if (newLookUp.After.Length == 0)
                                {
                                    newLookUp.value = TextLines[line.LineNumber].Value;

                                    newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, newLookUp.value.Length - (newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement));

                                }
                                else
                                {
                                    newLookUp.value = TextLines[line.LineNumber].Value;
                                    int beforeendposition = newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement;
                                    int afterposition = newLookUp.value.IndexOf(newLookUp.After, newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase);
                                    if (newLookUp.Displacement == 0 && afterposition == beforeendposition)
                                    {
                                        newLookUp.Displacement = 1;
                                        afterposition = newLookUp.value.IndexOf(newLookUp.After, newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase);
                                    }
                                    if (afterposition > 0)
                                        newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - (newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement)));
                                    else
                                        newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, (newLookUp.value.Length - (newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement)));

                                }
                            }
                        }
                        else
                        {
                            Util.DEBUG("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value);
                            Zamba.AppBlock.ZException.Log(new Exception(string.Format("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value)));

                        }
                    }
                    else
                    {
                        if (newLookUp.Find.Length > 0)
                        {
                            Line line = TextLines.Find(x => x.Value.ToLower().Contains(newLookUp.Find.ToLower()));
                            if (line != null)
                            {
                                newLookUp.value = TextLines[line.LineNumber].Value;
                                newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Find, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Displacement, newLookUp.value.IndexOf(newLookUp.After, (newLookUp.value.IndexOf(newLookUp.Find, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Displacement), StringComparison.CurrentCultureIgnoreCase) - (newLookUp.value.IndexOf(newLookUp.Find, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Displacement));
                            }
                            else
                            {
                                Util.DEBUG("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value);
                                Zamba.AppBlock.ZException.Log(new Exception(string.Format("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value)));
                            }
                        }
                        else
                        {
                            if (newLookUp.After.Length == 0)
                            {
                                string concat = string.Empty;
                                foreach (Line line in TextLines)
                                {
                                    concat += line.Value + " ";
                                }
                                if (newLookUp.Conditions.Count > 0)
                                {
                                    foreach (Condition cond in newLookUp.Conditions)
                                    {
                                        if (cond.AppliesTo == ConditionsObjects.Value && cond.Type == ConditionsTypes.DataType)
                                        {
                                            if (cond.Action == ConditionsActions.OnlyWordWithNumbers)
                                            {
                                                string[] splited = concat.Split(new Char[] { Char.Parse(" ") }, StringSplitOptions.RemoveEmptyEntries);
                                                if (splited != null && splited.Length > 0)
                                                {
                                                    string newconcat = string.Empty;
                                                    foreach (string s in splited)
                                                    {
                                                        if (s.Any(char.IsDigit))
                                                        {
                                                            newconcat += s + " ";
                                                        }
                                                    }
                                                    newLookUp.value = newconcat;
                                                    concat = newconcat;
                                                }
                                                else
                                                {
                                                    newLookUp.value = concat;
                                                }
                                            }
                                        }


                                        if (cond.AppliesTo == ConditionsObjects.Value && cond.Type == ConditionsTypes.Exists)
                                        {
                                            if (cond.Action == ConditionsActions.Find && cond.ActionFor == ConditionsObjects.Value)
                                            {
                                                Line line = TextLines.Find(x => x.Value.ToLower().Contains(cond.Value.ToLower()));
                                                if (line != null)
                                                {
                                                    newLookUp.value = cond.Value;
                                                    goto ValueAsigned;
                                                }
                                            }
                                        }


                                    }
                                }
                                newLookUp.value = concat;
                            }
                            else
                            {
                                string concat = string.Empty;
                                foreach (Line line in TextLines)
                                {
                                    concat += line.Value + " ";
                                }

                                if (newLookUp.Conditions.Count > 0)
                                {
                                    foreach (Condition cond in newLookUp.Conditions)
                                    {
                                        if (cond.AppliesTo == ConditionsObjects.Value && cond.Type == ConditionsTypes.DataType)
                                        {
                                            if (cond.Action == ConditionsActions.OnlyWordWithCharacter && cond.ActionFor == ConditionsObjects.Value)
                                            {

                                                string[] splited = concat.Split(new Char[] { Char.Parse(" ") }, StringSplitOptions.RemoveEmptyEntries);
                                                if (splited != null && splited.Length > 0)
                                                {
                                                    string newconcat = string.Empty;
                                                    foreach (string s in splited)
                                                    {
                                                        if (s.Contains(cond.Value))
                                                        {
                                                            newconcat += s + " ";
                                                        }
                                                    }
                                                    newLookUp.value = newconcat;
                                                    concat = newconcat;
                                                }
                                            }
                                        }
                                    }
                                }

                                newLookUp.value = concat;
                                newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));

                            }
                        }
                    }

                    ValueAsigned:

                    newLookUp.value = newLookUp.value.Trim();

                    foreach (PostAction PA in newLookUp.PostActions)
                    {
                        if (PA.objectType == ConditionsObjects.Value && PA.Action == ConditionsActions.Remove)
                        {
                            newLookUp.value = newLookUp.value.Replace(PA.Value, "");
                        }
                        if (PA.objectType == ConditionsObjects.Value && PA.Action == ConditionsActions.Replace)
                        {
                            newLookUp.value = newLookUp.value.Replace(PA.Value, PA.OtherValue);
                        }

                        //poner reemplazo, apply regex, validacion, numeros, fechas, alpha, etc.
                    }

                    if (newLookUp.value.Length == 0 && flagretry == false)
                    {
                        ReadAllText(filename, regenerate, WorkDirectory, ref AllTextLines, ref ZOCR);
                        flagretry = true;
                        TextLines = AllTextLines;
                        goto TryAlltext;
                    }

                    newlooks.Add(newLookUp);
                }
                catch (Exception ex)
                {
                    Util.DEBUG("Error: {0} {1} {2} {3} {4} {5} {6} {7}", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value, ex.ToString());
                    Zamba.AppBlock.ZException.Log(new Exception(string.Format("Error: {0} {1} {2} {3} {4} {5} {6} {7}", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value, ex.ToString())));
                    if (flagretry == false)
                    {
                        ReadAllText(filename, regenerate, WorkDirectory, ref AllTextLines, ref ZOCR);
                        flagretry = true;
                        TextLines = AllTextLines;
                        goto TryAlltext;
                    }
                }
            }
            ZOCR = null;
            return newlooks;
        }

        private List<LookUp> ParseText(List<Line> AllTextLines, List<LookUp> looks, bool regenerate, string WorkDirectory)
        {

            List<Line> TextLines = new List<Line>();
            TextLines.AddRange(AllTextLines);

            List<LookUp> newlooks = new List<LookUp>();

            foreach (LookUp lookdef in looks)
            {

                LookUp newLookUp = new LookUp(lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value, lookdef.rectangular);
                newLookUp.Conditions = lookdef.Conditions;
                newLookUp.Synonymous = lookdef.Synonymous;
                newLookUp.PostActions = lookdef.PostActions;

                Boolean flagretry = false;
                TryAlltext:

                try
                {

                    if (newLookUp.Before.ToLower().Contains("lookup"))
                    {
                        string lookuptofind = newLookUp.Before.Replace("LookUp.", "");
                        LookUp ReferencedLookUpBefore = newlooks.Find(x => x.Name.ToLower().Contains(lookuptofind.ToLower()));
                        if (ReferencedLookUpBefore != null && ReferencedLookUpBefore.value.Trim().Length > 0)
                        {
                            newLookUp.Before = ReferencedLookUpBefore.value;
                        }
                    }
                    if (newLookUp.After.ToLower().Contains("lookup"))
                    {
                        string lookuptofind = newLookUp.After.Replace("LookUp.", "");
                        LookUp ReferencedLookUpAfter = newlooks.Find(x => x.Name.ToLower().Contains(lookuptofind.ToLower()));
                        if (ReferencedLookUpAfter != null && ReferencedLookUpAfter.value.Trim().Length > 0)
                        {
                            newLookUp.After = ReferencedLookUpAfter.value;
                        }
                    }


                    if (newLookUp.Before.Length > 0)
                    {

                        Line line = TextLines.Find(x => x.Value.ToLower().Contains(newLookUp.Before.ToLower()));


                        if (newLookUp.Conditions.Count > 0)
                        {
                            foreach (Condition cond in newLookUp.Conditions)
                            {
                                if (cond.AppliesTo == ConditionsObjects.Before && cond.Type == ConditionsTypes.Len)
                                {
                                    if (line != null && line.Value.Length != int.Parse(cond.Value))
                                    {
                                        line = null;
                                    }
                                }
                                if (cond.AppliesTo == ConditionsObjects.Line && cond.Type == ConditionsTypes.Always)
                                {
                                    if (line != null && cond.Action == ConditionsActions.Concat)
                                    {
                                        string concat = string.Empty;
                                        foreach (Line _line in TextLines)
                                        {
                                            concat += _line.Value + " ";
                                        }
                                        line.Value = concat;
                                        line.LineNumber = 0;
                                        TextLines.Clear();
                                        TextLines.Add(line);
                                    }
                                }
                            }
                        }

                        if (line == null)
                        {
                            if (newLookUp.Synonymous.Count > 0)
                            {
                                foreach (Synonym s in newLookUp.Synonymous)
                                {
                                    if (s.ObjectType == ConditionsObjects.Before)
                                    {
                                        line = TextLines.Find(x => x.Value.ToLower().Contains(s.Value.ToLower()));
                                        if (line != null)
                                        {
                                            newLookUp.Before = s.Value;
                                            break;
                                        }
                                    }
                                }
                            }


                            if (newLookUp.Conditions.Count > 0)
                            {
                                foreach (Condition cond in newLookUp.Conditions)
                                {
                                    if (cond.AppliesTo == ConditionsObjects.Before && cond.Type == ConditionsTypes.NonExists)
                                    {
                                        if (cond.Action == ConditionsActions.TryNextMethod && cond.ActionFor == ConditionsObjects.Value)
                                        {
                                            line = TextLines.Find(x => x.Value.ToLower().Contains(cond.Value.ToLower()));
                                            if (line != null)
                                            {
                                                newLookUp.value = cond.Value;
                                                goto ValueAsigned;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (line == null)
                        {
                            line = AllTextLines.Find(x => x.Value.ToLower().Contains(newLookUp.Before.ToLower()));
                            TextLines = AllTextLines;
                        }

                        if (line != null)
                        {
                            if (newLookUp.LineBreaks != 0)
                            {
                                if (newLookUp.After.Length == 0)
                                {
                                    newLookUp.value = TextLines[line.LineNumber + newLookUp.LineBreaks].Value;
                                }
                                else
                                {
                                    newLookUp.value = TextLines[line.LineNumber + newLookUp.LineBreaks].Value;

                                    if (newLookUp.Conditions.Count > 0)
                                    {
                                        string TryState = string.Empty;
                                        foreach (Condition cond in newLookUp.Conditions)
                                        {
                                            if (cond.AppliesTo == ConditionsObjects.After && cond.Type == ConditionsTypes.NonExists)
                                            {
                                                if (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) != -1)
                                                {
                                                    newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                                    goto ValueAsigned;
                                                }
                                                else
                                                {
                                                    if (cond.Action == ConditionsActions.TakeAll)
                                                    {
                                                        newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.Length - newLookUp.Displacement));
                                                    }
                                                    else if (cond.Action == ConditionsActions.TryNextMethod)
                                                    {
                                                        TryState = "Trying";
                                                        if (newLookUp.value.IndexOf(cond.Value, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) >= 0)
                                                        {
                                                            newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(cond.Value, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                                            TryState = "Found";
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                            }
                                        }

                                        if (TryState == "Trying")
                                        {
                                            newLookUp.value = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));
                                    }
                                }
                            }
                            else
                            {
                                if (newLookUp.After.Length == 0)
                                {
                                    newLookUp.value = TextLines[line.LineNumber].Value;

                                    newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, newLookUp.value.Length - (newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement));

                                }
                                else
                                {
                                    newLookUp.value = TextLines[line.LineNumber].Value;
                                    int beforeendposition = newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement;
                                    int afterposition = newLookUp.value.IndexOf(newLookUp.After, newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase);
                                    if (newLookUp.Displacement == 0 && afterposition == beforeendposition)
                                    {
                                        newLookUp.Displacement = 1;
                                        afterposition = newLookUp.value.IndexOf(newLookUp.After, newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase);
                                    }
                                    if (afterposition > 0)
                                        newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - (newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement)));
                                    else
                                        newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement, (newLookUp.value.Length - (newLookUp.value.IndexOf(newLookUp.Before, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Before.Length + newLookUp.Displacement)));

                                }
                            }
                        }
                        else
                        {
                            Util.DEBUG("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value);
                            Zamba.AppBlock.ZException.Log(new Exception(string.Format("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value)));

                        }
                    }
                    else
                    {
                        if (newLookUp.Find.Length > 0)
                        {
                            Line line = TextLines.Find(x => x.Value.ToLower().Contains(newLookUp.Find.ToLower()));
                            if (line != null)
                            {
                                newLookUp.value = TextLines[line.LineNumber].Value;
                                newLookUp.value = newLookUp.value.Substring(newLookUp.value.IndexOf(newLookUp.Find, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Displacement, newLookUp.value.IndexOf(newLookUp.After, (newLookUp.value.IndexOf(newLookUp.Find, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Displacement), StringComparison.CurrentCultureIgnoreCase) - (newLookUp.value.IndexOf(newLookUp.Find, StringComparison.CurrentCultureIgnoreCase) + newLookUp.Displacement));
                            }
                            else
                            {
                                Util.DEBUG("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value);
                                Zamba.AppBlock.ZException.Log(new Exception(string.Format("Error: Line Null {0} {1} {2} {3} {4} {5} {6} ", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value)));
                            }
                        }
                        else
                        {
                            if (newLookUp.After.Length == 0)
                            {
                                string concat = string.Empty;
                                foreach (Line line in TextLines)
                                {
                                    concat += line.Value + " ";
                                }
                                if (newLookUp.Conditions.Count > 0)
                                {
                                    foreach (Condition cond in newLookUp.Conditions)
                                    {
                                        if (cond.AppliesTo == ConditionsObjects.Value && cond.Type == ConditionsTypes.DataType)
                                        {
                                            if (cond.Action == ConditionsActions.OnlyWordWithNumbers)
                                            {
                                                string[] splited = concat.Split(new Char[] { Char.Parse(" ") }, StringSplitOptions.RemoveEmptyEntries);
                                                if (splited != null && splited.Length > 0)
                                                {
                                                    string newconcat = string.Empty;
                                                    foreach (string s in splited)
                                                    {
                                                        if (s.Any(char.IsDigit))
                                                        {
                                                            newconcat += s + " ";
                                                        }
                                                    }
                                                    newLookUp.value = newconcat;
                                                    concat = newconcat;
                                                }
                                                else
                                                {
                                                    newLookUp.value = concat;
                                                }
                                            }
                                        }


                                        if (cond.AppliesTo == ConditionsObjects.Value && cond.Type == ConditionsTypes.Exists)
                                        {
                                            if (cond.Action == ConditionsActions.Find && cond.ActionFor == ConditionsObjects.Value)
                                            {
                                                Line line = TextLines.Find(x => x.Value.ToLower().Contains(cond.Value.ToLower()));
                                                if (line != null)
                                                {
                                                    newLookUp.value = cond.Value;
                                                    goto ValueAsigned;
                                                }
                                            }
                                        }


                                    }
                                }
                                newLookUp.value = concat;
                            }
                            else
                            {
                                string concat = string.Empty;
                                foreach (Line line in TextLines)
                                {
                                    concat += line.Value + " ";
                                }

                                if (newLookUp.Conditions.Count > 0)
                                {
                                    foreach (Condition cond in newLookUp.Conditions)
                                    {
                                        if (cond.AppliesTo == ConditionsObjects.Value && cond.Type == ConditionsTypes.DataType)
                                        {
                                            if (cond.Action == ConditionsActions.OnlyWordWithCharacter && cond.ActionFor == ConditionsObjects.Value)
                                            {

                                                string[] splited = concat.Split(new Char[] { Char.Parse(" ") }, StringSplitOptions.RemoveEmptyEntries);
                                                if (splited != null && splited.Length > 0)
                                                {
                                                    string newconcat = string.Empty;
                                                    foreach (string s in splited)
                                                    {
                                                        if (s.Contains(cond.Value))
                                                        {
                                                            newconcat += s + " ";
                                                        }
                                                    }
                                                    newLookUp.value = newconcat;
                                                    concat = newconcat;
                                                }
                                            }
                                        }
                                    }
                                }

                                newLookUp.value = concat;
                                newLookUp.value = newLookUp.value.Substring(0 + newLookUp.Displacement, (newLookUp.value.IndexOf(newLookUp.After, newLookUp.Displacement, StringComparison.CurrentCultureIgnoreCase) - newLookUp.Displacement));

                            }
                        }
                    }

                    ValueAsigned:

                    newLookUp.value = newLookUp.value.Trim();

                    foreach (PostAction PA in newLookUp.PostActions)
                    {
                        if (PA.objectType == ConditionsObjects.Value && PA.Action == ConditionsActions.Remove)
                        {
                            newLookUp.value = newLookUp.value.Replace(PA.Value, "");
                        }
                        if (PA.objectType == ConditionsObjects.Value && PA.Action == ConditionsActions.Replace)
                        {
                            newLookUp.value = newLookUp.value.Replace(PA.Value, PA.OtherValue);
                        }

                        //poner reemplazo, apply regex, validacion, numeros, fechas, alpha, etc.
                    }

                    if (newLookUp.value.Length == 0 && flagretry == false)
                    {
                        flagretry = true;
                        TextLines = AllTextLines;
                        goto TryAlltext;
                    }

                    newlooks.Add(newLookUp);
                }
                catch (Exception ex)
                {
                    Util.DEBUG("Error: {0} {1} {2} {3} {4} {5} {6} {7}", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value, ex.ToString());
                    Zamba.AppBlock.ZException.Log(new Exception(string.Format("Error: {0} {1} {2} {3} {4} {5} {6} {7}", lookdef.Name, lookdef.Before, lookdef.After, lookdef.LineBreaks, lookdef.Find, lookdef.Displacement, lookdef.value, ex.ToString())));
                    if (flagretry == false)
                    {
                        flagretry = true;
                        TextLines = AllTextLines;
                        goto TryAlltext;
                    }
                }
            }
            return newlooks;
        }
        private void ReadAllText(string filename, bool regenerate, string WorkDirectory, ref List<Line> AllTextLines, ref ZambaOCR ZOCR)
        {

            if (AllTextLines == null || AllTextLines.Count == 0)
            {
                string OCRFile = WorkDirectory + "\\" + new FileInfo(filename).Name.Replace(".jpg", " OCR.TXT");

                ZOCR.ExtractTextToTxt(filename, OCRFile, null, regenerate, string.Empty, WorkDirectory);

                StreamReader sr = new StreamReader(OCRFile);

                int count = 0;
                while (sr.Peek() > -1)
                {
                    string line = sr.ReadLine();
                    if (line.Length > 0)
                    {
                        line = line.Trim();
                        line = RemoveAccents(line);
                        if (line.Length > 0)
                        {
                            AllTextLines.Add(new Line(line, count));
                            count++;
                        }
                    }
                }
                sr.Close();
                sr.Dispose();
            }
        }

        public string RemoveAccents(string inputString)
        {
            Regex a = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex e = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex i = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex o = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex u = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            Regex n = new Regex("[ñ|Ñ]", RegexOptions.Compiled);
            inputString = a.Replace(inputString, "a");
            inputString = e.Replace(inputString, "e");
            inputString = i.Replace(inputString, "i");
            inputString = o.Replace(inputString, "o");
            inputString = u.Replace(inputString, "u");
            inputString = n.Replace(inputString, "n");
            inputString = inputString.Replace(".", "");
            inputString = inputString.Replace(",", " ");
            inputString = inputString.Replace("º", "");
            inputString = inputString.Replace("*", "");
            Regex w = new Regex("[^a-zA-Z0-9: /]+");
            inputString = w.Replace(inputString, "");

            return inputString;
        }




        public Bitmap Crop(Bitmap bmp)
        {
            int w = bmp.Width, h = bmp.Height;

            Func<int, bool> allWhiteRow = row =>
            {
                for (int i = 0; i < w; ++i)
                    if (bmp.GetPixel(i, row).R != 255)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = col =>
            {
                for (int i = 0; i < h; ++i)
                    if (bmp.GetPixel(col, i).R != 255)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (allWhiteRow(row))
                    topmost = row;
                else break;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (allWhiteRow(row))
                    bottommost = row;
                else break;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (allWhiteColumn(col))
                    leftmost = col;
                else
                    break;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (allWhiteColumn(col))
                    rightmost = col;
                else
                    break;
            }

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;
            try
            {
                Bitmap target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3}", topmost, bottommost, leftmost, rightmost),
                  ex);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog FB = new FolderBrowserDialog();
            if (FB.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(FB.SelectedPath);
                Directories.Add(dir);
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    Directories.Add(di);
                }
            }

            List<FileToProcess> FileNames = new List<FileToProcess>();

            DataSet ds = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "Select archivo, directory, state from BostonSolicitudes where (STATE like '%PENDIENTE%' OR STATE like  '%REPROCESAR%' OR STATE like  '%ACTUALIZAR%')");

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    foreach (DirectoryInfo dir in Directories)
                    {
                        if (dir.Exists && dir.Name == r["Directory"].ToString())
                        {
                            var FileName = dir.FullName + "\\" + r["Archivo"].ToString();
                            if (File.Exists(FileName))
                            {
                                FileToProcess filetoProcess = new FileToProcess();
                                filetoProcess.filename = FileName;
                                filetoProcess.Archivo = r["Archivo"].ToString();
                                filetoProcess.DirectoryPath = dir.FullName;
                                filetoProcess.directory = r["Directory"].ToString();
                                filetoProcess.state = r["State"].ToString();
                                FileNames.Add(filetoProcess);
                                break;
                            }
                        }
                    }
                }

                Total = FileNames.Count;
                label1.Text = Total.ToString();
                Completed = 0;

                this.label2.Text = "Procesados: " + Completed.ToString();
                this.label3.Text = "Threads: " + Workers.ToString();
                if (FileNames.Count > 0)
                {
                    ReParseOCR(FileNames, looks);
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {

            OpenFileDialog fi = new OpenFileDialog();
            fi.Multiselect = true;

            if (fi.ShowDialog() == DialogResult.OK)
            {
                Total = fi.FileNames.Count();
                label1.Text = Total.ToString();
                Completed = 0;

                this.label2.Text = "Procesados: " + Completed.ToString();
                this.label3.Text = "Threads: " + Workers.ToString();

                List<string> FileNames = new List<string>();
                FileNames.AddRange(fi.FileNames);
                ForceParseOCR(FileNames, looks);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Directories.Clear();
            FolderBrowserDialog FB = new FolderBrowserDialog();
            if (FB.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(FB.SelectedPath);
                Directories.Add(dir);
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    Directories.Add(di);
                }
            }

            List<string> FileNames = new List<string>();


            foreach (DirectoryInfo dir in Directories)
            {
                if (dir.Exists)
                {
                    var Files = dir.GetFiles();
                    foreach (FileInfo fi in Files)
                    {
                        FileNames.Add(fi.FullName);
                        object count = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("Select count(1) from BostonSolicitudes where archivo = '{0}' and directory = '{1}'", fi.Name, fi.Directory.Name));

                        if (count == null || (int)count == 0)
                        {
                            object count1 = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("Select count(1) from BostonSolicitudes where archivo = '{0}' and directory = ''", fi.Name));

                            if (count1 != null && (int)count1 == 1)
                            {
                                string state = "Procesado " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("update BostonSolicitudes set directory = '{2}', state = '{1}' where archivo = '{0}'  and directory = ''", fi.Name, state, fi.Directory.Name));
                            }
                            else if (count1 != null && (int)count1 > 1)
                            {
                                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("delete BostonSolicitudes where archivo = '{0}'", fi.Name));
                                string state = "Pendiente " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("Insert into BostonSolicitudes (archivo, state, directory) values ('{0}','{1}','{2}')", fi.Name, state, fi.Directory.Name));
                            }
                            else
                            {
                                string state = "Pendiente " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("Insert into BostonSolicitudes (archivo, state, directory) values ('{0}','{1}','{2}')", fi.Name, state, fi.Directory.Name));
                            }
                        }
                        else if (count != null && (int)count > 1)
                        {
                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("delete BostonSolicitudes where archivo = '{0}'", fi.Name));
                            string state = "Pendiente " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, string.Format("Insert into BostonSolicitudes (archivo, state, directory) values ('{0}','{1}','{2}')", fi.Name, state, fi.Directory.Name));
                        }

                        Total = FileNames.Count;
                        label1.Text = Total.ToString();
                        Application.DoEvents();
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Directories.Clear();
            FolderBrowserDialog FB = new FolderBrowserDialog();
            if (FB.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(FB.SelectedPath);
                Directories.Add(dir);
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    Directories.Add(di);
                }
            }

            List<string> FileNames = new List<string>();


            foreach (DirectoryInfo dir in Directories)
            {
                if (dir.Exists)
                {
                    var Files = dir.GetFiles();
                    foreach (FileInfo fi in Files)
                    {
                        FileNames.Add(fi.FullName);
                        Total = FileNames.Count;
                        label1.Text = Total.ToString();
                        Application.DoEvents();
                    }

                    List<LookUp> looks = new List<LookUp>();
                    LookUp NROFACTURALookUp = new LookUp("NROFACTURA", "FACTURA", string.Empty, 1, "-", 0, string.Empty, new Rectangular(new Point(700, 0), new Point(1000, 100), "FACTURA"));
                    looks.Add(NROFACTURALookUp);
                    SplitPDF(FileNames, "FACTURA NRO", looks, dir.FullName);
                }
            }
        }

        private void SplitPDF(List<string> files, string SplitText, List<LookUp> looks, string workdirectory)
        {
            foreach (string filename in files)
            {
                String newdocfilename = string.Empty;
                //Create a pdf document.
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(filename);

                PdfDocument newdoc = new PdfDocument();

                StringBuilder buffer = new StringBuilder();
                Boolean IsFirstDoc = true;
                int PageIndex = 0;
                foreach (PdfPageBase page in doc.Pages)
                {
                    PageIndex++;
                    String currentText = page.ExtractText();

                    List<Line> AllTextLines = new List<Line>();
                    int count = 0;
                    foreach (string s in currentText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
                    {
                        string line = s;
                        if (line.Length > 0)
                        {
                            line = line.Trim();
                            line = RemoveAccents(line);
                            if (line.Length > 0)
                            {
                                AllTextLines.Add(new Line(line, count));
                                count++;
                            }
                        }
                    }

                    List<LookUp> resultsLooks = new List<LookUp>();
                    if (looks != null)
                    {
                        resultsLooks = ParseText(AllTextLines, looks, true, workdirectory);
                    }

                    if (currentText.IndexOf(SplitText, 0, StringComparison.CurrentCultureIgnoreCase) != -1 && IsFirstDoc == false)
                    {
                        //Save the previus document
                        foreach (LookUp look in resultsLooks)
                        {
                            if (look.variableName == "newDocFileName")
                            {
                                newdocfilename = Path.Combine(workdirectory,look.value);
                            }
                        }
                        newdoc.SaveToFile(newdocfilename);
                        newdoc.Close();

                        //continue with the nextone
                        newdoc = new PdfDocument();
                        //Is FirstPage
                        newdoc.InsertPage(doc, page);
                    }
                    else
                    {
                        if (IsFirstDoc)
                        {
                            //Inserts First Page of First Document
                            //Is FirstPage
                            newdoc.InsertPage(doc, page);
                        }
                        else
                        {
                            //Inserts other Page
                            newdoc.InsertPage(doc, page);
                        }
                    }
                    IsFirstDoc = false;


                }

                //Save the last document
                newdoc.SaveToFile(newdocfilename);
                newdoc.Close();

                doc.Close();

            }
        }
    }

    internal class FileToProcess
    {
        public string filename;
        public string state;
        public string directory;

        public string DirectoryPath { get; set; }
        public string Archivo { get; set; }
    }

    //        works perfectly except when the croppedWidth or croppedHeight is zero, in this case I set them to the bmp.Width or bmp.Height respectively, and it works like a charm :)



    internal class PostAction
    {
        public ConditionsActions Action;
        public string Value;
        public ConditionsObjects objectType;
        public string OtherValue;

        public PostAction(ConditionsObjects objecttype, ConditionsActions action, string value, string otherValue)
        {
            this.objectType = objecttype;
            this.Action = action;
            this.Value = value;
            this.OtherValue = otherValue;
        }
    }

    internal class Synonym
    {
        public ConditionsObjects ObjectType;
        public string Value;

        public Synonym(ConditionsObjects objectType, string value)
        {
            this.ObjectType = objectType;
            this.Value = value;
        }
    }

    internal class Condition
    {
        public ConditionsActions Action { get; internal set; }

        public ConditionsTypes Type { get; internal set; }
        public string Value { get; internal set; }
        public ConditionsObjects AppliesTo { get; set; }
        public ConditionsObjects ActionFor { get; set; }

        public Condition(ConditionsTypes type, string value, ConditionsObjects appliesTo, ConditionsActions action, ConditionsObjects actionFor)
        {
            Type = type;
            Value = value;
            AppliesTo = appliesTo;
            this.Action = action;
            this.ActionFor = actionFor;
        }
    }

    internal class LookUp
    {

        public LookUp(string name, string before, string after, int lineBreaks, string find, int displacement, string Value, Rectangular _rectangular)
        {
            Name = name;
            Before = before;
            After = after;
            LineBreaks = lineBreaks;
            Find = find;
            Displacement = displacement;
            value = Value;
            rectangular = _rectangular;
        }

        public string Name { get; private set; }
        public string Before { get; set; }
        public string After { get; set; }
        public int LineBreaks { get; private set; }
        public string value { get; set; }

        public string Find { get; set; }
        public int Displacement { get; set; }
        public Rectangular rectangular { get; set; }
        private List<Condition> _conditions;
        private List<Synonym> _Synonymous;
        private List<PostAction> _PostActions;
        public List<Condition> Conditions
        {
            get
            {
                if (_conditions == null)
                {
                    _conditions = new List<Condition>();
                }
                return _conditions;
            }
            set
            {
                _conditions = value;
            }
        }

        public List<Synonym> Synonymous
        {
            get
            {
                if (_Synonymous == null)
                {
                    _Synonymous = new List<Synonym>();
                }
                return _Synonymous;
            }
            set
            {
                _Synonymous = value;
            }
        }
        public List<PostAction> PostActions
        {
            get
            {
                if (_PostActions == null)
                {
                    _PostActions = new List<PostAction>();
                }
                return _PostActions;
            }
            set
            {
                _PostActions = value;
            }
        }

        public int PageIndex { get; set; }
        public string variableName { get; internal set; }
    }
    internal class newFileslook
    {
        public string filename;
        public List<LookUp> newlooks;

        public newFileslook(string filename, List<LookUp> newlooks)
        {
            this.filename = filename;
            this.newlooks = newlooks;
        }
    }

    internal class Line
    {
        public string Value { get; set; }
        public int LineNumber { get; set; }

        public Line(string value, int lineNumber)
        {
            this.Value = value;
            this.LineNumber = lineNumber;
        }

    }
}
