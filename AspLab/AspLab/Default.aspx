<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AspLab.Default" %>

<%@ Register Src="~/Controls/ProductsControl.ascx" TagPrefix="userControls" TagName="ProductsControl" %>
<%@ Register Src="~/Controls/CategoriesControl.ascx" TagPrefix="userControls" TagName="CategoriesControl" %>

<%--<%@ OutputCache Duration="300" VaryByControl="CategoriesControl" Location="ServerAndClient" %>--%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Catalog</title>
</head>
<body>
	<form id="mainForm" runat="server">
		<div id="nameBlock" visible="false" runat="server">
			<asp:ValidationSummary ID="ValidationSummary1" runat="server"
				HeaderText="There were errors on the page:" />
			<b>What is your name? </b><asp:TextBox ID="nameTextBox" runat="server" />
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
				ControlToValidate="nameTextBox"
				ErrorMessage="User name is required."> *
			</asp:RequiredFieldValidator>
		</div>
		<div id="cookiesBlock" visible="false" runat="server">
			<b>Hello, </b>
			<label id="nameLabel" runat="server" />
		</div>
		<asp:ScriptManager ID="ScriptManager1" runat="server" />
		<asp:UpdatePanel ID="Updatepanel1" runat="server">
			<ContentTemplate>
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
			</ContentTemplate>
		</asp:UpdatePanel>
		<br />

		<asp:UpdateProgress ID="UpdateProgress1" runat="server">
			<ProgressTemplate>
				Getting products...
			</ProgressTemplate>
		</asp:UpdateProgress>
		<asp:Button runat="server" Text="Basket" OnClick="Unnamed_Click"/>
	</form>
</body>
</html>
