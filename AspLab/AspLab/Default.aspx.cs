using AspLab_AppCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class Default : System.Web.UI.Page
	{
		private static DataBaseProvider sqlProvider = new DataBaseProvider();

		protected void Page_Load(object sender, EventArgs e)
		{
			
		
			ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
				new ScriptResourceDefinition
				{
					Path = "~/scripts/jquery-1.7.2.min.js",
					DebugPath = "~/scripts/jquery-1.7.2.min.js",
					CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.min.js",
					CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.4.1.js"
				});

			if (this.IsPostBack)
			{
				Validate();

				if (Request.Cookies["userName"] == null && !string.IsNullOrWhiteSpace(this.nameTextBox.Text))
				{
					var cookie = new HttpCookie("userName", this.nameTextBox.Text);
					cookie.Expires = DateTime.Now.AddMinutes(10); // for testing
					Response.Cookies.Add(cookie);
				}
				if (!IsValid)
				{
					return;
				}
			}

			#region Name from Cookies

			var userNameCookies = Request.Cookies["userName"];
			if (userNameCookies != null)
			{
				this.nameLabel.InnerText = userNameCookies.Value.ToString();
				this.cookiesBlock.Visible = true;
				this.nameBlock.Visible = false;
			}
			else
			{
				this.nameBlock.Visible = true;
				this.cookiesBlock.Visible = false;
			}

			#endregion

			#region DataBase Connection

			this.CategoriesControl.SqlProvider = sqlProvider;
			this.CategoriesControl.ProductsControl = this.ProductsControl;

			if (sqlProvider.CategoriesList.Count == 0)
				this.CategoriesControl.ItemsSource = sqlProvider.LoadCategoriesList();
			else
				this.CategoriesControl.ItemsSource = sqlProvider.CategoriesList;

			this.ProductsControl.ItemsSource = sqlProvider.ProductsList;

			#endregion
		}

		private bool CustomErrorPage(string Message)
		{
			Session["CurrentError"] = Message;
			Server.Transfer("ApplicationError.aspx");
			return false;
		}

		protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
		{

		}

		protected void Unnamed_Click(object sender, EventArgs e)
		{
			Validate();
			if (IsValid)
				Response.Redirect("~/Basket.aspx");
		}
	}
}