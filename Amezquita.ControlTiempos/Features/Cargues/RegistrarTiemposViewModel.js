function RegistrarTiemposViewModel() {
    var self = this;

    var dateTime = moment();

    self.proyectos = ko.observableArray([]);
    self.proyectoId = ko.observable(undefined);
    self.servicios = ko.observableArray([]);
    self.servicioId = ko.observable(undefined);
    self.actividades = ko.observableArray([]);
    self.actividadId = ko.observable(undefined);
    self.fecha = ko.observable(moment({ year: dateTime.year(), month: dateTime.month(), day: dateTime.date() }));
    self.horaInicio = ko.observable(dateTime);
    self.horaFin = ko.observable(dateTime);
    self.observaciones = ko.observable(undefined);
    
    self.obtenerProyectos = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerProyectos",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerServicios = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerServicios/{id}",
            type: "GET",
            tokens: { id: id },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerActividades = function(id) {
        return $.ajax({
            url: apiUrl + "/ObtenerActividades/{id}",
            type: "GET",
            tokens: { id: id },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerCronometro = function() {
        return $.ajax({
            url: apiUrl + "/ObtenerCronometro?proyectoId={proyectoId}&servicioId={servicioId}&actividadId={actividadId}",
            type: "GET",
            tokens: {
                proyectoId: self.proyectoId(),
                servicioId: self.servicioId(),
                actividadId: self.actividadId()
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    self.obtenerProyectos()
        .done(function(data) {
            self.proyectos(data);
        }).fail(function(jqXHR) {
            bootbox.alert(jqXHR.responseJSON.exceptionMessage);
        });

    self.proyectoSeleccionado = function(e) {
        self.obtenerServicios(self.proyectoId())
            .done(function(data) {
                self.servicios(data);
            }).fail(function(jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });

        self.obtenerActividades(self.proyectoId())
            .done(function(data) {
                self.actividades(data);
            }).fail(function(jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
    };

    self.cargarHoras = function() {
        if ($("#cargar-horas-form").valid()) {
            $("#buttonCargue").attr("disabled", "disabled");
            var cargue = {
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
                url: apiUrl + "/CargarHoras",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(cargue)
            }).done(function () {
                $("#buttonCargue").removeAttr("disabled");
                bootbox.alert("El cargue de horas se ha realizado correctamente.");
            }).fail(function (jqXHR) {
                $("#buttonCargue").removeAttr("disabled");
              
                bootbox.alert({
                    message:"<p style='color:red'>"+ jqXHR.responseJSON.exceptionMessage +"</p>"
                   
                })

               
            });
        }
    };

    self.iniciarCronometro = function() {
        if ($("#cargar-horas-form").valid()) {

            var cronometro = {
                proyectoId: self.proyectoId(),
                servicioId: self.servicioId(),
                actividadId: self.actividadId()
            };

            $.ajax({
                contentType: "application/json; chartset=utf-8",
                url: apiUrl + "/IniciarCronometro",
                type: "POST",
                dataType: "json",
                data: JSON.stringify(cronometro)
            }).done(function () {
                bootbox.alert("El cronómetro se ha iniciado correctamente.");
            }).fail(function(jqXHR) {
                bootbox.alert(jqXHR.responseJSON.exceptionMessage);
            });
        }
    };

    self.actividadSeleccionada = function (e) {
        self.obtenerCronometro()
            .done(function (data) {
                if (data) {
                    var fecha = moment(data.inicio);
                    self.fecha(fecha);
                    self.horaInicio(fecha);
                    self.horaFin(moment())
                }
            }).fail(function (jqXHR) {
                bootbox.alert("Ha ocurrido un error al tratar de obtener el cronómetro.");
            });
    };
}

var viewModel = new RegistrarTiemposViewModel();

ko.applyBindings(viewModel);