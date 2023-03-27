var serverErrorMessage = "Hubo un problema al procesar la información";
var InvitacionOKMessage = "Invitacion enviada.";
var ConsultaOKMessage = "Hemos registrado tu consulta, te enviaremos la respuesta a tu mail.";
var SugerenciaOKMessage = "Gracias por tu sugerencia, la tendremos en cuenta para el plan de mejoras.";
var ReclamoOKMessage = "Hemos registrado tu reclamo, lo atenderemos y te enviaremos la respuesta a tu mail.";

// Create our number formatter.
var currencyFormatter = new Intl.NumberFormat('es-AR', {
    style: 'currency',
    currency: 'ARS',
    maximumFractionDigits: '0'
});

function showServerErrorMessage() {
    swal({
        type: 'error',
        html: '<h6>' + serverErrorMessage + '</h6>'
    });
}

function showCustomServerErrorMessage(mensaje) {
    swal({
        type: 'error',
        html: '<h6>' + mensaje + '</h6>'
    });
}

function showValidationMessage(message) {
    swal({
        type: 'error',
        html: '<h6>' + message + '</h6>'
    });
}

function showInvitacionOKMessage() {
    swal({
        type: 'success',
        html: '<h6>' + InvitacionOKMessage + '</h6>'
    });
}

function showContactoOKMessage(tipo) {
    var mensaje = "";

    if (tipo == "C")
        mensaje = ConsultaOKMessage;
    if (tipo == "S")
        mensaje = SugerenciaOKMessage;
    if (tipo == "R")
        mensaje = ReclamoOKMessage;

    swal({
        type: 'success',
        html: '<h6>' + mensaje + '</h6>'
    });
}

function disableButton(boton) {
    $(boton).html('<i class="fa fa-fw fa-spinner fa-pulse fa-fw"></i>');
    $(boton).prop('disabled', true);
}

function enableButton(boton, texto) {
    $(boton).html(texto);
    $(boton).prop('disabled', false);
}

function isUrl(s) {
    var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/
    return regexp.test(s);
}

//Deshabilito el cache del browser
$.ajaxSetup({ cache: false });


//Convierto fecha para validacion
function NuevaFecha(texto,separador) {
    var fecha;
    var primeraBarra;
    var segundaBarra;
    var dia;
    var mes;
    var year;

    fecha = texto;
    primeraBarra = fecha.indexOf(separador);
    segundaBarra = fecha.indexOf(separador, primeraBarra + 1);
    dia = fecha.substring(0, primeraBarra);
    mes = fecha.substring(primeraBarra + 1, segundaBarra);
    year = fecha.substring(segundaBarra + 1);

    return new Date(year, mes - 1, dia);
}