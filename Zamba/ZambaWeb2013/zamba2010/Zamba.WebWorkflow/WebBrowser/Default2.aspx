<html>
<head>
    <link rel="stylesheet" href="Orden%20de%20Servicio.css" type="text/css">

    <script src="Orden%20de%20Servicio.js" type="text/javascript" language="JavaScript">
    </script>

    <script type="text/javascript" language="JavaScript">
function __doPostBack(){
if (Number(TotalPercent.value) == 100 || TotalPercent.value == "") {
document.myForm.submit();
}
else if (Number(TotalPercent.value) > 100) {
alert("Los Porcentajes ingresados superan la totalidad");
}
else {
alert("Debe Completar el 100% del costo de la factura");
}
}
function setNumTicket(){
if (document.getElementById("zamba_Index_10558").value != "" || document.getElementById("zamba_Index_10560").value != "" || document.getElementById("zamba_Index_10559").value != "") {
document.getElementById("zamba_Index_10556").value = document.getElementById("zamba_Index_10558").value + " " + document.getElementById("zamba_Index_10560").value + " " + document.getElementById("zamba_Index_10559").value
}
}
function setTotalPercent(){
document.getElementById("TotalPercent").value = Number(document.getElementById("zamba_Index_10565").value) + Number(document.getElementById("zamba_Index_10566").value) + Number(document.getElementById("zamba_Index_10567").value) +
Number(document.getElementById("zamba_Index_10568").value) +
Number(document.getElementById("zamba_Index_10569").value) +
Number(document.getElementById("zamba_Index_10570").value) +
Number(document.getElementById("zamba_Index_10571").value) +
Number(document.getElementById("zamba_Index_10572").value) +
Number(document.getElementById("zamba_Index_10573").value) +
Number(document.getElementById("zamba_Index_10574").value);
}
    </script>

    <style type="text/css">
        span.label
        {
            color: black;
            width: 30;
            height: 16;
            text-align: center;
            margin-top: 0;
            background: #ffF;
            font: bold 13px Arial;
        }
        span.c1
        {
            cursor: hand;
            color: black;
            width: 30;
            height: 16;
            text-align: center;
            margin-top: 0;
            background: #ffF;
            font: bold 13px Arial;
        }
        span.c2
        {
            cursor: hand;
            color: red;
            width: 30;
            height: 16;
            text-align: center;
            margin-top: 0;
            background: #ffF;
            font: bold 13px Arial;
        }
        span.c3
        {
            cursor: hand;
            color: #b0b0b0;
            width: 30;
            height: 16;
            text-align: center;
            margin-top: 0;
            background: #ffF;
            font: bold 12px Arial;
        }
        #zamba_index_5
        {
            width: 303px;
        }
        .style1
        {
            height: 112px;
        }
        .style2
        {
            height: 147px;
            width: 320px;
        }
        .style3
        {
            width: 155px;
        }
        .style4
        {
            width: 205px;
        }
        .style5
        {
            width: 147px;
        }
        .style6
        {
            width: 259px;
        }
        .style7
        {
            width: 320px;
        }
        .style8
        {
            height: 112px;
            width: 320px;
        }
        .style10
        {
            height: 22px;
        }
        .style11
        {
            height: 48px;
        }
        .style12
        {
            height: 32px;
        }
    </style>
