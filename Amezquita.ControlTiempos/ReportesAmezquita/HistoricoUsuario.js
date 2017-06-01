
var idcliente;
var proyectoIdFijo = "";
var servicioIdFijo = "";
var GnombreProyecto = "";
var divAntiguo;
var divAntiguoUsu;
var auxTiempo = "";
var ProyectomodificarId = "";
var diaAux = "";
var tiempo = "";
var auxProyecto = "";

$(document).ready(function () {

    $('#modalUsuarioCargueModificar').on('hidden.bs.modal', function () {
       
        consultarF();

    })



    var CargarHorasEliminar = $("#CargarHorasEliminar");
    CargarHorasEliminar.click(function () {

        var data = {
            fecha: diaAux,
            idProyectomodificar: ProyectomodificarId
        }
        $.ajax({

            url: "HistoricoUsuario.aspx/EliminarTiempo",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            dataType: "json",
            success: function (result) {

                consultarF();
                var modalUsuarioCargueModificar = $("#modalUsuarioCargueModificar");
                modalUsuarioCargueModificar.modal("toggle");

            }
        });

    });


    var CargarHorasModificar = $("#CargarHorasModificar");
    CargarHorasModificar.click(function () {
       

        var rsult = validarModificacionTiempo();
      

        if (rsult == true) {

            var obserservaciones = $("#commentModificar").val();
            var idActividad = $("#selecActividadesModificar option:selected").val();
            var idservicio = $("#selecServiciosModificar option:selected").val();
            var idproyecto = $("#selectProyectosModificar option:selected").val();
            var diaCargar = $("#diaCargarModificar").val();
            var horas = $("#horasModificar")[0].value;
            var data = {

                Observaciones: obserservaciones,
                ActividadId: idActividad,
                ServicioId: idservicio,
                diaCargar: diaCargar,
                horas: horas,
                idproyecto: idproyecto,
                fecha: diaAux,
                idProyectomodificar: ProyectomodificarId
            }
            $.ajax({

                url: "HistoricoUsuario.aspx/EditarTiempo",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data),
                dataType: "json",
                success: function (result) {

                    var rSult = result.d;
                    if (rSult != "bn") {
                        var CargarlabelErrorTiempo = $("#CargarlabelErrorTiempo");
                        CargarlabelErrorTiempo.text(rSult);
                        CargarlabelErrorTiempo.css("display","block");
                       

                    }
                    if (rSult == "bn") {
                        var CargarlabelErrorTiempo = $("#CargarlabelErrorTiempo");
                        CargarlabelErrorTiempo.css("display", "none");
                        consultarF();
                        var modalUsuarioCargueModificar = $("#modalUsuarioCargueModificar");
                        modalUsuarioCargueModificar.modal("toggle");
                    }
                }
            });
        }

    });


    $(function () {
        $('#datetimepicker3').datetimepicker({
         
            format: 'HH:mm'
        });

    });
    $(function () {
        $('#datetimepicker3Modificar').datetimepicker({

            format: 'HH:mm'
        });

    });
   
    
    var CargarHoras = $("#CargarHoras");
    CargarHoras.click(function () {
        $(CargarHoras).prop("disabled", false);
        var CargarHoras = $("#CargarHoras");
        var idActividad = $("#selectProyectos option:selected").val();
       

       registroTiempo();

    });


    $('#selectProyectos').on('change', function () {

        ObtenerServicioProyecto(this.value);
    })



    $('#selectProyectosModificar').on('change', function () {
     
        ObtenerServicioProyectoModificar(this.value);
    })

    $('#selecServicios').on('change', function () {
      
        obtenerActividades();
    })

    $('#selecServiciosModificar').on('change', function () {

        obtenerActividadesModificar();
    })

    $('.selectpicker').addClass('col-lg-12').selectpicker('setStyle');
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


function validarModificacionTiempo() {


    var Rsult = true;
    var selecActividadesModificar = $("#selecActividadesModificar").val();
    if (selecActividadesModificar == "Seleccionar") {

        var labelErrorActividades = $("#labelErrorActividades");
        labelErrorActividades.css("display", "block");
        Rsult = false;
    }
    if (selecActividadesModificar != "Seleccionar") {

        var labelErrorActividades = $("#labelErrorActividades");
        labelErrorActividades.css("display", "none");
      
    }


    var selecServiciosModificar = $("#selecServiciosModificar").val();
    if (selecServiciosModificar == "Seleccionar") {

        var labelErrorServicios = $("#labelErrorServicios");
        labelErrorServicios.css("display", "block");
        Rsult = false;
    }
    if (selecServiciosModificar != "Seleccionar") {

        var labelErrorServicios = $("#labelErrorServicios");
        labelErrorServicios.css("display", "none");
      
    }


    var selectProyectosModificar = $("#selectProyectosModificar").val();
    if (selectProyectosModificar == "Seleccionar") {

        var labelErrorProyectos = $("#labelErrorProyectos");
        labelErrorProyectos.css("display", "block");
        Rsult = false;
    }
    if (selectProyectosModificar != "Seleccionar") {

        var labelErrorProyectos = $("#labelErrorProyectos");
        labelErrorProyectos.css("display", "none");
       
    }
    var horasModificar = $("#horasModificar").val();
 
    if (horasModificar == "") {
       
        var labelErrorModificar = $("#labelErrorModificar");
        labelErrorModificar.css("display", "block");
        Rsult = false;
    }
    if (horasModificar != "") {

        var labelErrorModificar = $("#labelErrorModificar");
        labelErrorModificar.css("display", "none");
       
    }
    if (Rsult == true) {
        var labelErrorActividades = $("#labelErrorActividades");
        labelErrorActividades.css("display", "none");

        var labelErrorServicios = $("#labelErrorServicios");
        labelErrorServicios.css("display", "none");

        var labelErrorProyectos = $("#labelErrorProyectos");
        labelErrorProyectos.css("display", "none");

        var labelErrorModificar = $("#labelErrorModificar");
        labelErrorModificar.css("display", "none");


    }

    return Rsult;
}



function validarRegistroTiempo() {
    var rsult = true;





    var selectProyectos = $("#selectProyectos option:selected").val();
   // alert(selectProyectos);
    if (selectProyectos == "Seleccionar") {
      
        var CarguelabelErrorProyectos = $("#CarguelabelErrorProyectos");
        CarguelabelErrorProyectos.css("display","block");
        rsult = false;
    }

    if (selectProyectos != "Seleccionar") {

        var CarguelabelErrorProyectos = $("#CarguelabelErrorProyectos");
        CarguelabelErrorProyectos.css("display", "none");

    }
    var selectServicio = $("#selecServicios option:selected").val();
    // alert(selectProyectos);
    if (selectServicio == "Seleccionar") {

        var CarguelabelErrorServicio = $("#CarguelabelErrorServicio");
        CarguelabelErrorServicio.css("display", "block");
        rsult = false;
    }

    if (selectServicio != "Seleccionar") {

        var CarguelabelErrorServicio = $("#CarguelabelErrorServicio");
        CarguelabelErrorServicio.css("display", "none");

    }
    var selecActividades = $("#selecActividades option:selected").val();
    // alert(selectProyectos);
    if (selecActividades == "Seleccionar") {

        var CarguelabelErrorActividad = $("#CarguelabelErrorActividad");
        CarguelabelErrorActividad.css("display", "block");
        rsult = false;
    }

    if (selecActividades != "Seleccionar") {

        var CarguelabelErrorActividad = $("#CarguelabelErrorActividad");
        CarguelabelErrorActividad.css("display", "none");

    }
    var horas = $("#horas").val();
    // alert(selectProyectos);
    if (horas == "") {

        var CargarlabelErrorHora = $("#CargarlabelErrorHora");
        CargarlabelErrorHora.css("display", "block");
        rsult = false;
    }

    if (horas != "") {

        var CargarlabelErrorHora = $("#CargarlabelErrorHora");
        CargarlabelErrorHora.css("display", "none");

    }

    return rsult;
}


function registroTiempo() {
    
   var rsult = validarRegistroTiempo();

   if (rsult == true) {
       var obserservaciones = $("#comment").val();
       var idActividad = $("#selecActividades option:selected").val();
       var idservicio = $("#selecServicios option:selected").val();
       var idproyecto = $("#selectProyectos option:selected").val();
       var diaCargar = $("#diaCargar").val();
       var horas = $("#horas")[0].value;
       var data = {

           Observaciones: obserservaciones,
           ActividadId: idActividad,
           ServicioId: idservicio,
           diaCargar: diaCargar,
           horas: horas,
           idproyecto: idproyecto
       }

       $.ajax({

           url: "HistoricoUsuario.aspx/registrarTiempo",
           type: "POST",
           contentType: "application/json; charset=utf-8",
           data: JSON.stringify(data),
           dataType: "json",
           success: function (result) {




               if (result.d == "bn") {

                   consultarF();
                   var modalUsuarioCargue = $("#modalUsuarioCargue");
                   modalUsuarioCargue.modal("toggle");
               }
               else {


                   var error = $("#labelError");
                   error.text(result.d);
                   error.css("display","block");

               }

           }
       });
   }
}
function datepicker(i) {

   

    for (j = 0; j <= i-1; j++) {
        
        $('#datetimes' + j).datetimepicker({
            format: 'HH:mm'
        });
    }   
};


  
function DescargarDocumento() {


   
    
    $("#dataTablesReport").table2excel({

        // exclude CSS class

        exclude: ".noExl",
        name: "Prueba.xls",
        filename: "Archivos"

    });


}

function modificar(id , dia) {

    validacionModificacionNumeroDias(dia,id);

}

        
function consultarF () {

 

var validacion = validarFechas();
           

var rSult = $("#datepicker").val();

var mesReporte = $("#mesReporte");
mesReporte.empty();
mesReporte.text(rSult);

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
        console.log(data);
        var temp = "";
        var tempR = "";
            
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


        temp += "<tr class='bg-primary' style='background-color:#428bca'>";
        temp += "<td id='myDiv'>Nombre del Proyecto</td>";
        temp += "<td onClick='validacionCargueNumeroDias(1)'><a href='#' style='color:white'><u>1</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(2)'><a href='#' style='color:white'><u>2</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(3)'><a href='#' style='color:white'><u>3</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(4)'><a href='#' style='color:white'><u>4</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(5)'><a href='#' style='color:white'><u>5</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(6)'><a href='#' style='color:white'><u>6</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(7)'><a href='#' style='color:white'><u>7</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(8)'><a href='#' style='color:white'><u>8</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(9)'><a href='#' style='color:white'><u>9</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(10)'><a href='#' style='color:white'><u>10</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(11)'><a href='#' style='color:white'><u>11</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(12)'><a href='#' style='color:white'><u>12</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(13)'><a href='#' style='color:white'><u>13</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(14)'><a href='#' style='color:white'><u>14</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(15)'><a href='#' style='color:white'><u>15</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(16)'><a href='#' style='color:white'><u>16</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(17)'><a href='#' style='color:white'><u>17</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(18)'><a href='#' style='color:white'><u>18</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(19)'><a href='#' style='color:white'><u>19</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(20)'><a href='#' style='color:white'><u>20</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(21)'><a href='#' style='color:white'><u>21</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(22)'><a href='#' style='color:white'><u>22</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(23)'><a href='#' style='color:white'><u>23</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(24)'><a href='#' style='color:white'><u>24</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(25)'><a href='#' style='color:white'><u>25</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(26)'><a href='#' style='color:white'><u>26</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(27)'><a href='#' style='color:white'><u>27</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(28)'><a href='#' style='color:white'><u>28</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(29)'><a href='#' style='color:white'><u>29</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(30)'><a href='#' style='color:white'><u>30</u></a></td>";
        temp += "<td onClick='validacionCargueNumeroDias(31)'><a href='#' style='color:white'><u>31</u></a></td>";
        temp += "<td>TOTAL</td>";
        temp += "</tr>";

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
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',1 )"><a style="cursor:pointer">' + reescribirFecha(data[i].primero) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].segundo) == "0") {
                variableTotal2 = variableTotal2 + parseFloat(data[i].segundo);
                total = total + parseFloat(data[i].segundo);
                temp += "<td>" + reescribirFecha(data[i].segundo) + "</td>";

            } else {
                variableTotal2 = variableTotal2 + parseFloat(data[i].segundo);
                total = total + parseFloat(data[i].segundo);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',2)"><a style="cursor:pointer">' + reescribirFecha(data[i].segundo) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].tercero) == "0") {
                variableTotal3 = variableTotal3 + parseFloat(data[i].tercero);
                total = total + parseFloat(data[i].tercero);
                temp += "<td>" + reescribirFecha(data[i].tercero) + "</td>";

            } else {
                variableTotal3 = variableTotal3 + parseFloat(data[i].tercero);
                total = total + parseFloat(data[i].tercero);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',3)"><a style="cursor:pointer">' + reescribirFecha(data[i].tercero) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].cuarto) == "0") {
                variableTotal4 = variableTotal4 + parseFloat(data[i].cuarto);
                total = total + parseFloat(data[i].cuarto);
                temp += "<td>" + reescribirFecha(data[i].cuarto) + "</td>";

            } else {
                variableTotal4 = variableTotal4 + parseFloat(data[i].cuarto);
                total = total + parseFloat(data[i].cuarto);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',4)"><a style="cursor:pointer">' + reescribirFecha(data[i].cuarto) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].quinto) == "0") {
                variableTotal5 = variableTotal5 + parseFloat(data[i].quinto);
                total = total + parseFloat(data[i].quinto);
                temp += "<td>" + reescribirFecha(data[i].quinto) + "</td>";

            } else {
                variableTotal5 = variableTotal5 + parseFloat(data[i].quinto);
                total = total + parseFloat(data[i].quinto);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',5)"><a style="cursor:pointer">' + reescribirFecha(data[i].quinto) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].sexto) == "0") {
                variableTotal6 = variableTotal6 + parseFloat(data[i].sexto);
                total = total + parseFloat(data[i].sexto);
                temp += "<td>" + reescribirFecha(data[i].sexto) + "</td>";

            } else {
                variableTotal6 = variableTotal6 + parseFloat(data[i].sexto);
                total = total + parseFloat(data[i].sexto);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',6)"><a style="cursor:pointer">' + reescribirFecha(data[i].sexto) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].septimo) == "0") {
                variableTotal7 = variableTotal7 + parseFloat(data[i].septimo);
                total = total + parseFloat(data[i].septimo);
                temp += "<td>" + reescribirFecha(data[i].septimo) + "</td>";

            } else {
                variableTotal7 = variableTotal7 + parseFloat(data[i].septimo);
                total = total + parseFloat(data[i].septimo);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',7)"><a style="cursor:pointer">' + reescribirFecha(data[i].septimo) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].octavo) == "0") {
                variableTotal8 = variableTotal8 + parseFloat(data[i].octavo);
                total = total + parseFloat(data[i].octavo);
                temp += "<td>" + reescribirFecha(data[i].octavo) + "</td>";

            } else {
                variableTotal8 = variableTotal8 + parseFloat(data[i].octavo);
                total = total + parseFloat(data[i].octavo);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',8)"><a style="cursor:pointer">' + reescribirFecha(data[i].octavo) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].noveno) == "0") {
                variableTotal9 = variableTotal9 + parseFloat(data[i].noveno);
                total = total + parseFloat(data[i].noveno);
                temp += "<td>" + reescribirFecha(data[i].noveno) + "</td>";

            } else {
                variableTotal9 = variableTotal9 + parseFloat(data[i].noveno);
                total = total + parseFloat(data[i].noveno);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',9)"><a style="cursor:pointer">' + reescribirFecha(data[i].noveno) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].decimo) == "0") {
                variableTotal10 = variableTotal10 + parseFloat(data[i].decimo);
                total = total + parseFloat(data[i].decimo);
                temp += "<td>" + reescribirFecha(data[i].decimo) + "</td>";

            } else {
                variableTotal10 = variableTotal10 + parseFloat(data[i].decimo);
                total = total + parseFloat(data[i].decimo);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',10)"><a style="cursor:pointer">' + reescribirFecha(data[i].decimo) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].once) == "0") {
                variableTotal11 = variableTotal11 + parseFloat(data[i].once);
                total = total + parseFloat(data[i].once);
                temp += "<td>" + reescribirFecha(data[i].once) + "</td>";

            } else {
                variableTotal11 = variableTotal11 + parseFloat(data[i].once);
                total = total + parseFloat(data[i].once);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',11)"><a style="cursor:pointer">' + reescribirFecha(data[i].once) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].doce) == "0") {
                variableTotal12 = variableTotal12 + parseFloat(data[i].doce);
                total = total + parseFloat(data[i].doce);
                temp += "<td>" + reescribirFecha(data[i].doce) + "</td>";

            } else {
                variableTotal12 = variableTotal12 + parseFloat(data[i].doce);
                total = total + parseFloat(data[i].doce);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',12)"><a style="cursor:pointer">' + reescribirFecha(data[i].doce) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].trece) == "0") {
                variableTotal13 = variableTotal13 + parseFloat(data[i].trece);
                total = total + parseFloat(data[i].trece);
                temp += "<td>" + reescribirFecha(data[i].trece) + "</td>";

            } else {
                variableTotal13 = variableTotal13 + parseFloat(data[i].trece);
                total = total + parseFloat(data[i].trece);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',13)"><a style="cursor:pointer">' + reescribirFecha(data[i].trece) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].catorce) == "0") {
                variableTotal14 = variableTotal14 + parseFloat(data[i].catorce);
                        
                total = total + parseFloat(data[i].catorce);
                temp += "<td>" + reescribirFecha(data[i].catorce) + "</td>";

            } else {
                variableTotal14 = variableTotal14 + parseFloat(data[i].catorce);
                total = total + parseFloat(data[i].catorce);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',14)"><a style="cursor:pointer">' + reescribirFecha(data[i].catorce) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].quince) == "0") {
                variableTotal15 = variableTotal15 + parseFloat(data[i].quince);
                total = total + parseFloat(data[i].quince);
                temp += "<td>" + reescribirFecha(data[i].quince) + "</td>";

            } else {
                variableTotal15 = variableTotal15 + parseFloat(data[i].quince);
                total = total + parseFloat(data[i].quince);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',15)"><a style="cursor:pointer">' + reescribirFecha(data[i].quince) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].diesiceis) == "0") {
                variableTotal16 = variableTotal16 + parseFloat(data[i].diesiceis);
                total = total + parseFloat(data[i].diesiceis);
                temp += "<td>" + reescribirFecha(data[i].diesiceis) + "</td>";

            } else {
                variableTotal16 = variableTotal16 + parseFloat(data[i].diesiceis);
                total = total + parseFloat(data[i].diesiceis);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',16)"><a style="cursor:pointer">' + reescribirFecha(data[i].diesiceis) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].diesiciete) == "0") {
                variableTotal17 = variableTotal17 + parseFloat(data[i].diesiciete);
                total = total + parseFloat(data[i].diesiciete);
                temp += "<td>" + reescribirFecha(data[i].diesiciete) + "</td>";

            } else {
                variableTotal17 = variableTotal17 + parseFloat(data[i].diesiciete);
                total = total + parseFloat(data[i].diesiciete);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',17)"><a style="cursor:pointer">' + reescribirFecha(data[i].diesiciete) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].dieciocho) == "0") {
                variableTotal18 = variableTotal18 + parseFloat(data[i].dieciocho);
                total = total + parseFloat(data[i].dieciocho);
                temp += "<td>" + reescribirFecha(data[i].dieciocho) + "</td>";

            } else {
                variableTotal18 = variableTotal18 + parseFloat(data[i].dieciocho);
                total = total + parseFloat(data[i].dieciocho);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',18)"><a style="cursor:pointer">' + reescribirFecha(data[i].dieciocho) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].diecinueve) == "0") {
                variableTotal19 = variableTotal19 + parseFloat(data[i].diecinueve);
                total = total + parseFloat(data[i].diecinueve);
                temp += "<td>" + reescribirFecha(data[i].diecinueve) + "</td>";

            } else {
                variableTotal19 = variableTotal19 + parseFloat(data[i].diecinueve);
                total = total + parseFloat(data[i].diecinueve);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',19)"><a style="cursor:pointer">' + reescribirFecha(data[i].diecinueve) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veinte) == "0") {
                variableTotal20 = variableTotal20 + parseFloat(data[i].veinte);
                total = total + parseFloat(data[i].veinte);
                temp += "<td>" + reescribirFecha(data[i].veinte) + "</td>";

            } else {
                variableTotal20 = variableTotal20 + parseFloat(data[i].veinte);
                total = total + parseFloat(data[i].veinte);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',20)"><a style="cursor:pointer">' + reescribirFecha(data[i].veinte) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintiuno) == "0") {
                variableTotal21 = variableTotal21 + parseFloat(data[i].veintiuno);
                total = total + parseFloat(data[i].veintiuno);
                temp += "<td>" + reescribirFecha(data[i].veintiuno) + "</td>";

            } else {
                variableTotal21 = variableTotal21 + parseFloat(data[i].veintiuno);
                total = total + parseFloat(data[i].veintiuno);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',21)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintiuno) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintidos) == "0") {
                variableTotal22 = variableTotal22 + parseFloat(data[i].veintidos);
                total = total + parseFloat(data[i].veintidos);
                temp += "<td>" + reescribirFecha(data[i].veintidos) + "</td>";

            } else {
                variableTotal22 = variableTotal22 + parseFloat(data[i].veintidos);
                total = total + parseFloat(data[i].veintidos);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',22)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintidos) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintres) == "0") {
                variableTotal23 = variableTotal23 + parseFloat(data[i].veintres);
                total = total + parseFloat(data[i].veintres);
                temp += "<td>" + reescribirFecha(data[i].veintres) + "</td>";

            } else {
                variableTotal23 = variableTotal23 + parseFloat(data[i].veintres);
                total = total + parseFloat(data[i].veintres);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',23)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintres) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintiCuatro) == "0") {
                variableTotal24 = variableTotal24 + parseFloat(data[i].veintiCuatro);
                total = total + parseFloat(data[i].veintiCuatro);
                temp += "<td>" + reescribirFecha(data[i].veintiCuatro) + "</td>";

            } else {
                variableTotal24 = variableTotal24 + parseFloat(data[i].veintiCuatro);
                total = total + parseFloat(data[i].veintiCuatro);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',24)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintiCuatro) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veinticinco) == "0") {
                variableTotal25 = variableTotal25 + parseFloat(data[i].veinticinco);
                total = total + parseFloat(data[i].veinticinco);
                temp += "<td>" + reescribirFecha(data[i].veinticinco) + "</td>";

            } else {
                variableTotal25 = variableTotal25 + parseFloat(data[i].veinticinco);
                total = total + parseFloat(data[i].veinticinco);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',25)"><a style="cursor:pointer">' + reescribirFecha(data[i].veinticinco) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintiSeis) == "0") {
                variableTotal26 = variableTotal26 + parseFloat(data[i].veintiSeis);
                total = total + parseFloat(data[i].veintiSeis);
                temp += "<td>" + reescribirFecha(data[i].veintiSeis) + "</td>";

            } else {
                variableTotal26 = variableTotal26 + parseFloat(data[i].veintiSeis);
                total = total + parseFloat(data[i].veintiSeis);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',26)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintiSeis) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintiSiete) == "0") {
                variableTotal27 = variableTotal27 + parseFloat(data[i].veintiSiete);
                total = total + parseFloat(data[i].veintiSiete);
                temp += "<td>" + reescribirFecha(data[i].veintiSiete) + "</td>";

            } else {
                variableTotal27 = variableTotal27 + parseFloat(data[i].veintiSiete);
                total = total + parseFloat(data[i].veintiSiete);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',27)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintiSiete) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintiOcho) == "0") {
                variableTotal28 = variableTotal28 + parseFloat(data[i].veintiOcho);
                total = total + parseFloat(data[i].veintiOcho);
                temp += "<td>" + reescribirFecha(data[i].veintiOcho) + "</td>";

            } else {
                variableTotal28 = variableTotal28 + parseFloat(data[i].veintiOcho);
                total = total + parseFloat(data[i].veintiOcho);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',28)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintiOcho) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].veintiNueve) == "0") {
                variableTotal29 = variableTotal29 + parseFloat(data[i].veintiNueve);
                total = total + parseFloat(data[i].veintiNueve);
                temp += "<td>" + reescribirFecha(data[i].veintiNueve) + "</td>";

            } else {
                variableTotal29 = variableTotal29 + parseFloat(data[i].veintiNueve);
                total = total + parseFloat(data[i].veintiNueve);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',29)"><a style="cursor:pointer">' + reescribirFecha(data[i].veintiNueve) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].Trienta) == "0") {
                variableTotal30 = variableTotal30 + parseFloat(data[i].Trienta);
                total = total + parseFloat(data[i].Trienta);
                temp += "<td>" + reescribirFecha(data[i].Trienta) + "</td>";

            } else {
                variableTotal30 = variableTotal30 + parseFloat(data[i].Trienta);
                total = total + parseFloat(data[i].Trienta);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',30)"><a style="cursor:pointer">' + reescribirFecha(data[i].Trienta) + '</a></div></span></td>';
            }
            if (reescribirFecha(data[i].TrientaUno) == "0") {
                variableTotal31 = variableTotal31 + parseFloat(data[i].TrientaUno);
                total = total + parseFloat(data[i].TrientaUno);
                temp += "<td>" + reescribirFecha(data[i].TrientaUno) + "</td>";

            } else {
                variableTotal31 = variableTotal31 + parseFloat(data[i].TrientaUno);
                total = total + parseFloat(data[i].TrientaUno);
                temp += '<td class="info"><div onclick="modificar(\'' + data[i].proyectoId + '\',31)"><a style="cursor:pointer">' + reescribirFecha(data[i].TrientaUno) + '</a></div></span></td>';
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

         total = 0;
         variableTotal1 = 0;
         variableTotal2 = 0;
         variableTotal3 = 0;
         variableTotal4 = 0;
         variableTotal5 = 0;
         variableTotal6 = 0;
         variableTotal7 = 0;
         variableTotal8 = 0;
         variableTotal9 = 0;
         variableTotal10 = 0;
         variableTotal11 = 0;
         variableTotal12 = 0;
         variableTotal13 = 0;
         variableTotal14 = 0;
         variableTotal15 = 0;
         variableTotal16 = 0;
         variableTotal17 = 0;
         variableTotal18 = 0;
         variableTotal19 = 0;
         variableTotal20 = 0;
         variableTotal21 = 0;
         variableTotal22 = 0;
         variableTotal23 = 0;
         variableTotal24 = 0;
         variableTotal25 = 0;
         variableTotal26 = 0;
         variableTotal27 = 0;
         variableTotal28 = 0;
         variableTotal29 = 0;
         variableTotal30 = 0;
         variableTotal31 = 0;
         totalFinal = 0;



        for (var i = 0; i < data.length; i++) {




            tempR += "<tr;>";
            tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + data[i].nombreProyecto + "</td>";
            if (reescribirFecha(data[i].primero) == "0") {
                variableTotal1 = variableTotal1 + parseFloat(data[i].primero);
                total = total + parseFloat(data[i].primero);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].primero) + "</td>";

            } else {
                variableTotal1 = variableTotal1 + parseFloat(data[i].primero);
                total = total + parseFloat(data[i].primero);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].primero) + "</span></td>";
            }

            if (reescribirFecha(data[i].segundo) == "0") {
                variableTotal2 = variableTotal2 + parseFloat(data[i].segundo);
                total = total + parseFloat(data[i].segundo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].segundo) + "</td>";

            } else {
                variableTotal2 = variableTotal2 + parseFloat(data[i].segundo);
                total = total + parseFloat(data[i].segundo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].segundo) + "</span></td>";
            }
            if (reescribirFecha(data[i].tercero) == "0") {
                variableTotal3 = variableTotal3 + parseFloat(data[i].tercero);
                total = total + parseFloat(data[i].tercero);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].tercero) + "</td>";

            } else {
                variableTotal3 = variableTotal3 + parseFloat(data[i].tercero);
                total = total + parseFloat(data[i].tercero);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].tercero) + "</span></td>";
            }
            if (reescribirFecha(data[i].cuarto) == "0") {
                variableTotal4 = variableTotal4 + parseFloat(data[i].cuarto);
                total = total + parseFloat(data[i].cuarto);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].cuarto) + "</td>";

            } else {
                variableTotal4 = variableTotal4 + parseFloat(data[i].cuarto);
                total = total + parseFloat(data[i].cuarto);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].cuarto) + "</span></td>";
            }
            if (reescribirFecha(data[i].quinto) == "0") {
                variableTotal5 = variableTotal5 + parseFloat(data[i].quinto);
                total = total + parseFloat(data[i].quinto);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].quinto) + "</td>";

            } else {
                variableTotal5 = variableTotal5 + parseFloat(data[i].quinto);
                total = total + parseFloat(data[i].quinto);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].quinto) + "</span></td>";
            }
            if (reescribirFecha(data[i].sexto) == "0") {
                variableTotal6 = variableTotal6 + parseFloat(data[i].sexto);
                total = total + parseFloat(data[i].sexto);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].sexto) + "</td>";

            } else {
                variableTotal6 = variableTotal6 + parseFloat(data[i].sexto);
                total = total + parseFloat(data[i].sexto);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].sexto) + "</span></td>";
            }
            if (reescribirFecha(data[i].septimo) == "0") {
                variableTotal7 = variableTotal7 + parseFloat(data[i].septimo);
                total = total + parseFloat(data[i].septimo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].septimo) + "</td>";

            } else {
                variableTotal7 = variableTotal7 + parseFloat(data[i].septimo);
                total = total + parseFloat(data[i].septimo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].septimo) + "</span></td>";
            }
            if (reescribirFecha(data[i].octavo) == "0") {
                variableTotal8 = variableTotal8 + parseFloat(data[i].octavo);
                total = total + parseFloat(data[i].octavo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].octavo) + "</td>";

            } else {
                variableTotal8 = variableTotal8 + parseFloat(data[i].octavo);
                total = total + parseFloat(data[i].octavo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].octavo) + "</span></td>";
            }
            if (reescribirFecha(data[i].noveno) == "0") {
                variableTotal9 = variableTotal9 + parseFloat(data[i].noveno);
                total = total + parseFloat(data[i].noveno);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].noveno) + "</td>";

            } else {
                variableTotal9 = variableTotal9 + parseFloat(data[i].noveno);
                total = total + parseFloat(data[i].noveno);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].noveno) + "</span></td>";
            }
            if (reescribirFecha(data[i].decimo) == "0") {
                variableTotal10 = variableTotal10 + parseFloat(data[i].decimo);
                total = total + parseFloat(data[i].decimo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].decimo) + "</td>";

            } else {
                variableTotal10 = variableTotal10 + parseFloat(data[i].decimo);
                total = total + parseFloat(data[i].decimo);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].decimo) + "</span></td>";
            }
            if (reescribirFecha(data[i].once) == "0") {
                variableTotal11 = variableTotal11 + parseFloat(data[i].once);
                total = total + parseFloat(data[i].once);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].once) + "</td>";

            } else {
                variableTotal11 = variableTotal11 + parseFloat(data[i].once);
                total = total + parseFloat(data[i].once);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].once) + "</span></td>";
            }
            if (reescribirFecha(data[i].doce) == "0") {
                variableTotal12 = variableTotal12 + parseFloat(data[i].doce);
                total = total + parseFloat(data[i].doce);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].doce) + "</td>";

            } else {
                variableTotal12 = variableTotal12 + parseFloat(data[i].doce);
                total = total + parseFloat(data[i].doce);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].doce) + "</span></td>";
            }
            if (reescribirFecha(data[i].trece) == "0") {
                variableTotal13 = variableTotal13 + parseFloat(data[i].trece);
                total = total + parseFloat(data[i].trece);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].trece) + "</td>";

            } else {
                variableTotal13 = variableTotal13 + parseFloat(data[i].trece);
                total = total + parseFloat(data[i].trece);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].trece) + "</span></td>";
            }
            if (reescribirFecha(data[i].catorce) == "0") {
                variableTotal14 = variableTotal14 + parseFloat(data[i].catorce);

                total = total + parseFloat(data[i].catorce);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].catorce) + "</td>";

            } else {
                variableTotal14 = variableTotal14 + parseFloat(data[i].catorce);
                total = total + parseFloat(data[i].catorce);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].catorce) + "</span></td>";
            }
            if (reescribirFecha(data[i].quince) == "0") {
                variableTotal15 = variableTotal15 + parseFloat(data[i].quince);
                total = total + parseFloat(data[i].quince);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].quince) + "</td>";

            } else {
                variableTotal15 = variableTotal15 + parseFloat(data[i].quince);
                total = total + parseFloat(data[i].quince);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].quince) + "</span></td>";
            }
            if (reescribirFecha(data[i].diesiceis) == "0") {
                variableTotal16 = variableTotal16 + parseFloat(data[i].diesiceis);
                total = total + parseFloat(data[i].diesiceis);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].diesiceis) + "</td>";

            } else {
                variableTotal16 = variableTotal16 + parseFloat(data[i].diesiceis);
                total = total + parseFloat(data[i].diesiceis);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].diesiceis) + "</span></td>";
            }
            if (reescribirFecha(data[i].diesiciete) == "0") {
                variableTotal17 = variableTotal17 + parseFloat(data[i].diesiciete);
                total = total + parseFloat(data[i].diesiciete);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].diesiciete) + "</td>";

            } else {
                variableTotal17 = variableTotal17 + parseFloat(data[i].diesiciete);
                total = total + parseFloat(data[i].diesiciete);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].diesiciete) + "</span></td>";
            }
            if (reescribirFecha(data[i].dieciocho) == "0") {
                variableTotal18 = variableTotal18 + parseFloat(data[i].dieciocho);
                total = total + parseFloat(data[i].dieciocho);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].dieciocho) + "</td>";

            } else {
                variableTotal18 = variableTotal18 + parseFloat(data[i].dieciocho);
                total = total + parseFloat(data[i].dieciocho);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].dieciocho) + "</span></td>";
            }
            if (reescribirFecha(data[i].diecinueve) == "0") {
                variableTotal19 = variableTotal19 + parseFloat(data[i].diecinueve);
                total = total + parseFloat(data[i].diecinueve);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].diecinueve) + "</td>";

            } else {
                variableTotal19 = variableTotal19 + parseFloat(data[i].diecinueve);
                total = total + parseFloat(data[i].diecinueve);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].diecinueve) + "</span></td>";
            }
            if (reescribirFecha(data[i].veinte) == "0") {
                variableTotal20 = variableTotal20 + parseFloat(data[i].veinte);
                total = total + parseFloat(data[i].veinte);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veinte) + "</td>";

            } else {
                variableTotal20 = variableTotal20 + parseFloat(data[i].veinte);
                total = total + parseFloat(data[i].veinte);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veinte) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiuno) == "0") {
                variableTotal21 = variableTotal21 + parseFloat(data[i].veintiuno);
                total = total + parseFloat(data[i].veintiuno);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiuno) + "</td>";

            } else {
                variableTotal21 = variableTotal21 + parseFloat(data[i].veintiuno);
                total = total + parseFloat(data[i].veintiuno);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiuno) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintidos) == "0") {
                variableTotal22 = variableTotal22 + parseFloat(data[i].veintidos);
                total = total + parseFloat(data[i].veintidos);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintidos) + "</td>";

            } else {
                variableTotal22 = variableTotal22 + parseFloat(data[i].veintidos);
                total = total + parseFloat(data[i].veintidos);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintidos) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintres) == "0") {
                variableTotal23 = variableTotal23 + parseFloat(data[i].veintres);
                total = total + parseFloat(data[i].veintres);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintres) + "</td>";

            } else {
                variableTotal23 = variableTotal23 + parseFloat(data[i].veintres);
                total = total + parseFloat(data[i].veintres);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintres) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiCuatro) == "0") {
                variableTotal24 = variableTotal24 + parseFloat(data[i].veintiCuatro);
                total = total + parseFloat(data[i].veintiCuatro);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiCuatro) + "</td>";

            } else {
                variableTotal24 = variableTotal24 + parseFloat(data[i].veintiCuatro);
                total = total + parseFloat(data[i].veintiCuatro);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiCuatro) + "</span></td>";
            }
            if (reescribirFecha(data[i].veinticinco) == "0") {
                variableTotal25 = variableTotal25 + parseFloat(data[i].veinticinco);
                total = total + parseFloat(data[i].veinticinco);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veinticinco) + "</td>";

            } else {
                variableTotal25 = variableTotal25 + parseFloat(data[i].veinticinco);
                total = total + parseFloat(data[i].veinticinco);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veinticinco) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiSeis) == "0") {
                variableTotal26 = variableTotal26 + parseFloat(data[i].veintiSeis);
                total = total + parseFloat(data[i].veintiSeis);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiSeis) + "</td>";

            } else {
                variableTotal26 = variableTotal26 + parseFloat(data[i].veintiSeis);
                total = total + parseFloat(data[i].veintiSeis);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiSeis) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiSiete) == "0") {
                variableTotal27 = variableTotal27 + parseFloat(data[i].veintiSiete);
                total = total + parseFloat(data[i].veintiSiete);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiSiete) + "</td>";

            } else {
                variableTotal27 = variableTotal27 + parseFloat(data[i].veintiSiete);
                total = total + parseFloat(data[i].veintiSiete);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiSiete) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiOcho) == "0") {
                variableTotal28 = variableTotal28 + parseFloat(data[i].veintiOcho);
                total = total + parseFloat(data[i].veintiOcho);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiOcho) + "</td>";

            } else {
                variableTotal28 = variableTotal28 + parseFloat(data[i].veintiOcho);
                total = total + parseFloat(data[i].veintiOcho);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiOcho) + "</span></td>";
            }
            if (reescribirFecha(data[i].veintiNueve) == "0") {
                variableTotal29 = variableTotal29 + parseFloat(data[i].veintiNueve);
                total = total + parseFloat(data[i].veintiNueve);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiNueve) + "</td>";

            } else {
                variableTotal29 = variableTotal29 + parseFloat(data[i].veintiNueve);
                total = total + parseFloat(data[i].veintiNueve);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].veintiNueve) + "</span></td>";
            }
            if (reescribirFecha(data[i].Trienta) == "0") {
                variableTotal30 = variableTotal30 + parseFloat(data[i].Trienta);
                total = total + parseFloat(data[i].Trienta);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].Trienta) + "</td>";

            } else {
                variableTotal30 = variableTotal30 + parseFloat(data[i].Trienta);
                total = total + parseFloat(data[i].Trienta);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].Trienta) + "</span></td>";
            }
            if (reescribirFecha(data[i].TrientaUno) == "0") {
                variableTotal31 = variableTotal31 + parseFloat(data[i].TrientaUno);
                total = total + parseFloat(data[i].TrientaUno);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].TrientaUno) + "</td>";

            } else {
                variableTotal31 = variableTotal31 + parseFloat(data[i].TrientaUno);
                total = total + parseFloat(data[i].TrientaUno);
                tempR += "<td style=' border:1px solid  black'>" + reescribirFecha(data[i].TrientaUno) + "</span></td>";
            }

            totalFinal = totalFinal + parseFloat(total);

            tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(total) + "</span></td>";


            // tempR += "<td>" + data[i]. + "</td>";

            tempR += "</tr>";

            total = 0;

        }

        tempR += "<tr>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>Total</td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal1) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal2) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal3) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal4) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal5) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal6) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal7) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal8) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal9) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal10) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal11) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal12) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal13) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal14) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal15) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal16) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal17) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal18) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal19) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal20) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal21) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal22) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal23) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal24) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal25) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal26) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal27) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal28) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal29) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal30) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(variableTotal31) + "</span></td>";
        tempR += "<td style=' background-color:#CDCDCD;  border:1px solid  black'>" + reescribirFecha(totalFinal) + "</span></td>";
        tempR += "</tr>";
        
        var cabezaTable = $("#cabezaTable");
        cabezaTable.empty();
        var ReporteMensual = $("#table");
        var ReporteMensualR = $("#tables");
        ReporteMensualR.empty();
        ReporteMensual.empty();
        ReporteMensual.append(temp);
        ReporteMensualR.append(tempR);
               


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

