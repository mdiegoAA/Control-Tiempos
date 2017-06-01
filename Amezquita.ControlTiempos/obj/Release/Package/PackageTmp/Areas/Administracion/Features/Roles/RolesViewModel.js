﻿function RolesViewModel() {
    var self = this;

    self.id = ko.observable(undefined);
    self.name = ko.observable(undefined);

    self.refrescar = function() {
        $("#roles-repeater").repeater("render");
    };

    self.mostrar = function() {
        $("#rol-form").resetValidation();
        $("#rol-modal").modal({ backdrop: "static" });
    };

    self.ocultar = function() {
        $("#rol-modal").modal("hide");
        $("#rol-form").resetValidation();
    };

    self.reestablecer = function() {
        self.id(undefined);
        self.name(undefined);
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
        if ($("#rol-form").valid()) {

            var rol = {
                id: self.id(),
                name: self.name()
            };

            var opt = {
                contentType: "application/json; chartset=utf-8",
                dataType: "json",
                type: "POST",
                data: JSON.stringify(rol)
            };

            if (rol.id == undefined) {
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
            self.name(data.name);
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
}

var viewModel = new RolesViewModel();

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
                        label: "Rol",
                        property: "name"
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

$("#roles-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});