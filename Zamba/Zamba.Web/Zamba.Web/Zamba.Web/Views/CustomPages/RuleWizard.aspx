<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleWizard.aspx.cs" Inherits="Zamba.Web.Views.CustomPages.RuleWizard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../scripts/mstratman-jQuery-Smart-Wizard-a975a34/styles/demo_style.css" rel="stylesheet" type="text/css" />

    <link href="../../scripts/mstratman-jQuery-Smart-Wizard-a975a34/styles/smart_wizard.css" rel="stylesheet" type="text/css" />
     <link href="../../content/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/mstratman-jQuery-Smart-Wizard-a975a34/js/jquery-2.0.0.min.js"></script>
    <script type="text/javascript" src="../../scripts/mstratman-jQuery-Smart-Wizard-a975a34/js/jquery.smartWizard.js"></script>
    <script type="text/javascript" src="../../scripts/jquery-ui-1.11.4.js"></script>
    <style>#ui-datepicker-div { font-size: 12px; } </style>
    <script type="text/javascript">
        $(document).ready(function () {
            // Smart Wizard     	
            $('#wizard').smartWizard({ transitionEffect: 'slideleft', onLeaveStep: leaveAStepCallback, onFinish: onFinishCallback, enableFinishButton: true });

            function leaveAStepCallback(obj) {
                var step_num = obj.attr('rel');
                return validateSteps(step_num);
            }

            function onFinishCallback() {
                if (validateAllSteps()) {
                    $('form').submit();
                }
            }
            $(".dateWizard").datepicker();
        });

        function validateAllSteps() {
            var isStepValid = true;

            //if (validateStep1() == false) {
            //    isStepValid = false;
            //    $('#wizard').smartWizard('setError', { stepnum: 1, iserror: true });
            //} else {
            //    $('#wizard').smartWizard('setError', { stepnum: 1, iserror: false });
            //}

            //if (validateStep3() == false) {
            //    isStepValid = false;
            //    $('#wizard').smartWizard('setError', { stepnum: 3, iserror: true });
            //} else {
            //    $('#wizard').smartWizard('setError', { stepnum: 3, iserror: false });
            //}

            //if (!isStepValid) {
            //    $('#wizard').smartWizard('showMessage', 'Please correct the errors in the steps and continue');
            //}

            return isStepValid;
        }


        function validateSteps(step) {
            var isStepValid = true;
            // validate step 1
            //if (step == 1) {
            //    if (validateStep1() == false) {
            //        isStepValid = false;
            //        $('#wizard').smartWizard('showMessage', 'Please correct the errors in step' + step + ' and click next.');
            //        $('#wizard').smartWizard('setError', { stepnum: step, iserror: true });
            //    } else {
            //        $('#wizard').smartWizard('hideMessage');
            //        $('#wizard').smartWizard('setError', { stepnum: step, iserror: false });
            //    }
            //}

            //// validate step3
            //if (step == 3) {
            //    if (validateStep3() == false) {
            //        isStepValid = false;
            //        $('#wizard').smartWizard('showMessage', 'Please correct the errors in step' + step + ' and click next.');
            //        $('#wizard').smartWizard('setError', { stepnum: step, iserror: true });
            //    } else {
            //        $('#wizard').smartWizard('hideMessage');
            //        $('#wizard').smartWizard('setError', { stepnum: step, iserror: false });
            //    }
            //}

            return isStepValid;
        }

        function validateStep1() {
            var isValid = true;
            // Validate Username
            var un = $('#username').val();
            if (!un && un.length <= 0) {
                isValid = false;
                $('#msg_username').html('Por favor complete numero de Orden de trabajo').show();
            } else {
                $('#msg_username').html('').hide();
            }

            // validate password
            //var pw = $('#password').val();
            //if (!pw && pw.length <= 0) {
            //    isValid = false;
            //    $('#msg_password').html('Please fill password').show();
            //} else {
            //    $('#msg_password').html('').hide();
            //}

            // validate confirm password
            //var cpw = $('#cpassword').val();
            //if (!cpw && cpw.length <= 0) {
            //    isValid = false;
            //    $('#msg_cpassword').html('Please fill confirm password').show();
            //} else {
            //    $('#msg_cpassword').html('').hide();
            //}

            //// validate password match
            //if (pw && pw.length > 0 && cpw && cpw.length > 0) {
            //    if (pw != cpw) {
            //        isValid = false;
            //        $('#msg_cpassword').html('Password mismatch').show();
            //    } else {
            //        $('#msg_cpassword').html('').hide();
            //    }
            //}
            return isValid;
        }

        function validateStep3() {
            var isValid = true;
            //validate email  email
            //var email = $('#email').val();
            //if (email && email.length > 0) {
            //    if (!isValidEmailAddress(email)) {
            //        isValid = false;
            //        $('#msg_email').html('Email is invalid').show();
            //    } else {
            //        $('#msg_email').html('').hide();
            //    }
            //} else {
            //    isValid = false;
            //    $('#msg_email').html('Please enter email').show();
            //}
            return isValid;
        }

        // Email Validation
        function isValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
            return pattern.test(emailAddress);
        }


    </script>