function divFunction(dia) {


  

    var fechaCargue = $('#datepicker').val();
    fechaCargue = fechaCargue + "/" + dia;
    var selecServicios = $("#selecServicios");
    selecServicios.empty();
    var CarguelabelErrorProyectos = $("#CarguelabelErrorProyectos");
    CarguelabelErrorProyectos.css("display", "none");

    var CarguelabelErrorServicio = $("#CarguelabelErrorServicio");
    CarguelabelErrorServicio.css("display", "none");

    var CarguelabelErrorActividad = $("#CarguelabelErrorActividad");
    CarguelabelErrorServicio.css("display", "none");

    var CargarlabelErrorHora = $("#CargarlabelErrorHora");
    CargarlabelErrorHora.css("display", "none");

    var modalUsuarioCargue = $("#modalUsuarioCargue");
    modalUsuarioCargue.modal("show");
  

    $("#horas")[0].value = "";
    $("#comment")[0].value = "";
  

    var selecActividades = $("#selecActividades");
    selecActividades.empty();

  
    var fecha = formatoFecha(fechaCargue);   
    $('#diaCargar').val(fecha);   
    var labelError = $("#labelError");
    labelError.css("display","none");
    obtenerProyectoGrupoTrabajo();

}

function validacionCargueNumeroDias(dia) {

    var fechaCargue = $('#datepicker').val();
    fechaCargue = fechaCargue + "/" + dia;

    var data =
       {
           fecha: fechaCargue,

       }
    return $.ajax({

        url: "HistoricoUsuario.aspx/validacionCargueNumeroDias",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

          

            var rSult = result.d;
            if (rSult == "False") {

                var ModalErrorCargue = $("#ModalErrorCargue");
                ModalErrorCargue.modal("show");

            }
            if (rSult == "true") {

                divFunction(dia);


            }



        }
    });



}

