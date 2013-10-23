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
			if (this.IsPostBack)
			{
				if (Request.Cookies["userName"] == null && !string.IsNullOrWhiteSpace(this.nameTextBox.Value))
				{
					var cookie = new HttpCookie("userName", this.nameTextBox.Value);
					cookie.Expires = DateTime.Now.AddMinutes(10); // for testing
					Response.Cookies.Add(cookie);
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
	}
}