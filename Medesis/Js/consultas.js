$(document).ready(function () {
   
}); 

function llenarTablaConsultas(){

    var tablaListadoConsultas =

        $(".tablaListCons").DataTable({

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
            url: 'ListadoConsultas.aspx/listadoConsultasTabla',
            contentType: "application/json; charset=utf-8",
            dataSrc: function (r) {

                info = r.d;
                let datos = [];
                let arr = [];

                for (var i = 0; i < info.length; i++) {

                    btns = `<button type="button" title="Ver" id="btnVerC" class="btn btn-sm btn-info"> <i class="fas fa-eye"></i> </button>
                            <button type="button" title="Modificar" id="btnModC" class="btn btn-sm btn-primary"> <i class="fas fa-pen"></i> </button>
                            <button type="button" title="Baja" id="btnElC" class="btn btn-sm btn-danger"> <i class="fas fa-trash"></i> </button>`
                    arr = [
                        info[i].Id,
                        info[i].NroConsulta,
                        info[i].Medico,
                        info[i].Paciente,
                        moment(info[i].UltimaFechaAtencion).format('DD/MM/YYYY - HH:mm a'),
                        moment(info[i].FechaSiguienteConsulta).format('DD/MM/YYYY - HH:mm a'),
                        btns
                    ];

                    datos.push(arr);
                }

                return datos;
            }
        },

        columns: [
            { title: "Id", visible: false },
            { title: "Nro", autoWidth: true },
            { title: "Medico", autoWidth: true },
            { title: "Paciente", autoWidth: true },
            { title: "Ultima atencion", autoWidth: true },
            { title: "Siguiente consulta", autoWidth: true },
            { title: "Acciones", autoWidth: true }
        ],

        buttons: [
            {
                text: '<i class="fas fa-undo"></i> Actualizar',
                className: 'btn btn-sm btn-info btnActualizarTablaConsultas'
            }
        ]
    });

    $(".btnActualizarTablaConsultas").off('click');
    $(".btnActualizarTablaConsultas").on('click', function () {
        tablaListadoConsultas.ajax.reload(null, false);
    });

    //Funciones para acceder a los metodos.
    $('.tablaListCons').off('click', 'button');
    $('.tablaListCons').on('click', 'button', function (e) {
        var nombreAccion = $(this).attr('id');
        var idCon = tablaListadoConsultas.row($(this).parents().parents('tr')).data()[0];

        if (nombreAccion == 'btnVerC') {
            verConsulta(idCon);
        } else if (nombreAccion == 'btnModC') {
            modificarConsulta(idCon);
        } else if (nombreAccion == 'btnElC') {
            eliminarConsulta(idCon);
        }
    });

    return tablaListadoConsultas;
}

function validarCamposConsulta() {
    alertify.success('validar consulta');
    return false;
}

function verConsulta(id) {
    alertify.success('Ver consulta: ' + id);
}

function modificarConsulta(id) {
    alertify.notify('Modificar consulta: ' + id);
    $("[id*=modalModificarConsulta]").modal('show');
}

function eliminarConsulta(id) {
    alertify.error('Baja consulta: ' + id);
}


function limpiarCamposAgConsulta() {

    $("[id*=contenidoPagina_medicoConsulta]").removeClass('is-invalid').val(0);
    $("[id*=contenidoPagina_pacienteConsulta]").removeClass('is-invalid').val(0);
    $("[id*=contenidoPagina_fechaSiguienteConsulta]").removeClass('is-invalid').val("");

    return false;
}