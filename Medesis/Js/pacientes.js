$(document).ready(function () {

    $("[id*=mdModPac]").on('hidden.bs.modal', function () {
        limpiarCamposAgPaciente(1);
    });

});


function validarPaciente(docComp, emailComp) {

    nombrePacienteVal = $("[id*=contenidoPagina_nombrePaciente]").val();
    apellidoPacienteVal = $("[id*=contenidoPagina_apellidoPaciente]").val();
    fechaNacPacienteVal = moment($("[id*=contenidoPagina_fechaNacPaciente]").val(), 'YYYY/MM/DD').format('DD/MM/YYYY');
    documentoPacienteVal = $("[id*=contenidoPagina_documentoPaciente]").val();
    emailPacienteVal = $("[id*=contenidoPagina_emailPaciente]").val();
    telefonoPacienteVal = $("[id*=contenidoPagina_telefonoPaciente]").val();
    obraSocialPacienteVal = $("[id*=contenidoPagina_obraSocialPaciente]").val();

    //Obtenemos el valor del documento y el email ya registrados para que no se cuente como existente en la validacion.

    if (docComp) {
        var docAcomparar = docComp;
    } else {
        var docAcomparar = " ";
    }

    if (emailComp) {
        var emailAcomparar = emailComp;
    } else {
        var emailAcomparar = " ";
    }

    respuestaGeneral = false;

    var validarNombre = validacionCampos('contenidoPagina_nombrePaciente', nombrePacienteVal);
    var validarApellido = validacionCampos('contenidoPagina_apellidoPaciente', apellidoPacienteVal);
    var validarFechaNac = validacionCampos('contenidoPagina_fechaNacPaciente', fechaNacPacienteVal);
    var validarDocumento = validacionCampos('contenidoPagina_documentoPaciente', documentoPacienteVal);
    var validarEmail = validacionCampos('contenidoPagina_emailPaciente', emailPacienteVal);
    var validarTelefono = validacionCampos('contenidoPagina_telefonoPaciente', telefonoPacienteVal);
    var validarObraSocial = validacionCampos('contenidoPagina_obraSocialPaciente', obraSocialPacienteVal, 'select');

    if (validarNombre &&
        validarApellido &&
        validarFechaNac &&
        validarDocumento &&
        validarEmail &&
        validarTelefono &&
        validarObraSocial
    ) {
        respuestaGeneral = true;
    }
    else {
        respuestaGeneral = false;
    }

    //Funciones extras para la funcionalidad.
    function validacionCampos(nombreCampo, valorCampo, tipoCampo) {

        var expSoloCaracteres = /^[A-Za-zÁÉÍÓÚñáéíóúÑ /s]{3,20}?$/;
        var expDocumento = /^([1-6]{1}?)+([0-9]{6,7}?)$/;
        var expFecha = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
        var expEmail = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
        var expTelefono = /^[\s\.-]?\d{2,6}[\s-]?\d{3,9}$/;

        if (tipoCampo == 'select') {
            valorComparar = '0';
        } else {
            valorComparar = '';
        }

        //validamos si el campo es rellenado.
        if (valorCampo != valorComparar) {
            $('[id*=' + nombreCampo + ']').removeClass('is-invalid');

            //Aplicamos validaciones personalizadas a cada campo.
            if (nombreCampo == 'contenidoPagina_nombrePaciente') {
                if (expSoloCaracteres.test(valorCampo)) {
                    return true;
                } else {
                    mostrarMensaje(nombreCampo, 'noDisponible');
                    return false;
                }
            }
            else if (nombreCampo == 'contenidoPagina_apellidoPaciente') {
                if (expSoloCaracteres.test(valorCampo)) {
                    return true;
                } else {
                    mostrarMensaje(nombreCampo, 'noDisponible');
                    return false;
                }

            }
            else if (nombreCampo == 'contenidoPagina_fechaNacPaciente') {
                if (expFecha.test(valorCampo)) {
                    return true;
                } else {
                    mostrarMensaje(nombreCampo, 'noDisponible');
                    return false;
                }
            }
            else if (nombreCampo == 'contenidoPagina_documentoPaciente') {

                if (expDocumento.test(valorCampo)) {
                    $("[id*=contenidoPagina_documentoPaciente]").off('keyup');
                    $("[id*=contenidoPagina_documentoPaciente]").on('keyup', function () {
                        return validarDocumentoUnico($(this).val(), docAcomparar);
                    });
                    return validarDocumentoUnico($("[id*=contenidoPagina_documentoPaciente]").val(), docAcomparar);
                } else {
                    mostrarMensaje(nombreCampo, 'estructuraInc');
                    return false;
                }

            }
            else if (nombreCampo == 'contenidoPagina_emailPaciente') {
                if (expEmail.test(valorCampo)) {
                    $("[id*=contenidoPagina_emailPaciente]").off('keyup');
                    $("[id*=contenidoPagina_emailPaciente]").on('keyup', function () {
                        return validarEmailUnico($(this).val(), emailAcomparar);
                    });
                    return validarEmailUnico($("[id*=contenidoPagina_emailPaciente]").val(), emailAcomparar);
                } else {
                    mostrarMensaje(nombreCampo, 'estructuraInc');
                    return false;
                }
            }
            else if (nombreCampo == 'contenidoPagina_telefonoPaciente') {
                if (expTelefono.test(valorCampo)) {
                    return true
                } else {
                    mostrarMensaje(nombreCampo, 'noDisponible');
                    return false;
                }
            }
            else if (nombreCampo == 'contenidoPagina_obraSocialPaciente') {
                return true;
            } 

        } else {
            mostrarMensaje(nombreCampo, 'incompleto');
            return false;
        }
    }
    function validarDocumentoUnico(documento, docAcomparar) {

        var doc = JSON.stringify({ documento });

        $.ajax({
            type: "POST",
            async: false,
            url: "NuevoPaciente.aspx/validarDocumentoUnico",
            data: doc,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (r) {

                if (documento == docAcomparar) {
                    mostrarMensaje('contenidoPagina_documentoPaciente', 'mismoRegistro')
                    respuesta = true;
                } else {
                    if (r.d) {
                        mostrarMensaje('contenidoPagina_documentoPaciente', 'disponible')
                        respuesta = true;
                    } else if (!r.d) {
                        mostrarMensaje('contenidoPagina_documentoPaciente', 'noDisponible')
                        respuesta = false;
                    }
                }
            }
        });

        return respuesta;
    }
    function validarEmailUnico(email, emailAcomparar) {

        var corr = JSON.stringify({ email });

        $.ajax({
            type: "POST",
            async: false,
            url: "NuevoPaciente.aspx/validarEmailUnico",
            data: corr,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (r) {

                if (email == emailAcomparar) {
                    mostrarMensaje('contenidoPagina_emailPaciente', 'mismoRegistro')
                    respuesta = true;
                } else {
                    if (r.d) {
                        mostrarMensaje('contenidoPagina_emailPaciente', 'disponible')
                        respuesta = true;
                    } else if (!r.d) {
                        mostrarMensaje('contenidoPagina_emailPaciente', 'noDisponible')
                        respuesta = false;
                    }
                }
            }
        });

        return respuesta;
    }
    function mostrarMensaje(nombreCampo, tipoMensaje) {

        if (nombreCampo == 'contenidoPagina_nombrePaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un nombre";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Nombre incorrecto";
            }
        }
        else if (nombreCampo == 'contenidoPagina_apellidoPaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un apellido";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Apellido incorrecto";
            }

        }
        else if (nombreCampo == 'contenidoPagina_fechaNacPaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Seleccione una fecha";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Fecha no disponible";
            }

        }
        else if (nombreCampo == 'contenidoPagina_documentoPaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un Documento";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Documento ya registrado";
            } else if (tipoMensaje == 'disponible') {
                mensaje = "Documento disponible";
            } else if (tipoMensaje == 'estructuraInc') {
                mensaje = "Estructura incorrecta (Ej: 41035556).";
            } else if (tipoMensaje == 'mismoRegistro') {
                mensaje = "Documento igual al registrado.";
            }
        }
        else if (nombreCampo == 'contenidoPagina_emailPaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un Email";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Email ya registrado";
            } else if (tipoMensaje == 'disponible') {
                mensaje = "Email disponible";
            } else if (tipoMensaje == 'estructuraInc') {
                mensaje = "Estructura incorrecta (Ej: nombre.apellido@ej.com).";
            } else if (tipoMensaje == 'mismoRegistro') {
                mensaje = "Email igual al registrado.";
            }

        }
        else if (nombreCampo == 'contenidoPagina_telefonoPaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un telefono";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Telefono no disponible (Ej: 2302-699523)";
            }

        }
        else if (nombreCampo == 'contenidoPagina_obraSocialPaciente') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Seleccione una obra social";
            }

        }

        //Mostramos el color del mensaje dependiendo del tipo.
        if (tipoMensaje == 'disponible' || tipoMensaje == 'mismoRegistro') {
            $('[id*=' + nombreCampo + ']').removeClass('is-invalid').addClass('is-valid');
            $('[id*=' + nombreCampo + ']').siblings('div.valid-feedback').html(mensaje);
        } else {
            $('[id*=' + nombreCampo + ']').addClass('is-invalid').removeClass('is-valid');
            $('[id*=' + nombreCampo + ']').siblings('div.invalid-feedback').html(mensaje);
        }

    }

    return respuestaGeneral;
}

