function RegistrarNovedadesViewModel() {
    var self = this;

    var dateTime = moment();

    self.usuarios = ko.observableArray([]);
    self.usuarioId = ko.observable(undefined);
    self.proyectos = ko.observableArray([]);
    self.proyectoId = ko.observable(undefined);
    self.servicios = ko.observableArray([]);
    self.servicioId = ko.observable(undefined);
    self.actividades = ko.observableArray([]);
    self.actividadId = ko.observable(undefined);
    self.fechaInicio = ko.observable(dateTime);
    self.fechaFin = ko.observable(dateTime);
    self.observaciones = ko.observable(undefined);

    self.obtenerUsuarios = function () {
        return $.ajax({
            url: apiUrl + "/ObtenerUsuarios",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerProyectos = function (id) {
        return $.ajax({
            url: apiUrl + "/ObtenerProyectos/{id}",
            type: "GET",
            tokens: { id: id },
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

    self.obtenerUsuarios()
        .done(function (data) {
            self.usuarios(data);
        }).fail(function (jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });

    self.usuarioSeleccionado = function (e) {
        self.obtenerProyectos(self.usuarioId())
            .done(function (data) {
                self.proyectos(data);
            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
    };

    self.proyectoSeleccionado = function (e) {
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
    };

    self.registrarNovedad = function () {
        if ($("#registrar-novedad-form").valid()) {

            var novedad = {
                usuarioId: self.usuarioId(),
                proyectoId: self.proyectoId(),
                servicioId: self.servicioId(),
                actividadId: self.actividadId(),
                fechaInicio: self.fechaInicio(),
                fechaFin: self.fechaFin(),
                observaciones: self.observaciones()
            };

            $.ajax({
                contentType: "application/json; chartset=utf-8",
                url: apiUrl + "/RegistrarNovedad",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(novedad)
            }).done(function () {
                bootbox.alert("El registro de novedades se ha realizado correctamente.");
            }).fail(function (jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
        }
    };
}

var viewModel = new RegistrarNovedadesViewModel();

ko.applyBindings(viewModel);