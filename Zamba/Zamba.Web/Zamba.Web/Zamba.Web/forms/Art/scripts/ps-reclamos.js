
function setTaskData(_this) {
    $("#zamba_index_2685").focus();

    var indexId = _this.id.split("_")[2];

    var indexValue = _this.value;

    var entityId = getElementFromQueryString("DocType");
    var parentResultId = getElementFromQueryString("docid");
    var taskId = 0;

    if (entityId == null) {
        taskId = getElementFromQueryString("taskid");
        entityId = 0;
    }


    var validoC = false;
    var validoR = false;

    var validoS = false;

    var validoFS = false;
    var validoP = false;
    var validoCe = false;

    //CIA
    if ($("#zamba_index_10191").val() == '' || $("#zamba_index_10191").val() == 'A Definir') {
        $("#zamba_index_10191").css('border-color', 'red');
        validoC = false;
        console.log('Falta cargar la CIA');
        toastr.warning('Falta cargar la CIA');
    }
    else {
        $("#zamba_index_10191").css('border-color', 'blue');
        validoC = true;
    }

    //RAMO
    if ($("#zamba_index_19").val() == '' || $("#zamba_index_19").val() == 'A Definir') {
        $("#zamba_index_19").css('border-color', 'red');
        validoR = false;
        console.log('Falta cargar el RAMO');
        toastr.warning('Falta cargar el RAMO');
    }
    else {
        $("#zamba_index_19").css('border-color', 'blue');
        validoR = true;
    }


    if (validoC && validoR && $("#zamba_index_17").val() != '') {
        //Tengo siniestro
        validoS = true;
        var ruleId = 11528954;


        $("#zamba_index_17").css('border-color', 'blue');
        $("#zamba_index_1020022").css('border-color', 'blue');

        var subsin = ($("#zamba_index_2694").val() != '') ? $("#zamba_index_2694").val() : "0";

        if (saveIndexValidated(2694, entityId, parentResultId, taskId, subsin)
            && saveIndexValidated(10191, entityId, parentResultId, taskId, $("#zamba_index_10191").val())
            && saveIndexValidated(19, entityId, parentResultId, taskId, $("#zamba_index_19").val())
            && saveIndexValidated(17, entityId, parentResultId, taskId, $("#zamba_index_17").val())
            && saveIndexValidated(10180, entityId, parentResultId, taskId, $("#zamba_index_10180").val())
            && saveIndexValidated(1020022, entityId, parentResultId, taskId, $("#zamba_index_1020022").val())
            && saveIndexValidated(10165, entityId, parentResultId, taskId, $("#zamba_index_10165").val())) {

            console.log('Guardando Atributo y SubSiniestro');
            //aca se grabo el nro de subsiniestro
            try {
                console.log('Buscando Siniestro');
                executeTaskRule(ruleId, parentResultId, SiniestroActualizadoOK, SiniestroActualizadoError);
            }
            catch (err) {
                console.log(err);
                toastr.warning('Error al obtener los datos del Siniestro desde Rector' + err);
            }
        }

    }
    else if (validoC && validoR && $("#zamba_index_1020022").val() != '') {
        validoS = false;

        //Tengo poliza
        validoP = true;
        var ruleId = 11528930;
        $("#zamba_index_1020022").css('border-color', 'blue');
        $("#zamba_index_17").css('border-color', 'blue');


        if ($("#zamba_index_10180").val() == '') {
            //Tengo poliza pero no Fecha de Siniestro
            validoFS = false;
            $("#zamba_index_10180").css('border-color', 'red');

            console.log('Falta cargar la Fecha de Siniestro');
            toastr.warning('Falta cargar la Fecha de Siniestro');
        }
        else {
            validoFS = true;
            $("#zamba_index_10180").css('border-color', 'blue');
        }

        if ($("#zamba_index_10165").val() == '') {
            //Tengo poliza pero no Certificado
            validoCe = false;
            $("#zamba_index_10165").css('border-color', 'red');
            console.log('Falta cargar el Certificado');
            toastr.warning('Falta cargar el Certificado');
        }
        else {
            validoCe = true;
            $("#zamba_index_10165").css('border-color', 'blue');
        }

        if (validoCe && validoFS) {

            if (saveIndexValidated(2694, entityId, parentResultId, taskId, subsin)
                && saveIndexValidated(10191, entityId, parentResultId, taskId, $("#zamba_index_10191").val())
                && saveIndexValidated(19, entityId, parentResultId, taskId, $("#zamba_index_19").val())
                && saveIndexValidated(17, entityId, parentResultId, taskId, $("#zamba_index_17").val())
                && saveIndexValidated(10180, entityId, parentResultId, taskId, $("#zamba_index_10180").val())
                && saveIndexValidated(1020022, entityId, parentResultId, taskId, $("#zamba_index_1020022").val())
                && saveIndexValidated(10165, entityId, parentResultId, taskId, $("#zamba_index_10165").val())) {
                console.log('Guardando Atributo y SubSiniestro');
                //aca se grabo el nro de subsiniestro
                try {
                    console.log('Buscando Poliza');
                    executeTaskRule(ruleId, parentResultId, PolizaActualizadoOK, PolizaActualizadoError);
                }
                catch (err) {
                    console.log(err);
                    toastr.warning('Error al obtener los datos de la Poliza desde Rector' + err);
                }
            }
        }
    }
    else {
        $("#zamba_index_1020022").css('border-color', 'red');
        $("#zamba_index_17").css('border-color', 'red');

    }


}

