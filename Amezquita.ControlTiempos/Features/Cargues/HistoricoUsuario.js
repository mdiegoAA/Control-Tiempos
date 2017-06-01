
var idcliente;
var proyectoIdFijo = "";
var servicioIdFijo = "";
var GnombreProyecto = "";
var divAntiguo;
var divAntiguoUsu;

$(document).ready(function () {


    var Exportar = $("#Exportar");
    Exportar.click(function () {

        DescargarDocumento();

    });


    var prueba = $("#Prueba");
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

        var rSult = validarFechas();
        if (rSult == false) {

           
        }
        else{
            consultarF();  
            
        }      

    });

});
  
function DescargarDocumento() {
    
    $("#dataTables").table2excel({

        // exclude CSS class

        exclude: ".noExl",
        name: "Prueba.xls",
        filename: "Archivos"

    });


}
        
function consultarF () {


var validacion = validarFechas();
           

var rSult = $("#datepicker").val();

var data =
{
    Fecha: rSult

}

$.ajax({

    url: "HistoricoUsuario.aspx/ObtenerReporteXcliente",
    type: "POST",
    contentType: "application/json; charset=utf-8",
    data: JSON.stringify(data),
    dataType: "json",

    success: function (result) {

        var data = JSON.parse(result.d);
        var temp = "";
            
        var total = 0;
        var variableTotal1 = 0;
        var variableTotal2 = 0;
        var variableTotal3 = 0;
        var variableTotal4 = 0;
        var variableTotal5 = 0;
        var variableTotal6 = 0;
        var variableTotal7 = 0;
        var variableTotal8 = 0;
        var variableTotal9 = 0;
        var variableTotal10 = 0;
        var variableTotal11 = 0;
        var variableTotal12 = 0;
        var variableTotal13 = 0;
        var variableTotal14 = 0;
        var variableTotal15 = 0;
        var variableTotal16 = 0;
        var variableTotal17 = 0;
        var variableTotal18 = 0;
        var variableTotal19 = 0;
        var variableTotal20 = 0;
        var variableTotal21 = 0;
        var variableTotal22 = 0;
        var variableTotal23 = 0;
        var variableTotal24 = 0;
        var variableTotal25 = 0;
        var variableTotal26 = 0;
        var variableTotal27 = 0;
        var variableTotal28 = 0;
        var variableTotal29 = 0;
        var variableTotal30 = 0;
        var variableTotal31 = 0;
        var totalFinal = 0;

        for (var i = 0; i < data.length; i++) {
                   



            temp += "<tr>";
            temp += "<td>" + data[i].nombreProyecto + "</td>";
            if (reescribirFecha(data[i].primero) == "0") {
                variableTotal1 = variableTotal1 + parseFloat(data[i].primero);
                total = total + parseFloat(data[i].primero);
                temp += "<td>" + reescribirFecha(data[i].primero) + "</td>";

            } else {
                variableTotal1 = variableTotal1 + parseFloat(data[i].primero);
                total = total + parseFloat(data[i].primero);
                temp += "<td class='info'>" + reescribirFecha(data[i].primero) + "</span></td>";
            }
            if (reescribirFecha(data[i].segundo) == "0") {
                variableTotal2 = variableTotal2 + parseFloat(data[i].segundo);
                total = total + parseFloat(data[i].segundo);
                temp += "<td>" + reescribirFecha(data[i].segundo) + "</td>";

            } else {
                variableTotal2 = variableTotal2 + parseFloat(data[i].segundo);
                total = total + parseFloat(data[i].segundo);
                temp += "<td class='info'>" + reescribirFecha(data[i].segundo) + "</span></td>";
            }
            if (reescribirFecha(data[i].tercero) == "0") {
                variableTotal3 = variableTotal3 + parseFloat(data[i].tercero);
                total = total + parseFloat(data[i].tercero);
                temp += "<td>" + reescribirFecha(data[i].tercero) + "</td>";

            } else {
                variableTotal3 = variableTotal3 + parseFloat(data[i].tercero);
                total = total + parseFloat(data[i].tercero);
                temp += "<td class='info'>" + reescribirFecha(data[i].tercero) + "</span></td>";
            }
            if (reescribirFecha(data[i].cuarto) == "0") {
                variableTotal4 = variableTotal4 + parseFloat(data[i].cuarto);
                total = total + parseFloat(data[i].cuarto);
                temp += "<td>" + reescribirFecha(data[i].cuarto) + "</td>";

            } else {
                variableTotal4 = variableTotal4 + parseFloat(data[i].cuarto);
                total = total + parseFloat(data[i].cuarto);
                temp += "<td class='info'>" + reescribirFecha(data[i].cuarto) + "</span></td>";
            }
            if (reescribirFecha(data[i].quinto) == "0") {
                variableTotal5 = variableTotal5 + parseFloat(data[i].quinto);
                total = total + parseFloat(data[i].quinto);
                temp += "<td>" + reescribirFecha(data[i].quinto) + "</td>";

            } else {
                variableTotal5 = variableTotal5 + parseFloat(data[i].quinto);
                total = total + parseFloat(data[i].quinto);
                temp += "<td class='info'>" + reescribirFecha(data[i].quinto) + "</span></td>";
            }
            if (reescribirFecha(data[i].sexto) == "0") {
                variableTotal6 = variableTotal6 + parseFloat(data[i].sexto);
                total = total + parseFloat(data[i].sexto);
                temp += "<td>" + reescribirFecha(data[i].sexto) + "</td>";

            } else {
                variableTotal6 = variableTotal6 + parseFloat(data[i].sexto);
                total = total + parseFloat(data[i].sexto);
                temp += "<td class='info'>" + reescribirFecha(data[i].sexto) + "</span></td>";
            }
            if (reescribirFecha(data[i].septimo) == "0") {
                variableTotal7 = variableTotal7 + parseFloat(data[i].septimo);
                total = total + parseFloat(data[i].septimo);
                temp += "<td>" + reescribirFecha(data[i].septimo) + "</td>";

            } else {
                variableTotal7 = variableTotal7 + parseFloat(data[i].septimo);
                total = total + parseFloat(data[i].septimo);
                temp += "<td class='info'>" + reescribirFecha(data[i].septimo) + "</span></td>";
            }
            if (reescribirFecha(data[i].octavo) == "0") {
                variableTotal8 = variableTotal8 + parseFloat(data[i].octavo);
                total = total + parseFloat(data[i].octavo);
                temp += "<td>" + reescribirFecha(data[i].octavo) + "</td>";

            } else {
                variableTotal8 = variableTotal8 + parseFloat(data[i].octavo);
                total = total + parseFloat(data[i].octavo);
                temp += "<td class='info'>" + reescribirFecha(data[i].octavo) + "</span></td>";
            }
            if (reescribirFecha(data[i].noveno) == "0") {
                variableTotal9 = variableTotal9 + parseFloat(data[i].noveno);
                total = total + parseFloat(data[i].noveno);
                temp += "<td>" + reescribirFecha(data[i].noveno) + "</td>";

            } else {
                variableTotal9 = variableTotal9 + parseFloat(data[i].noveno);
                total = total + parseFloat(data[i].noveno);
                temp += "<td class='info'>" + reescribirFecha(data[i].noveno) + "</span></td>";
            }
            if (reescribirFecha(data[i].decimo) == "0") {
                variableTotal10 = variableTotal10 + parseFloat(data[i].decimo);
                total = total + parseFloat(data[i].decimo);
                temp += "<td>" + reescribirFecha(data[i].decimo) + "</td>";

            } else {
                variableTotal10 = variableTotal10 + parseFloat(data[i].decimo);
                total = total + parseFloat(data[i].decimo);
                temp += "<td class='info'>" + reescribirFecha(data[i].decimo) + "</span></td>";
            }
            if (reescribirFecha(data[i].once) == "0") {
                variableTotal11 = variableTotal11 + parseFloat(data[i].once);
                total = total + parseFloat(data[i].once);
                temp += "<td>" + reescribirFecha(data[i].once) + "</td>";

            } else {
                variableTotal11 = variableTotal11 + parseFloat(data[i].once);
                total = total + parseFloat(data[i].once);
                temp += "<td class='info'>" + reescribirFecha(data[i].once) + "</span></td>";
            }
            if (reescribirFecha(data[i].doce) == "0") {
                variableTotal12 = variableTotal12 + parseFloat(data[i].doce);
                total = total + parseFloat(data[i].doce);
                temp += "<td>" + reescribirFecha(data[i].doce) + "</td>";

            } else {
                variableTotal12 = variableTotal12 + parseFloat(data[i].doce);
                total = total + parseFloat(data[i].doce);
                temp += "<td class='info'>" + reescribirFecha(data[i].doce) + "</span></td>";
            }
            if (reescribirFecha(data[i].trece) == "0") {
                variableTotal13 = variableTotal13 + parseFloat(data[i].trece);
                total = total + parseFloat(data[i].trece);
                temp += "<td>" + reescribirFecha(data[i].trece) + "</td>";

            } else {
                variableTotal13 = variableTotal13 + parseFloat(data[i].trece);
                total = total + parseFloat(data[i].trece);
                temp += "<td class='info'>" + reescribirFecha(data[i].trece) + "</span></td>";
            }
            if (reescribirFecha(data[i].catorce) == "0") {
                variableTotal14 = variableTotal14 + parseFloat(data[i].catorce);
                        
                total = total + parseFloat(data[i].catorce);
                temp += "<td>" + reescribirFecha(data[i].catorce) + "</td>";

            } else {
                variableTotal14 = variableTotal14 + parseFloat(data[i].catorce);
                total = total + parseFloat(data[i].catorce);
                temp += "<td class='info'>" + reescribirFecha(data[i].catorce) + "</span></td>";
            }
            if (reescribirFecha(data[i].quince) == "0") {
                variableTotal15 = variableTotal15 + parseFloat(data[i].quince);
                total = total + parseFloat(data[i].quince);
                temp += "<td>" + reescribirFecha(data[i].quince) + "</td>";

            } else {
                variableTotal15 = variableTotal15 + parseFloat(data[i].quince);
                total = total + parseFloat(data[i].quince);
                temp += "<td class='info'>" + reescribirFecha(data[i].quince) + "</span></td>";
            }
            if (reescribirFecha(data[i].diesiceis) == "0") {
                variableTotal16 = variableTotal16 + parseFloat(data[i].diesiceis);
                total = total + parseFloat(data[i].diesiceis);
                temp += "<td>" + reescribirFecha(data[i].diesiceis) + "</td>";

            } else {
                variableTotal16 = variableTotal16 + parseFloat(data[i].diesiceis);
                total = total + parseFloat(data[i].diesiceis);
                temp += "<td class='info'>" + reescribirFecha(data[i].diesiceis) + "</span></td>";
            }
            if (reescribirFecha(data[i].diesiciete) == "0") {
                variableTotal17 = variableTotal17 + parseFloat(data[i].diesiciete);
                total = total + parseFloat(data[i].diesiciete);
                temp += "<td>" + reescribirFecha(data[i].diesiciete) + "</td>";

            } else {
                variableTotal17 = variableTotal17 + parseFloat(data[i].diesiciete);
                total = total + parseFloat(data[i].diesiciete);
                temp += "<td class='info'>" + reescribirFecha(data[i].diesiciete) + "</span></td>";
            }
            if (reescribirFecha(data[i].dieciocho) == "0") {
                variableTotal18 = variableTotal18 + parseFloat(data[i].dieciocho);
                total = total + parseFloat(data[i].dieciocho);
                temp += "<td>" + reescribirFecha(data[i].dieciocho) + "</td>";

            } else {
                variableTotal18 = variableTotal18 + parseFloat(data[i].dieciocho);
                total = total + parseFloat(data[i].dieciocho);
                temp += "<td class='info'>" + reescribirFecha(data[i].dieciocho) + "</span></td>";
            }
            if (reescribirFecha(data[i].diecinueve) == "0") {
                variableTotal19 = variableTotal19 + parseFloat(data[i].diecinueve);
                total = total + parseFloat(data[i].diecinueve);
                temp += "<td>" + reescribirFecha(data[i].diecinueve) + "</td>";

            } else {
                variableTotal19 = variableTotal19 + parseFloat(data[i].diecinueve);
                total = total + parseFloat(data[i].diecinueve);
                temp += "<td class='info'>" + reescribirFecha(data[i].diecinueve) + "</span></td>";
            }
            if (reescribirFecha(data[i].veinte) == "0") {
                variableTotal20 = variableTotal20 + parseFloat(data[i].veinte);
                total = total + parseFloat(data[i].veinte);
                temp += "<td>" + reescribirFecha(data[i].veinte) + "</td>";

            } else {
                variableTotal20 = variableTotal20 + parseFloat(data[i].veinte);
                total = total + parseFloat(data[i].veinte);
                temp += "<td class='info'>" + reescribirFecha(data[i].veinte) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiuno) == "0") {
                variableTotal21 = variableTotal21 + parseFloat(data[i].veintiuno);
                total = total + parseFloat(data[i].veintiuno);
                temp += "<td>" + reescribirFecha(data[i].veintiuno) + "</td>";

            } else {
                variableTotal21 = variableTotal21 + parseFloat(data[i].veintiuno);
                total = total + parseFloat(data[i].veintiuno);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintiuno) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintidos) == "0") {
                variableTotal22 = variableTotal22 + parseFloat(data[i].veintidos);
                total = total + parseFloat(data[i].veintidos);
                temp += "<td>" + reescribirFecha(data[i].veintidos) + "</td>";

            } else {
                variableTotal22 = variableTotal22 + parseFloat(data[i].veintidos);
                total = total + parseFloat(data[i].veintidos);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintidos) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintres) == "0") {
                variableTotal23 = variableTotal23 + parseFloat(data[i].veintres);
                total = total + parseFloat(data[i].veintres);
                temp += "<td>" + reescribirFecha(data[i].veintres) + "</td>";

            } else {
                variableTotal23 = variableTotal23 + parseFloat(data[i].veintres);
                total = total + parseFloat(data[i].veintres);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintres) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiCuatro) == "0") {
                variableTotal24 = variableTotal24 + parseFloat(data[i].veintiCuatro);
                total = total + parseFloat(data[i].veintiCuatro);
                temp += "<td>" + reescribirFecha(data[i].veintiCuatro) + "</td>";

            } else {
                variableTotal24 = variableTotal24 + parseFloat(data[i].veintiCuatro);
                total = total + parseFloat(data[i].veintiCuatro);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintiCuatro) + "</span></td>";
            }
            if (reescribirFecha(data[i].veinticinco) == "0") {
                variableTotal25 = variableTotal25 + parseFloat(data[i].veinticinco);
                total = total + parseFloat(data[i].veinticinco);
                temp += "<td>" + reescribirFecha(data[i].veinticinco) + "</td>";

            } else {
                variableTotal25 = variableTotal25 + parseFloat(data[i].veinticinco);
                total = total + parseFloat(data[i].veinticinco);
                temp += "<td class='info'>" + reescribirFecha(data[i].veinticinco) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiSeis) == "0") {
                variableTotal26 = variableTotal26 + parseFloat(data[i].veintiSeis);
                total = total + parseFloat(data[i].veintiSeis);
                temp += "<td>" + reescribirFecha(data[i].veintiSeis) + "</td>";

            } else {
                variableTotal26 = variableTotal26 + parseFloat(data[i].veintiSeis);
                total = total + parseFloat(data[i].veintiSeis);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintiSeis) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiSiete) == "0") {
                variableTotal27 = variableTotal27 + parseFloat(data[i].veintiSiete);
                total = total + parseFloat(data[i].veintiSiete);
                temp += "<td>" + reescribirFecha(data[i].veintiSiete) + "</td>";

            } else {
                variableTotal27 = variableTotal27 + parseFloat(data[i].veintiSiete);
                total = total + parseFloat(data[i].veintiSiete);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintiSiete) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiOcho) == "0") {
                variableTotal28 = variableTotal28 + parseFloat(data[i].veintiOcho);
                total = total + parseFloat(data[i].veintiOcho);
                temp += "<td>" + reescribirFecha(data[i].veintiOcho) + "</td>";

            } else {
                variableTotal28 = variableTotal28 + parseFloat(data[i].veintiOcho);
                total = total + parseFloat(data[i].veintiOcho);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintiOcho) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiNueve) == "0") {
                variableTotal29 = variableTotal29 + parseFloat(data[i].veintiNueve);
                total = total + parseFloat(data[i].veintiNueve);
                temp += "<td>" + reescribirFecha(data[i].veintiNueve) + "</td>";

            } else {
                variableTotal29 = variableTotal29 + parseFloat(data[i].veintiNueve);
                total = total + parseFloat(data[i].veintiNueve);
                temp += "<td class='info'>" + reescribirFecha(data[i].veintiNueve) + "</span></td>";
            }
            if (reescribirFecha(data[i].Trienta) == "0") {
                variableTotal30 = variableTotal30 + parseFloat(data[i].Trienta);
                total = total + parseFloat(data[i].Trienta);
                temp += "<td>" + reescribirFecha(data[i].Trienta) + "</td>";

            } else {
                variableTotal30 = variableTotal30 + parseFloat(data[i].Trienta);
                total = total + parseFloat(data[i].Trienta);
                temp += "<td class='info'>" + reescribirFecha(data[i].Trienta) + "</span></td>";
            }
            if (reescribirFecha(data[i].TrientaUno) == "0") {
                variableTotal31 = variableTotal31 + parseFloat(data[i].TrientaUno);
                total = total + parseFloat(data[i].TrientaUno);
                temp += "<td>" + reescribirFecha(data[i].TrientaUno) + "</td>";

            } else {
                variableTotal31 = variableTotal31 + parseFloat(data[i].TrientaUno);
                total = total + parseFloat(data[i].TrientaUno);
                temp += "<td class='info'>" + reescribirFecha(data[i].TrientaUno) + "</span></td>";
            }

            totalFinal = totalFinal + parseFloat(total);

            temp += "<td class='info'>" + reescribirFecha(total) + "</span></td>";
               

            // temp += "<td>" + data[i]. + "</td>";
                   
            temp += "</tr>";

            total = 0;

        }

        temp += "<tr>";
        temp += "<td>Total</td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal1) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal2) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal3) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal4) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal5) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal6) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal7) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal8) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal9) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal10) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal11) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal12) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal13) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal14) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal15) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal16) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal17) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal18) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal19) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal20) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal21) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal22) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal23) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal24) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal25) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal26) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal27) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal28) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal29) + "</span></td>";
        temp +=  "<td class='warning'>" + reescribirFecha(variableTotal30) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(variableTotal31) + "</span></td>";
        temp += "<td class='warning'>" + reescribirFecha(totalFinal) + "</span></td>";
        temp += "</tr>";

                                    
        var ReporteMensual = $("#table");
        ReporteMensual.empty();
        ReporteMensual.append(temp);               
               


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

            alert(result.d);


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
                temp += "<td><button type='button' value=" + data[i].ProyectoId + " onclick='reporteTiempoMensual(this.value)' class='btn btn-default btn-xs'><span class='fa fa-file-excel-o' aria-hidden='true'></span> Generar Excel</button></td>";


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

    if (data == "0") {
            
        return 0;

    }

    var fecha = parseInt(data);
    var hora = parseInt(fecha / 60);
    var minutos = (fecha % 60);
    var result = hora + ":" + minutos;

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




    var data = {

        idArchivo: idArchivo

    }

    $.ajax({

        url: "Index.aspx/reporteTiempoMensual",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

            var data = JSON.parse(result.d);
            var temp = "";
            //  temp += "<table class='table table-striped table-bordered table-hover' id='ReporteMensual'>";
            temp += "<thead>";
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
            temp += "</thead>";
            temp += "<tbody>";
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

}

function formatoFecha(fecha) {

    var date = new Date(fecha);
    var año = moment(date).format('YYYY');
    var mes = moment(date).format('MM');

    var fechaReturn = mes + "/" + año;
    return fechaReturn;

}

function validarFechas() {

    var rSult = $("#datepicker").val();
    if (rSult == "") {

        var validar = $("#validar");
        validar.css("display","block");

        return false;

    } else {
        var validar = $("#validar");
        validar.css("display","none");

        return true;

    }



}