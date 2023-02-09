$(document).ready(function () {
	$("#BtnSaveModal").on("click", function () {
		$("#modalDoShowForm").hide();
		window.parent.document.getElementsByClassName("ExecuteDoshowFromRule")[0].click();
	});

	$("#BtncloseModal").on("click", function () {
		$("#modalDoShowForm").hide();
		window.parent.document.getElementsByClassName("ExecutecloseDoshowFromRule")[0].click();
	});
})