</head>
<body>
    <form name="frmMain">
    <center>
        <table id="Main" style="margin-left: auto; width: 640px; margin-right: auto; text-align: left"
            border="0">
            <thead>
                <tr>
                <td class="style5" style="background-color: rgb(119,169,181);text-align:center">
                <span style="font-weight: bold">J.V.C</span>
                </td>
                    <td style="background-color: rgb(119,169,181);text-align:center" class="style4">
                        <br>
                        <img style="width: 62px; height: 63px" alt="" src="file://///Buesrvsql02/D/AYSA/ZAMBA/FORM/Aysa.bmp">
                    </td>
                <td style="vertical-align:middle;text-align:right;background-color: rgb(119,169,181)"> 
                <table>
                    <tr>
                    <td style="text-align:right">
                    <span style="font-weight: bold">Orden de Servicio</span> <span style="font-weight: bold">
                                Nro </span>
                            <input id="zamba_index_23" 
                        style="border-style: solid; border-width: 2px; width: 108px; height: 22px" 
                        size="9" name="zamba_index_23"
                                maxlenght="10">
                    </td>
                    </tr>
                    <tr><td style="text-align:right"><span style="font-weight: bold">Fecha 
                            <input id="zamba_index_36" style="border-style: solid; border-width: 2px; width: 107px;
                                height: 22px" onfocus="showCalendarControl(this);" size="5" 
                                name="zamba_index_36" /></span></td></tr></table>
                 
                </td>    
                </tr>
                
            </thead>
            <tbody>
                <tr>
                </tr>
                <tr>
                </tr>
                <table style="width:641px">
                    <tr>
                        <td >
                            
                        </td>
                        <td >
                           
                        </td>
                    </tr>
                   
                </table>
                <table>
                    <tr>
                        <td >
                        </td>
                    </tr>
                </table>
                <table style="text-align:left; width:640px;background-color: rgb(204,204,204)">
                    <tr>
                        <td>
                            Contratista
                        </td>
                        <td>
                          <div style="border:2px solid #000000;width:303px;height:1px">  
                              <select id="zamba_index_5" name="zamba_index_5" 
                                style=" width:303px; height: 2px;">
                            </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Obra
                        </td>
                        <td>
                        <div style="border: 2px solid #000000; width:303px;height:1px">
                            <select id="zamba_index_2" name="zamba_index_2" 
                                style=" width:303px">
                            </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nro deProyecto
                        </td>
                        <td>
                        <div style="border: 2px solid #000000; width:303px;height:1px">
                            <select id="zamba_index_3" name="zamba_index_3" 
                                style=" width:303px">
                            </select>
                            </div>
                        </td>
                        <tr>
                            <td class="style10">
                                Renglon
                            </td>
                            <td class="style10">
                            <div style="border: 2px solid #000000; width:303px;height:1px">
                                <select id="zamba_index_8" name="zamba_index_8" 
                                    style="width:303px">
                                </select>
                                </div>
                            </td>
                        </tr>                        
                </table>
                <table>
                <tr>
                <td style="height:40px">
                
                </td>
                </tr>
                
                </table>
                <table style="width:640px;background-color: rgb(204,204,204)">
                    <tr>
                        <td style="background-color: rgb(204,204,204)" class="style3">
                            Asunto
                        </td>
                        <td>
                            <input id="zamba_index_28" 
                                style="border: 2px solid #000000; width: 402px; height: 22px" size="51" name="zamba_index_28"
                                maxlenght="200">
                        </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                Referencia Contractual
                            </td>
                            <td>
                                <input id="zamba_index_29" 
                                    style="border: 2px solid #000000; width: 403px; height: 22px" size="49" name="zamba_index_29"
                                    maxlenght="200">
                            </td>
                        </tr>                    
                </table>
                
                <table style="width:640px">
                <tr><td class="style11"></td></tr>
                <tr><td></td></tr>
                    <tr>
                        <td style="background-color: rgb(255,255,255)" class="style6">
                            <input id="zamba_index_32" type="checkbox" name="zamba_index_32"/>Necesita Respuesta
                        </td>
                        <td>
                            Responder antes del
                            <input id="zamba_index_31" 
                                style="border-style: solid; border-width: 2px; width: 124px; height: 22px" onfocus="showCalendarControl(this);"
                                size="12" name="zamba_index_31" maxlenght="10">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: rgb(255,255,255)" class="style6">
                            
                                Contesta NP Nro
                                <input id="zamba_index_30" 
                                    style="border-style: solid; border-width: 2px; width: 43px; height: 22px" 
                                    size="4" name="zamba_index_30"
                                    maxlenght="30">
                       </td>
                       <td>
                                    
                                Parcial<input id="zamba_index_34" type="checkbox" name="zamba_index_34">Total<input
                                    id="zamba_index_35" type="checkbox" name="zamba_index_35">
                       </td>             
                       </tr>             
                </table>
                
                <table style="width:640px">
                
                <tr><td class="style12"></td></tr>
                <tr>
                <td>
                    Detalle de Orden de Servicio
                </td>
                </tr>
                
                <tr>
                <td style="width:640px">
                    <textarea id="zamba_index_18" 
                        style="border-style: solid; border-width: 2px; width: 640px; height: 221px; overflow: auto;" name="zamba_index_18"
                        maxlenght="1020" size="92"></textarea>
                
                </td>
                </tr>
                </table>
                
                <table style="width:640px">
                <tr>
                    <td class="style2" style="vertical-align:bottom">
                    
                -----------------------------------------------------</td>
                            </tr>
                            <tr>
                    <td style="text-align:center" class="style7">
                            Fima Autorizada y Sello
                    </td>
                    </tr>
                    
                    
                
                <tr>
                    <td class="style7">
                    </td>
                </tr>
                <tr>
                    <td class="style8" style="vertical-align:bottom">
                        <input class="button" id="zamba_save" type="submit" value="Guardar" name="zamba_save">
                      </td>
                      <td style="text-align:right;vertical-align:bottom" class="style1">
                        <input class="button" id="zamba_cancel" type="submit" value="Cancelar" name="zamba_cancel">
                      
                    </td>
                </tr>
                
                <tfoot>
                    <tr align="middle">
                        <td class="style7">
                            <br>
                        </td>
                        <td a="">
                            <br>
                        </td>
                    </tr>
                </tfoot>
        </table>
    </center>
    </form>
</body>
</html>
