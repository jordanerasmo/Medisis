$(document).ready(function () {

    $("[id*=mdModMedico]").on('hidden.bs.modal', function () {
        limpiarCamposMedico(1);
    });

});

function validarMedico(matComp) {

    nombreMedicoVal = $("[id*=contenidoPagina_nombreMedico]").val();
    apellidoMedicoVal = $("[id*=contenidoPagina_apellidoMedico]").val();
    especialidadMedicoVal = $("[id*=contenidoPagina_especialidadMedico]").val();
    matriculaMedicoVal = $("[id*=contenidoPagina_matriculaMedico]").val();

    //Obtenemos el valor de la matricula ya registrada para que no se cuente como existente en la validacion.
    if (matComp) {
        var matAcomparar = matComp;
    } else {
        var matAcomparar = " ";
    }
    
    respuestaGeneral = false;

    var validarNombre = validacionCampos('contenidoPagina_nombreMedico', nombreMedicoVal);
    var validarApellido = validacionCampos('contenidoPagina_apellidoMedico', apellidoMedicoVal);
    var validarEspecialidad = validacionCampos('contenidoPagina_especialidadMedico', especialidadMedicoVal, 'select');
    var validarMatricula = validacionCampos('contenidoPagina_matriculaMedico', matriculaMedicoVal);

    if (validarNombre &&
        validarApellido &&
        validarEspecialidad &&
        validarMatricula
    ) {
        respuestaGeneral = true;
    }
    else {
        respuestaGeneral = false;
    }

    //Funciones extras para la funcionalidad.
    function validacionCampos(nombreCampo, valorCampo, tipoCampo) {

        var expSoloCaracteres = /^[A-Za-zÁÉÍÓÚñáéíóúÑ]{3,10}?$/;
        var expMatricula = /^([A-Za-z]{1}?)+([1-9]{2}?)$/;

        if (tipoCampo == 'select') {
            valorComparar = 0;
        } else {
            valorComparar = '';
        }

        //validamos si el campo es rellenado.
        if (valorCampo != valorComparar) {
            $('[id*=' + nombreCampo + ']').removeClass('is-invalid');

            //Aplicamos validaciones personalizadas a cada campo.
            if (nombreCampo == 'contenidoPagina_nombreMedico') {
                if (expSoloCaracteres.test(valorCampo)) {
                    return true;
                } else {
                    mostrarMensaje(nombreCampo, 'noDisponible');
                    return false;
                }

            }
            else if (nombreCampo == 'contenidoPagina_apellidoMedico') {
                if (expSoloCaracteres.test(valorCampo)) {
                    return true;
                } else {
                    mostrarMensaje(nombreCampo, 'noDisponible');
                    return false;
                }
    
            }
            else if (nombreCampo == 'contenidoPagina_especialidadMedico') {
                return true;

            }
            else if (nombreCampo == 'contenidoPagina_matriculaMedico') {

                if (expMatricula.test(valorCampo)) {

                    $("[id*=contenidoPagina_matriculaMedico]").off('keyup');
                    $("[id*=contenidoPagina_matriculaMedico]").on('keyup', function () {
                        return validarMatriculaUnica($(this).val(), matAcomparar);
                    });

                    return validarMatriculaUnica($("[id*=contenidoPagina_matriculaMedico]").val(), matAcomparar);
                } else {
                    mostrarMensaje(nombreCampo, 'estructuraInc');
                    return false;
                }                
            }
        } else {
            mostrarMensaje(nombreCampo, 'incompleto');
            return false;
        }
    }
    function validarMatriculaUnica(matricula, matAcomparar) {

        var mat = JSON.stringify({ matricula });

        $.ajax({
            type: "POST",
            async: false,
            url: "NuevoMedico.aspx/validarMatriculaUnica",
            data: mat,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (r) {

                if (matricula.toUpperCase() == matAcomparar.toUpperCase()) {
                    mostrarMensaje('contenidoPagina_matriculaMedico', 'mismoRegistro')
                    respuesta = true;
                } else {
                    if (r.d) {
                        mostrarMensaje('contenidoPagina_matriculaMedico', 'disponible')
                        respuesta = true;
                    } else if (!r.d) {
                        mostrarMensaje('contenidoPagina_matriculaMedico', 'noDisponible')
                        respuesta = false;
                    }
                }
            }
        });

        return respuesta;
    }
    function mostrarMensaje(nombreCampo, tipoMensaje) {

        if (nombreCampo == 'contenidoPagina_nombreMedico') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un nombre";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Nombre incorrecto";
            }
            
        }
        else if (nombreCampo == 'contenidoPagina_apellidoMedico') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese un apellido";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Apellido incorrecto";
            }

        }
        else if (nombreCampo == 'contenidoPagina_especialidadMedico') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Seleccione una especialidad";
            }

        }
        else if (nombreCampo == 'contenidoPagina_matriculaMedico') {
            if (tipoMensaje == 'incompleto') {
                mensaje = "Ingrese una matricula";
            } else if (tipoMensaje == 'noDisponible') {
                mensaje = "Matricula ya registrada";
            } else if (tipoMensaje == 'disponible') {
                mensaje = "Matricula disponible";
            } else if (tipoMensaje == 'estructuraInc') {
                mensaje = "Estructura incorrecta (Ej: A56).";
            } else if (tipoMensaje == 'mismoRegistro') {
                mensaje = "Matricula igual a la registrada.";
            }
            
        }

        //Mostramos el color del mensaje dependiendo del tipo.
        if (tipoMensaje == 'disponible' || tipoMensaje =='mismoRegistro') {
            $("[id*=contenidoPagina_matriculaMedico]").removeClass('is-invalid').addClass('is-valid');
            $("[id*=contenidoPagina_matriculaMedico]").siblings('div.valid-feedback').html(mensaje);
        } else {
            $('[id*=' + nombreCampo + ']').addClass('is-invalid').removeClass('is-valid');
            $('[id*=' + nombreCampo + ']').siblings('div.invalid-feedback').html(mensaje);
        }

        
    }

    return respuestaGeneral;
}