function cargarTiempo(id) {

    $.ajax({

        url: "HistoricoUsuario.aspx/obtenerProyectoGrupoTrabajo",
        type: "POST",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (result) {
            var temp;
            var selectProyectos = $("#selectProyectos");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ProyectoId + ">" + data[i].NombreProyecto + "</option>";

            }

            for (var j = 0; j <= id; j++) {

                var idModificar = $("#modificarProyecto" + j);
                idModificar.append(temp);
                idModificar.selectpicker('refresh');;

            }
          //  alert(temp);

        }
    });

};

function cargarModificarServicio(idArchivo, num) {


    

    var data =
{
    idProyecto: idArchivo.value

}

    $.ajax({

        url: "HistoricoUsuario.aspx/ObtenerServicioProyecto",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

            var temp;

            var selecServicios = $("#modificarServicio" + num);
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar' selected='selected'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ServicioId + ">" + data[i].NombreServicio + "</option>";

            }
            selecServicios.empty();
            selecServicios.append(temp);
            selecServicios.selectpicker('refresh');
        }
    });



   

}

function elminarRegistro(idCargue , numdiv) {

   // alert(numdiv);

    var data =
      {
          idCargue: idCargue,

      }
    $.ajax({

        url: "HistoricoUsuario.aspx/eliminarRegistroId",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {



            var formularioModificar = $('#formularioModificar' + numdiv + '');
            formularioModificar.empty();
            var labelnotificacion = $("#labelnotificacion" + numdiv + "");
            labelnotificacion.text("Eliminado Correctamente");
            labelnotificacion.css("display", "block");

        }
    });

}

