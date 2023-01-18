using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Reflection;
//using Zamba.WFUserControl;
using Zamba.Core;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Valida Fecha y Hora de una tarea
    /// </summary>
    [RuleDescription("VALIDA FECHA DE LA TAREA"), RuleHelp("Valida Fecha y Hora de una tarea")]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    [Designer(typeof(ParallelDesigner), typeof(IDesigner))]
public partial class IfDates : ZCompositeRule, IResultActivity, IIfDates
	{
        public IfDates()
		{
			InitializeComponent();
		}

        private Comparators comp;
        private TipoComparaciones tipoComp;
        private DateTime fechaFija;
        private TaskResult.DocumentDates fechaTarea;
        private TaskResult.DocumentDates fechaComp;
        private Int32 dias, horas;
        private String sNombre;

    //Public Sub New(ByVal Id As Int64, ByVal Name As String, ByRef wfstep As WFStep, ByVal FechaDoc As TaskResult.DocumentDates, ByVal Comparador As Comparadores, ByVal TipoComparacion As TipoComparaciones, ByVal ValorComparativo As String)
    //    MyBase.New(Id, Name, WFStep)

    //    Me.FechaTarea = FechaDoc
    //    Trace.WriteLineIf(ZTrace.IsVerbose,"Fecha de la Tarea: " & Me.FechaTarea.ToString)
    //    Me.Comp = Comparador
    //    Trace.WriteLineIf(ZTrace.IsVerbose,"Comparador: " & Me.Comp.ToString)
    //    Me.TipoComp = TipoComparacion
    //    If ValorComparativo = "" Then
    //        Select Case TipoComp
    //            Case TipoComparaciones.Dias
    //                Me.Dias = 0
    //            Case TipoComparaciones.Especifica
    //            Case TipoComparaciones.Fija
    //                Me.FechaFija = DateTime.Now
    //            Case TipoComparaciones.Horas
    //                Me.Horas = 0
    //            Case Else
    //                Dim ex As New Exception("Tipo de comparación no valida")
    //                Throw ex
    //        End Select
    //    Else
    //        Select Case TipoComp
    //            Case TipoComparaciones.Dias
    //                Me.Dias = Integer.Parse(ValorComparativo)
    //            Case TipoComparaciones.Especifica
    //            Case TipoComparaciones.Fija
    //                Me.FechaFija = DateTime.Parse(ValorComparativo)
    //            Case TipoComparaciones.Horas
    //                Me.Horas = Integer.Parse(ValorComparativo)
    //            Case Else
    //                Dim ex As New Exception("Tipo de comparación no valida")
    //                Throw ex
    //        End Select
    //    End If
    //    sNombre = "Valido la fecha de " & Me.FechaTarea.ToString
    //End Sub

        private Int32 time;
        /// <summary>
        /// Title of the Form
        /// </summary>
        public Int32 Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// Wait the specified time
        /// </summary>
        /// <param name="executionContext"></param>
        /// <returns></returns>
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            for (int child = 0; child < this.EnabledActivities.Count; child++)
            {
                IResultActivity branch = this.EnabledActivities[child] as IResultActivity;
                branch.Results = this.Results;
                Zamba.WFExecution.PlayIfDates play =
                new Zamba.WFExecution.PlayIfDates();
                play.Play(this.Results, (Zamba.Core.IIfDates)this);
                // Ejecutar la actividad
                executionContext.ExecuteActivity((System.Workflow.ComponentModel.Activity)branch);
            }
            
            return ActivityExecutionStatus.Closed;
        }

        #region IIfDates Members

        public int CantidadDias
        {
            get
            {
                return this.dias;
            }
            set
            {
                this.dias = value;
            }
        }
        public int CantidadHoras
        {
            get
            {
                return this.horas;
            }
            set
            {
                this.horas = value;
            }
        }
        public Zamba.Core.Comparadores Comparador
        {
            get
            {
                return (Zamba.Core.Comparadores)comp;
            }
            set
            {
                comp = (Zamba.Core.Comparators)value;
            }
        }
        public DateTime FechaAComparar
        {
            get
            {
                return this.fechaFija;
            }
            set
            {
                this.fechaFija = value;
            }
        }
        public Zamba.Core.DocumentDates FechaDocumentoComparar
        {
            get
            {
                return (Zamba.Core.DocumentDates)this.fechaComp;
            }
            set
            {
                this.fechaComp = (Zamba.Core.Result.DocumentDates)value;
            }
        }
        public Zamba.Core.DocumentDates MiFecha
        {
            get
            {
                return (Zamba.Core.DocumentDates)this.fechaTarea;
            }
            set
            {
                this.fechaTarea = (Zamba.Core.Result.DocumentDates)value;
            }
        }
        public Zamba.Core.TipoComparaciones TipoComparacion
        {
            get
            {
                return this.tipoComp;
            }
            set
            {
                this.tipoComp = value;
            }
        }
        public String SNombre
        {
            get
            {
                return this.sNombre;
            }
            set
            {
                this.sNombre = value;
            }
        }


        #endregion
        #region Miembros de IResultActivity
        

        ///// <summary>
        ///// Devuelve el diseñador de messageActivity
        ///// </summary>
        ///// <returns></returns>
        //public ZRuleControl GetDesigner()
        //{
        //    Zamba.Core.IIfDates rule = (Zamba.Core.IIfDates)this;
        //    UCIfDates cont = new UCIfDates(rule);
        //    return cont;
        //    return null;
        //}

        public override void SetParams(object[] Params)
        {
            this.MiFecha = (Zamba.Core.DocumentDates)Params.GetValue(0);
            System.Diagnostics.Trace.WriteLineIf(ZTrace.IsVerbose,"Fecha de la Tarea : " + this.fechaTarea.ToString());
            this.Comparador = (Zamba.Core.Comparadores)Params.GetValue(1);
            System.Diagnostics.Trace.WriteLineIf(ZTrace.IsVerbose,"Comparador: " + this.Comparador.ToString());
            this.TipoComparacion = (Zamba.Core.TipoComparaciones)Params.GetValue(2);
            if (String.Compare(Params.GetValue(3).ToString(), "") == 0)
            {
                switch (TipoComparacion)
                {
                    case Zamba.Core.TipoComparaciones.Dias: this.CantidadDias = 0; break;
                    case Zamba.Core.TipoComparaciones.Especifica:
                    case Zamba.Core.TipoComparaciones.Fija: this.FechaAComparar = DateTime.Now; break;
                    case Zamba.Core.TipoComparaciones.Horas: this.CantidadHoras = 0; break;
                    default: Exception ex = new Exception(); MessageBox.Show("Tipo de comparación no valida"); throw ex;
                }
            }
            else
            {
                switch(TipoComparacion)
                {
                    case Zamba.Core.TipoComparaciones.Dias: this.CantidadDias = Int32.Parse(Params.GetValue(3).ToString());break;
                    case Zamba.Core.TipoComparaciones.Especifica :
                    case Zamba.Core.TipoComparaciones.Fija: this.FechaAComparar = DateTime.Parse(Params.GetValue(3).ToString()); break;
                    case Zamba.Core.TipoComparaciones.Horas: this.CantidadHoras = Int32.Parse(Params.GetValue(3).ToString()); break;
                    default: Exception ex = new Exception(); MessageBox.Show("Tipo de comparación no valida"); throw ex;
                }
            }
            SNombre = "Valido la fecha de " + this.MiFecha.ToString();
        }

        public override System.Collections.Generic.List<object> GetParams()
        {
            System.Collections.Generic.List<object> Params = new System.Collections.Generic.List<object>();
            Params.Add(this.MiFecha);
            Params.Add(this.Comparador);
            Params.Add(this.TipoComparacion);
            return Params;
        }

        #endregion
        #region Copiar Y Pegar en cada regla que herede de sequential o composite
        /// <summary>
        /// Manejo del ItemChanging
        /// </summary>
        public event ItemListChanging OnItemListChanging
        {
            add
            {
                this.dItemListChanging += value;
            }
            remove
            {
                this.dItemListChanging -= value;
            }
        }

        ///// <summary>
        ///// Se da cuando se modifica la lista de items
        ///// </summary>
        ///// <param name="e"></param>
        protected override void OnListChanging(ActivityCollectionChangeEventArgs e)
        {
            foreach (IResultActivity time in e.AddedItems)
            {
                time.OnItemListChanging += new ItemListChanging(this.passItemListChanging);
            }

            if (this.dItemListChanging != null)
                this.dItemListChanging(e);

            base.OnListChanging(e);
        }
        private ItemListChanging dItemListChanging = null;

        /// <summary>
        /// Dispara el evento ListChanging para que lo capture el padre
        /// </summary>
        /// <param name="e"></param>
        private void passItemListChanging(ActivityCollectionChangeEventArgs e)
        {
            if (this.dItemListChanging != null)
                dItemListChanging(e);
        }
        #endregion

        #region IRule Members

        #endregion
    }
}
