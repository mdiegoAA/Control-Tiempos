
var idcliente;
var proyectoIdFijo = "";
var servicioIdFijo = "";
var GnombreProyecto = "";
var divAntiguo;
var divAntiguoUsu;

$(document).keyup(function (event) {
    if (event.which == 27) {

        var tableRemove = $("#tables_" + divAntiguo + "");
        tableRemove.remove();
        var tabless = $("#table_" + divAntiguo + "");
        tabless.css('color', 'black');
        tabless.css('background-color', '#f9f9f9');
    }
});

$(document).ready(function () {

  
 

    var prueba = $("#salir");
    prueba.click(function () {

        window.location.href = '../Inicio/Index'

    });


    var generarReporte = $("#generarReporte");
    generarReporte.click(function () {


        DescargarDocumento();

    });




    var generarReporte = $("#generarReporte");
    generarReporte.click(function () {
        DescargarDocumento();

    });

    var Reporte = $("#Reporte");
    Reporte.click(function () {
        ReporteMeseTiempo();


    });
    var Tree = $("#Tree");
    Tree.click(function () {

        var Modaltree = $("#Modaltree");
        Modaltree.modal("show");

    });

    var Consultar = $("#Consultar");

    Consultar.click(function () {

        ObtenerUsuariosxDirector();
        
    });




    var ListadoClientes = $("#ListadoClientes");
    ListadoClientes.click(function () {

        var rSultIdArchivo = $("#ListadoClientes option:selected").val();
        var data =
        {
            idArchivo: rSultIdArchivo

        }

        $.ajax({

            url: "Index.aspx/ObtenerListadoServicioXcliente",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            dataType: "json",

            success: function (result) {


                var data = JSON.parse(result.d);
                var temp = "";
                for (var i = 0; i < data.length; i++) {

                    temp += "<option value=" + data[i].ServicioId + ">" + LimpiarNombreCliente(data[i].NombreServicio) + "</option>";

                }

                var serviciosCliente = $("#serviciosCliente");
                serviciosCliente.empty();
                serviciosCliente.append(temp);

            }
        });





    });



});



function ObtenerUsuariosxDirector() {
  
    var fechaInicial = $("#fechass").val();
  
  
    var data =
          {
              ano: fechaInicial

          }
    $.ajax({

        url: "ReporteUsuariosDirector.aspx/ObtenerListadoUsuariosXgruposTrabajo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {



            var tablesRsult = $("#tablesRsult");
            tablesRsult.empty();
            var data = JSON.parse(result.d);
            var temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th colspna='2'>Nombre </th>";
            temp += "<th colspna='2'>Cargo</th>";          
            temp += "<th colspna='2'>Seleccionar</th>";         
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";
            var tiempoTotal = 0;
            for (var i = 0; i < data.length; i++) {

                //     var data = data[i].ProyectoId + "%" + data[i].ServicioId;
                var nombre = data[i].nombreUsuario.replace(/ /g, "_");
                console.log(nombre);
                temp += "<tr id='table_" + i + "'>";
                temp += "<td colspna='2'>" + data[i].nombreUsuario + "</td>";
                temp += "<td colspna='2'>" + data[i].NombreCargo + "</td>";
              

                //   temp += "<td><button class='btn btn-primary btn-xs'  value="+i+" onClick='prueba(this.value)' type='submit'>Seleccionar</button></td>";
                temp += "<td colspna='2'><button class='btn btn-primary btn-xs'  value=" + data[i].UsuarioId + "%"+nombre + " onClick='calendario(this.value)' type='submit'>Seleccionar</button></td>";
              


                temp += "</tr>";

            }
            temp += "</tbody>";
            temp += "</table>";
            tablesRsult.append(temp);
            $('#dataTables-example').DataTable({
                responsive: true,
                "order": [[0, "asc"]]
            });



        }
    });

   
    

}

