
var idcliente;
var proyectoIdFijo = "";
var servicioIdFijo = "";
var GnombreProyecto = "";
var divAntiguo;
var divAntiguoUsu;
var idGerente = "";
var cont = 1;



$(document).ready(function () {


    var generalExcel = $("#generalExcel");
    generalExcel.click(function () {

        DescargarDocumento();

    });

    var prueba = $("#Prueba");
    prueba.click(function () {

        window.location.href = '../Inicio/Index'

    });

    var buscarUsuario = $("#buscarUsuario");
    buscarUsuario.change(function () {


        var idGrente = idGerente;
        var buscarUsuario = $("#buscarUsuario").val();
     //   alert(buscarUsuario);

        var data =
            {

                usuario: buscarUsuario,
                idGerente:idGrente
            }
        $.ajax({

            url: "AgregarGrupoTrabajos.aspx/obtenerListadoUsuario",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            dataType: "json",
            success: function (result) {

                var data = JSON.parse(result.d);
                var temp = "";
                for (var i = 0; i < data.length; i++) {

                    var auxNombre = data[i].Nombre;
                    auxNombre = auxNombre.replace(/ /g, "&");
                    temp += "<tr id='table"+i+"' >";
                    temp += "<td colspan='2'>" + (data[i].Nombre) + "</td>";
                    temp += "<td><button type='button' value=" + data[i].UsuarioId + "%" + auxNombre + "!" + i + " onclick='agregarUsuario(this.value)' class='btn btn-primary'>Agregar Grupo</button></td>";
                    temp += "</tr>";

                }

                var rSultUsuarios = $("#rSultUsuarios");
                rSultUsuarios.empty();
                rSultUsuarios.append(temp);

            }
        });


    });


    $('#dataTables-example').DataTable({
        responsive: true,
        "order": [[0, "asc"]]
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


function eliminarUsuarioGrupos(idUsuario) {


    console.log("kdkdhfd");



    var idGrente = idGerente;
    var data =
        {

            idUsuario: idUsuario,
            idGerente: idGrente
        }
    $.ajax({

        url: "AgregarGrupoTrabajos.aspx/eliminarUsuariosxGrupos",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {
        }
    });


}


function agregarUsuario(id) {

    console.log(id);

    cont = cont + 1;
    var idAuxl = id.indexOf("%");
    var idI = id.indexOf("!");
    var idUsuario = id.substring(0, idAuxl);
    var nombreUsuario = id.substring(idAuxl + 1, idI);
    var table = id.substring(idI + 1, id.length);
    nombreUsuario = nombreUsuario.replace(/&/g, " ");
    var tables = $("#table" + table);
    tables.empty();
    agregarUsuarioGrupoTrabajo(idUsuario);
    var temp = "<tr id='tableEliminar" + cont + "'><td id='NombreCont" + cont + "'>" + nombreUsuario + "</td><td><button type='button' value='" + idUsuario + '*' + cont + "'onclick='eliminarUsuarioGrupo(this.value)' class='btn btn-danger'>Eliminar del Grupo</button></td>";

    var estado = true;
    if (estado == true) {
    
        temp += "<td><label class=''><input type='checkbox' id='checkBoxNombress" + cont + "' value= '" + idUsuario + '?' + nombreUsuario + '%' + cont + "' onClick='AutoCalculateMandateOnChangesinBd(this.value)' checked='checked'   name='blabla' /></label></td>"

    }
    if (estado == false) {
       
        temp += "<td><label class=''><input type='checkbox' id='checkBoxNombre" + cont + "'  value= '" + idUsuario + '?' + nombreUsuario + '%' + cont + "' onClick='AutoCalculateMandateOnChange(this.value)'  name='blabla' /></label></td>"

    }

    temp += "</tr>";
    var rSultGrupoTrabjo = $("#rSultGrupoTrabjo");  
    rSultGrupoTrabjo.append(temp);


}



function eliminarUsuarioGrupo(idUsuario) {
 
    var auxIdUsuario = idUsuario.indexOf("*");
    var idUsuarios = idUsuario.substring(0, auxIdUsuario);
    var contTable = idUsuario.substring(auxIdUsuario + 1, idUsuario.length);
    eliminarUsuarioGrupos(idUsuarios);
    var tableEliminar = $("#tableEliminar" + contTable);
    tableEliminar.empty();
   

}


function agregarUsuarioGrupoTrabajo(idUsuario) {

    var data =
           {

               idGerente: idGerente,
               idUsuario: idUsuario

           }

    $.ajax({

        url: "AgregarGrupoTrabajos.aspx/agregarUsuarioGrupoTrabajo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

        }
    });
  




}



function AgregarUsuario(id) {
   // alert(id);
    id = id.replace(/ /g, "&");
    var IProyecto = id.indexOf("#");
    var idUsuario = id.substring(0, IProyecto);  
    var nombreGerente = id.substring(IProyecto + 1, id.length);
    var nombreGerentediv = $("#nombreGerente");
    nombreGerente = nombreGerente.replace(/&/g, " ");
    nombreGerentediv.text(nombreGerente);
    idGerente = idUsuario;
    $('#dataTables-example2').DataTable({
        responsive: true,
        "order": [[0, "asc"]]
    });

    obtenerListadoUsuariosxGrupo(idGerente);
    var rSultUsuarios = $("#rSultUsuarios");
    rSultUsuarios.empty();
    var buscarUsuario = $("#buscarUsuario");
    buscarUsuario.val("");

    var myModal = $("#ModalGerente");
    myModal.modal("show");



}

function obtenerListadoUsuariosxGrupo(idGerente) {
    var rSultUsuarios = $("#rSultGrupoTrabjo");
    rSultUsuarios.empty();
    var data =
       {

           idGerente: idGerente
          
       }
    $.ajax({

        url: "AgregarGrupoTrabajos.aspx/obtenerGrupoTrabajo",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {

          //  alert(result.d);
            var temp = "";
            var data = JSON.parse(result.d);
            for (var i = 0; i < data.length; i++) {
               
                var estado = data[i].estado;
                console.log(estado);
                temp += "<tr id='tableEliminarBd"+i+"'>";
               
                if (estado == true) {
                    temp += "<td id='tableNombre" + i + "'style='color:#000'>" + data[i].Nombre + "</td>";
                    temp += "<td><button type='button' class='btn btn-danger' value= '" + data[i].UsuarioId + '%' + i + "' onClick='eliminarUsuarioGrupoAgregadoBd(this.value)'>Eliminar del  Grupo</button> </td>";
                    temp += "<td><label class=''><input type='checkbox' id='checkBoxNombre" + i + "' value= '" + data[i].UsuarioId + '?' + data[i].Nombre + '%' + i + "' onClick='AutoCalculateMandateOnChange(this.value)' checked='checked'   name='blabla' /></label></td>"

                }
                if (estado == false) {
                    temp += "<td id='tableNombre" + i + "'style='color:#777'><u>" + data[i].Nombre + "</u></td>";
                    temp += "<td><button type='button' class='btn btn-danger' value= '" + data[i].UsuarioId + '%' + i + "' onClick='eliminarUsuarioGrupoAgregadoBd(this.value)'>Eliminar del  Grupo</button> </td>";
                    temp += "<td><label class=''><input type='checkbox' id='checkBoxNombre" + i + "'  value= '" + data[i].UsuarioId + '?' + data[i].Nombre + '%' + i + "' onClick='AutoCalculateMandateOnChange(this.value)'  name='blabla' /></label></td>"

                }
             
                temp += "</tr>";
            }

            rSultUsuarios.append(temp);
        }

       

    });



}

function AutoCalculateMandateOnChange(idUsuario) {


  

    var auxIdUsuario = idUsuario.indexOf("?");
    var auxIdUsuarios = idUsuario.indexOf("%");
    var idUsuarios = idUsuario.substring(0, auxIdUsuario);
    var nombre = idUsuario.substring(auxIdUsuario + 1, auxIdUsuarios);
    var contTable = idUsuario.substring(auxIdUsuarios + 1, idUsuario.length);

    

    var data =
        {

            idUsuario: idUsuarios
           
        }
    $.ajax({

        url: "AgregarGrupoTrabajos.aspx/CambiarEstadoUsuario",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {



            if ($("#checkBoxNombre" + contTable).is(':checked')) {



                var temp = "<div id='tableNombre" + contTable + "'style='color:#000'>" + nombre + "</div>";
                var Celdanombre = $('#tableNombre' + contTable + '');

               

            } else {

                var temp = "<div id='tableNombre" + contTable + "'style='color:#777'><footer><u>" + nombre + "</u></footer></div>";
                var Celdanombre = $('#tableNombre' + contTable + '');

            }
           

         
            Celdanombre.empty();
            Celdanombre.append(temp);


        }
    });

}


function AutoCalculateMandateOnChangesinBd(idUsuario) {
    var auxIdUsuario = idUsuario.indexOf("?");
    var auxIdUsuarios = idUsuario.indexOf("%");
    var idUsuarios = idUsuario.substring(0, auxIdUsuario);
    var nombre = idUsuario.substring(auxIdUsuario + 1, auxIdUsuarios);
    var contTable = idUsuario.substring(auxIdUsuarios + 1, idUsuario.length);



    var data =
        {

            idUsuario: idUsuarios

        }
    $.ajax({

        url: "AgregarGrupoTrabajos.aspx/CambiarEstadoUsuario",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        dataType: "json",
        success: function (result) {



            if ($("#checkBoxNombress" + contTable).is(':checked')) {



                var temp = "<div id='tableNombre" + contTable + "'style='color:#000'>" + nombre + "</div>";
                var Celdanombre = $('#NombreCont' + contTable + '');



            } else {

                var temp = "<div id='tableNombre" + contTable + "'style='color:#777'><footer><u>" + nombre + "</u></footer></div>";
                var Celdanombre = $('#NombreCont' + contTable + '');

            }



            Celdanombre.empty();
            Celdanombre.append(temp);


        }
    });

}



function eliminarUsuarioGrupoAgregadoBd(idUsuario) {
    console.log("sfdfde fsf");
    var auxIdUsuario = idUsuario.indexOf("%");
    var idUsuarios = idUsuario.substring(0, auxIdUsuario);   
    var contTable = idUsuario.substring(auxIdUsuario + 1, idUsuario.length);
    eliminarUsuarioGrupos(idUsuarios);
    var tableEliminar = $("#tableEliminarBd" + contTable);
    tableEliminar.empty();


}

