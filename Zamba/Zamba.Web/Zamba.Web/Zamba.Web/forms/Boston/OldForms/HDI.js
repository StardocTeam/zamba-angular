function CrearLinks(indexEntityId, indexDocumentId, controlId) {
    try {
        var DocId;
        var DocTypeId;
        var htmlName;
        var newInput;
        var primerFila = true;
        $(controlId + ' tr').each(function () {
            if (!primerFila) { //La primera fila esta vacia
                DocTypeId = $(this).children('td:nth(' + indexEntityId + ')').text();
                DocId = $(this).children('td:nth(' + indexDocumentId + ')').text();
                htmlName = 'zamba_asoc_' + DocId + '_' + DocTypeId;
                //			newInput = '<button type=\"button\"  id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  ><span class=\"glyphicon glyphicon-paperclip\" aria-hidden=\"true\"></span></button>';
                if (DocTypeId == 26033) {
                    newInput = '<img alt=\"\" id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  src=\"images/toolbars/note_pinned.png\" />';
                    //                newInput = '<img alt=\"\" id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  src=\"images/toolbars/page_text_32.png\" />';
                } else {
                    newInput = '<img alt=\"\" id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  src=\"images/toolbars/bullet_ball_blue.png\" />';
                }

                $(this).children('td:nth(0)').html(newInput);
            } else {
                primerFila = false;
            }
        });
        //Si hace clic en cualquier parte de la fila, abre el link en de la columna ejecutar(que esta oculto)
        $(controlId + ' td').click(function () {
            var inputHtml = $(this).parent().children('td:nth(0)').html();
            var botonId = inputHtml.substring(inputHtml.indexOf("id"), inputHtml.indexOf(" onclick")).replace("id=", "");
            var boton = document.getElementById(botonId);
          if (boton != undefined)  boton.click();
        });
    }
    catch (err)
    { }
    }