
//Funcion Abrir modal formulario para registrar nuevos usuarios
$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("../Users/NuevoUsuario");
});

//Funcion Abrir modal formulario para Editar  usuarios

$(".btnEditar").click(function (eve) {
    $("#modal-content").load("../Users/UpdateUsers/" + $(this).data("id"));
});

//Delete
//Show The Popup Modal For DeleteComfirmation
$(".btnDelete").click(function (eve) {
    var xId = $(this).data("id");
    $("#DeleteConfirmation").modal("show");
    //alert(xId);

    $(".btnConfir").click(function (eve) {
        //alert(xId);
        $("#modal-content").load("../Users/Delete/" + xId);
        //location.reload();
        $("#DeleteConfirmation").modal("hide");
        setTimeout("location.href='/Crud/DatosUsuarios'", 1000);
    });


});

