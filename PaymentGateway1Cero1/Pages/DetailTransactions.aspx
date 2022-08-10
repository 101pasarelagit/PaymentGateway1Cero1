<%@ Page Title="" Language="C#" MasterPageFile="~/SitePayments.Master" AutoEventWireup="true" CodeBehind="DetailTransactions.aspx.cs" Inherits="PaymentGateway1Cero1.Pages.DetailTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 process">
                <div class="col-lg12 col-md-12 sideber pt-5 pt-lg-0">
                    <div class="widget-area">
                        <div class="widget-area">
                            <h4 class="widget-title">Historial Transaccional</h4>
                            <p>Por favor ingresa el número de identificación. </p>
                            <%-- <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="inputEmail4">Email</label>
                                    <input type="email" class="form-control" id="inputEmail4" placeholder="Email">
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="inputPassword4">Password</label>
                                    <input type="password" class="form-control" id="inputPassword4" placeholder="Password">
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <label for="txtIdentificación">* Identificación</label>
                                <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdentificacion" ErrorMessage="* Campo Obligatorio"></asp:RequiredFieldValidator>
                            </div>
                            <asp:Button ID="btnConsultar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" runat="server" Text="Consultar Transacciones" />
                        </div>
                        <div class="widget-area" id="dvContent" visible="false" runat="server">
                            <h4 class="widget-title">Transacciones en Línea</h4>
                            <p>A continiación se detalla las transacciones realizadas.</p>
                            <asp:GridView ID="GridTransac" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" runat="server">
                                <Columns>
                                    <asp:BoundField DataField="Tramite" HeaderText="Trámite" SortExpression="Tramite" ReadOnly="True" />
                                    <asp:BoundField DataField="Factura" HeaderText="Factura" SortExpression="Factura" ReadOnly="True" />
                                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ReadOnly="True" />
                                    <asp:BoundField DataField="CUS" HeaderText="CUS" SortExpression="CUS" />
                                    <asp:BoundField DataField="FechaTransaccion" HeaderText="Fecha Transacción" SortExpression="FechaTransaccion" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="/Content/js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#' + '<%= GridTransac.ClientID %>').DataTable({
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                }],
                'order': [1, 'asc']
            });

            $('#select-all').on('click', function () {
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            $('#example tbody').on('change', 'input[type="checkbox"]', function () {
                if (!this.checked) {
                    var el = $('#select-all').get(0);
                    if (el && el.checked && ('indeterminate' in el)) {
                        el.indeterminate = true;
                    }
                }
            });
        });
    </script>
</asp:Content>
