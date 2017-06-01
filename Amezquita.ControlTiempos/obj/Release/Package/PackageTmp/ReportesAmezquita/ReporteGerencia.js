
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

    var Exportar = $("#Exportar");
    Exportar.click(function () {
        DescargarDocumento();

    });

    var Consultar = $("#Consultar");
    Consultar.click(function () {

        obtenerReporte();

    });
    var generarExcel = $("#generarExcel");
    generarExcel.click(function () {

        DescargarDocumento();

    });
   

    var prueba = $("#salir");
    prueba.click(function () {

        window.location.href = '../Inicio/Index'

    });



    var generarReporte = $("#generarReportes");
    generarReporte.click(function () {
        obtenerReporte();

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


function obtenerReporte() {

    var fechas = $("#fechass").val();
    var data =
   {
       fecha: fechas
   }
    $.ajax({

        url: "ReporteGerencia.aspx/traerReporte",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {
            var data = JSON.parse(result.d);
            var temp;
            var directorF = "";

        

          
            for (var i = 0; i < data.length; i++) {
                if (data[i].NombreCliente == "AMEZQUITA") {

                    directorF = data[i].gerenteGrupo;

                } else {

                    directorF = data[i].Gerente;

                }

                temp += "<tr>";
                temp += "<td>" + (i + 1) + "</td>";
                temp += "<td>" + data[i].NombreCliente + "</td>";
                temp += "<td>" +directorF + "</td>";              
                temp += "<td>" + data[i].Director + "</td>";
                temp += "<td>" + data[i].NombreActividad + "</td>";
                temp += "<td>" + data[i].Nombre + "</td>";
                temp += "<td>" + data[i].Cedula + "</td>";
                temp += "<td>" + data[i].NombreCargo + "</td>";
                temp += "<td>" + data[i].Mes + "</td>";
                temp += "<td>" + data[i].Año + "</td>";
                temp += "<td>" + data[i].HorasEjecutadas + "</td>";
                temp += "<tr>";
            }
            temp += "</tbody>";
            temp += "</table>";

            var tableReport = $("#tablesRsult");
            tableReport.empty();
            tableReport.append(temp);


            

        }
    });


}



function reescribirFecha(data) {

    var fecha = parseInt(data);
    var hora = parseInt(fecha / 60);
    var minutos = (fecha % 60);
    if (minutos == "0" && hora == "0") {

        var result = "0.0"

    } else {

        var result = hora + "." + minutos;

    }



    return result;

}
function calendario(id) {




    var table2 = $("#table2");


    var NProyecto = id.indexOf("%");
    var IProyecto = id.indexOf("!");
    var proyecto = id.substring(0, NProyecto);
    var nombreProyecto = id.substring(NProyecto + 1, IProyecto);
    var ids = id.substring(IProyecto + 1, id.length);
    nombreProyecto = nombreProyecto.replace(/&/g, " ");
    var textProyectoServicio = $("#textProyectoServicio");
    GnombreProyecto = nombreProyecto;

    textProyectoServicio.text(nombreProyecto);
    var data =
          {
              idArchivo: proyecto

          }
    $.ajax({

        url: "ReportesAmezquita.aspx/ServicioxActividadesxTiempo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

            //     alert(result.d);



            var data = JSON.parse(result.d);

            var temp = "";
            var tiempoTotal = 0;



            temp += "<td id='tables_" + ids + "' colspan='7'>";
            temp += " <div class='panel panel-primary'>";
            temp += "<div class='panel-body'>";
            temp += "<table class='table table-striped table-bordered table-hover'>";
            var table = $("#table_" + ids + "");
            for (var i = 0; i < data.length; i++) {

                temp += "<tr id='tablesss_" + i + "'>";
                temp += "<td colspan='3'><i class='fa fa-long-arrow-right' aria-hidden='true'></i>   " + data[i].NombreServicio + "</td>";
                temp += "<td colspan='2' class='text-right'>" + reescribirFecha(data[i].tiempo) + "</td>";
                temp += "<td colspan='1' class='text-right'><button type='button' value=" + data[i].ProyectoId + '%' + data[i].ServicioId + '?' + i + " onclick='usuarios(this.value)' class='btn btn-success btn-xs'>Seleccionar</button></td>";
                //    temp += "<td>Generar Reporte</td>";
                temp += "</tr>";


            }

            temp += "<tr>";
            temp += "<td colspan='3'></td>";
            temp += "<td colspan='2'></td>";
            temp += "<td colspan='1'>";
            temp += "<button type='submit' class='btn btn-default'>";
            temp += "<span class='glyphicon glyphicon-search'></span> Buscar</button></td>";

            temp += "</tr>";

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
            var sumTiempo = "";
            var sumaEnero = 0;
            var sumaFebrero = 0;
            var sumaMarzo = 0;
            var sumaAbril = 0;
            var sumaMarzo = 0;
            var sumaMayo = 0;
            var sumaJunio = 0;
            var sumaJulio = 0;
            var sumaAgosto = 0;
            var sumaSeptiembre = 0;
            var sumaOctubre = 0;
            var SumaNoviembre = 0;
            var sumaDiciembre = 0;

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
            temp += "<th>Total</th>";

            temp += "</tr>";


            for (var i = 0; i < data.length; i++) {


                sumTiempo = 0;

                temp += "<tr>";
                temp += "<td>" + data[i].Usuario + "</td>";

                if (reescribirFecha(data[i].enero) == "0:0") {

                    sumTiempo += parseFloat(data[i].enero);
                    sumaEnero += parseFloat(data[i].enero);
                    temp += "<td>" + reescribirFecha(data[i].enero) + "</td>";

                } else {
                    sumTiempo += parseFloat(data[i].enero);
                    sumaFebrero += parseFloat(data[i].enero);
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].enero) + "<span></td>";
                }

                if (reescribirFecha(data[i].febrero) == "0:0") {
                    sumTiempo += parseFloat((data[i].febrero));
                    sumaFebrero += parseFloat(data[i].febrero);
                    temp += "<td>" + reescribirFecha(data[i].febrero) + "</td>";

                } else {

                    sumTiempo += parseFloat((data[i].febrero));
                    sumaFebrero += parseFloat(data[i].febrero);
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].febrero) + "<span></td>";
                }

                if (reescribirFecha(data[i].marzo) == "0:0") {

                    sumTiempo += parseFloat((data[i].marzo));
                    sumaMarzo += parseFloat(data[i].marzo);

                    temp += "<td>" + reescribirFecha(data[i].marzo) + "</td>";


                } else {
                    sumTiempo += parseFloat((data[i].marzo));
                    sumaMarzo += parseFloat(data[i].marzo);
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].marzo) + "<span></td>";
                }
                if (reescribirFecha(data[i].abril) == "0:0") {
                    sumTiempo += parseFloat((data[i].abril));
                    sumaAbril += parseFloat(data[i].abril);
                    temp += "<td>" + reescribirFecha(data[i].abril) + "</td>";

                } else {
                    sumTiempo += parseFloat((data[i].abril));
                    sumaAbril += parseFloat(data[i].abril);
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].abril) + "<span></td>";
                }
                if (reescribirFecha(data[i].mayo) == "0:0") {

                    sumTiempo += parseFloat((data[i].mayo));
                    sumaMayo += parseFloat(data[i].mayo);
                    temp += "<td>" + reescribirFecha(data[i].mayo) + "</td>";

                } else {
                    sumTiempo += parseFloat((data[i].mayo));
                    sumaMayo += parseFloat(data[i].mayo);
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].mayo) + "<span></td>";
                }
                if (reescribirFecha(data[i].junio) == "0:0") {
                    sumTiempo += parseFloat((data[i].junio));
                    sumaJunio += parseFloat(data[i].junio);
                    temp += "<td>" + reescribirFecha(data[i].junio) + "</td>";

                } else {
                    sumTiempo += parseFloat((data[i].junio));
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].junio) + "<span></td>";
                    sumaJunio += parseFloat(data[i].junio);
                }
                if (reescribirFecha(data[i].julio) == "0:0") {
                    sumTiempo += parseFloat((data[i].julio));
                    temp += "<td>" + reescribirFecha(data[i].julio) + "</td>";
                    sumaJulio += parseFloat(data[i].julio);

                } else {
                    sumTiempo += parseFloat((data[i].julio));
                    sumaJulio += parseFloat(data[i].julio);
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].julio) + "<span></td>";
                }
                if (reescribirFecha(data[i].agosto) == "0:0") {
                    sumTiempo += parseFloat((data[i].agosto));
                    sumaAgosto += parseFloat(data[i].agosto);
                    temp += "<td>" + reescribirFecha(data[i].agosto) + "</td>";

                } else {
                    sumaAgosto += parseFloat(data[i].agosto);
                    sumTiempo += parseFloat((data[i].agosto));
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].agosto) + "<span></td>";
                }
                if (reescribirFecha(data[i].septiembre) == "0:0") {
                    sumaSeptiembre += parseFloat(data[i].septiembre);
                    sumTiempo += parseFloat((data[i].septiembre));
                    temp += "<td>" + reescribirFecha(data[i].septiembre) + "</td>";

                } else {
                    sumaSeptiembre += parseFloat(data[i].septiembre);
                    sumTiempo += parseFloat((data[i].septiembre));
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].septiembre) + "<span></td>";
                }
                if (reescribirFecha(data[i].Octubre) == "0:0") {

                    sumaOctubre += parseFloat(data[i].Octubre);
                    sumTiempo += parseFloat((data[i].Octubre));
                    temp += "<td>" + reescribirFecha(data[i].Octubre) + "</td>";

                } else {
                    sumaOctubre += parseFloat(data[i].Octubre);
                    sumTiempo += parseFloat((data[i].Octubre));
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].Octubre) + "<span></td>";
                }
                if (reescribirFecha(data[i].noviembre) == "0:0") {
                    SumaNoviembre += parseFloat(data[i].noviembre);
                    sumTiempo += parseFloat((data[i].noviembre));
                    temp += "<td>" + reescribirFecha(data[i].noviembre) + "</td>";

                } else {
                    SumaNoviembre += parseFloat(data[i].noviembre);
                    sumTiempo += parseFloat((data[i].noviembre));
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].noviembre) + "<span></td>";
                }
                if (reescribirFecha(data[i].Diciembre) == "0:0") {
                    sumaDiciembre += parseFloat(data[i].Diciembre);
                    sumTiempo += parseFloat((data[i].Diciembre));
                    temp += "<td>" + reescribirFecha(data[i].Diciembre) + "</td>";

                } else {
                    sumaDiciembre += parseFloat(data[i].Diciembre);
                    sumTiempo += parseFloat((data[i].Diciembre));
                    temp += "<td><span class='label label-primary'>" + reescribirFecha(data[i].Diciembre) + "<span></td>";
                }

                temp += "<td><span class='label label-primary'>" + reescribirFecha(sumTiempo) + "<span></td>";
                temp += "</tr>";
            }

            var tiempoTotal = sumaEnero + sumaFebrero + sumaMarzo + sumaAbril + sumaMayo + sumaJunio + sumaJulio + sumaAgosto + sumaSeptiembre + sumaOctubre + SumaNoviembre + sumaDiciembre;

            temp += "<tr>";
            temp += "<td> Tiempo Total</td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaEnero) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaFebrero) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaMarzo) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaAbril) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaMayo) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaJunio) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaJulio) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaAgosto) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaSeptiembre) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaOctubre) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(SumaNoviembre) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(sumaDiciembre) + "<span></td>";
            temp += "<td><span class='label label-primary'>" + reescribirFecha(tiempoTotal) + "<span></td>";
            temp += "</tr>";
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


    $("#dataTables-example").table2excel({

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