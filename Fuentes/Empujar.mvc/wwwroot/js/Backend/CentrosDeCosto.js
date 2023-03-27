﻿$(document).ready(function () {
    var iPulsado;
    var btnLabel;

    //Compleo la lista
    GetInfo();

    
    $("div[tipo=btnContainer]").on('click', 'i', function (e) {
        var tipo = e.target.attributes["tipo"].value;
        var mode = e.target.attributes["mode"].value;
        var numeral = e.target.attributes["numeral"].value;

        iPulsado = e.target;

        if (tipo == 'btnAdd' || tipo == 'btnEdit' || tipo == 'btnDel') {

            clearValidation($("#Formulario"));

            $("#Mode").val(mode);
            $('#Numeral').val(numeral);

            if (tipo == 'btnAdd') {

                $("#hTitulo").html("Nuevo Centro de costo");

                $('#Nombre').val("");
                $("#Nombre").prop("disabled", false);
                $('#Identificacion').val("");
                $("#Identificacion").prop("disabled", false);


                btnLabel = "Guardar";
            }


            if (tipo == 'btnEdit') {

                $("#hTitulo").html("Edicion");

                $('#Nombre').val($(iPulsado).attr('fn'));
                $('#Identificacion').val($(iPulsado).attr('fn'));

                disableInputs(false);

                btnLabel = "Guardar";
            }

            if (tipo == 'btnDel') {

                $("#hTitulo").html("Borrado");

                $('#Nombre').val($(iPulsado).attr('fn'));


                disableInputs(true);

                btnLabel = "Borrar";
            }

            $("#btnSave").html(btnLabel);
            $("#divView").hide();
            $("#divEdit").show();
            $("#Nombre").focus();
        }
    });

    $("#btnCancel").click(function () {
        $("#divEdit").hide();
        $("#divView").show();
    });

    $("#btnSave").click(function () {
        //submito el form solo si es (ADD o EDIT y es valido) o es DEL
        if ($('#Formulario').valid() || $("#Mode").val() == "D") {
            //Habilitar todos los inputs porque sino no se POSTEAN
            disableInputs(false);
            $("#Formulario").submit();
        }
    });

    $("#Formulario").submit(function (event) {
        event.preventDefault(); //prevent default action

        var post_url = $(this).attr("action"); //get form action url
        var request_method = $(this).attr("method"); //get form GET/POST method
        var form_data = $(this).serialize(); //Encode form elements for submission

        //Deshabilito el boton y pongo el spinner
        disableButton($('#btnSave'));
        $("#btnCancel").prop('disabled', true);

        $.ajax({
            url: post_url,
            type: request_method,
            data: form_data,
            dataType: 'json',
            success: function (data) {
                if (data.returnValue == "") {
                    if ($("#Mode").val() == "E") {
                        $(iPulsado).attr('fn', $('#Nombre').val());
                        $(iPulsado).attr('fn', $('#Identificacion').val());

                        $("div[numeral=" + $(iPulsado).attr('numeral') + "][tipo=Nombre]").html($('#Nombre').val());
                        $("div[numeral=" + $(iPulsado).attr('numeral') + "][tipo=Identificacion]").html($('#Identificacion').val());
                    }

                    if ($("#Mode").val() == "A") {
                        var item = new Object();

                        $("#divListaVacia").hide();

                        //Completo el objeto. vigente es true porque al crear la ONG se setea como habilitado por default.
                        item.nombre = $('#Nombre').val();
                        item.PrecioCompra = $('#Identificacion').val();
                        item.numeral = data.numeralID;
                        console.log(item);
                        console.log(item
)
                        AddTipo(item, $("#divTipos"));
                    }

                    if ($("#Mode").val() == "D") {
                        $(iPulsado).closest('div').parent().parent().remove();

                        if ($('#divTipos').children().length == 0)
                            $("#divListaVacia").show();

                    }

                    $("#divView").show();
                    $("#divEdit").hide();
                }
                else {
                    showCustomServerErrorMessage(data.returnValue);
                }
            },
            error: function (xhr, status) {
                showServerErrorMessage();
            },
            complete: function (xhr, status) {
                $('#btnSave').html(btnLabel);
                $('#btnSave').prop('disabled', false);
                $('#btnCancel').prop('disabled', false);
            }
        });
    });
});



    function GetInfo() {
        var hayTipos = false;
        $.ajax({
            url: '/CentrosDeCosto/GetInfo',
            type: "json",
            data: null,
            success: function (data) {
                $.each(data, function (i, item) {
                    AddTipo(item, $("#divTipos"));
                    hayTipos = true;
                    console.log(data)
                });
            },
            error: function (xhr, status) {
                showServerErrorMessage();
            },
            complete: function (xhr, status) {
                $('#divTipos').show();
                $('#divSpinner').hide();
                if (!hayTipos)
                    $("#divListaVacia").show();
            }
        });
    }



function AddTipo(item, contenedor) {
    
        contenedor.append("<div class=\"list-group-item list-group-item-action d-flex justify-content-between\">"
            + " <div class=\"row w-100\">"
            + "     <div class=\"col-1\" numeral=\"" + item.numeral + "\" tipo=\"Numeral\">" + item.numeral + "</div>"
            + "     <div class=\"col nombre\" numeral=\"" + item.numeral + "\" tipo=\"Nombre\">" + item.nombre + "</div>"
            + "     <div class=\"col precio-compra\" numeral=\"" + item.numeral + "\" tipo=\"Identificacion\">" + item.Identificacion + "</div>"
            + "     <div class=\"col-2 ps-0 pe-0 text-end\">"
            + "         <span class=\"float-right ms-3 pointer\">"
            + "             <i class=\"fas fa-pen\" tipo=\"btnEdit\" mode=\"E\" numeral=\"" + item.numeral + "\" fn=\"" + item.nombre + "\"></i>"
            + "         </span>"
            + "         <span class=\"float-right ms-3 pointer\">"
            + "             <i class=\"fas fa-trash\" tipo=\"btnDel\" mode=\"D\" numeral=\"" + item.numeral + "\" fn=\"" + item.nombre + "\"></i>"
            + "         </span>"
            + "     </div>"
            + " </div>"
            + "</div>");
        console.log(item);
}




function clearValidation(formElement) {
    //Internal $.validator is exposed through $(form).validate()
    var validator = $(formElement).validate();
    //Iterate through named elements inside of the form, and mark them as error free
    $('[name]', formElement).each(function () {
        validator.successList.push(this);//mark as error free
        validator.showErrors();//remove error messages if present
    });
    validator.resetForm();//remove error class on name elements and clear history
    validator.reset();//remove all error and success data
}

function disableInputs(disabled) {
    $("#Nombre").prop("disabled", disabled);
}

