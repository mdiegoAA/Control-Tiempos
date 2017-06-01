<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoricoUsuario.aspx.cs" Inherits="Amezquita.ControlTiempos.ReportesAmezquita.HistoricoUsuario" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.1/css/font-awesome.css" rel="stylesheet">   
    <script src="moment.min.js"></script>   
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome.css" rel="stylesheet" />
    <link href="../Content/fuelux.css" rel="stylesheet" />
    <link href="../Content/blog.css" rel="stylesheet" />
    <link href="../Content/style.css" rel="stylesheet" />
    <link href="../Content/select2.min.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../Scripts/modernizr-2.8.3.js"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js"></script>  
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="bower_components/datatables/media/js/jquery.dataTables.min.js"></script>
    <script src="bower_components/datatables-plugins/integration/bootstrap/3/dataTables.bootstrap.min.js"></script>
    <script src="jquery.table2excel.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/css/bootstrap-select.min.css">

<!-- Latest compiled and minified JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/bootstrap-select.min.js"></script>

    <!-- (Optional) Latest compiled and minified JavaScript translation files -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.1/js/i18n/defaults-*.min.js"></script>

    <script type="text/javascript" src="HistoricoUsuario.js"></script>
  
    <script>
         $(function () {
             $("#datepicker").datepicker({               
                 
                     dateFormat: 'MM yy',
                     changeMonth: true,
                     changeYear: true,
                     showButtonPanel: true,

                     onClose: function(dateText, inst) {
                         var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                         var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                         $(this).val($.datepicker.formatDate('MM yy', new Date(year, month, 1)));
                     }
                 });

                     $(".monthPicker").focus(function () {
                         $(".ui-datepicker-calendar").hide();
                         $("#ui-datepicker-div").position({
                             my: "center top",
                             at: "center bottom",
                             of: $(this)
                         });
                     });               
             });
         $(function () {
             $('#datetimepicker1').datetimepicker();
         });
         $(function () {
             $('#datetimepicker4').datetimepicker();
         });

         $(function () {
             $("#datepicker2").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 changeDay: true,
                 showButtonPanel: true,
                 dateFormat: 'yy-mm-dd',
                 onClose: function (dateText, inst) {
                     var day = $("#ui-datepicker-div .ui-datepicker-day :selected").val();
                     var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                     var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                     $('#startDate').datepicker({ defaultDate: -30 });
                     $('#endDate').datepicker({ defaultDate: new Date() });
                 }
             });
         });


    </script>
</head>
    

  

<!-- Latest compiled and minified JavaScript -->


    
  
    
  


