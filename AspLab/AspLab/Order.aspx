﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="AspLab.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Заказ</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:Table ID="Table1" runat="server" BorderWidth="1">
				<asp:TableHeaderRow>
					<asp:TableHeaderCell Width="100">Категория</asp:TableHeaderCell>
					<asp:TableHeaderCell Width="300">Товар</asp:TableHeaderCell>
				</asp:TableHeaderRow>
			</asp:Table>
		</div>
	</form>
</body>
</html>