function llenarTablaPacientes() {

    var tablaListadoPacientes = 

        $(".tablaListPac").DataTable({

            scrollY: 400,
            bPaginate: false,
            "dom": 'Bft',

            language: {
                "search": "Buscar ",
                "processing": "Procesando...",
                "emptyTable": "No existe fuente de informacion en la tabla",
                "loadingRecords": "Cargando...",
                "zeroRecords": "No se encontraron resultados"
            },

            ajax: {
                type: "POST",
                url: 'ListadoPacientes.aspx/llenarTablaPacientes',
                contentType: "application/json; charset=utf-8",
                dataSrc: function (r) {

                    info = r.d;
                    let datos = [];
                    let arr = [];

                    for (var i = 0; i < info.length; i++) {

                        if (info[i].Estado == 0) {
                            var estado = "Activo";
                        } else if (info[i].Estado == 1) {
                            var estado = "Baja";
                        }

                        btns = `<button type="button" title="Ver" id="btnVerP" class="btn btn-sm btn-info"> <i class="fas fa-eye"></i> </button>
                                <button type="button" title="Modificar" id="btnModP" class="btn btn-sm btn-primary"> <i class="fas fa-pen"></i> </button>
                                <button type="button" title="Baja" id="btnElP" class="btn btn-sm btn-danger"> <i class="fas fa-trash"></i> </button>`
                        arr = [
                            info[i].Id,
                            info[i].Numero,
                            info[i].Nombres+" "+info[i].Apellidos,
                            moment(info[i].FechaNacimiento).format('DD/MM/YYYY'),
                            info[i].ObraSocialNombre.charAt(0).toUpperCase() + info[i].ObraSocialNombre.slice(1).toLowerCase(),
                            estado,
                            btns
                        ];

                        datos.push(arr);
                    }

                    return datos;
                }
            },

            columns: [
                { title: "Id", visible: false },
                { title: "Numero", autoWidth: true },
                { title: "Nombre", autoWidth: true },
                { title: "Nacimiento", autoWidth: true },
                { title: "Obra Social", autoWidth: true },
                { title: "Estado", autoWidth: true },
                { title: "Acciones", autoWidth: true }
            ],

            buttons: [
                {
                    text: '<i class="fas fa-undo"></i> Actualizar',
                    className: 'btn btn-sm btn-info btnActualizarTablaPacientes'
                }
            ]
        });

    $(".btnActualizarTablaPacientes").off('click');
    $(".btnActualizarTablaPacientes").on('click', function () {
        tablaListadoPacientes.ajax.reload(null, false);
    });

    //Funciones para acceder a los metodos.
    $('.tablaListPac').off('click', 'button');
    $('.tablaListPac').on('click', 'button', function (e) {
        var nombreAccion = $(this).attr('id');
        var idPaciente = tablaListadoPacientes.row($(this).parents().parents('tr')).data()[0];

        if (nombreAccion == 'btnVerP') {
            verPaciente(idPaciente);
        } else if (nombreAccion == 'btnModP') {
            modificarPaciente(idPaciente);
        } else if (nombreAccion == 'btnElP') {
            eliminarPaciente(idPaciente);
        }
    });


    return tablaListadoPacientes;
}

