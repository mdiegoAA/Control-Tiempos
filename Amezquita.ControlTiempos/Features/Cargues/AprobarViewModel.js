function HistorialViewModel() {
    var self = this;

    self.aprobar = function (id) {
        $.ajax({
            url: apiUrl + "/AprobarHoras",
            type: "POST",
            data: JSON.stringify({ id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "Json"
        })
        .done(function () {
            $("#pendientes-repeater").repeater("render");
        }).fail(function (jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });
    };
}

var viewModel = new HistorialViewModel();

ko.applyBindings(viewModel);

function generarColumnas(helpers, callback) {
    var column = helpers.columnAttr;
    var rowData = helpers.rowData;
    var customMarkup = "";

    switch (column) {
        case "id":
            var aprobar = "<button name=\"apro-" + rowData.id + "\" id=\"apro-" + rowData.id + "\" type=\"button\" class=\"fa fa-check-square-o btn btn-sm btn-default\" data-bind=\"click: aprobar.bind($data, '" + rowData.id + "')\"></button>";
            customMarkup = "<div class=\"TAC\">" + aprobar + "</div>";
            break;
        case 'fechaInicio':
        case 'fechaFin':
        case 'fechaRegistro':
            customMarkup = moment(helpers.item.text()).format('DD/MM/YYYY HH:mm');
            break;
        default:
            customMarkup = helpers.item.text();
            break;
    }

    var html = helpers.item.html(customMarkup);
    ko.applyBindings(viewModel, $(html)[0]);

    callback();
}

var dataSource = function (options, callback) {
    var parametros = {
        buscar: "",
        numeroPagina: 0,
        registrosPagina: options.pageSize,
        EsApp: false
    };

    if (options.pageIndex === 0) {
        parametros.numeroPagina = 1;
    } else {
        parametros.numeroPagina = options.pageIndex;
    }

    if (options.search) {
        parametros.buscar = options.search;
    } else {
        parametros.buscar = "";
    }

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: apiUrl + "/ObtenerHorasPorAprobar",
        data: parametros,
        dataType: "json",
        success: function (result) {
            var data = {
                count: result.total,
                columns: [
                    {
                        label: "Proyecto",
                        property: "proyectoNombre"
                    },
                    {
                        label: "Servicio",
                        property: "servicioNombre"
                    },
                    {
                        label: "Actividad",
                        property: "actividadNombre"
                    },
                    {
                        label: "Usuario",
                        property: "usuarioNombre"
                    },
                    {
                        label: "Inicio",
                        property: "fechaInicio"
                    },
                    {
                        label: "Fin",
                        property: "fechaFin"
                    },
                    {
                        label: "Aprobada",
                        property: "aprobada"
                    },
                    {
                        label: "Registrado",
                        property: "fechaRegistro"
                    },
                    {
                        label: "",
                        property: "id"
                    }
                ],
                items: result.datos,
                page: options.pageIndex,
                pages: Math.ceil(result.total / (options.pageSize || 50))
            };

            callback(data);
        }
    });
};

$("#pendientes-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});