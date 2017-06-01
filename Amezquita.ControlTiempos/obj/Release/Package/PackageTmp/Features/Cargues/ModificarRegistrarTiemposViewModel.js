function ModificarRegistarTiemposViewModel() {
    var self = this;

    var dateTime = moment("");

    self.proyectos = ko.observableArray([]);
    self.proyectoId = ko.observable(undefined);
    self.proyectoSeleccionado = ko.observable(undefined);
    self.servicios = ko.observableArray([]);
    self.servicioId = ko.observable(undefined);
    self.servicioSeleccionado = ko.observable(undefined);

    self.actividades = ko.observableArray([]);
    self.actividadId = ko.observable(undefined);
    self.actividadSeleccionado = ko.observable(undefined);

    self.id = ko.observable(undefined);
    self.fecha = ko.observable(moment({ year: dateTime.year(), month: dateTime.month(), day: dateTime.date() }));
    self.horaInicio = ko.observable(dateTime);
    self.horaFin = ko.observable(dateTime); 
    self.observaciones = ko.observable(undefined);


    self.fechaInicio = ko.observable(moment({ year: dateTime.year(), month: dateTime.month(), day: dateTime.date() }));
    self.fechaFin = ko.observable(moment({ year: dateTime.year(), month: dateTime.month(), day: dateTime.date() }));

    self.obtenerProyectos = function () {
        return $.ajax({
            url: apiUrl + "/ObtenerProyectos",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerServicios = function (id) {
        return $.ajax({
            url: apiUrl + "/ObtenerServicios/{id}",
            type: "GET",
            tokens: { id: id },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerActividades = function (id) {
        return $.ajax({
            url: apiUrl + "/ObtenerActividades/{id}",
            type: "GET",
            tokens: { id: id },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };
    
    self.limpiarFechas = function () {
        if (!$("#search").hasClass("glyphicon-search")) {
            self.fechaInicio = ko.observable(moment({ year: dateTime.year(), month: dateTime.month(), day: dateTime.date() }));
            self.fechaFin = ko.observable(moment({ year: dateTime.year(), month: dateTime.month(), day: dateTime.date() }));
        }
    }

    self.obtenerPorId = function (id) {
        return $.ajax({
            url: apiUrl + "/ObtenerPorId/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.mostrar = function () {
        $("#modificarRegistrarTiempo-form").resetValidation();
        $("#modificarRegistrarTiempo-modal").modal({ backdrop: "static" });
    };

    self.editar = function (id) {
        self.obtenerPorId(id).done(function (data) {

            var dateTimeEditarInicio = moment(data.fechaInicio);
            var fechaEditarInicio = moment({ year: dateTimeEditarInicio.year(), month: dateTimeEditarInicio.month(), day: dateTimeEditarInicio.date() });

            var dateTimeEditarFin = moment(data.fechaFin);

            self.id(data.id);
            self.fecha(fechaEditarInicio);
            self.horaInicio(dateTimeEditarInicio);
            self.horaFin(dateTimeEditarFin);
            self.observaciones(data.observaciones);
            self.mostrar();

            self.obtenerProyectos().done(function (dataProyecto) {
                self.proyectos(dataProyecto);
                self.proyectoId(data.proyectoId);

                if (self.proyectoId() != undefined) {
                    self.obtenerServicios(self.proyectoId())
                        .done(function (datosServicio) {
                            self.servicios(datosServicio);
                            self.servicioId(data.servicioId);
                        }).fail(function (jqXHR) {
                            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
                        });

                    self.obtenerActividades(self.proyectoId())
                        .done(function (datosActividad) {
                            self.actividades(datosActividad);
                            self.actividadId(data.actividadId);
                        }).fail(function (jqXHR) {
                            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
                        });
                }

            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });

        }).fail(function (jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.proyectoSeleccionado = function (e) {
        if (self.proyectoId() !== undefined) {
            self.obtenerServicios(self.proyectoId())
                .done(function (data) {
                    self.servicios(data);
                }).fail(function (jqXHR) {
                    bootbox.alert(jqXHR.responseJSON.exceptionMessage);
                });

            self.obtenerActividades(self.proyectoId())
                .done(function (data) {
                    self.actividades(data);
                }).fail(function (jqXHR) {
                    bootbox.alert(jqXHR.responseJSON.exceptionMessage);
                });
        }
        
    };

    self.guardar = function() {
        if ($("#modificarRegistrarTiempo-form").valid()) {

            var cargue = {
                id: self.id(),
                proyectoId: self.proyectoId(),
                servicioId: self.servicioId(),
                actividadId: self.actividadId(),
                fecha: self.fecha(),
                horaInicio: self.horaInicio().format("HH:mm"),
                horaFin: self.horaFin().format("HH:mm"),
                observaciones: self.observaciones()
            };

            $.ajax({
                contentType: "application/json; chartset=utf-8",
                url: apiUrl + "/ModificarControlTiempos",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(cargue)
            }).done(function () {
                $('#modificarRegistrarTiempo-modal').modal('hide');

                bootbox.dialog({
                    message: "La modificacion del cargue de horas se ha realizado correctamente.",
                    buttons: {
                        success: {
                            label: "Ok",
                            className: "btn-primary",
                            callback: function() {
                                $(location).attr("href", "../Cargues/ModificarRegistrarTiempos");
                            }
                        }
                    }
                });

                //bootbox.alert("La modificacion del cargue de horas se ha realizado correctamente.", function );
            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
        }
    }
}

var viewModel = new ModificarRegistarTiemposViewModel();

ko.applyBindings(viewModel);

function generarColumnas(helpers, callback) {
    var column = helpers.columnAttr;
    var rowData = helpers.rowData;
    var customMarkup = "";

    switch (column) {
        case 'fechaInicio':
        case 'fechaFin':
        case 'fechaRegistro':
            customMarkup = moment(helpers.item.text()).format('DD/MM/YYYY HH:mm');
            break;
        case "id":
            var editar = "<button name=\"edi-" + rowData.id + "\" id=\"edi-" + rowData.id + "\" type=\"button\" class=\"fa fa-pencil-square-o btn btn-sm btn-default\" data-bind=\"click: editar.bind($data, '" + rowData.id + "')\"></button>";
            customMarkup = "<div class=\"TAC\">" + editar + "</div>";
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

    if (viewModel.fechaInicio().format('YYYY') !== "Invalid date" && viewModel.fechaFin().format('YYYY') !== "Invalid date") {

        var parametros = {
            buscar: "",
            numeroPagina: 0,
            registrosPagina: options.pageSize,
            esApp: false,
            fechaInicio: null,
            fechaFin: null
        };

        var fechaInicio = viewModel.fechaInicio().format('YYYY') + "-" + viewModel.fechaInicio().format('M') + "-" + viewModel.fechaInicio().format('D');
        var fechaFin = viewModel.fechaFin().format('YYYY') + "-" + viewModel.fechaFin().format('M') + "-" + viewModel.fechaFin().format('D');

        parametros.fechaInicio = fechaInicio;
        parametros.fechaFin = fechaFin;

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
            url: apiUrl + "/ObtenerActividadesRegistradas",
            data: parametros,
            dataType: "json",
            success: function(result) {
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
                            label: "Inicio",
                            property: "fechaInicio"
                        },
                        {
                            label: "Fin",
                            property: "fechaFin"
                        },
                        {
                            label: "Modificar",
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
    }
    else {
        callback(null);
    }
};

$("#historial-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});