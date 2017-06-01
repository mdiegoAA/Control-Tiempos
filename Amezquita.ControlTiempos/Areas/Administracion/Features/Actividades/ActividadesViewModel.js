﻿function ActividadesViewModel() {
    var self = this;

    self.id = ko.observable(undefined);
    self.codigo = ko.observable(undefined);
    self.nombre = ko.observable(undefined);
    self.necesitaAprobacion = ko.observable(undefined);
    self.esGenerica = ko.observable(undefined);

    self.refrescar = function() {
        $("#actividades-repeater").repeater("render");
    };

    self.mostrar = function() {
        $("#actividad-form").resetValidation();
        $("#actividad-modal").modal({ backdrop: "static" });
    };

    self.ocultar = function() {
        $("#actividad-modal").modal("hide");
        $("#actividad-form").resetValidation();
    };

    self.reestablecer = function() {
        self.id(undefined);
        self.codigo(undefined);
        self.nombre(undefined);
        self.necesitaAprobacion(undefined);
        self.esGenerica(undefined);
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
        if ($("#actividad-form").valid()) {

            var actividad = {
                id: self.id(),
                codigo: self.codigo(),
                nombre: self.nombre(),
                necesitaAprobacion: self.necesitaAprobacion(),
                esGenerica: self.esGenerica()
            };

            var opt = {
                contentType: "application/json; chartset=utf-8",
                dataType: "json",
                type: "POST",
                data: JSON.stringify(actividad)
            };

            if (actividad.id == undefined) {
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
            self.codigo(data.codigo);
            self.nombre(data.nombre);
            self.necesitaAprobacion(data.necesitaAprobacion);
            self.esGenerica(data.esGenerica);
            self.mostrar();
        }).fail(function (jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.eliminar = function(data) {
        $.ajax({
                url: apiUrl + "/Eliminar/{id}",
                tokens: { id: data },
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "Json"
            })
            .done(function() {
                self.refrescar();
                self.ocultar();
                self.reestablecer();
            }).fail(function(jqXHR) {
                console.log(jqXHR);
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
    };
}

var viewModel = new ActividadesViewModel();

ko.applyBindings(viewModel);

function generarColumnas(helpers, callback) {
    var column = helpers.columnAttr;
    var rowData = helpers.rowData;
    var customMarkup = "";

    switch (column) {
    case "id":
        var editar = "<button name=\"edi-" + rowData.id + "\" id=\"edi-" + rowData.id + "\" type=\"button\" class=\"fa fa-pencil-square-o btn btn-sm btn-default\" data-bind=\"click: editar.bind($data, '" + rowData.id + "')\"></button>";
        var eliminar = "<button name=\"eli-" + rowData.id + "\" id=\"eli-" + rowData.id + "\" type=\"button\" class=\"fa fa-trash-o btn btn-sm btn-danger\"  data-bind=\"click: eliminar.bind($data, '" + rowData.id + "')\"></button>";
        customMarkup = "<div class=\"TAC\">" + editar + "&nbsp;" + eliminar + "</div>";
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
                        label: "Código",
                        property: "codigo"
                    },
                    {
                        label: "Actividad",
                        property: "nombre"
                    },
                   {
                       label: "Genérica",
                       property: "esGenerica"
                   },
                    {
                        label: "Aprobar",
                        property: "necesitaAprobacion"
                    },
                    {
                        label: "",
                        property: "id",
                        width: 200
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

$("#actividades-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});