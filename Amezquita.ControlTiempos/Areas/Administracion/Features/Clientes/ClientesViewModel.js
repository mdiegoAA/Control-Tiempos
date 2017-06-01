function ClientesViewModel() {
    var self = this;

    self.id = ko.observable(undefined);
    self.codigo = ko.observable(undefined);
    self.nit = ko.observable(undefined);
    self.nombre = ko.observable(undefined);

    self.refrescar = function () {
        $("#clientes-repeater").repeater("render");
    };

    self.mostrar = function () {
        $("#cliente-form").resetValidation();
        $("#cliente-modal").modal({ backdrop: "static" });
    };

    self.ocultar = function () {
        $("#cliente-modal").modal("hide");
        $("#cliente-form").resetValidation();
    };

    self.reestablecer = function () {
        self.id(undefined);
        self.codigo(undefined);
        self.nit(undefined);
        self.nombre(undefined);
    };

    self.obtenerPorId = function (id) {
        return $.ajax({
            url: apiUrl + "/ObtenerPorId/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.guardar = function () {
        if ($("#cliente-form").valid()) {

            var cliente = {
                id: self.id(),
                codigo: self.codigo(),
                nit: self.nit(),
                nombre: self.nombre()
            };

            var opt = {
                contentType: "application/json; chartset=utf-8",
                dataType: "json",
                type: "POST",
                data: JSON.stringify(cliente)
            };

            if (cliente.id == undefined) {
                opt.url = apiUrl + "/Crear";
            } else {
                opt.url = apiUrl + "/Editar";
            }

            $.ajax(opt).done(function () {
                self.refrescar();
                self.ocultar();
                self.reestablecer();
            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
        }
    };

    self.crear = function () {
        self.reestablecer();
        self.mostrar();
    };

    self.editar = function (id) {
        self.reestablecer();

        self.obtenerPorId(id).done(function (data) {
            self.id(data.id);
            self.codigo(data.codigo);
            self.nit(data.nit);
            self.nombre(data.nombre);
            self.mostrar();
        }).fail(function (jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.eliminar = function (data) {
        $.ajax({
            url: apiUrl + "/Eliminar",
            data: JSON.stringify({ id: data }),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "Json"
        })
            .done(function () {
                self.refrescar();
                self.ocultar();
                self.reestablecer();
            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
    };
}

var viewModel = new ClientesViewModel();

ko.applyBindings(viewModel);

function generarColumnas(helpers, callback) {
    var column = helpers.columnAttr;
    var rowData = helpers.rowData;
    var customMarkup = "";

    switch (column) {
        case "id":
            var proy = "<a name=\"pro-" + rowData.id + "\" id=\"edi-" + rowData.id + "\" type=\"button\" class=\"fa fa-bar-chart btn btn-sm btn-default\" href=\"" + proyectosUrl + "?clienteId=" + rowData.id + "\" id=\"eli-" + rowData.id + "\"></a>";
            var editar = "<button name=\"edi-" + rowData.id + "\" id=\"edi-" + rowData.id + "\" type=\"button\" class=\"fa fa-pencil-square-o btn btn-sm btn-default\" data-bind=\"click: editar.bind($data, '" + rowData.id + "')\"></button>";
            var eliminar = "<button name=\"eli-" + rowData.id + "\" id=\"eli-" + rowData.id + "\" type=\"button\" class=\"fa fa-trash-o btn btn-sm btn-danger\"  data-bind=\"click: eliminar.bind($data, '" + rowData.id + "')\"></button>";
            customMarkup = "<div class=\"TAC\">" + proy + "&nbsp;" + editar + "&nbsp;" + eliminar + "</div>";
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
        success: function (result) {
            var data = {
                count: result.total,
                columns: [
                    {
                        label: "Código",
                        property: "codigo"
                    },
                    {
                        label: "NIT",
                        property: "nit"
                    },
                    {
                        label: "Cliente",
                        property: "nombre"
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

$("#clientes-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});