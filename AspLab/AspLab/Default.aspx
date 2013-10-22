<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspLab.Default" %>

<%@ Register Src="~/Controls/ProductsControl.ascx" TagPrefix="userControls" TagName="ProductsControl" %>
<%@ Register Src="~/Controls/CategoriesControl.ascx" TagPrefix="userControls" TagName="CategoriesControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Каталог</title>
</head>
<body>
	<form id="mainForm" runat="server">
		<div id="nameBlock" visible="false" runat="server">
			<b>Представьтесь пожалуйста,</b><input id="nameTextBox" runat="server" value="" />
		</div>
		<div id="cookiesBlock" visible="false" runat="server">
			<b>Добро пожаловать, </b>
			<label id="nameLabel" runat="server" />
		</div>
		<table>
			<tr>
				<td>
					<userControls:CategoriesControl runat="server" ID="CategoriesControl" />
				</td>
				<td valign="top">
					<userControls:ProductsControl runat="server" ID="ProductsControl" />
				</td>
			</tr>
		</table>
		<br />
		<asp:Button runat="server" Text="Корзина" PostBackUrl="~/Basket.aspx" />
	</form>
</body>
</html>
