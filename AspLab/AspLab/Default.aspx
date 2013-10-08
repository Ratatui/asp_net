<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspLab.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Каталог</title>
</head>
<body>
	<style type="text/css">
		.hidden {
			display: none;
		}

		.visible {
			display: inline;
		}
	</style>

	<form id="mainForm" runat="server">
		<div>
			<asp:PlaceHolder ID="PlaceHolder" runat="server" />
		</div>
		<div>
			<br />
			<asp:Button runat="server" Text="Заказать" PostBackUrl="~/Order.aspx" />
		</div>
	</form>
</body>
</html>