function limpiarCamposAgPaciente(accion) {

    //Si la variable accion es 1, los campos se limpian directamente (ejem al cerrar el modal).

    if (accion == 1) {

        $("[id*=contenidoPagina_nombrePaciente]").removeClass('is-invalid').val("");
        $("[id*=contenidoPagina_apellidoPaciente]").removeClass('is-invalid').val("");
        $("[id*=contenidoPagina_fechaNacPaciente]").removeClass('is-invalid').val("");
        $("[id*=contenidoPagina_documentoPaciente]").removeClass('is-invalid').removeClass('is-valid').val("");
        $("[id*=contenidoPagina_emailPaciente]").removeClass('is-invalid').removeClass('is-valid').val("");
        $("[id*=contenidoPagina_telefonoPaciente]").removeClass('is-invalid').val("");
        $("[id*=contenidoPagina_obraSocialPaciente]").removeClass('is-invalid').val(0);

    } else {
        if ($("[id*=contenidoPagina_nombrePaciente]").val() != "" ||
            $("[id*=contenidoPagina_apellidoPaciente]").val() != "" ||
            $("[id*=contenidoPagina_fechaNacPaciente]").val() != "" ||
            $("[id*=contenidoPagina_documentoPaciente]").val() != "" ||
            $("[id*=contenidoPagina_emailPaciente]").val() != "" ||
            $("[id*=contenidoPagina_telefonoPaciente]").val() != "" ||
            $("[id*=contenidoPagina_obraSocialPaciente]").val() != "0") {

            alertify.confirm('Cancelar cambios', '¿Desea limpiar los campos con los datos ingresados?',
                function () {
                    $("[id*=contenidoPagina_nombrePaciente]").removeClass('is-invalid').val("");
                    $("[id*=contenidoPagina_apellidoPaciente]").removeClass('is-invalid').val("");
                    $("[id*=contenidoPagina_fechaNacPaciente]").removeClass('is-invalid').val("");
                    $("[id*=contenidoPagina_documentoPaciente]").removeClass('is-invalid').removeClass('is-valid').val("");
                    $("[id*=contenidoPagina_emailPaciente]").removeClass('is-invalid').removeClass('is-valid').val("");
                    $("[id*=contenidoPagina_telefonoPaciente]").removeClass('is-invalid').val("");
                    $("[id*=contenidoPagina_obraSocialPaciente]").removeClass('is-invalid').val(0);
                },
                function () { }).set({
                    transition: 'fade',
                    position: 'bottom-center',
                    closable: false,
                    movable: false,
                    labels: { ok: 'Aceptar', cancel: 'Cancelar' }
                });
        } else {
            $("[id*=contenidoPagina_nombrePaciente]").removeClass('is-invalid').val("");
            $("[id*=contenidoPagina_apellidoPaciente]").removeClass('is-invalid').val("");
            $("[id*=contenidoPagina_fechaNacPaciente]").removeClass('is-invalid').val("");
            $("[id*=contenidoPagina_documentoPaciente]").removeClass('is-invalid').removeClass('is-valid').val("");
            $("[id*=contenidoPagina_emailPaciente]").removeClass('is-invalid').removeClass('is-valid').val("");
            $("[id*=contenidoPagina_telefonoPaciente]").removeClass('is-invalid').val("");
            $("[id*=contenidoPagina_obraSocialPaciente]").removeClass('is-invalid').val(0);
        }
    }

    return false;
}