function limpiarCamposMedico(accion) {

    //Si la variable accion es 1, los campos se limpian directamente (ejem al cerrar el modal).

    if (accion == 1) {
        $("[id*=contenidoPagina_nombreMedico]").removeClass('is-invalid').val("");
        $("[id*=contenidoPagina_apellidoMedico]").removeClass('is-invalid').val("");
        $("[id*=contenidoPagina_especialidadMedico]").removeClass('is-invalid').val(0);
        $("[id*=contenidoPagina_matriculaMedico]").removeClass('is-invalid').removeClass('is-valid').val("");

    } else {
        if ($("[id*=contenidoPagina_nombreMedico]").val() != "" ||
            $("[id*=contenidoPagina_apellidoMedico]").val() != "" ||
            $("[id*=contenidoPagina_especialidadMedico]").val() != 0 ||
            $("[id*=contenidoPagina_matriculaMedico]").val() != "") {

            alertify.confirm('Cancelar cambios', '¿Desea limpiar los campos con los datos ingresados?',
                function () {
                    $("[id*=contenidoPagina_nombreMedico]").removeClass('is-invalid').val("");
                    $("[id*=contenidoPagina_apellidoMedico]").removeClass('is-invalid').val("");
                    $("[id*=contenidoPagina_especialidadMedico]").removeClass('is-invalid').val(0);
                    $("[id*=contenidoPagina_matriculaMedico]").removeClass('is-invalid').val("");
                },
                function () { }).set({
                    transition: 'fade',
                    position: 'bottom-center',
                    closable: false,
                    movable: false,
                    labels: { ok: 'Aceptar', cancel: 'Cancelar' }
                });
        } else {
            $("[id*=contenidoPagina_nombreMedico]").removeClass('is-invalid').val("");
            $("[id*=contenidoPagina_apellidoMedico]").removeClass('is-invalid').val("");
            $("[id*=contenidoPagina_especialidadMedico]").removeClass('is-invalid').val(0);
            $("[id*=contenidoPagina_matriculaMedico]").removeClass('is-invalid').removeClass('is-valid').val("");
        }
    }

    return false;
}

