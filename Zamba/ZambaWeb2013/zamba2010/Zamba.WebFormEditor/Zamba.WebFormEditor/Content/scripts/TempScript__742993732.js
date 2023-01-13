
        function SetRuleId(sender) {
            document.getElementById("hdnRuleId").name = sender.id;
        }
        function SetAsocId(sender) {
            document.getElementById("hdnAsocId").name = sender.id;
        }
        function makeCalendar(id) {
            $(function () {
                $('#' + id).datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOn: 'both',
                    buttonText: 'Abrir calendario',
                    buttonImage: 'images/calendar.png',
                    buttonImageOnly: true,
                    duration: "",
                    maxDate: '+0d',
                    dateFormat: 'dd/mm/yy',
                    constrainInput: true
                });
            });
        }

        function MakeCalendarAfterValue(idSource, idDestiny) {
            var mindate = $("#" + idSource).val().split("/");
            $('#' + idDestiny).datepicker("option", "minDate", new Date(mindate[2], mindate[1] - 1, mindate[0]));
        }

        function zamba_save_onclick() {
            document.getElementById("hdnRuleId").name = "zamba_save";
        }

        //Ezequiel: Funcion que selecciona automaticamente los valores relacionados en dos indices
        function AssociateIndexs(index1, index2) {
            $("#" + index1).change(
                    function() {
                        $("#" + index2 + " option").each(
                          function() {
                              if ($(this).val().trim() == $("#" + index1 + " option:selected").val().trim()) {
                                  $(this).attr("selected", "selected");
                              }
                          }
                       );
                       $("#" + index2).valid();
                    }
                );
        }

        //Crea las validaciones de los campos
        function makerequired() {
            $('#aspnetForm').validate(
				{
				    onsubmit: false,
				    ignore: ".disabled",
				    rules: {
				        //Razon social
				        zamba_index_1188: {
				            required: true,
				            maxlength: 150
				        },
				        //CIC
				        zamba_index_1220: {
				            required: true
				        },
				        //Region
				        zamba_index_1181: {
				            required: true,
				            digits: true
				        },
				        //Distrito
				        zamba_index_1183: {
				            required: true,
				            digits: true
				        },
				        //Calle
				        zamba_index_1192: {
				            required: true,
				            maxlength: 50
				        },
				        //Num
				        zamba_index_1193: {
				            required: true,
				            maxlength: 5,
				            digits: true
				        },
				        //Localidad
				        zamba_index_1202: {
				            required: true,
				            maxlength: 30
				        },
				        //Tipo de industria
				        zamba_index_1185: {
				            required: true,
				            digits: {//Se utiliza el tipo de validacion digits solo para aplicar si contiene o no un espacio.
				                depends: function(element) {
				                    return $(element).val() == " ";
				                }
				            }
				        },
						//Tipo de actividad
				        zamba_index_1189: {
				            required: true,
				            digits: {//Se utiliza el tipo de validacion digits solo para aplicar si contiene o no un espacio.
				                depends: function(element) {
				                    return $(element).val() == " " && $("#zamba_index_1220").val() != "00";
				                }
				            }
				        },
				        //Cod.Tipo de actividad
				        zamba_index_1223: {
				            required: true,
				            digits: {//Se utiliza el tipo de validacion digits solo para aplicar si contiene o no un espacio.
				                depends: function(element) {
				                    return $(element).val() == " " && $("#zamba_index_1220").val() != "00";
				                }
				            }
				        },
				        //Cod.Region
				        zamba_index_1221: {
				            required: true,
				            digits: true
				        },
				        //Cod.Distrito
				        zamba_index_1222: {
				            required: true,
				            digits: true
				        },
				        //Calle Alternativa 1
				        zamba_index_1194: {
				            maxlength: 50
				        },
				        //Num alternativo 1
				        zamba_index_1198: {
				            maxlength: 5,
				            digits: true
				        },
				        //Calle Alternativa 2
				        zamba_index_1195: {
				            maxlength: 50
				        },
				        //Num alternativo 2
				        zamba_index_1199: {
				            maxlength: 5,
				            digits: true
				        },
				        //Calle Alternativa 3
				        zamba_index_1196: {
				            maxlength: 50
				        },
				        //Num alternativo 3
				        zamba_index_1200: {
				            maxlength: 5,
				            digits: true
				        },
				        //Calle Alternativa 4
				        zamba_index_1197: {
				            maxlength: 50
				        },
				        //Num alternativo 4
				        zamba_index_1201: {
				            maxlength: 5,
				            digits: true
				        },
				        
				        //				        //Fecha ingreso FHV
				        //				        zamba_index_1207: {
				        //				            maxlength: 10,
				        //				            required: {
				        //				                depends: function(element) {
				        //				                    return !$(element).hasClass("disable");
				        //				                }
				        //				            },
				        //				            date: true
				        //				        },
				        //Nro. autorizaci�n de vuelco
				        zamba_index_1210: {
				            maxlength: 10,
				            digits: true
				        },
				        //Caudal FHV m3/h
				        //  zamba_index_1212: {
				        //       maxlength: 9,
				        //       number: true
				        //   },
				        //Caudal FHV m3/dia
				        //     zamba_index_1213: {
				        //        maxlength: 9,
				        //        number: true,
				        //        required: true
				        //    },
				        //Fecha alta
				        zamba_index_1214: {
				            maxlength: 10,
				            date: true
				        },
				        //Fecha baja
				        zamba_index_1215: {
				            maxlength: 10,
				            date: true
				        },
				        //CIC
				        zamba_index_1220: {
				            required: true,
				            maxlength: 10,
				            digits: {
				                depends: function(element) {
				                    return $(element).val() == " ";
				                }
				            }
				        },
				        //Nro. Expediente referencial
				        zamba_index_1231: {
				            required: true,
				            maxlength: 9,
						minlength: 9,
				            digits: true,
				            date: {//Se usa este tipo de validacion ya que no lo utiliza este campo, y esto hace que se pueda validar el formato
				                depends: function(element) {
				                    var index = $(element).val().indexOf($("#zamba_index_1222").val());
				                    return (index == -1) || (index + 3 == $(element).val().length);
				                }
				            }
				        }
				    },
				    messages: {
				        //Cod.Region
				        zamba_index_1221: {
				            required: "Es necesario una regi�n valida.",
				            digits: "Es necesario una regi�n valida."
				        },
				        //Region
				        zamba_index_1181: {
				            required: "Es necesario una regi�n valida.",
				            digits: "Es necesario una regi�n valida."
				        },
				        //Cod.Distrito
				        zamba_index_1222: {
				            required: "Es necesario un distrito valido.",
				            digits: "Es necesario un distrito valido."
				        },
				        //Distrito
				        zamba_index_1183: {
				            required: "Es necesario un distrito valido.",
				            digits: "Es necesario un distrito valido."
				        },
				        //Tipo de industria
				        zamba_index_1185: {
				            required: "Es necesario un tipo de industria valido.",
				            digits: "Es necesario un tipo de industria valido."
				        },
				        //Tipo de actividad
				        zamba_index_1189: {
				            required: "Es necesario un tipo de actividad valido.",
				            digits: "Es necesario un tipo de actividad valido."
				        },
				        //Cod.Tipo de actividad
				        zamba_index_1223: {
				            required: "Es necesario un tipo de actividad valido.",
				            digits: "Es necesario un tipo de actividad valido."
				        },
				        //Raz�n social
				        zamba_index_1188: {
				            required: "Es necesario una raz�n social.",
				            maxlength: "Se ha exedido el limite de 150 caracteres."
				        },
				        //Calle
				        zamba_index_1192: {
				            required: "Es necesario una calle.",
				            maxlength: "Se ha exedido el limite de 50 caracteres."
				        },
				        //Num
				        zamba_index_1193: {
				            required: "Es necesario un numero de calle.",
				            maxlength: "Se ha exedido el limite de 5 caracteres.",
				            digits: "Debe ingresar solo numeros."
				        },
				        //Calle Alternativa 1
				        zamba_index_1194: {
				            maxlength: "Se ha exedido el l�mite de 50 caracteres."
				        },
				        //Num alternativo 1
				        zamba_index_1198: {
				            maxlength: "Se ha exedido el limite de 5 caracteres.",
				            digits: "Debe ingresar solo numeros."
				        },
				        //Calle Alternativa 2
				        zamba_index_1195: {
				            maxlength: "Se ha exedido el limite de 50 caracteres."
				        },
				        //Num alternativo 2
				        zamba_index_1199: {
				            maxlength: "Se ha exedido el limite de 5 caracteres.",
				            digits: "Debe ingresar solo numeros."
				        },
				        //Calle Alternativa 3
				        zamba_index_1196: {
				            maxlength: "Se ha exedido el limite de 50 caracteres."
				        },
				        //Num alternativo 3
				        zamba_index_1200: {
				            maxlength: "Se ha exedido el limite de 5 caracteres.",
				            digits: "Debe ingresar solo numeros."
				        },
				        //Calle Alternativa 4
				        zamba_index_1197: {
				            maxlength: "Se ha exedido el limite de 50 caracteres."
				        },
				        //Num alternativo 4
				        zamba_index_1201: {
				            maxlength: "Se ha exedido el limite de 5 caracteres.",
				            digits: "Debe ingresar solo numeros."
				        },
				        //Localidad
				        zamba_index_1202: {
				            maxlength: "Se ha exedido el limite de 30 caracteres.",
				            required: "Es necesario una localidad."
				        },
				         //FHV
				        //   zamba_index_1205: {
				        //       maxlength: "Se ha exedido el l�mite de 10 caracteres.",
				        //      required: "Es necesario un FHV.",
				        //          digits: "Es necesario un FHV."
				        //    },
				        //     //Nro.FHV
				        //   zamba_index_1206: {
				        //         maxlength: "Se ha exedido el l�mite de 10 caracteres.",
				        //        required: "Es necesario un n�mero de FHV.",
				        //          digits: "Debe ingresar solo n�meros."
				        //   },
				        //Fecha ingreso FHV
				        //				        zamba_index_1207: {
				        //				            maxlength: "Se ha exedido el l�mite de 10 caracteres.",
				        //				            required: "Es necesario una fecha de ingreso.",
				        //				            date: "Debe ingresar una fecha."
				        //				        },
				        //Nro. Autorizacion de vuelco
				        zamba_index_1210: {
				            maxlength: "Se ha exedido el limite de 10 caracteres.",
				            digits: "Debe ingresar solo numeros."
				        },
				        //Caudal FHV m3/h
				        //  zamba_index_1212: {
				        //     maxlength: "Se ha exedido el l�mite de 9 caracteres.",
				        //      number: "Debe ingresar solo n�meros."
				        // },
				        //Caudal FHV m3/dia
				        //  zamba_index_1213: {
				        //     maxlength: "Se ha exedido el l�mite de 9 caracteres.",
				        //   number: "Debe ingresar solo n�meros.",
				        //    required: "Es necesario un caudal FHV m3/dia."
				        //  },
				        //Fecha alta
				        zamba_index_1214: {
				            maxlength: "Se ha exedido el limite de 10 caracteres.",
				            date: "Debe ingresar una fecha."
				        },
				        //Fecha baja
				        zamba_index_1215: {
				            maxlength: "Se ha exedido el limite de 10 caracteres.",
				            date: "Debe ingresar una fecha."
				        },
				        //CIC
				        zamba_index_1220: {
				            maxlength: "Se ha exedido el limite de 10 caracteres.",
				            required: "Es necesario un CIC.",
				            digits: "Es necesario un CIC."
				        },
				        //Nro. Expediente referencial
				        zamba_index_1231: {
				            required: "Es necesario un Nro. Expediente referencial.",
				            maxlength: "El Nro. Expediente referencial debe ser de 9 caracteres.",
						minlength: "El Nro. Expediente referencial debe ser de 9 caracteres.",
				            digits: "Debe ingresar solo numeros.",
				            date: "Por favor ingrese el codigo de distrito y luego el nro. de expediente referencial."
				        }
				    }
				}
				);
        }

        function ajustarselects() {
            if ($.browser.msie) {
                $('select').each(function () {
                    if ($(this).attr('multiple') == false) {
                        $(this).mousedown(function () {
                            if ($(this).css("width") != "auto") {
                                var width = $(this).width();
                                $(this).data("origWidth", $(this).css("width")).css("width", "auto");

                                // If the width is now less than before then undo 
                                if ($(this).width() < width) {
                                    $(this).unbind('mousedown');
                                    $(this).css("width", $(this).data("origWidth"));
                                }
                            }
                        })

                        // Handle blur if the user does not change the value 
                .blur(function () {
                    $(this).css("width", $(this).data("origWidth"));
                })

                        // Handle change of the user does change the value 
                .change(function () {
                    $(this).css("width", $(this).data("origWidth"));
                });
                    }
                });
            }
        }

        $(document).ready(function() {
            //	$("#zamba_index_1181 option").each(function() {
            //  alert($(this).val() + " - " + $(this).text());
            //});  
//            		$("#zamba_index_1223").append('<option value=" ">A Definir</option>');
//                        $("#zamba_index_1223 option").each(function() {
//                         if ($(this).val() != ' ') {
//                                       if ($(this).val().trim() == $("#zamba_index_1223 option:selected").val().trim()) {
//                    
//				$("#zamba_index_1223").append('<option selected="selected" value="' + $(this).val() + '">' + $(this).val() + '</option>'); 
//			}
//			else
//			{
//				$("#zamba_index_1223").append('<option value="' + $(this).val() + '">' + $(this).val() + '</option>'); 
//			}
//			}
//                        });

            ajustarselects();
            makeCalendar('zamba_index_1214');
            //makeCalendar('zamba_index_1207');

            makerequired();
            AssociateIndexs("zamba_index_1181", "zamba_index_1221");
            AssociateIndexs("zamba_index_1221", "zamba_index_1181");
            AssociateIndexs("zamba_index_1183", "zamba_index_1222");
            AssociateIndexs("zamba_index_1222", "zamba_index_1183");
            AssociateIndexs("zamba_index_1260", "zamba_index_1259");
            AssociateIndexs("zamba_index_1259", "zamba_index_1260");
            AssociateIndexs("zamba_index_1189", "zamba_index_1223");
            AssociateIndexs("zamba_index_1223", "zamba_index_1189");


            //  ConditionalEnable("zamba_index_1205", "zamba_index_1206", "Si");
            //  ConditionalEnable("zamba_index_1205", "zamba_index_1207", "Si", "En tr�mite");
            // $("#zamba_index_1205").change(function() {
            //      ConditionalEnable("zamba_index_1205", "zamba_index_1206", "Si");
            //       ConditionalEnable("zamba_index_1205", "zamba_index_1207", "Si", "En tr�mite");
            //  });

            ConditionalEnable("zamba_index_1219", "zamba_index_1210", "00");
            $("#zamba_index_1219").change(function() {
                ConditionalEnable("zamba_index_1219", "zamba_index_1210", "00");
            });

            ConditionalEnable("zamba_index_1233", "zamba_index_1234", "01");
            $("#zamba_index_1233").change(function() {
                ConditionalEnable("zamba_index_1233", "zamba_index_1234", "01");
            });

            $("#zamba_save").click(ValidateIndustryBeforeSave);

            MakeCalendarAfterValue('zamba_index_1214', 'zamba_index_1215');
            //MakeCalendarAfterValue('zamba_index_1214', 'zamba_index_1207');

            var tempValue = $("#zamba_index_1222").val();
            var tempValue3 = $("#zamba_index_1259").val();
            var tempValue2 = $("#zamba_index_1183").val();
            var tempValue4 = $("#zamba_index_1260").val();
            var tempValue5 = $("#zamba_index_1262").val();
            var tempValue6 = $("#zamba_index_1261").val();


            Temp_SaveData();
            Temp_ChangeCbox(document.getElementById('zamba_index_1221'));
            $("#zamba_index_1222").val(tempValue);
            $("#zamba_index_1259").val(tempValue3);
            $("#zamba_index_1183").val(tempValue2);
            $("#zamba_index_1260").val(tempValue4);
            LoadPartido();
            $("#zamba_index_1261").val(tempValue6);
            $("#zamba_index_1262").val(tempValue5);
            /////////Temporal
            $("#zamba_index_1222").change(LoadAsociateSelectGestion);
            $("#zamba_index_1183").change(LoadAsociateSelectGestion);
            $("#zamba_index_1261").change(LoadCodPatido);
            $("#zamba_index_1262").change(LoadPartidoByCodPartido);
            /////////

            
            //SelectActiveState();
            $("#zamba_index_1259").change(LoadGestionByCodDistritoGestion);
            $("#zamba_index_1260").change(LoadCodDistritoGestionByGestion);
            ///////////////////

            SeparateGridInspecciones();

            LoadLoadingRuleButtons();
            ConditionalEnableFalse("zamba_index_1221", "zamba_index_1231", "0"," ");
            ConditionalEnableFalse("zamba_index_1221", "zamba_index_1233", "0"," ");
            ConditionalEnableFalse("zamba_index_1221", "zamba_index_1232", "0"," ");
            $("#zamba_index_1221").change(function() {
                ConditionalEnableFalse("zamba_index_1221", "zamba_index_1231", "0"," ");
                ConditionalEnableFalse("zamba_index_1221", "zamba_index_1233", "0"," ");
                ConditionalEnableFalse("zamba_index_1221", "zamba_index_1232", "0"," ");
            });

			//ReadOnlyform();
			$(".ReadOnly").css("color","black").css("background-color","white");
			
			ConditionalRequire("zamba_index_1220","zamba_index_1223","01", " ");
			ConditionalRequire("zamba_index_1220","zamba_index_1189","01", " ");
			$("#zamba_index_1220").change(function(){
				ConditionalRequire("zamba_index_1220","zamba_index_1223","01", " ");
				ConditionalRequire("zamba_index_1220","zamba_index_1189","01", " ");
			});
        });
        

        function insertform() {
            var formid = 1056;
            var docid = $('#zamba_doc_id').val();
            var doctypeid = $('#zamba_doc_t_id').val();
            tb_show("Zamba Web - Inserci n de Documentos", "../WF/DocInsertModal.aspx?formid=" + formid + "&DocTypeID=" + doctypeid + "&DocID=" + docid + "&modal=true&TB_iframe=true&height=550&width=800", false);
        }

        function insertform2() {
            var formid = 1061;
            var docid = $('#zamba_doc_id').val();
            var doctypeid = $('#zamba_doc_t_id').val();
            tb_show("Zamba Web - Inserci n de Documentos", "../WF/DocInsertModal.aspx?formid=" + formid + "&DocTypeID=" + doctypeid + "&DocID=" + docid + "&modal=true&TB_iframe=true&height=550&width=800", false);
        }

         function insertform3() {
            var formid = 1065;
            var docid = $('#zamba_doc_id').val();
            var doctypeid = $('#zamba_doc_t_id').val();
            tb_show("Zamba Web - Inserci n de Documentos", "../WF/DocInsertModal.aspx?formid=" + formid + "&DocTypeID=" + doctypeid + "&DocID=" + docid + "&modal=true&TB_iframe=true&height=550&width=900", false);
        }
        function zamba_save_onclick() {
            document.getElementById("hdnRuleId").name = "zamba_save";
        }



        ////////////Estas funciones son temporalres
        var temp_ArrayData;
        var temp_ArrayDataCod;
        var temp_ArrayDataCodGest;
        function Temp_SaveData() {
            temp_ArrayData = $("#zamba_index_1183 option");
            temp_ArrayDataGest = $("#zamba_index_1260 option");
            temp_ArrayDataCod = $("#zamba_index_1222 option");
            temp_ArrayDataCodGest = $("#zamba_index_1259 option");
            //alert(temp_ArrayData.length);
//            $("#zamba_index_1183").html("");

//            $("#zamba_index_1183").html('<option value="">A Definir</option>');
//            $("#zamba_index_1260").html("");

//            $("#zamba_index_1260").html('<option value="">A Definir</option>');
//            $("#zamba_index_1222").html("");

//            $("#zamba_index_1222").html('<option value="">A Definir</option>');
//            $("#zamba_index_1259").html("");

//            $("#zamba_index_1259").html('<option value="">A Definir</option>');
        }

        function Temp_ShowData(val) {
            //alert(temp_ArrayData[i].innerHTML);
            //alert(temp_ArrayData[i].value);

            var html = "";
            var valueArray;
            switch (val) {
                case "1":
                    for (var i = 0; i < temp_ArrayData.length; i++) {
                        valueArray = temp_ArrayData[i].value;
                        if (valueArray == "101" || valueArray == "102" || valueArray == "103" || valueArray == "201" || valueArray == "202" || valueArray == "203")
                            html += '<option value="' + valueArray + '">' + temp_ArrayData[i].text + '</option>';

                    }
                    break;
                case "2":
                    for (var i = 0; i < temp_ArrayData.length; i++) {
                        valueArray = temp_ArrayData[i].value;
                        if (valueArray == "116" || valueArray == "118" || valueArray == "154" || valueArray == "141" || valueArray == "136")
                            html += '<option value="' + valueArray + '">' + temp_ArrayData[i].text + '</option>';
                    }
                    break;
                case "3":
                    for (var i = 0; i < temp_ArrayData.length; i++) {
                        valueArray = temp_ArrayData[i].value;
                        if (valueArray == "006" || valueArray == "069" || valueArray == "301")
                            html += '<option value="' + valueArray + '">' + temp_ArrayData[i].text + '</option>';
                    }
                    break;
                case "4":
                    for (var i = 0; i < temp_ArrayData.length; i++) {
                        valueArray = temp_ArrayData[i].value;
                        if (valueArray == "076" || valueArray == "003" || valueArray == "161" || valueArray == "130")
                            html += '<option value="' + valueArray + '">' + temp_ArrayData[i].text + '</option>';
                    }
                    break;
                case "5":
                    for (var i = 0; i < temp_ArrayData.length; i++) {
                        valueArray = temp_ArrayData[i].value;
                        if (valueArray == "155" || valueArray == "086" || valueArray == "166" || valueArray == "157")
                            html += '<option value="' + valueArray + '">' + temp_ArrayData[i].text + '</option>';
                    }
                    break;
                case "0":
                    for (var i = 0; i < temp_ArrayData.length; i++) {
                        valueArray = temp_ArrayData[i].value;
                        if (valueArray == "000")
                            html += '<option value="' + valueArray + '">' + temp_ArrayData[i].text + '</option>';
                    }
                    break;

                case "":
                    //html = '<option value="">A Definir</option>';
                    break;
            }

            html = '<option value="">A Definir</option>' + html;
            
            
            return html;

        }


        function Temp_ShowDataCod(val) {
            //alert(temp_ArrayData[i].innerHTML);
            //alert(temp_ArrayData[i].value);

            var html = "";
            var valueArray;
            switch (val) {
                case "1":
                    for (var i = 0; i < temp_ArrayDataCod.length; i++) {
                        valueArray = temp_ArrayDataCod[i].value;
                        if (valueArray == "101" || valueArray == "102" || valueArray == "103" || valueArray == "201" || valueArray == "202" || valueArray == "203")
                            html += '<option value="' + valueArray + '">' + temp_ArrayDataCod[i].text + '</option>';

                    }
                    break;
                case "2":
                    for (var i = 0; i < temp_ArrayDataCod.length; i++) {
                        valueArray = temp_ArrayDataCod[i].value;
                        if (valueArray == "116" || valueArray == "118" || valueArray == "154" || valueArray == "141" || valueArray == "136")
                            html += '<option value="' + valueArray + '">' + temp_ArrayDataCod[i].text + '</option>';
                    }
                    break;
                case "3":
                    for (var i = 0; i < temp_ArrayDataCod.length; i++) {
                        valueArray = temp_ArrayDataCod[i].value;
                        if (valueArray == "006" || valueArray == "069" || valueArray == "301")
                            html += '<option value="' + valueArray + '">' + temp_ArrayDataCod[i].text + '</option>';
                    }
                    break;
                case "4":
                    for (var i = 0; i < temp_ArrayDataCod.length; i++) {
                        valueArray = temp_ArrayDataCod[i].value;
                        if (valueArray == "076" || valueArray == "003" || valueArray == "161" || valueArray == "130")
                            html += '<option value="' + valueArray + '">' + temp_ArrayDataCod[i].text + '</option>';
                    }
                    break;
                case "5":
                    for (var i = 0; i < temp_ArrayDataCod.length; i++) {
                        valueArray = temp_ArrayDataCod[i].value;
                        if (valueArray == "155" || valueArray == "086" || valueArray == "166" || valueArray == "157")
                            html += '<option value="' + valueArray + '">' + temp_ArrayDataCod[i].text + '</option>';
                    }
                    break;
                case "0":
                    for (var i = 0; i < temp_ArrayDataCod.length; i++) {
                        valueArray = temp_ArrayDataCod[i].value;
                        if (valueArray == "000")
                            html += '<option value="' + valueArray + '">' + temp_ArrayDataCod[i].text + '</option>';
                    }
                    break;

                case "":
                    //html = '<option value="">A Definir</option>';
                    break;
            }

            html = '<option value="">A Definir</option>' + html;


            return html;

        }



        function Temp_ChangeCbox(ctl) {
            var objSelected = $("#" + ctl.id.toString() + " option:selected");
            //alert(objSelected.text());
            //alert(objSelected.val());
            var html = Temp_ShowData(objSelected.val());
            $("#zamba_index_1183").html(html);
            var html = Temp_ShowData(objSelected.val());
            $("#zamba_index_1260").html(html);
            var html = Temp_ShowDataCod(objSelected.val());
            $("#zamba_index_1222").html(html);
            var html = Temp_ShowDataCod(objSelected.val());
            $("#zamba_index_1259").html(html);
            LoadPartido();

       }



       function LoadPartido() {
           //alert("load partido");
           var id_distrito = $("#zamba_index_1222").val();
           var html = '<option value="">A Definir</option>';
           switch (id_distrito) {
               case "000":
                   $("#zamba_index_1262").html(html + '<option value="000">000</option>');
                   $("#zamba_index_1261").html(html + '<option value="000">Fuera del �rea de concesi�n</option>');
                   break;
               case "003":
                   $("#zamba_index_1262").html(html + '<option value="3">3</option>');
                   $("#zamba_index_1261").html(html + '<option value="3">ALMTE BROWN</option>');
                   break;
               case "006":
                   $("#zamba_index_1262").html(html + '<option value="4">4</option>');
                   $("#zamba_index_1261").html(html + '<option value="4">AVELLANEDA</option>');
                   break;
               case "069":
                   $("#zamba_index_1262").html(html + '<option value="25">25</option>');
                   $("#zamba_index_1261").html(html + '<option value="25">LANUS</option>');
                   break;
               case "076":
                   $("#zamba_index_1262").html(html + '<option value="63">63</option>');
                   $("#zamba_index_1261").html(html + '<option value="63">LOMAS DE ZAMORA</option>');
                   break;
               case "086":
                   //html = "";
                   html += '<option value="101">101</option>';
                   html += '<option value="135">135</option>';
                   html += '<option value="136">136</option>';

                   $("#zamba_index_1262").html(html);
                   html = '<option value="">A Definir</option>';
                   html += '<option value="101">MORON</option>';
                   html += '<option value="135">HURLINGHAM</option>';
                   html += '<option value="136">ITUZAINGO</option>';

                   $("#zamba_index_1261").html(html);
                   break;
               case "101":
               case "102":
               case "103":
                   $("#zamba_index_1262").html(html + '<option value="990">990</option>');
                   $("#zamba_index_1261").html(html + '<option value="990">CAPITAL FEDERAL</option>');
                   break;
               case "116":
                   $("#zamba_index_1262").html(html + '<option value="96">96</option>');
                   $("#zamba_index_1261").html(html + '<option value="96">SAN FERNANDO</option>');
                   break;
               case "118":
                   $("#zamba_index_1262").html(html + '<option value="97">97</option>');
                   $("#zamba_index_1261").html(html + '<option value="97">SAN ISIDRO</option>');
                   break;
               case "130":
                   $("#zamba_index_1262").html(html + '<option value="130">130</option>');
                   $("#zamba_index_1261").html(html + '<option value="130">EZEIZA</option>');
                   break;
               case "136":
                   $("#zamba_index_1262").html(html + '<option value="57">57</option>');
                   $("#zamba_index_1261").html(html + '<option value="57">TIGRE</option>');
                   break;
               case "141":
                   $("#zamba_index_1262").html(html + '<option value="110">110</option>');
                   $("#zamba_index_1261").html(html + '<option value="110">VICENTE LOPEZ</option>');
                   break;
               case "154":
                   $("#zamba_index_1262").html(html + '<option value="47">47</option>');
                   $("#zamba_index_1261").html(html + '<option value="47">SAN MARTIN</option>');
                   break;
               case "155":
                   $("#zamba_index_1262").html(html + '<option value="70">70</option>');
                   $("#zamba_index_1261").html(html + '<option value="70">LA MATANZA</option>');
                   break;
               case "157":
                   $("#zamba_index_1262").html(html + '<option value="117">117</option>');
                   $("#zamba_index_1261").html(html + '<option value="117">TRES DE FEBRERO</option>');
                   break;
               case "160":
                   $("#zamba_index_1262").html(html + '<option value="70">70</option>');
                   $("#zamba_index_1261").html(html + '<option value="70">LA MATANZA</option>');
                   break;
               case "161":
                   //html = "";
                   html += '<option value="30">30</option>';
                   html += '<option value="130">130</option>';
                   $("#zamba_index_1262").html(html);

                   html = '<option value="">A Definir</option>';
                   html += '<option value="30">ESTEBAN ECHEVERRIA</option>';
                   html += '<option value="130">EZEIZA</option>';
                   $("#zamba_index_1261").html(html);
                   break;
               case "166":
                   $("#zamba_index_1262").html(html + '<option value="70">70</option>');
                   $("#zamba_index_1261").html(html + '<option value="70">LA MATANZA</option>');
                   break;
               case "201":
               case "202":
               case "203":
                   $("#zamba_index_1262").html(html + '<option value="990">990</option>');
                   $("#zamba_index_1261").html(html + '<option value="990">CAPITAL FEDERAL</option>');
                   break;
               case "301":
                   $("#zamba_index_1262").html(html + '<option value="86">86</option>');
                   $("#zamba_index_1261").html(html + '<option value="86">QUILMES</option>');
                   break;
               case "":
                   $("#zamba_index_1262").html(html + '<option value="">A Definir</option>');
                   $("#zamba_index_1261").html(html + '<option value="">A Definir</option>');
                   break;
               default:
                   $("#zamba_index_1262").html(html + '<option value=""></option>');
                   $("#zamba_index_1261").html(html + '<option value=""></option>');
                   break;
           }

       }

       function LoadCodPatido() {
           $("#zamba_index_1262").val($("#zamba_index_1261").val());
       }

       function LoadPartidoByCodPartido() {
           $("#zamba_index_1261").val($("#zamba_index_1262").val());
       }


       ////////////////////////////////

       //Funciones agregadas 15-11-211

       function GetCurrentDate() {
			if($("#zamba_index_1214").val() == ''){
				var myDate = new Date();
				var d = myDate.getDate() + '/' + (myDate.getMonth() + 1) + '/' + myDate.getFullYear();
				//Asignamos a fecha de alta el valor actual
				$("#zamba_index_1214").val(d);
			}
       }

       function SelectActiveState() {
           var option = $("#zamba_index_1211").find("option[value='Activa']");
           if (option != null)
               option.attr("selected", "selected");
       }

       function LoadAsociateSelectGestion() {
           //Cargamos los partidos asociados
           LoadPartido();

           //Cargamos el codigo de distrito gestion a partir del cbox de gestion
           SelectCodDistritoGestionByGestion();

           //Cargamos el distrito gestion a partir del cbox de gestion
           SelectDistritoGestionByGestion();
       }

       function SelectCodDistritoGestionByGestion() {
           $("#zamba_index_1259").val($("#zamba_index_1222").val());
       }

       function SelectDistritoGestionByGestion() {
           $("#zamba_index_1260").val($("#zamba_index_1222").val());
       }

       function LoadCodDistritoGestionByGestion() {
           $("#zamba_index_1259").val($("#zamba_index_1260").val());
       }

       function LoadGestionByCodDistritoGestion() {
           $("#zamba_index_1260").val($("#zamba_index_1259").val());
       }
       
       
 function ExecuteRule(RuleId){
        if (RuleId == 7859 && (document.getElementById("zamba_index_1221").value == null || document.getElementById("zamba_index_1222").value == null || document.getElementById("zamba_index_1262").value == null || document.getElementById("zamba_index_1221").value == '' || document.getElementById("zamba_index_1222").value == '' || document.getElementById("zamba_index_1262").value == ''))
             {
             alert('Deben estar completos los campos Region, Distrito y Partido.');
             }
             else if (RuleId == 7835 && (document.getElementById("zamba_index_1221").value == null || document.getElementById("zamba_index_1222").value == null || document.getElementById("zamba_index_1262").value == null || document.getElementById("zamba_index_1221").value == '' || document.getElementById("zamba_index_1222").value == '' || document.getElementById("zamba_index_1262").value == '' || document.getElementById("zamba_index_1202").value == '' || document.getElementById("zamba_index_1202").value == null))
             {
                  alert('Deben estar completos los campos Region, Distrito, Partido y Localidad.');
             }
             else {
			parent.ShowLoadingAnimation();
			document.getElementById("hdnRuleId").name = "zamba_rule_" + RuleId;
			$('#aspnetForm').submit();
			}
		}
        
        
        //Funciones agregadas 23-11-2011
        function ConvertToUpperForm() {
            $('.ConvertToUpper').each(function(i, item) {
                var cadena = $(item).val();
                cadena = cadena.toUpperCase();
                $(item).val(cadena);
            });
        }

       
       ////////////////////////////////////

        
        function zamba_index_1261_onclick() {

        }

        //Funciones agregadas 23-11-2011

        function ConvertToUpperForm() {
            $('.ConvertToUpper').each(function(i, item) {
                var cadena = $(item).val();
                cadena = cadena.toUpperCase();
                $(item).val(cadena);
            });
        }

    
