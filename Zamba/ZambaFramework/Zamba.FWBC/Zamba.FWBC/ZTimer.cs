using System;
using System.Collections.Generic;
using System.Text;
namespace Zamba.ZTimers
{
    public class ZTimer : IZtimer,IDisposable
    {

        #region Atributos

        private Int64 _period;
        private Int64 _dueTime;
        private System.Threading.TimerCallback _tCallBack;
        private object _state;
        private ZTState _tState;
        private System.Threading.Timer _timer;
        private Int16 _timeStart;
        private Int16 _timeEnd;
        private System.Threading.TimerCallback call;

        #endregion

        #region Propiedades

        public Int64 Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        public Int64 DueTime
        {
            get { return this._dueTime; }
            set { this._dueTime = value; }
        }

        public System.Threading.TimerCallback CallBack
        {
            get { return this._tCallBack; }
            set { this._tCallBack = value; }
        }

        public object State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        public ZTState TState
        {
            get { return this._tState; }
            set { this._tState = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Crea una instancia de ZTimer.
        /// </summary>
        /// <param name="cb">Un delegado TimerCallBack el cuañl representa al metodo a ejecutar</param>
        /// <param name="st">Un objeto el cual va a contener informacion utilizada por el timer</param>
        /// <param name="due">En cuanto tiempo de iniciara el timer.</param>
        /// <param name="per">Intervalo de tiempo entre ejecuciones</param>
        /// <param name="timeS">La hora de inicio en la cual puede ejecutar el metodo.</param>
        /// <param name="timeE">Hora de finalizacion en la cual puede ejecutar el metodo</param>
        /// <history>
        /// [Ezequiel] 13/05/08 Created.
        /// </history>
        public ZTimer(System.Threading.TimerCallback cb, object st, Int64 due, Int64 per, Int16 timeS, Int16 timeE)
        {
            if (per == 0) per = 120000;
                this._dueTime = due;
                this._period = per;
                this._tCallBack = cb;
                this._timeStart = timeS;
                this._timeEnd = timeE;
                this._state = st;
                this.call = new System.Threading.TimerCallback(Execute);
                this._timer = new System.Threading.Timer(call, this.State, due, per);
                this._tState = ZTState.Run;              
        }

        #endregion

        #region Metodos

        public void Resume()
        {
            this._timer.Change(this._period, this._period);
            this._tState = ZTState.Run;
        }
        public void Pause()
        {
            this._timer.Change(-1, -1);
            this._tState = ZTState.Pause;
        }

        private Boolean blnNewExecution;
        /// <summary>
        /// Metodo que ejecuta el CallBack que se paso por parametros en el constructor.
        /// </summary>
        /// <param name="state"></param>
        /// <history>
        /// [Ezequiel] 12/05/09: Created
        /// </history>
        private void Execute(object state)
        {
                if (DateTime.Now.Hour >= this._timeStart && DateTime.Now.Hour <= this._timeEnd)
                {
                    if (blnNewExecution == true)
                    {
                        blnNewExecution = false;

                       // Zamba.Servers.Server.closeConnections();
                    }
                    
                    this.CallBack.Invoke(state);  //****
                }
                else
                    blnNewExecution = true;
           
        }

        /// <summary>
        /// Cambia los tiempos de los timers.
        /// </summary>
        /// <param name="duetime"></param>
        /// <param name="period"></param>
        /// <history>
        /// [Ezequiel] 13/05/09: Created.
        /// </history>
        public void Change(Int64 duetime, Int64 period)
        {
            this._dueTime = duetime;
            this._period = period;
            this._timer.Change(duetime, period);
        }



        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this._timer.Dispose();
            this.CallBack = null;
            this.State = null;
        }

        #endregion
    }
}
