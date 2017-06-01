function UsuariosViewModel() {
    var self = this;

    self.cargos = ko.observableArray([]);
    self.id = ko.observable(undefined);
    self.cargoId = ko.observable(undefined);
    self.userName = ko.observable(undefined);
    self.cedula = ko.observable(undefined);
    self.clave = ko.observable(undefined);
    self.nombre = ko.observable(undefined);
    self.email = ko.observable(undefined);
    self.emailConfirmado = ko.observable(undefined);
    self.accesosFallidos = ko.observable(undefined);
    self.bloqueado = ko.observable(undefined);
    self.fechaBloqueo = ko.observable(undefined);
    self.reglaCargueLimiteDias = ko.observable(undefined);
    self.reglaCargueLimiteHoraFraccion = ko.observable(undefined);

    self.roles = ko.observableArray([]);
    self.rolesUsuario = ko.observableArray([]);
    self.rolUsuario = ko.observable(undefined);

    self.refrescar = function () {
        $("#usuarios-repeater").repeater("render");
    };

    self.mostrar = function () {
        $("#usuario-form").resetValidation();
        $("#usuario-modal").modal({ backdrop: "static" });
    };

    self.ocultar = function () {
        $("#usuario-modal").modal("hide");
        $("#usuario-form").resetValidation();
    };

    self.obtenerCargos = function () {
        return $.ajax({
            url: apiUrl + "/ObtenerCargos",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerRoles = function () {
        return $.ajax({
            url: apiUrl + "/ObtenerRoles",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerRolesUsuario = function (id) {
        return $.ajax({
            url: apiUrl + "/ObtenerRolesUsuario/{id}",
            tokens: { id: id },
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.mostrarRoles = function (id) {
        self.obtenerRolesUsuario(id).done(function (data) {
            self.id(id);
            self.rolesUsuario(data);
            $("#roles-usuario-modal").modal({ backdrop: "static" });
        }).fail(function (jqXHR) {
            bootbox.alert("Obtener los roles del usuario. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.agregarRolUsuario = function () {
        var existe = _.find(self.rolesUsuario(), function (s) {
            return s.id === self.rolUsuario().id;
        });

        if (existe === undefined) {
            self.rolesUsuario.push(self.rolUsuario());
        }
    };

    self.eliminarRolUsuario = function () {
        self.rolesUsuario.remove(this);
    };

    self.guardarRolesUsuario = function () {
        var roles = {
            usuarioId: self.id(),
            roles: _.map(self.rolesUsuario(), function (s) { return s.id; })
        };

        $.ajax({
            url: apiUrl + "/GuardarRolesUsuario",
            type: "POST",
            data: JSON.stringify(roles),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function () {
            $("#roles-usuario-modal").modal("hide");
            bootbox.alert("Los roles han sido guardadas correctamente.");
        }).fail(function (jqXHR) {
            bootbox.alert("Guardar los roles del usuario. " + jqXHR.responseJSON.exceptionMessage);
        });
    };

    self.reestablecer = function () {
        self.id(undefined);
        self.cargoId(undefined);
        self.userName(undefined);
        self.cedula(undefined);
        self.clave(undefined);
        self.nombre(undefined);
        self.email(undefined);
        self.emailConfirmado(undefined);
        self.accesosFallidos(undefined);
        self.bloqueado(undefined);
        self.fechaBloqueo(undefined);
        self.reglaCargueLimiteDias(undefined);
        self.reglaCargueLimiteHoraFraccion(undefined);
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
        if ($("#usuario-form").valid()) {

            var servicio = {
                id: self.id(),
                cargoId: self.cargoId(),
                userName: self.userName(),
                cedula: self.cedula(),
                clave: self.clave(),
                nombre: self.nombre(),
                email: self.email(),
                emailConfirmado: self.emailConfirmado(),
                accesosFallidos: self.accesosFallidos(),
                bloqueado: self.bloqueado(),
                fechaBloqueo: self.fechaBloqueo(),
                reglaCargueLimiteDias: self.reglaCargueLimiteDias(),
                reglaCargueLimiteHoraFraccion: self.reglaCargueLimiteHoraFraccion()
            };

            var opt = {
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                data: JSON.stringify(servicio)
            };

            if (servicio.id == undefined) {
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
            self.cargoId(data.cargoId);
            self.userName(data.userName);
            self.cedula(data.cedula);
            self.clave(data.clave);
            self.nombre(data.nombre);
            self.email(data.email);
            self.emailConfirmado(data.emailConfirmado);
            self.accesosFallidos(data.accesosFallidos);
            self.bloqueado(data.bloqueado);
            self.fechaBloqueo(data.fechaBloqueo);
            self.reglaCargueLimiteDias(data.reglaCargueLimiteDias);
            self.reglaCargueLimiteHoraFraccion(data.reglaCargueLimiteHoraFraccion);
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
            dataType: "json"
        })
            .done(function () {
                self.refrescar();
                self.ocultar();
                self.reestablecer();
            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
    };

    self.obtenerCargos().done(function (data) {
        self.cargos(data);
    }).fail(function (jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de cargos. " + jqXHR.responseJSON.exceptionMessage);
    });

    self.obtenerRoles().done(function (data) {
        self.roles(data);
    }).fail(function (jqXHR) {
        bootbox.alert("Error al tratar de obtener el listado de roles. " + jqXHR.responseJSON.exceptionMessage);
    });
}

var viewModel = new UsuariosViewModel();

ko.applyBindings(viewModel);

function generarColumnas(helpers, callback) {
    var column = helpers.columnAttr;
    var rowData = helpers.rowData;
    var customMarkup = "";

    switch (column) {
        case "id":
            var editar = "<button name=\"edi-" + rowData.id + "\" id=\"edi-" + rowData.id + "\" type=\"button\" class=\"fa fa-pencil-square-o btn btn-sm btn-default\" data-bind=\"click: editar.bind($data, '" + rowData.id + "')\"></button>";
            var eliminar = "<button name=\"eli-" + rowData.id + "\" id=\"eli-" + rowData.id + "\" type=\"button\" class=\"fa fa-trash-o btn btn-sm btn-danger\"  data-bind=\"click: eliminar.bind($data, '" + rowData.id + "')\"></button>";
            var roles = "<button title=\"Roles del Usuario\" name=\"roles-" + rowData.id + "\" id=\"roles-" + rowData.id + "\" type=\"button\" class=\"fa fa-gears btn btn-sm btn-default\"  data-bind=\"click: mostrarRoles.bind($data, '" + rowData.id + "')\"></button>";
            customMarkup = "<div class=\"TAC\">" + editar + "&nbsp;" + roles + "&nbsp;" + eliminar + "</div>";
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
                        label: "Nombre Usuario",
                        property: "userName"
                    },
                    {
                        label: "Cargo",
                        property: "cargoNombre"
                    },
                    {
                        label: "Nombre",
                        property: "nombre"
                    },
                    {
                        label: "Email",
                        property: "email"
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

$("#usuarios-repeater").repeater({
    dataSource: dataSource,
    list_columnRendered: generarColumnas
});