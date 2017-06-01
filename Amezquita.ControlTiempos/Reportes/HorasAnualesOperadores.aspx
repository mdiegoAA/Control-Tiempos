<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Reportes/Layout.Master" CodeBehind="HorasAnualesOperadores.aspx.cs" Inherits="Amezquita.ControlTiempos.Reportes.HorasAnualesOperadores" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ContentPlaceHolderID="reportesBody" runat="server">
    <asp:UpdatePanel ChildrenAsTriggers="true" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-6">
                    <div class="control-group">
                        <label class="control-label">Clientes</label>
                        <div class="controls">
                            <asp:DropDownList ID="ClientesDropDownList" runat="server" CssClass="form-control select2" DataSourceID="ClientesDataSource" DataTextField="NombreCliente" DataValueField="ClienteId"></asp:DropDownList>
                            <asp:ObjectDataSource ID="ClientesDataSource" runat="server" SelectMethod="GetData" TypeName="Amezquita.ControlTiempos.Reportes.DataSets.ClientesDataSetTableAdapters.ClientesTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">Directores</label>
                        <div class="controls">
                            <asp:DropDownList ID="DirectoresDropDownList" runat="server" CssClass="form-control select2" DataSourceID="DirectoresDataSource" DataTextField="Nombre" DataValueField="UsuarioId"></asp:DropDownList>
                            <asp:ObjectDataSource ID="DirectoresDataSource" runat="server" SelectMethod="GetData" TypeName="Amezquita.ControlTiempos.Reportes.DataSets.DirectoresDataSetTableAdapters.DirectoresTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">Fecha Inicio</label>
                        <div class="controls">
                            <div class='input-group date datetimepicker'>
                                <asp:TextBox ID="FechaInicioTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="FechaInicioTextBox"
                                ErrorMessage="Por favor seleccione una fecha."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">Fecha Fin</label>
                        <div class="controls">
                            <div class='input-group date datetimepicker'>
                                <asp:TextBox ID="FechaFinTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="FechaFinTextBox"
                                ErrorMessage="Por favor seleccione una fecha."
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="control-group">
                        <asp:Button runat="server" Text="Generar Reporte" OnClick="GenerarReporte" />
                    </div>
                </div>
            </div>
            <rsweb:ReportViewer ID="Report" Height="100%" Width="100%" runat="server">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        ResizeReport();
        function ResizeReport() {
            var viewer = document.getElementById("<%= Report.ClientID %>");
                var htmlheight = document.documentElement.clientHeight;
                viewer.style.height = (htmlheight - 30) + "px";
            }
            window.onresize = function resize() { ResizeReport(); };
    </script>
</asp:Content>