function SiniestroActualizadoOK(response) {
    var CustomIndexs = [];
    CustomIndexs.push(1020002);
    CustomIndexs.push(10165);
    CustomIndexs.push(1020022);
    CustomIndexs.push(10180);

    //Siniestro
    //Patente, Nro Cliente, Nro Persona, Certificado, Productor, Patente, Poliza, Fecha Siniestro, Asegurado

    //Poliza
    //Asegurado,Nro Cliente, Cuit, Nro Persona -- Asegurado

    if (response.Vars != undefined && response.Vars != null) {
        if (response.Vars['error'] != undefined && response.Vars['error'] != null && response.Vars['error'] != '') {
            if ((response.Vars['error'].indexOf("no existe el archivo de la copia poliza en el servidor") == -1) && response.Vars['error'].indexOf("File unavailable") == -1 ) {
		   swal(response.Vars['error'], '', 'error');
            SiniestroActualizadoError(response);
            return;
        }
        }
    }


    processResultUpdates(response, CustomIndexs);
    var parentResultId = response.results[0].ID;
    executeTaskRule(1016970, parentResultId, RefSiniestroActualizadoOK, RefSiniestroActualizadoError);
    swal('Se pidio a Rector la Referenciacion del Siniestro', 'Para verla podes esperar un minuto y consultar el Expediente!    \n    Por favor no olvides de adjuntar la copia de la Poliza!!', 'success');

    //  location.reload();

}

function SiniestroActualizadoError(response) {
    console.log(response);
    //    toastr.warning('Error al obtener los datos del Siniestro desde Rector' + response.message);

    var CustomIndexs = [];
    CustomIndexs.push(1020002);
    CustomIndexs.push(10165);
    CustomIndexs.push(1020022);
    CustomIndexs.push(10180);

    cleanIndexs(CustomIndexs);
}

function PolizaActualizadoOK(response) {
    //error
    //msg
    if (response.Vars != undefined && response.Vars != null) {
        if (
            response.Vars["error"] != undefined &&
            response.Vars["error"] != null &&
            response.Vars["error"] != ""
        ) {
			//SI NO ES ALGUNO DE ESTOS ERRORES QUE AVANCE, DE LO CONTRARIO MOSTRAR ERROR Y BLANQUEAR DATOS
            if ((response.Vars['error'].indexOf("no existe el archivo de la copia poliza en el servidor") == -1) && response.Vars['error'].indexOf("File unavailable") == -1 ) {
               swal(response.Vars["error"], "", "error");
                PolizaActualizadoError(response);
            }
            return;
        }
    }

    swal("Se encontro la Poliza en Rector", {
        buttons: {
            ok: true
        }
    });

    var CustomIndexs = [];
    CustomIndexs.push(1020002);
    CustomIndexs.push(10165);
    CustomIndexs.push(1020022);
    CustomIndexs.push(10180);
    processResultUpdates(response, CustomIndexs);
    var parentResultId = response.results[0].ID;
    executeTaskRule(
        1016970,
        parentResultId,
        RefPolizaActualizadoOK,
        RefPolizaActualizadoError
    );
    swal(
        "Se pidio a Rector la Referenciacion de la Poliza",
        "Para verla podes esperar un minuto y consultar el Expediente!    \n    Por favor no olvides de adjuntar la copia de la Poliza!",
        "success"
    ); //                    location.reload();
}

function PolizaActualizadoError(response) {
    console.log(response);
    var CustomIndexs = [];
    CustomIndexs.push(1020002);
    CustomIndexs.push(10165);
    CustomIndexs.push(1020022);
    CustomIndexs.push(10180);

    cleanIndexs(CustomIndexs);

}

function RefSiniestroActualizadoOK(response) {

}

function RefSiniestroActualizadoError(response) {

}

function RefPolizaActualizadoOK(response) {

}

function RefPolizaActualizadoError(response) {

}
