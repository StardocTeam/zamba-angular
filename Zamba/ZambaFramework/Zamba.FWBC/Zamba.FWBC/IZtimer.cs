using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.ZTimers
{
    public interface IZtimer
    {
        #region Propiedades

        ZTState TState { get; set; }
        Int64 DueTime { get; set; }
        Int64 Period { get; set; }
        object State { get; set; }
        System.Threading.TimerCallback CallBack { get; set; }

        #endregion

        #region Metodos

        void Resume();
        void Pause();
        void Change(Int64 duetime,Int64 period);
       // void Dispose();

        #endregion
    }
}
