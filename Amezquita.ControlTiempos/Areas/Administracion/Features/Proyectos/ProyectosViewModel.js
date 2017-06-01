function ProyectosViewModel() {
    var self = this;

    var fecha = moment();

    self.id = ko.observable(undefined);
    self.nombre = ko.observable(undefined);
    self.año = ko.observable(undefined);
    self.fechaInicio = ko.observable(fecha);
    self.fechaFinal = ko.observable(fecha);
    self.bolsaHoras = ko.observable(undefined);
    self.clientes = ko.observableArray([]);
    self.clienteId = ko.observable(undefined);
    self.usuarios = ko.observableArray([]);
    self.gerenteId = ko.observable(undefined);
    self.directorId = ko.observable(undefined);
    self.supervisorId = ko.observable(undefined);
    self.auditorId = ko.observable(undefined);
    self.alertasProyectoPorcentajeEjecutado = ko.observable(undefined);
    self.alertasProyectoDiasSinRegistrar = ko.observable(undefined);
    self.servicios = ko.observableArray([]);
    self.servicioId = ko.observable(undefined);
    self.actividades = ko.observableArray([]);
    self.actividadId = ko.observable(undefined);
    self.usuarioId = ko.observable(undefined);
    self.serviciosProyecto = ko.observableArray([]);
    self.servicioProyecto = ko.observable(undefined);
    self.actividadesProyecto = ko.observableArray([]);
    self.actividadProyecto = ko.observable(undefined);
    self.usuariosProyecto = ko.observableArray([]);
    self.usuarioProyecto = ko.observable(undefined);
    self.areas = ko.observableArray([]);
    self.areasProyecto = ko.observableArray([]);
    self.areaProyectoHoras = ko.observable(undefined);
    self.areaProyecto = ko.observable(undefined);

    self.obtenerClientes = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerClientes",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerUsuarios = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerUsuarios",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerServicios = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerServicios",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerServiciosProyecto = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerServiciosPorProyecto/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerActividades = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerActividades",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerActividadesProyecto = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerActividadesPorProyecto/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerGrupoDeTrabajoProyecto = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerGrupoDeTrabajoPorProyecto/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.mostrarActividades = function(id) {
        self.obtenerActividadesProyecto(id).done(function(data) {
            self.id(id);
            self.actividadesProyecto(data);
            $("#actividades-proyecto-modal").modal({ backdrop: "static" });
        }).fail(function(jqXHR) {
            bootbox.alert("Obtener las actividades del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.guardarActividadesProyecto = function() {
        var actividades = {
            proyectoId: self.id(),
            actividades: _.map(self.actividadesProyecto(), function(s) { return s.id; })
        };

        $.ajax({
            url: apiUrl + "/GuardarActividadesProyecto",
            type: "POST",
            data: JSON.stringify(actividades),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function() {
            $("#actividades-proyecto-modal").modal("hide");
            bootbox.alert("Las actividades han sido guardadas correctamente.");
        }).fail(function(jqXHR) {
            bootbox.alert("Guardar las actividades del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.agregarActividadProyecto = function() {
        var existe = _.find(self.actividadesProyecto(), function(s) {
            return s.id === self.actividadProyecto().id;
        });

        if (existe === undefined) {
            self.actividadesProyecto.push(self.actividadProyecto());
        }
    };

    self.eliminarActividadProyecto = function() {
        self.actividadesProyecto.remove(this);
    };

    self.mostrarServicios = function(id) {
        self.obtenerServiciosProyecto(id).done(function(data) {
            self.id(id);
            self.serviciosProyecto(data);
            $("#servicios-proyecto-modal").modal({ backdrop: "static" });
        }).fail(function(jqXHR) {
            bootbox.alert("Obtener los servicios del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.guardarServiciosProyecto = function() {
        var servicios = {
            proyectoId: self.id(),
            servicios: _.map(self.serviciosProyecto(), function(s) { return s.id; })
        };

        $.ajax({
            url: apiUrl + "/GuardarServiciosProyecto",
            type: "POST",
            data: JSON.stringify(servicios),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function() {
            $("#servicios-proyecto-modal").modal("hide");
            bootbox.alert("Los servicios han sido guardados correctamente.");
        }).fail(function(jqXHR) {
            bootbox.alert("Guardar los servicios del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.agregarServicioProyecto = function() {
        var existe = _.find(self.serviciosProyecto(), function(s) {
            return s.id === self.servicioProyecto().id;
        });

        if (existe === undefined) {
            self.serviciosProyecto.push(self.servicioProyecto());
        }
    };

    self.eliminarServicioProyecto = function() {
        self.serviciosProyecto.remove(this);
    };

    self.mostrarGrupoDeTrabajo = function(id) {
        self.obtenerGrupoDeTrabajoProyecto(id).done(function(data) {
            self.id(id);
            self.usuariosProyecto(data);
            $("#grupo-proyecto-modal").modal({ backdrop: "static" });
        }).fail(function(jqXHR) {
            bootbox.alert("Obtener el grupo de trabajo del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.guardarGrupoDeTrabajo = function() {
        var usuarios = {
            proyectoId: self.id(),
            usuarios: _.map(self.usuariosProyecto(), function(s) { return s.id; })
        };

        $.ajax({
            url: apiUrl + "/GuardarGrupoDeTrabajoProyecto",
            type: "POST",
            data: JSON.stringify(usuarios),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function() {
            $("#grupo-proyecto-modal").modal("hide");
            bootbox.alert("Los usuarios han sido guardados correctamente.");
        }).fail(function(jqXHR) {
            bootbox.alert("Guardar el grupo de trabajo del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.agregarUsuarioGrupo = function() {
        var existe = _.find(self.usuariosProyecto(), function(s) {
            return s.id === self.usuarioProyecto().id;
        });

        if (existe === undefined) {
            self.usuariosProyecto.push(self.usuarioProyecto());
        }
    };

    self.eliminarUsuarioGrupo = function() {
        self.usuariosProyecto.remove(this);
    };

    self.obtenerAreas = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerAreas",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerAreasProyecto = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerAreasPorProyecto/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.mostrarAreasProyecto = function(id) {
        self.obtenerAreasProyecto(id).done(function(data) {
            self.id(id);
            self.areasProyecto(data);
            $("#areas-proyecto-modal").modal({ backdrop: "static" });
        }).fail(function(jqXHR) {
            bootbox.alert("Obtener áreas del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.guardarAreasProyecto = function() {
        var areas = {
            proyectoId: self.id(),
            areas: _.map(self.areasProyecto(), function(s) {
                return {
                    areaId: s.areaId,
                    horas: s.horas
                }
            })
        };

        $.ajax({
            url: apiUrl + "/GuardarAreasProyecto",
            type: "POST",
            data: JSON.stringify(areas),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function() {
            $("#areas-proyecto-modal").modal("hide");
            bootbox.alert("Las áreas han sido guardadas correctamente.");
        }).fail(function(jqXHR) {
            bootbox.alert("Guardar las áreas del proyecto. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.agregarAreaProyecto = function() {
        var existe = _.find(self.areasProyecto(), function(s) {
            return s.areaId === self.areaProyecto().id;
        });

        if (existe === undefined) {
            self.areasProyecto.push({
                areaId: self.areaProyecto().id,
                areaNombre: self.areaProyecto().nombre,
                horas: self.areaProyectoHoras()
            });
        }
    };

    self.eliminarAreaProyecto = function() {
        self.areasProyecto.remove(this);
    };

    self.refrescar = function() {
        $("#proyectos-repeater").repeater("render");
    };

    self.mostrar = function() {
        $("#proyecto-form").resetValidation();
        $("#proyecto-modal").modal({ backdrop: "static" });
    };

    self.ocultar = function() {
        $("#proyecto-modal").modal("hide");
        $("#proyecto-form").resetValidation();
    };

    self.reestablecer = function() {
        self.id(undefined);
        self.nombre(undefined);
        self.año(undefined);
        self.fechaInicio(moment());
        self.fechaFinal(moment());
        self.bolsaHoras(undefined);
        self.clienteId(undefined);
        self.gerenteId(undefined);
        self.directorId(undefined);
        self.supervisorId(undefined);
        self.auditorId(undefined);
        self.servicioId(undefined);
        self.actividadId(undefined);
        self.usuarioId(undefined);
        self.alertasProyectoDiasSinRegistrar(undefined);
        self.alertasProyectoPorcentajeEjecutado(undefined);
    };

    self.obtenerPorId = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerPorId/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.guardar = function() {
        if ($("#proyecto-form").valid()) {

            var proyecto = {
                id: self.id(),
                nombre: self.nombre(),
                año: self.año(),
                fechaInicio: self.fechaInicio(),
                fechaFinal: self.fechaFinal(),
                bolsaHoras: self.bolsaHoras(),
                clienteId: self.clienteId(),
                gerenteId: self.gerenteId(),
                directorId: self.directorId(),
                supervisorId: self.supervisorId(),
                auditorId: self.auditorId(),
                servicioId: self.servicioId(),
                actividadId: self.actividadId(),
                usuarioId: self.usuarioId(),
                alertasProyectoDiasSinRegistrar: self.alertasProyectoDiasSinRegistrar(),
                alertasProyectoPorcentajeEjecutado: self.alertasProyectoPorcentajeEjecutado()
            };

            var opt = {
                contentType: "application/json; chartset=utf-8",
                dataType: "json",
                type:"POST",
                data: JSON.stringify(proyecto)
            };

            if (proyecto.id == undefined) {
                opt.url = apiUrl + "/Crear";
            } else {
                opt.url = apiUrl + "/Editar";
            }

            $.ajax(opt).done(function() {
                self.refrescar();
                self.ocultar();
                self.reestablecer();
            }).fail(function(jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
        }
    };

    self.crear = function() {
        self.reestablecer();
        self.mostrar();
    };

    self.editar = function(id) {
        self.reestablecer();

        self.obtenerPorId(id).done(function(data) {
            self.id(data.id);
            self.nombre(data.nombre);
            self.año(data.año);
            self.fechaInicio(moment(data.fechaInicio));
            self.fechaFinal(moment(data.fechaFinal));
            self.bolsaHoras(data.bolsaHoras);
            self.clienteId(data.clienteId);
            self.gerenteId(data.gerenteId);
            self.directorId(data.directorId);
            self.supervisorId(data.supervisorId);
            self.auditorId(data.auditorId);
            self.servicioId(data.servicioId);
            self.actividadId(data.actividadId);
            self.usuarioId(data.usuarioId);
            self.alertasProyectoDiasSinRegistrar(data.alertasProyectoDiasSinRegistrar);
            self.alertasProyectoPorcentajeEjecutado(data.alertasProyectoPorcentajeEjecutado);
            self.mostrar();
        }).fail(function(jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.eliminar = function(data) {
        $.ajax({
                url: apiUrl + "/Eliminar",
                data: JSON.stringify({ id: data }),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "Json"
            })
            .done(function() {
                self.refrescar();
                self.ocultar();
                self.reestablecer();
            }).fail(function(jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
    };

    self.obtenerServicios().done(function(data) {
        self.servicios(data);
    }).fail(function(jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de servicios. " + jqXHR.responseJSON.exceptionMessage);
    });

    self.obtenerActividades().done(function(data) {
        self.actividades(data);
    }).fail(function(jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de actividades. " + jqXHR.responseJSON.exceptionMessage);
    });

    self.obtenerClientes().done(function(data) {
        self.clientes(data);
        if (clienteId !== undefined && clienteId != null && clienteId !== "") {
            self.clienteId(clienteId);
            self.mostrar();
        }
    }).fail(function(jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de clientes. " + jqXHR.responseJSON.exceptionMessage);
    });

    self.obtenerUsuarios().done(function(data) {
        self.usuarios(data);
    }).fail(function(jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de usuarios. " + jqXHR.responseJSON.exceptionMessage);
    });

    self.obtenerAreas().done(function(data) {
        self.areas(data);
    }).fail(function(jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de áreas. " + jqXHR.responseJSON.exceptionMessage);
    });
}

var viewModel = new ProyectosViewModel();

ko.applyBindings(viewModel);

function generarColumnas(helpers, callback) {
    var column = helpers.columnAttr;
    var rowData = helpers.rowData;
    var customMarkup = "";

    switch (column) {
    case "id":
        var editar = "<button title=\"Editar\" name=\"edi-" + rowData.id + "\" id=\"edi-" + rowData.id + "\" type=\"button\" class=\"fa fa-pencil-square-o btn btn-sm btn-default\" data-bind=\"click: editar.bind($data, '" + rowData.id + "')\"></button>";
        var eliminar = "<button title=\"Eliminar\" name=\"eli-" + rowData.id + "\" id=\"eli-" + rowData.id + "\" type=\"button\" class=\"fa fa-trash-o btn btn-sm btn-danger\"  data-bind=\"click: eliminar.bind($data, '" + rowData.id + "')\"></button>";
        var servicios = "<button title=\"Servicios\" name=\"ser-" + rowData.id + "\" id=\"ser-" + rowData.id + "\" type=\"button\" class=\"fa fa-list-ul btn btn-sm btn-default\"  data-bind=\"click: mostrarServicios.bind($data, '" + rowData.id + "')\"></button>";
        var actividades = "<button title=\"Actividades\" name=\"act-" + rowData.id + "\" id=\"act-" + rowData.id + "\" type=\"button\" class=\"fa fa-list-ol btn btn-sm btn-default\"  data-bind=\"click: mostrarActividades.bind($data, '" + rowData.id + "')\"></button>";
        var grupo = "<button title=\"Grupo de Trabajo\" name=\"gru-" + rowData.id + "\" id=\"gru-" + rowData.id + "\" type=\"button\" class=\"fa fa-users btn btn-sm btn-default\"  data-bind=\"click: mostrarGrupoDeTrabajo.bind($data, '" + rowData.id + "')\"></button>";
        var areas = "<button title=\"Áreas\" name=\"areas-" + rowData.id + "\" id=\"areas-" + rowData.id + "\" type=\"button\" class=\"fa fa-pie-chart  btn btn-sm btn-default\"  data-bind=\"click: mostrarAreasProyecto.bind($data, '" + rowData.id + "')\"></button>";
        customMarkup = "<div class=\"TAC\">" + editar + "&nbsp;" + servicios + "&nbsp;" + actividades + "&nbsp;" + grupo + "&nbsp;" + areas + "&nbsp;" + eliminar + "</div>";
        break;
    default:
        customMarkup = helpers.item.text();
        break;
    }

    var html = helpers.item.html(customMarkup);
    ko.applyBindings(viewModel, $(html)[0]);

    callback();
}

var dataSource = function(options, callback) {
    var parametros = {
        buscar: "",
        numeroPagina: 0,
        registrosPagina: options.pageSize
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
        url: apiUrl + "/ObtenerPorPagina",
        data: parametros,
        dataType: "json",
        success: function(result) {
            var data = {
                count: result.total,
                columns: [
                    {
                        label: "Cliente",
                        property: "clienteNombre"
                    },
                    {
                        label: "Proyecto",
                        property: "nombre"
                    },
                    {
                        label: "Gerente",
                        property: "gerenteNombre"
                    },
                    {
                        label: "Año",
                        property: "año"
                    },
                    {
                        label: "Horas",
                        property: "bolsaHoras"
                    },
                    {
                        label: "",
                        property: "id",
                        width: 260
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

$("#proyectos-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});

$("#servicios-proyecto-modal").on("hidden.bs.modal", function() {
    viewModel.servicioProyecto(undefined);
});

$("#actividades-proyecto-modal").on("hidden.bs.modal", function() {
    viewModel.actividadProyecto(undefined);
});

$("#grupo-proyecto-modal").on("hidden.bs.modal", function() {
    viewModel.usuarioProyecto(undefined);
});