<body class="fuelux">
    <div class="header-background-color">
        <div class="blog-header">
            <div class="container ">
                <div class="header-bg-image">
                    <div class="row">
                        <a href="#">
                            <img src="http://www.amezquita.com.co/wp-content/uploads/2015/03/Logo-en-Policrom--a-e1443198604156.png" alt="" />
                        </a>
                        <a class="second-logo" target="_blank" href="http://www.pkf.com/">
                            <img src="http://www.amezquita.com.co/wp-content/uploads/2015/03/pkf-logo.jpg" alt="" />
                        </a>
                    </div>
                </div>
               
            </div>
        </div>
        <nav class="navbar special-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
                         <ul class="nav navbar-nav navbar-right">
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><asp:Label ID="UsuarioNombre" runat="server"></asp:Label> <span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#" id="Prueba">Salir</a></li>
                                </ul>
                            </li>
                        </ul>

                </div>
                 <div id="navbar" class="navbar-collapse collapse">
                        <asp:SiteMapDataSource ShowStartingNode="false" runat="server" ID="siteMapDataSource" SiteMapProvider="AmezquitaXmlSiteMapProvider" />
                        <ul class="nav navbar-nav">
                            <asp:Repeater runat="server" DataSourceID="siteMapDataSource">
                                <ItemTemplate>
                                    <li class="dropdown">
                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><%#Eval("Title") %><span class="caret"></span></a>
                                        <asp:SiteMapDataSource ShowStartingNode="false" ID="hijosSiteMapDataSource" StartFromCurrentNode="false" StartingNodeUrl='<%#Eval("Url") %>' runat="server" SiteMapProvider="AmezquitaXmlSiteMapProvider" />
                                        <asp:Repeater runat="server" DataSourceID="hijosSiteMapDataSource">
                                            <HeaderTemplate>
                                                <ul class="dropdown-menu">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <li><a href="<%#ResolverUrl(Eval("Url")) %>"><%#Eval("Title") %></a></li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
            </div>
        </nav>
    </div>


     <div id="element" class="introLoading"></div>
  
    
    <div class="container">
    <div class="row">
      
        <div class="col-md-1">
        </div>
        <div class="col-md-4">          
            <div class="form-group">
                

              <%--  <select class="form-control" id="ListadoClientes">
                </select>--%>
                <div class="form-inline">
                    <div class="input-group">
                        
                       <div id="ListadoDirectores">

                       </div>
                    </div>
                </div>
                  
            </div>
         </div> 
    </div> 
        <div class="row">
         <div class="col-md-1">
        </div>       
          <div class="col-md-3">            
          <div class="form-group">
                <label>Fecha Inicial</label>
              <input type="text" placeholder="Fecha Archivo" id="datepicker" class="monthPicker"/>              
            </div>
         </div>  
       
        
         <div class="col-md-2"> 
               <label style="color:white">Servicio</label>  
              <button type="button" id="Consultar" class="btn btn-primary btn-xs form-control">Consultar</button>      
          </div>
        <div class="col-md-2"> 
                <label style="color:white">Servicio</label>  
              <button type="button" id="Exportar" class="btn btn-primary btn-xs form-control">Exportar</button>      
          </div>
        <div class="col-md-2" style="display:none">  
               <label style="color:white">Consultar</label>  
              <button type="button" id="Reporte" class="btn btn-primary btn-xs form-control">Reporte</button>      
          </div>
    </div>
  
     
     </div>
   
    <div class="container" id="info">
       <div class="row">
             <div class="col-md-1">
        </div> 
          <div class="col-md-10">
            <div><p class="bg-primary" id='NameProyecto'></p></div>
          </div>
             <div class="col-md-4">
            <%-- <div><strong id='servicio'> </strong></div>--%>
          </div>
     </div> 
    </div>
   
  
   <div class="container">
          <div class="row">
             <div class="col-md-1"></div> 
       
        <div class="row">
                    <div class="">
                        <div class="col-lg-12 container">
                            <div class="panel panel-default">
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="dataTable_wrapper">
                                        <div class="table-responsive">
                                           <table class="table table-striped table-bordered table-hover" id="dataTables" style="font-size:small">
                                             <thead>
                                                 <tr id="cabezaTable">
                                                     <td>Nombre del Proyecto </td>
                                                     <td>1</td>
                                                     <td>2</td>
                                                     <td>3</td>
                                                     <td>4</td>
                                                     <td>5</td>
                                                     <td>6</td>
                                                     <td>7</td>
                                                     <td>8</td>
                                                     <td>9</td>
                                                     <td>10</td>
                                                     <td>11</td>
                                                     <td>12</td>
                                                     <td>13</td>
                                                     <td>14</td>
                                                     <td>15</td>
                                                     <td>16</td>
                                                     <td>17</td>
                                                     <td>18</td>
                                                     <td>19</td>
                                                     <td>20</td>
                                                     <td>21</td>
                                                     <td>22</td>
                                                     <td>23</td>
                                                     <td>24</td>
                                                     <td>25</td>
                                                     <td>26</td>
                                                     <td>27</td>
                                                     <td>28</td>
                                                     <td>29</td>
                                                     <td>30</td>
                                                     <td>31</td>
                                                    <td>Total</td>
                                                 </tr>
                                             </thead>
                                             <tbody id="table">

                                             </tbody>
                                                
                                            </table>
                                         
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
         <div class="row" style="display:none" >
                    <div class="">
                        <div>
                            <div>
                            
                                           <table   id="dataTablesReport" >
                                             <thead>
                                                 <tr>
                                                     <td colspan ='9' rowspan='6'><img  src='http://www.amezquita.com.co/wp-content/uploads/2015/03/Logo-en-Policrom--a-e1443198604156.png' width='100px' height='100px'/></td>
                                                    
                                                    
                                                    </tr>
                                                 <tr>
                                                      <td></td>
                                                       <td></td>
                                                       <td></td>
                                                       <td></td>
                                                     
                                                       <th colspan="6">Reporte mensual</th>
                                                      <td colspan="6"><div id="mesReporte"></div></td>

                                                 </tr>
                                                 <tr>
                                                     <td></td>
                                                       <td></td>
                                                       <td></td>
                                                       <td></td>
                                                     <th colspan="6">Usuario</th>
                                                     <td colspan="6"><div id="nombreUsuario" runat="server"></div></td>

                                                 </tr>
                                                 <tr>

                                                 </tr>
                                                 <tr>

                                                 </tr>
                                                 <tr>

                                                 </tr>
                                                  <tr>

                                                 </tr>
                                                
                                                
                                                 <tr id="cabezaTables">
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>Nombre del Proyecto </th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>1</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>2</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>3</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>4</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>5</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>6</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>7</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>8</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>9</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>10</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>11</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>12</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>13</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>14</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>15</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>16</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>17</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>18</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>19</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>20</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>21</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>22</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>23</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>24</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>25</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>26</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>27</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>28</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>29</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>30</th>
                                                     <th style=' background-color:#CDCDCD;  border:1px solid  black'>31</th>
                                                    <th style=' background-color:#CDCDCD;  border:1px solid  black'>Total</th>
                                                 </tr>
                                             </thead>
                                             <tbody id="tables">

                                             </tbody>
                                                
                                            </table>
                                         
                                        </div>
                                  
                        </div>
                    </div>
                </div>
     </div> 
    </div>


    <div id="pp"></div>
  

   
  <div class="modal fade bs-example-modal-lg" id="modalUsuarioCargue" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">>
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Reporte Tiempo</h4>
      </div>
      <div class="modal-body">

     <div> 
   <div><label>Proyectos</label></div>
       <select class="selectpicker" id="selectProyectos" style="width:auto" data-live-search="true">