function functionActividades(idActividad ,k) {
    
    var data = {

        idActividad: idActividad.value

    }

    $.ajax({

        url: "HistoricoUsuario.aspx/obtenerActividadesxServicio",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

         
            var temp="";
            var selecActividades = $("#modificarActividades" + k);
            var data = JSON.parse(result.d);
            
           // alert(data);
            temp += "<option value='Seleccionar' selected='selected'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ActividadId + ">" + data[i].NombreActividad + "</option>";

            }
            console.log(temp);
            selecActividades.empty();
           // selecActividades.empty();
            selecActividades.append(temp);
            selecActividades.selectpicker('refresh');

        }
    });

}

function validacionModificacionNumeroDias(dia,idProyecto) {
    auxProyecto = idProyecto;
    var fechaCargue = $('#datepicker').val();
    fechaCargue = fechaCargue + "/" + dia;

    tiempo = fechaCargue;
   
    var data =
       {
           fecha: fechaCargue,
           idProyecto: idProyecto
       }
    return $.ajax({

        url: "HistoricoUsuario.aspx/validacionCargueNumeroDiasModificar",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {


            var rSult = result.d;
            if (rSult == "False") {

                var ModalErrorCargue = $("#ModalErrorCargue");
                ModalErrorCargue.modal("show");

            }
            if (rSult != "true") {


                console.log(result.d);

                var temp = "";

                var data = JSON.parse(result.d);
              
               

                for (var i = 0; i < data.length; i++) {
                    temp += "<div>";
                    if (i != 0) {

                      
                       
                    }
                    temp += "<div id='formularioModificar" + i + "'>";
                    temp += "<div>";
                    temp += "<div class='col-md-3'>";
                    temp += "<select  id='modificarProyecto" + i + "' onchange='cargarModificarServicio(this,"+i+")' class='selectpicker'  class='form-control'  style='width:auto'  data-live-search='true' >";
                    temp += "<option value='" + data[i].ProyectoId + "' >" + data[i].NombreProyecto + "</option>";
                    temp += "</select>";
                    temp += "</div>";
                    temp += "<div class='col-md-3'>";
                    temp += "<select onchange='functionActividades(this,"+i+")' id='modificarServicio" + i + "'  class='col-md-12'  class='selectpicker'  class='form-control'  style='width:auto'  data-live-search='true' >";
                    temp += "<option  value='" + data[i].ServicioId + "'>" + data[i].NombreServicio + "</option>";
                    temp += "</select>";
                    temp += "</div>";
                    temp += "<div class='col-md-3'>";
                    temp += "<select id='modificarActividades" + i + "' class='form-control' class='selectpicker' class='col-md-2' data-live-search='true' >";
                    temp += "<option value='" + data[i].ActividadId + "'>" + data[i].NombreActividad + "</option>";
                    temp += "</select>";
                    temp += "</div>";
                    temp += "<div class='input-group date' class='col-md-3'  id='datetimes" + i + "'>";
                    temp += "<input type='text' class='form-control' id='horas"+i+"' value="+data[i].horasCargadas+">";
                    temp += "<span class='input-group-addon'>";
                    temp += "<span class='glyphicon glyphicon-time'></span>";
                    temp += "</span>";
                    temp += "</div>";
                    temp += "</div>";
                    temp += "<div>";
                    temp += "<br/>";
                    temp += "<div class='col-md-6'><textarea  id='Observaciones" + i + "' class='form-control' > " + data[i].Observacion + "</textarea></div>";
                    temp += "<div class='col-md-3'><button class='btn btn-primary' style='width:100%'  onclick=editarRegistroTiempo('" + data[i].CargueHorasId + "','" + i + "') class='col-md-12' type='submit'>Editar</button></div>";
                    temp += "<div class='col-md-3'><button class='btn btn-danger' style='width:100%' onclick=elminarRegistro('" +data[i].CargueHorasId + "','"+i+"') class='col-md-12' type='submit'>Eliminar</button></div>";                  
                    temp += "<br/>";
                    temp += "</div>";
                    temp += "<br/>";
                    temp += "<br/>";
                    temp += "<br/>";
                    temp += "</div>";
                    temp += "<div id='labelError" + i + "' style='display:none ;color:red'></div>";
                    temp += "<div id='labelnotificacion" + i + "' style='display:none'>Modificado Correctamente</div>";
                    temp += "<hr>";
                    temp += "</div>";


                }


               

             
                cargarTiempo(i);

             
                var ModificarCargueTiempo = $("#ModificarCargueTiempo");
                ModificarCargueTiempo.empty();
                ModificarCargueTiempo.append(temp);

                for (j = 0; j <= i; j++) {

                    var modificarServicio = $("#modificarServicio" + j);
                    modificarServicio.selectpicker('refresh');


                }

                datepicker(i);




                var modalUsuarioCargueModificar = $("#modalUsuarioCargueModificar");
                modalUsuarioCargueModificar.modal("show");


            }

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

function editarRegistroTiempo(idTiempo, i) {

  

    var modificarServicio = $("#modificarServicio" + i + " option:selected").val();
    var modificarProyecto = $("#modificarProyecto" + i + " option:selected").val();
    var modificarActividades = $("#modificarActividades" + i + " option:selected").val();
    var horas = $("#horas"+i+"").val();
    var Observaciones = $("#Observaciones" + i + "").val();
    

    var data =
     {
         Observaciones: Observaciones,
         ActividadId: modificarActividades,
         ServicioId: modificarServicio,
         diaCargar: tiempo,
         horas: horas,
         idproyecto: modificarProyecto,
         idCargueTiempo: idTiempo,
         fecha: tiempo,
         idProyectomodificar: idTiempo,
         proyectoO: auxProyecto

     }
  

    $.ajax({

        url: "HistoricoUsuario.aspx/EditarTiempo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

            var rsultado =(result.d);
            if (rsultado == "bn") {
               
            var formularioModificar = $('#formularioModificar' + i + '');
            formularioModificar.empty();

            var labelnotificacion = $("#labelnotificacion" + i + "");          
            labelnotificacion.css("display", "block");

            var labelError = $("#labelError" + i + "");
            labelError.text(rsultado);
            labelError.css("display", "none");

            }

            if (rsultado != "bn") {

                var labelError = $("#labelError" + i + "");
                labelError.text(rsultado);
                labelError.css("display","block");

              //  alert(rsultado);

            }

        }
    });

}


function obtenerProyectoGrupoTrabajo() {

    $.ajax({

        url: "HistoricoUsuario.aspx/obtenerProyectoGrupoTrabajo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
      
        dataType: "json",
        success: function (result) {
            var temp;
            var selectProyectos = $("#selectProyectos");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option  selected='selected' value=" + data[i].ProyectoId + ">" + data[i].NombreProyecto + "</option>";


            }

            selectProyectos.empty();
            selectProyectos.append(temp);
            $('.selectpicker').selectpicker('refresh');

        }
    });

}

function obtenerProyectoGrupoTrabajoModificar() {

    $.ajax({

        url: "HistoricoUsuario.aspx/obtenerProyectoGrupoTrabajo",
        type: "POST",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (result) {
            var temp;
            var selectProyectos = $("#selectProyectosModificar");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ProyectoId + ">" + data[i].NombreProyecto + "</option>";


            }

            selectProyectos.empty();
            selectProyectos.append(temp);
            $('.selectpicker').selectpicker('refresh');

        }
    });

}

function obtenerActividades() {
  
    $.ajax({

        url: "HistoricoUsuario.aspx/obtenerActividades",
        type: "POST",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (result) {
          
            var temp;
            var selecActividades = $("#selecActividades");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ActividadId + ">" + data[i].NombreActividad + "</option>";


            }

            selecActividades.empty();
            selecActividades.append(temp);
            $('#selecActividades').selectpicker('refresh');

        }
    });

}

