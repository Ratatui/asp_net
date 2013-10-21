<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="AspLab.Basket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Заказ</title>
</head>
<body>
	<form id="form1" runat="server" method="post">
		<div>
			<asp:Table ID="Table1" runat="server" BorderWidth="1">
				<asp:TableHeaderRow>
					<asp:TableHeaderCell Width="300">Товар</asp:TableHeaderCell>
				</asp:TableHeaderRow>
			</asp:Table>
		</div>
		<asp:Button ID="CreateOrder" runat="server" OnClick="CreateOrder_Click" Text="Заказать" UseSubmitBehavior="true" />
	</form>
</body>
</html>