function proyectosXDirector() {
    // $("#element").introLoader();        

    var fechaInicial = $("#datepicker").val();
    var fechaFinal = $("#datepicker2").val();

    var data =
        {

            fechaInicial: fechaInicial,
            fechaFinal: fechaFinal
        }
    $.ajax({

        url: "ReportesAmezquita.aspx/obtenerListadoClientes",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {



            var tablesRsult = $("#tablesRsult");
            tablesRsult.empty();
            var data = JSON.parse(result.d);
            var temp = "";
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th>Proyectos</th>";
            temp += "<th>Servicio</th>";
            temp += "<th>Ejecutadas</th>";
            temp += "<th>Total Horas</th>";
            temp += "<th>Restante Horas</th>";
            temp += "<th>Seleccionar</th>";
            temp += "<th>Generar Reporte</th>";
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";
            var tiempoTotal = 0;
            for (var i = 0; i < data.length; i++) {

                //     var data = data[i].ProyectoId + "%" + data[i].ServicioId;
                var nombrePro = data[i].NombreProyecto;
                var AucnombrePro = nombrePro.replace(/ /g, "&");
                var servicioss = data[i].NombreServicio.replace(/ /g, "&");
                temp += "<tr id='table_" + i + "'>";
                temp += "<td>" + nombrePro + "</td>";
                temp += "<td>" + data[i].NombreServicio + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Tiempo) + "</td>";
                temp += "<td>" + data[i].BolsaHoras + "</td>";
                temp += "<td>" + reescribirFecha((parseFloat(data[i].BolsaHoras) * 60) - (parseFloat(data[i].Tiempo))) + "</td>";

                //   temp += "<td><button class='btn btn-primary btn-xs'  value="+i+" onClick='prueba(this.value)' type='submit'>Seleccionar</button></td>";
                temp += "<td><button class='btn btn-primary btn-xs'  value=" + data[i].ProyectoId + '%' + AucnombrePro + '!' + i + " onClick='calendario(this.value)' type='submit'>Seleccionar</button></td>";
                temp += "<td><button type='button' value=" + data[i].ProyectoId + '#' + AucnombrePro + '%' + servicioss + " onclick='reporteTiempoMensual(this.value)' class='btn btn-default btn-xs'><span class='fa fa-file-excel-o' aria-hidden='true'></span> Generar Excel</button></td>";


                temp += "</tr>";

            }
            temp += "</tbody>";
            temp += "</table>";
            tablesRsult.append(temp);
            $('#dataTables-example').DataTable({
                responsive: true,
                "order": [[0, "desc"]]
            });



        }
    });



}

function reescribirFecha(data) {

    var fecha = parseInt(data);
    var hora = parseInt(fecha / 60);
    var minutos = (fecha % 60);
    var result = hora + ":" + minutos;

    return result;

}

function calendario(id) {

   

    var index = id.indexOf("%");
    var idReal = id.substring(0, index);
    var nombreUsuario = id.substring(index + 1, id.length);
    nombreUsuario = nombreUsuario.replace(/_/g, " ");;

    var fechaInicial = $("#fechass").val();
   
    var año = fechaInicial;
    var data =
            {
                idUsuarios: idReal,
                año:año
            }
    $.ajax({

        url: "ReporteUsuariosDirector.aspx/ObtenerTiempoUsuarioDirector",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {
            var data = JSON.parse(result.d);
            var temp = "";
           
            temp += "<table class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            temp += "<thead>";
            temp += "<tr>";
            temp += "<th>Nombre Proyecto </th>";
            temp += "<th>Enero</th>";
            temp += "<th>febrero</th>";
            temp += "<th>Marzo</th>";
            temp += "<th>Abril</th>";
            temp += "<th>Mayo</th>";
            temp += "<th>Junio</th>";
            temp += "<th>Julio</th>";
            temp += "<th>Agosto</th>";
            temp += "<th>Septiembre</th>";
            temp += "<th>Octubre</th>";
            temp += "<th>Noviembre</th>";
            temp += "<th>Diciembre</th>";          
            temp += "</tr>";
            temp += "</thead>";
            temp += "<tbody>";
            for (var i = 0; i < data.length; i++) {

                //     var data = data[i].ProyectoId + "%" + data[i].ServicioId;

                temp += "<tr id='table_" + i + "'>";
                temp += "<td >" + data[i].nombreProyecto + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes1) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes2) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes3) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes4) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes5) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes6) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes7) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes8) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes9) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes10) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes11) + "</td>";
                temp += "<td>" + reescribirFecha(data[i].Mes12) + "</td>";



                temp += "</tr>";

            }
            temp += "</tbody>";
            temp += "</table>";
          
            var reporteUsuarios = $("#reporteUsuarios");
            reporteUsuarios.empty();
            reporteUsuarios.append(temp);

        }
    });

   
    var ModalNombreUsuario = $("#ModalNombreUsuario");
    ModalNombreUsuario.empty();
    ModalNombreUsuario.text(nombreUsuario);
    var ModalReportePersona = $("#ModalReportePersona");
    ModalReportePersona.modal("show");

   
}