</head>
<body>
    <form action="#" method="post">
        <input type='hidden' name="issubmit" value="1" />
        <!-- Tabs -->
        <div>
            <div id="wizard" class="swMain">

                <ul>
                    <li><a href="#step-1">
                        <label class="stepNumber">1</label>
                        <span class="stepDesc">Inspección<br />
                            <small>Ingrese los datos</small>
                        </span>
                    </a></li>
                    <li><a href="#step-2">
                        <label class="stepNumber">2</label>
                        <span class="stepDesc">Inspector<br />
                            <small>Asigne el Inspector</small>
                        </span>
                    </a></li>
                    <li><a href="#step-3">
                        <label class="stepNumber">3</label>
                        <span class="stepDesc">Programación<br />
                            <small>Seleccione la fecha</small>
                        </span>
                    </a></li>
                </ul>
                <div id="step-1" style="width: 787px;">
                    <h2 class="StepTitle">Step 1: Inspección</h2>
                    <table cellspacing="3" cellpadding="3" align="center">
                        <tr>
                            <td align="center" colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Nro de ODT: </td>
                            <td align="left">
                                <input type="text" id="username" name="username" value="" class="txtBox">
                            </td>
                            <td align="left"><span id="msg_username"></span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Resultado: </td>
                            <td align="left">
                                <%--<input type="text" id="password" name="password" value="" class="txtBox">--%>
                                <select class="txtBox">
                                    <option value="1">A definir</option>
                                    <option value="26">Abandonada</option>
                                    <option value="17">Ausencia de CTM-MC</option>
                                    <option value="28">Cambio de Actividad</option>
                                    <option value="19">Caudal mayor al concedido en la FHV o ACV.</option>
                                    <option value="02">Cerrada</option>
                                </select>
                            </td>

                            <%--<td align="left"><span id="msg_password"></span>&nbsp;</td>--%>
                        </tr>
                        <tr>
                            <td align="right">Motivo: </td>
                            <td align="left">
                                <select class="txtBox">
                                    <option value="11">A definir</option>
                                    <option value="12">Acumar</option>
                                    <option value="22">ACV</option>
                                    <option value="21">Circuito Cerrado</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Firmo CN: </td>
                            <td align="left">
                                <input type="checkbox" id="username" name="username" value="" >
                            </td>
                            <td align="left"><span id="msg_username"></span>&nbsp;</td>
                        </tr>

                        <tr>
                            <td align="right">Observaciones: </td>
                            <td align="left">
                                <input type="text" id="username" name="username" value="" class="txtBox">
                            </td>
                      
                        </tr>

                    </table>
                </div>
                <div id="step-2" style="width: 787px;">
                    <h2 class="StepTitle">Step 2: Inspector</h2>
                    <table cellspacing="3" cellpadding="3" align="center">
                        <tr>
                            <td align="center" colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Nombre :</td>
                            <td align="left">
                                <input type="text" id="firstname" name="firstname" value="" class="txtBox">
                            </td>
                            <td align="left"><span id="msg_firstname"></span>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Apellido :</td>
                            <td align="left">
                                <input type="text" id="lastname" name="lastname" value="" class="txtBox">
                            </td>
                            <td align="left"><span id="msg_lastname"></span>&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div id="step-3" style="width: 787px;">
                    <h2 class="StepTitle">Step 3: Programación</h2>
                    <table cellspacing="3" cellpadding="3" align="center">
                        <tr>
                            <td align="center" colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">Fecha Inspección: </td>
                            <td align="left">
                               <%-- <input type="text" id="email" name="email" value="" class="txtBox">--%>
                                <input class="dateWizard">
                            </td>
                            <td align="left"><span id="msg_email"></span>&nbsp;</td>
                        </tr>

                        <tr>
                            <td align="right">Fecha Próxima Inspección: </td>
                            <td align="left">
                                 <input class="dateWizard">
                            </td>
                            <td align="left"><span id="msg_phone"></span>&nbsp;</td>
                        </tr>

                        <tr>
                            <td align="right">Fecha programada: </td>
                            <td align="left">
                                 <input class="dateWizard">
                            </td>
                            <td align="left"><span id="msg_phone"></span>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- End SmartWizard Content -->
        </div>
    </form>
</body>
</html>