function verPaciente(id) {
    alertify.success('Ver Paciente: ' + id);
}

function modificarPaciente(idPaciente) {
    $("#mdModPac").modal('show');

    var idP = JSON.stringify({ idPaciente });

    $.ajax({
        type: "POST",
        url: "ListadoPacientes.aspx/generarFormularioModPaciente",
        data: idP,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (r) {

            var info = r.d[0];
            var fechaFormat = moment(info.FechaNacimiento).format("YYYY-MM-DD");

            $("#nroPac").html(info.Numero);

            $("[id*=contenidoPagina_nombrePaciente]").val(info.Nombres);
            $("[id*=contenidoPagina_apellidoPaciente]").val(info.Apellidos);
            $("[id*=contenidoPagina_fechaNacPaciente]").val(fechaFormat);
            $("[id*=contenidoPagina_documentoPaciente]").val(info.Documento);
            $("[id*=contenidoPagina_emailPaciente]").val(info.Email);
            $("[id*=contenidoPagina_telefonoPaciente]").val(info.Telefono);
            $("[id*=contenidoPagina_obraSocialPaciente]").val(info.ObraSocial);
            $("[id*=contenidoPagina_estadoPaciente]").val(info.Estado);

            //Pasamos el nro de matricula a la funcion de validacion para que la detecte como valida.
            $("#btnModPac").off('click');
            $("#btnModPac").on('click', function (e) {

                var r = validarPaciente(info.Documento, info.Email);

                if (!r) {
                    e.preventDefault();
                } else if (r) {
                    updatePaciente(idPaciente, info.Documento, info.Email);
                }
            });

        }

    }) 
}