function reporteTiempoMensual(idArchivo) {

    var NProyecto = idArchivo.indexOf("#");
    var proyectoN = idArchivo.indexOf("%");

    var proyecto = idArchivo.substring(0, NProyecto);
    var rSult = idArchivo.substring(NProyecto + 1, proyectoN);
    var servicio = idArchivo.substring(proyectoN + 1, idArchivo.length);
    var AucnombrePro = rSult.replace(/&/g, " ");
    var servicioFinal = servicio.replace(/&/g, " ");
    var data = {

        idArchivo: proyecto

    }

    $.ajax({

        url: "ReportesAmezquita.aspx/reporteTiempoMensual",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

            var data = JSON.parse(result.d);
            var temp = "";

            temp += "<thead style='display:none'";
            temp += " <tr><td colspan ='4' rowspan='6' ><img  src='http://www.amezquita.com.co/wp-content/uploads/2015/03/Logo-en-Policrom--a-e1443198604156.png' width='100px' height='100px'/></td><td></td><th>Cliente</th><td colspan='4'>" + AucnombrePro + "</td></tr><td></td><th>Servicio</th><td colspan='4'>" + servicioFinal + "</td><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr>";



            temp += "</thead>";

            temp += "<tbody>";
            temp += "<tr>";
            temp += "<th>Usuario</th>";
            temp += "<th>Ene</th>";
            temp += "<th>Feb</th>";
            temp += "<th>Mar</th>";
            temp += "<th>Abr</th>";
            temp += "<th>May</th>";
            temp += "<th>Jun</th>";
            temp += "<th>Jul</th>";
            temp += "<th>Ago</th>";
            temp += "<th>Sept</th>";
            temp += "<th>Oct</th>";
            temp += "<th>Nov</th>";
            temp += "<th>Dic</th>";

            temp += "</tr>";


            for (var i = 0; i < data.length; i++) {

                temp += "<tr>";
                temp += "<td>" + data[i].Usuario + "</td>";

                if (reescribirFecha(data[i].enero) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].enero) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].enero) + "<span></td>";
                }

                if (reescribirFecha(data[i].febrero) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].febrero) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].febrero) + "<span></td>";
                }

                if (reescribirFecha(data[i].marzo) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].marzo) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].marzo) + "<span></td>";
                }
                if (reescribirFecha(data[i].abril) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].abril) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].abril) + "<span></td>";
                }
                if (reescribirFecha(data[i].mayo) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].mayo) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].mayo) + "<span></td>";
                }
                if (reescribirFecha(data[i].junio) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].junio) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].junio) + "<span></td>";
                }
                if (reescribirFecha(data[i].julio) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].julio) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].julio) + "<span></td>";
                }
                if (reescribirFecha(data[i].agosto) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].agosto) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].agosto) + "<span></td>";
                }
                if (reescribirFecha(data[i].septiembre) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].septiembre) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].septiembre) + "<span></td>";
                }
                if (reescribirFecha(data[i].Octubre) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].Octubre) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].Octubre) + "<span></td>";
                }
                if (reescribirFecha(data[i].noviembre) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].noviembre) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].noviembre) + "<span></td>";
                }
                if (reescribirFecha(data[i].Diciembre) == "0:0") {

                    temp += "<td>" + reescribirFecha(data[i].Diciembre) + "</td>";

                } else {

                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].Diciembre) + "<span></td>";
                }


            }

            temp += "</tbody>";
            //    temp += "</table>";
            var reporteTiempo = $("#ReporteMensual");
            reporteTiempo.empty();
            reporteTiempo.append(temp);

            var modalRsult = $("#modalRsult");
            modalRsult.modal("show");
        }
    });

    var NombreCliente = $("#NombreCliente");
    NombreCliente.text(AucnombrePro);

}