</select>

    </div>
            <label id="CarguelabelErrorProyectos" style="color:red;display:none">Debe seleccionar un Proyecto obligatoriamente</label>
    <br />
      <div> 
   <div><label>Servicios</label></div>
       <select class="selectpicker" id="selecServicios" style="width:auto" data-live-search="true">

</select>
          </div>
            <label id="CarguelabelErrorServicio" style="color:red;display:none">Debe seleccionar un Servicio obligatoriamente</label>
            <br />
      <div> 
   <div><label>Actividades</label></div>
       <select class="selectpicker" id="selecActividades" style="width:auto" data-live-search="true">

</select>
</div>
             <label id="CarguelabelErrorActividad" style="color:red;display:none">Debe seleccionar una actividad obligatoriamente</label>
        <br />
        <div><label>Fecha</label></div>
        <div class="container">
        <div class="row">
        <div class='col-sm-10'>
            <div class="form-group">
                <div class='input-group date' id='datetimepicker4'>
                    <input type='text' id="diaCargar" disabled="disabled" class="form-control"/>
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
        </div>
    </div>
</div>
          <br />
         <div><label>Horas</label></div>
          <div class="container">
    <div class="row">
        <div class='col-sm-10'>
            <div class="form-group">
                <div class='input-group date' id='datetimepicker3'>
                    <input type='text' id="horas"   class="form-control" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-time"></span>
                    </span>
                </div>
            </div>
        </div>
     
    </div>
</div>
             <label id="labelError" style="color:red ; display:none"></label>
            <label id="CargarlabelErrorHora" style="color:red ; display:none">Debe seleccionar una hora obligatoriamente</label>
          <br />
         <div class="form-group">
  <label for="comment">Comentarios:</label>
  <textarea class="form-control"  rows="4" id="comment"></textarea>