function updatePaciente(idPaciente, docAcomp, emailAcomp) {

    var datosPaciente = JSON.stringify({
        id: idPaciente,
        nombrePacienteU: $("[id*=contenidoPagina_nombrePaciente]").val(),
        apellidoPacienteU: $("[id*=contenidoPagina_apellidoPaciente]").val(),
        telefonoPacienteU: $("[id*=contenidoPagina_telefonoPaciente]").val(),
        emailPacienteU: $("[id*=contenidoPagina_emailPaciente]").val(),
        fechaNacPacienteU: moment($("[id*=contenidoPagina_fechaNacPaciente]").val()).format("DD/MM/YYYY"),
        documentoPacienteU: $("[id*=contenidoPagina_documentoPaciente]").val(),
        obraSocialPacienteU: $("[id*=contenidoPagina_obraSocialPaciente]").val(),
        estadoPacienteU: $("[id*=contenidoPagina_estadoPaciente]").val(),
        docAcomp: docAcomp,
        emailAcomp: emailAcomp
    });

    $.ajax({
        type: "POST",
        url: "ListadoPacientes.aspx/updatePaciente",
        data: datosPaciente,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (r) {

            if (r.d == "modificado") {
                $("[id*=mdModPac]").modal('hide');
                $(".tablaListPac").DataTable().ajax.reload(null, false);
                alertify.success("Paciente modificado correctamente.");

            } else if (r.d == "no-modificado") {
                alertify.error("Ocurrio un problema al modificar los datos.");

            } else if (r.d == "mismo-registro") {
                $("[id*=mdModPac]").modal('hide');
                $(".tablaListPac").DataTable().ajax.reload(null, false);
                alertify.notify("No se realizaron modificaciones.");

            } else if (r.d == "error-validacion") {
                alertify.error("Error al procesar la informacion.");

            } else if (r.d == "datos-incompletos") {
                alertify.error("Complete todos los campos.");
            }
        }

    })

}

function eliminarPaciente(id) {
    alertify.error('Baja Paciente: ' + id);
}