function usuarios(id) {
    var num = id.indexOf("%");
    var nums = id.indexOf("?");

    var proyecto = id.substring(0, num);
    var servicio = id.substring(num + 1, nums);
    var ids = id.substring(nums + 1, id.length);
    var data =
     {
         idProyecto: proyecto,
         idServicio: servicio

     }
    $.ajax({

        url: "ReportesAmezquita.aspx/UsuariosXServicioXproyecto",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

            var data = JSON.parse(result.d);

            var temp = "";
            temp += "<td id='tableU_" + ids + "' colspan='10'>";
            temp += " <div class='panel panel-primary'>";
            temp += "<div class='panel-body'>";
            temp += "<table class='table table-striped table-bordered table-hover'>";
            var table = $("#tablesss_" + ids + "");
            for (var i = 0; i < data.length; i++) {

                temp += "<tr id='tablesss_" + i + "'>";
                temp += "<td colspan='3'><i class='fa fa-long-arrow-right' aria-hidden='true'></i>   " + data[i].Nombre + "</td>";
                temp += "<td colspan='2' class='text-right'>" + reescribirFecha(data[i].tiempo) + "</td>";
                //  temp += "<td colspan='1' class='text-right'><button type='button' value=" + data[i].ProyectoId + '%' + data[i].ServicioId + '?' + i + " onclick='usuarios(this.value)' class='btn btn-success btn-xs'>Seleccionar</button></td>";
                temp += "</tr>";

            }

            temp += "</table>";
            temp += "</div>";
            temp += "</td>";
            temp += "</div>";
            temp += "</div>";

            var tableRemove = $("#tableU_" + divAntiguoUsu + "");
            var modUsu = $("#tablesss_" + divAntiguoUsu + "");
            tableRemove.remove();

            modUsu.css('color', 'black');
            modUsu.css('background-color', '#f9f9f9');




            table.css('background-color', '#105795)');
            table.css('color', 'white');
            table.after(temp);
            divAntiguoUsu = ids;



        }
    });

}

function DescargarDocumento() {


    $("#ReporteMensual").table2excel({

        // exclude CSS class

        exclude: ".noExl",
        name: "Prueba.xls",
        filename: "Reporte"

    });



}

function validarFechas() {

    var fechaInicial = $("#datepicker").val();
    var fechaFinal = $("#datepicker2").val();

    if (fechaInicial == "" || fechaFinal == "") {

        if (fechaInicial == "") {
            var validacionTextInicial = $("#validacionTextInicial");
            validacionTextInicial.text("Escoja la fecha Inicial");
            var validacionFechaInicial = $("#validacionFechaInicial");
            validacionFechaInicial.css("display", "block");



        }

        if (fechaFinal == "") {
            var validacionFechaFinal = $("#validacionFechaFinal");
            validacionFechaFinal.text("Escoja la fecha Final");
            var validacionFechaFinal = $("#validacionFechaFinal");
            validacionFechaFinal.css("display", "block");

        }

        return false;

    } else {

        return true;

    }
}

function FormatoMes(mes) {


    switch (mes) {

        case 1:
            return "Enero";

        case 2:
            return "Febrero";

        case 3:
            return "Marzo";
        case 4:
            return "Abril";
        case 5:
            return "Mayo";
        case 6:
            return "Junio";
        case 7:
            return "Julio";
        case 8:
            return "Agosto";
        case 9:
            return "Noviembre";
        case 10:
            return "Octubre";
        case 11:
            return "Noviembre";
        case 12:
            return "Diciembre";
        default:
            return "";



    }


}