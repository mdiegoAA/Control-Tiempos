<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientesServicios.aspx.cs" Inherits="Amezquita.ControlTiempos.ReportesAmezquita.ClientesServicios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />



    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Control de Tiempos</title>


   

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

  
    <script type="text/javascript" src="ClientesServicios.js"></script>

     <script>
         $(function () {
             $("#datepicker").datepicker({
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

<body class="fuelux" runat="server">
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
                                    <li><a href="#" id="salir" onclick="pruebaclick" runat="server">Salir</a></li>
                                </ul>
                            </li>
                        </ul>

                </div>
                <div id="navbar" class="navbar-collapse collapse">
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
       
          <div class="col-md-10">
               <div class="panel panel-primary">
                        <div class="panel-heading">
                           Tabla Proyectos
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <div runat="server" id="tablesRsult"></div>
                            </div>
                            <!-- /.table-responsive -->
                       
                        </div>
                        <!-- /.panel-body -->
                    </div>
          </div>
     </div> 
    </div>
  

    <div id="pp"></div>
    <div class="modal fade bs-example-modal-lg" id="ModalCalendario" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel"></h4>
      </div>
      <div class="modal-body">
       <div class="row">
          <div class="col-md-12">
              <div class="row">
        <div class="col-md-1">
        </div>
                <div class="col-lg-10">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                           Tabla Proyectos
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <div id="dd" runat="server"></div>
                            </div>
                            <!-- /.table-responsive -->
                       
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
          </div>
     </div> 
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
 </div>
</div>

    
    <div class="modal fade bs-example-modal-lg" id="ModalServicios" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div class="modal-dialog modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="H1"></h4>
      </div>
      <div class="modal-body">
       <div class="row">
          <div class="col-md-12">
              <div class="row">
        <div class="col-md-1">
        </div>
                <div class="col-lg-10">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                          <div id="textProyectoServicio"></div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="dataTable_wrapper">
                                <div id="TextServicios"></div>
                            </div>
                            <!-- /.table-responsive -->
                       
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
          </div>
     </div> 
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
 </div>
</div>

    <div class="modal fade" tabindex="-1" id="Modaltree" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Modal title</h4>
      </div>
      <div class="modal-body">

        
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

    <div class="modal fade bs-example-modal-lg" id="modalReporte" tabindex="-1" role="dialog">
   <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header panel panel-default">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="TitleReporte"></h4>
      </div>
      <div class="modal-body">
       <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
        
</div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
       
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

  
    <div class="modal fade" id="modalRsult" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
  <div  class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Reporte</h4>
      </div>
      <div class="modal-body">
          <div class="row">
                    <div class="">
                        <div class="col-lg-12 container">
                            <div class="panel panel-default">
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="dataTable_wrapper">
                                        <div class="table-responsive">
                                         <table border="1" class='table table-striped table-bordered table-hover' style="font-size:x-small" id='ReporteMensual'>
                                           
                                                                 
                                   
                                            </table>
                                         
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
     
      </div>
      <div class="modal-footer">
       
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
</body>






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