function llenarTablaMedicos() {

    var tablaListadoMedicos =

        $(".tablaListMedico").DataTable({

            destroy: true,
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
                url: 'ListadoMedicos.aspx/listadoMedicosTabla',
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

                        btns = `<button type="button" title="Ver" id="btnVerM" class="btn btn-sm btn-info btnVerMedico"> <i class="fas fa-eye"></i> </button>
                                <button type="button" title="Modificar" id="btnModM" class="btn btn-sm btn-primary btnModificarMedico"> <i class="fas fa-pen"></i> </button>`
                        arr = [
                            info[i].Id,
                            info[i].Numero,
                            info[i].Nombres,
                            info[i].Especialidad,
                            info[i].Matricula,
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
                { title: "Numero", autoWidth: true},
                { title: "Nombre", autoWidth: true },
                { title: "Especialidad", autoWidth: true },
                { title: "Matricula", autoWidth: true },
                { title: "Estado", autoWidth: true },
                { title: "Acciones", autoWidth: true }
            ],

            buttons: [
                {
                    text: '<i class="fas fa-undo"></i> Actualizar',
                    className: 'btn btn-sm btn-info btnActualizarTablaMedicos'
                }
            ]
        });

    $(".btnActualizarTablaMedicos").off('click');
    $(".btnActualizarTablaMedicos").on('click', function () {
        tablaListadoMedicos.ajax.reload(null, false);
    })

    //Acciones para los metodos.
    $('.tablaListMedico').off('click', 'button');
    $('.tablaListMedico').on('click', 'button', function (e) {

        var nombreAccion = $(this).attr('id');
        var idMedico = tablaListadoMedicos.row($(this).parents().parents('tr')).data()[0];

        if (nombreAccion == 'btnVerM') {
            verMedico(idMedico);
        } else if (nombreAccion == 'btnModM') {
            modificarMedico(idMedico);
        } else if (nombreAccion == 'btnElM') {
            eliminarMedico(idMedico);
        }
    });

    return tablaListadoMedicos;
}

function verMedico(id) {
    /*
    $.ajax({
        type: "POST",
        url: "ListadoMedicos.aspx/listadoMedicosTabla",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (r) {
                
        }

    })    
    */
    alertify.success('Ver Medico: ' + id);
}

function modificarMedico(idMedico) {

    $("[id*=mdModMedico]").modal('show');

    var idM = JSON.stringify({idMedico});

    $.ajax({
        type: "POST",
        url: "ListadoMedicos.aspx/generarFormularioModMedico",
        data: idM,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (r) {

            var info = r.d[0];

            $("#nroMedico").html(info.Numero);
            $("[id*=contenidoPagina_nombreMedico]").val(info.Nombres);
            $("[id*=contenidoPagina_apellidoMedico]").val(info.Apellidos);
            $("[id*=contenidoPagina_especialidadMedico]").val(info.Especialidad);
            $("[id*=contenidoPagina_matriculaMedico]").val(info.Matricula);
            $("[id*=contenidoPagina_estadoMedico]").val(info.Estado);
           
            //Pasamos el nro de matricula a la funcion de validacion para que la detecte como valida.
            $("#btnModMedico").off('click');
            $("#btnModMedico").on('click', function (e) {
                
                var r = validarMedico(info.Matricula);

                if (!r) {
                    e.preventDefault();
                } else if (r) {
                    updateMedico(idMedico, info.Matricula);
                }
            });
            
        }

    })    
}

function updateMedico(idMedico, matComp) {

    var datosMedico = JSON.stringify({

        id: idMedico,
        nombreMedicoU: $("[id*=contenidoPagina_nombreMedico]").val(),
        apellidoMedicoU: $("[id*=contenidoPagina_apellidoMedico]").val(),
        especialidadMedicoU: $("[id*=contenidoPagina_especialidadMedico]").val(),
        matriculaMedicoU: $("[id*=contenidoPagina_matriculaMedico]").val(),
        estadoMedicoU: $("[id*=contenidoPagina_estadoMedico]").val(),
        matComp: matComp
    });

    $.ajax({
        type: "POST",
        url: "ListadoMedicos.aspx/updateMedico",
        data: datosMedico,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (r) {

            if (r.d == "modificado") {
                $("[id*=mdModMedico]").modal('hide');
                $(".tablaListMedico").DataTable().ajax.reload(null, false);
                alertify.success("Medico modificado correctamente.");

            } else if (r.d == "no-modificado") {
                alertify.error("Ocurrio un problema al modificar los datos.");

            } else if (r.d == "mismo-registro") {
                $("[id*=mdModMedico]").modal('hide');
                $(".tablaListMedico").DataTable().ajax.reload(null, false);
                alertify.notify("No se realizaron modificaciones.");

            }else if (r.d == "error-validacion") {
                alertify.error("Error al procesar la informacion.");

            } else if (r.d == "datos-incompletos") {
                alertify.error("Complete todos los campos.");
            }
        }

    })

}

function eliminarMedico(id) {
    alertify.error('Baja Medico: ' + id);
}