function obtenerActividadesModificar() {

    $.ajax({

        url: "HistoricoUsuario.aspx/obtenerActividades",
        type: "POST",
        contentType: "application/json; charset=utf-8",

        dataType: "json",
        success: function (result) {

            var temp;
            var selecActividades = $("#selecActividadesModificar");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ActividadId + ">" + data[i].NombreActividad + "</option>";


            }

            selecActividades.empty();
            selecActividades.append(temp);
            $('#selecActividadesModificar').selectpicker('refresh');

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
    var dia = moment(date).format('D');
    var dates = new Date(año, mes, dia);   
    var fechaReturn = dia +"/" + mes + "/" + año;
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

function ObtenerServicioProyecto(idProyecto) {
  
    var data =
  {
      idProyecto: idProyecto

  }

    $.ajax({

        url: "HistoricoUsuario.aspx/ObtenerServicioProyecto", type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {
            var temp;
            var selecServicios = $("#selecServicios");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ServicioId + ">" + data[i].NombreServicio + "</option>";

            }


            selecServicios.empty();
            selecServicios.append(temp);
            $("#selecServicios").selectpicker('refresh');
        }
    });
}

function ObtenerServicioProyectoModificar(idProyecto) {

 

    var data =
  {
      idProyecto: idProyecto

  }

    $.ajax({

        url: "HistoricoUsuario.aspx/ObtenerServicioProyecto",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

           

            var temp;
         
            var selecServicios = $("#selecServiciosModificar");
            var data = JSON.parse(result.d);
            temp += "<option value='Seleccionar'>Seleccionar</option>";
            for (var i = 0; i < data.length; i++) {
                temp += "<option value=" + data[i].ServicioId + ">" + data[i].NombreServicio + "</option>";

            }
            selecServicios.empty();
            selecServicios.append(temp);
            $("#selecServiciosModificar").selectpicker('refresh');
        }
    });
}