</div>
          <br />
          <div>
              <button type="button" id="CargarHoras" class="btn btn-primary">Cargar Horas</button>
          </div>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>      
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
    
     <div class="modal fade bs-example-modal-lg" id="modalUsuarioCargueModificar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">>
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Modificar Tiempo</h4>
      </div>
      <div class="modal-body">
        <div id="ModificarCargueTiempo"></div>
        <div style="visibility:hidden">vxc</div>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>      
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div>

  <div class="modal fade" id="ModalErrorCargue" tabindex="-1" role="dialog">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Amezquita - Control Tiempos</h4>
      </div>
      <div class="modal-body">
        <p>No  tiene permisos para modificar tiempos en este dia </p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>     
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->


    <div class="blog-footer dark-blue">
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-6 footer-1 ">
                    <div class="footer-module">
                        <div id="black-studio-tinymce-4" class="widget widget_black_studio_tinymce">
                            <div class="textwidget">
                                <table style="border: none; min-height: 8em;" width="100%">
                                    <tbody>
                                        <tr>
                                            <th style="vertical-align: middle; border: none;">
                                                <a href="http://www.pkf.com" target="_blank">
                                                    <img src="http://mateo.colombiahosting.com.co/~amezqu/wp-content/uploads/2015/03/pkf.png" alt="" />
                                                </a>
                                            </th>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 footer-2 ">
                    <div class="footer-module">
                        <div id="black-studio-tinymce-5" class="widget widget_black_studio_tinymce">
                            <div class="textwidget">
                                <p style="text-align: right;">
                                    <a href="http://www.amezquita.com.co/legales/">Legales </a>
                                </p>
                                <p style="text-align: right;">
                                    <a href="http://www.amezquita.com.co/mapa-del-sitio/">Mapa del sitio </a>
                                </p>
                                <p style="text-align: right;">
                                    <a href="http://www.amezquita.com.co/habeas-data/" target="_blank">Protección de Datos</a><a href="http://www.amezquita.com.co/wp-content/uploads/2015/03/Politica-para-el-tratamiento-de-la-informacion-relacionada-con-datos-personales-HABEAS-DATA.pdf" target="_blank"> </a>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix visible-xs visible-sm col-xs-12 ">
                    <hr />
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 footer-3">
                    <div class="footer-module">
                        <div id="black-studio-tinymce-3" class="widget widget_black_studio_tinymce">
                            <div class="textwidget">
                                <p style="text-align: right;">
                                    BOGOTÁ:<br />
                                    CALLE 37<br />
                                    No. 24 - 28<br />
                                    PBX: (1) 208 75 00<br />
                                    amezquita@amezquita.com.co
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 footer-4">
                    <div class="footer-module">
                        <div id="black-studio-tinymce-2" class="widget widget_black_studio_tinymce">
                            <div class="textwidget">
                                <p style="text-align: right;">
                                    MEDELLÍN:<br />
                                    CARRERA 43A No.1-50 Piso 6<br />
                                    San Fernando Plaza.<br />
                                    Torre Protección 1<br />
                                    PBX: (2) 605 27 57<br />
                                    medellín@amezquita.com.co
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <p style="text-align: center;">
            <a href="#">Arriba</a>
        </p>
    </div>
    <div class="atributions dark-blue">
        <div class="container">
            <div class="col-xs-12">
                <p style="text-align: center;"><span><strong>Amézquita &amp; Cía</strong>.<strong> </strong>es una firma miembro de PKF International Limited, red de firmas legalmente Independientes. Ninguna de las otras firmas miembro ni PKF International Limited son responsables o aceptan responsabilidad alguna por el trabajo o asesoramiento prestado por Amézquita &amp; Cía.</span></p>
                <p style="text-align: center;"><span><strong>Amézquita &amp; Cía</strong>. is a member firm of the International Limited network of legally independent firms. Neither the other member firms nor PKF International Limited are responsable or accept liability for the work or advice which Amézquita &amp; Cía. provides to its clients.</span></p>
                <p style="text-align: center;"></p>
            </div>
        </div>
    </div>



 
</body>
     
</html>
