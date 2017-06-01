
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


    $('#dataTables-example').DataTable({
        responsive: true,
        "order": [[0, "desc"]]
    });



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

        var validacionFechaInicial = $("#validacionFechaInicial");
        validacionFechaInicial.css("display", "none");

        var validacionFechaFinal = $("#validacionFechaFinal");
        validacionFechaFinal.css("display", "none");

        var rSult = validarFechas();

        if (rSult == false) {

        } else {

            proyectosXDirector();
        }
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
            temp += "<th colspna='2'>Proyectos</th>";
            temp += "<th colspna='2'>Servicio</th>";
            temp += "<th colspna='2'>Ejecutadas</th>";
            temp += "<th colspna='2'>Total Horas</th>";
            temp += "<th colspna='2'>Restante Horas</th>";
            temp += "<th colspna='2'>Seleccionar</th>";
            temp += "<th colspna='2'>Generar Reporte</th>";
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
                temp += "<td colspna='2'>" + nombrePro + "</td>";
                temp += "<td colspna='2'>" + data[i].NombreServicio + "</td>";
                temp += "<td colspna='2'>" + reescribirFecha(data[i].Tiempo) + "</td>";
                temp += "<td colspna='2'>" + data[i].BolsaHoras + "</td>";
                temp += "<td colspna='2'>" + reescribirFecha((parseFloat(data[i].BolsaHoras) * 60) - (parseFloat(data[i].Tiempo))) + "</td>";

                //   temp += "<td><button class='btn btn-primary btn-xs'  value="+i+" onClick='prueba(this.value)' type='submit'>Seleccionar</button></td>";
                temp += "<td colspna='2'><button class='btn btn-primary btn-xs'  value=" + data[i].ProyectoId + '%' + AucnombrePro + '!' + i + " onClick='calendario(this.value)' type='submit'>Seleccionar</button></td>";
                temp += "<td colspna='2'><button type='button' value=" + data[i].ProyectoId + '#' + AucnombrePro + '%' + servicioss + '¿' + i + " onclick='reporteTiempoMensual(this.value)' class='btn btn-default btn-xs'><span class='fa fa-file-excel-o' aria-hidden='true'></span> Generar Excel</button></td>";


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

 

    
    var table2 = $("#table2");


    var NProyecto = id.indexOf("%");
 
    var proyecto = id.substring(0, NProyecto);
 
    var ids = id.substring(NProyecto + 1, id.length);
 
    
    var data =
          {
              idArchivo: proyecto

          }
    $.ajax({

        url: "ClientesServicios.aspx/ObtenerRegistroXMes_Tiempo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {          


            var data = JSON.parse(result.d);

            var temp = "";
            var tiempoTotal = 0;



            temp += "<td id='tables_" + ids + "' colspan='7'>";
            temp += " <div class='panel panel-primary'>";
            temp += "<div class='panel-body'>";
            temp += "<table class='table table-striped table-bordered table-hover'>";


            temp += "<thead>";
            temp += "<tr>";
            for (var i = 0; i <= data.length -1 ; i++) {

                temp += "<td>" + FormatoMes(data[i].mes) + "-" + data[i].año + "</td>";

            }



            temp += "</tr>";
            temp += "</thead>";

            temp += "<tbody>";
                
            var table = $("#table_" + ids + "");
           

            temp += "<tr>";

            for (var i = 0; i <= data.length - 1 ; i++) {

                temp += "<td>" + data[i].HorasEjecutadas + "</td>";

            }

            temp += "</tr>";


          

            temp += "</tbody>";
            temp += "</table>";
            temp += "</div>";
            temp += "</td>";
            temp += "</div>";
            temp += "</div>";






            var tableRemove = $("#tables_" + divAntiguo + "");
            tableRemove.remove();
            var tabless = $("#table_" + divAntiguo + "");
            tabless.css('color', 'black');
            tabless.css('background-color', '#f9f9f9');
            table.css('background-color', '#105795)');
            table.css('color', 'white');
            table.after(temp);
            divAntiguo = ids;
        }






    });

}

function reporteTiempoMensual(idArchivo) {

    var NProyecto = idArchivo.indexOf("#");
    var proyectoN = idArchivo.indexOf("%");
    var idTable = idArchivo.indexOf("¿");

    var proyecto = idArchivo.substring(0, NProyecto);
    var rSult = idArchivo.substring(NProyecto + 1, proyectoN);
    var servicio = idArchivo.substring(proyectoN + 1, idTable);
    var idTableF = idArchivo.substring(idTable + 1, idArchivo.length);
    var AucnombrePro = rSult.replace(/&/g, " ");
    var servicioFinal = servicio.replace(/&/g, " ");

    alert(idTableF);
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
            temp += " <div class='table-responsive'>";
            temp += "<td id='tables_" + idTableF + "' colspan='15'>";
            temp += " <div class='panel panel-primary'>";
            temp += "<div class='panel-body'>";
            temp += "<table class='table table-striped table-bordered table-hover'>";
            var table = $("#table_" + idTableF + "");
            for (var i = 0; i < data.length; i++) {

                temp += "<tr id='tablesss_" + i + "'>";
                temp += "<td><i class='fa fa-long-arrow-right' aria-hidden='true'></i>lñasldñ</td>";
                temp += "<td>class='text-right'>ksakdlkdlsa</td>";
                temp += "<td><i class='fa fa-long-arrow-right' aria-hidden='true'></i>lñasldñ</td>";
                temp += "<td class='text-right'>ksakdlkdlsa</td>";
                temp += "<td><i class='fa fa-long-arrow-right' aria-hidden='true'></i>lñasldñ</td>";

                temp += "<td><i class='fa fa-long-arrow-right' aria-hidden='true'></i>lñasldñ</td>";
                temp += "<td>class='text-right'>ksakdlkdlsa</td>";
                temp += "<td><i class='fa fa-long-arrow-right' aria-hidden='true'></i>lñasldñ</td>";
                temp += "<td class='text-right'>ksakdlkdlsa</td>";
                temp += "<td><i class='fa fa-long-arrow-right' aria-hidden='true'></i>lñasldñ</td>";

            
               
                //    temp += "<td>Generar Reporte</td>";
                temp += "</tr>";


            }

            temp += "</table>";
            temp += "</div>";
            temp += "</td>";
            temp += "</div>";
            temp += "</div>";
            temp += "</div>";






            var tableRemove = $("#tables_" + divAntiguo + "");
            tableRemove.remove();
            var tabless = $("#table_" + divAntiguo + "");
            tabless.css('color', 'black');
            tabless.css('background-color', '#f9f9f9');
            table.css('background-color', '#105795)');
            table.css('color', 'white');
            table.after(temp);
            divAntiguo = ids;


          

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