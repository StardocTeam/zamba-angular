$(document).ready(function () {
    if (document.querySelector("#zamba_index_2732").value == '') {
        document.querySelector('#zamba_save').disabled = true;
    }
    document.querySelector("#zamba_index_2732").addEventListener('change', function (event) {
        if (event.target.value != '') {
            document.querySelector('#zamba_save').disabled = false;
        } else {
            document.querySelector('#zamba_save').disabled = true;
        }
    })
});