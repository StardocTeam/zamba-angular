using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Services;
using Zamba.Core;
using System.Web.UI;
using System.Web.SessionState;

/// <summary>
/// Controlador de la vista y sus acciones para botones dinamicos
/// </summary>
public class DynamicButtonController
{
    public DynamicButtonController()
    {
    }

    /// <summary>
    /// Obtiene la vista del control donde se mostrarán los botones.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public DynamicButtonPartialViewBase GetViewHomeButtons(IUser user)
    {
        SDynamicButtons service = null;
        Page tempPage = null;

        try
        {
            tempPage = new Page();
            service = new SDynamicButtons();
            //Obtiene los botones del modelo
            List<IDynamicButton> model = service.GetRuleHomeButtons(user);
            //Carga ela vista
            DynamicButtonPartialViewBase view = (DynamicButtonPartialViewBase)tempPage.LoadControl("~/Views/UC/Home/DynamicButtonPartialView.ascx");
            //Le pasa los botones del modelo a la vista
            view.RenderButtons = model.OrderBy(b => b.ButtonOrder).ToList();
            return view;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return null;
        }
        finally
        {
            if (tempPage != null)
            {
                tempPage.Dispose();
                tempPage = null;
            }
            service = null;
        }
    }

    /// <summary>
    /// Obtiene la vista del control donde se mostrarán los botones.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public DynamicButtonPartialViewBase GetViewHeaderButtons(IUser user)
    {
        SDynamicButtons service = null;
        Page tempPage = null;

        try
        {
            tempPage = new Page();
            service = new SDynamicButtons();
            //Obtiene los botones del modelo
            List<IDynamicButton> model = service.GetRuleHeaderButtons(user);
            //Carga ela vista
            DynamicButtonPartialViewBase view = (DynamicButtonPartialViewBase)tempPage.LoadControl("~/Views/UC/Home/DynamicListPartialView.ascx");
            //Le pasa los botones del modelo a la vista
            view.RenderButtons = model.OrderBy(b => b.ButtonOrder).ToList();
            return view;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return null;
        }
        finally
        {
            if (tempPage != null)
            {
                tempPage.Dispose();
                tempPage = null;
            }
            service = null;
        }
    }

    /// <summary>
    /// Metodo utilizado para capturar el click de los botones de la vista
    /// </summary>
    /// <param name="user"></param>
    /// <param name="ruleID"></param>
    public void SetNewRuleExecution(IUser user, long ruleID)
    {
        HttpSessionState session = HttpContext.Current.Session;

        if (session != null)
        {
            IExecutionRequest newExecution = new ExecutionRequest();
            newExecution.ExecutionTask = new TaskResult();
            newExecution.ExecutionTask.AsignedToId = user.ID;
            newExecution.ExecutionTask.UserId = (int)user.ID;
            newExecution.ExecutionTask.TaskId = 0;
            newExecution.StartRule = ruleID;

            if (session["ListOfTask"] == null)
            {
                session["ListOfTask"] = new List<IExecutionRequest>();
            }

            ((List<IExecutionRequest>)session["ListOfTask"]).Add(newExecution);
        }
    }
}