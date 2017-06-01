<%@ Page Title="Horas Anuales Gerentes" Language="C#" MasterPageFile="~/Reportes/Layout.Master" AutoEventWireup="true" CodeBehind="HorasAnualesGerentes.aspx.cs" Inherits="Amezquita.ControlTiempos.Reportes.HorasAnualesGerentes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ContentPlaceHolderID="reportesBody" runat="server">
    <asp:UpdatePanel ChildrenAsTriggers="true" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-6">
                    <div class="control-group">
                        <label class="control-label">Gerentes</label>
                        <div class="controls">
                            <asp:DropDownList ID="GerentesDropDownList" runat="server" CssClass="form-control select2" DataSourceID="GerentesDataSource" DataTextField="Nombre" DataValueField="UsuarioId" AutoPostBack="True"></asp:DropDownList>
                            <asp:ObjectDataSource ID="GerentesDataSource" runat="server" SelectMethod="GetData" TypeName="Amezquita.ControlTiempos.Reportes.DataSets.GerentesDataSetTableAdapters.GerentesTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">Directores</label>
                        <div class="controls">
                            <asp:DropDownList ID="DirectoresDropDownList" runat="server" CssClass="form-control select2" DataSourceID="DirectoresDataSource" DataTextField="Nombre" DataValueField="UsuarioId"></asp:DropDownList>
                            <asp:ObjectDataSource ID="DirectoresDataSource" runat="server" SelectMethod="GetData" TypeName="Amezquita.ControlTiempos.Reportes.DataSets.DirectoresDataSetTableAdapters.DirectoresFiltroTableAdapter" OldValuesParameterFormatString="original_{0}">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="GerentesDropDownList" DbType="Guid" Name="GerenteId" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
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
                        <asp:Button runat="server" Text="Generar Reporte" OnClick="ActualizarReporte" />
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
