using AspLab.App_Code;
using AspLab_AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class Basket : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			List<Product> ordersList = Session["OrdersList"] as List<Product> ?? new List<Product>();
			foreach (var item in ordersList)
			{
				TableRow tableRow = new TableRow();
				tableRow.BorderWidth = 1;

				tableRow.Cells.Add(new TableCell() { Text = item.ProductName, BorderWidth = 1 });
				this.Table1.Rows.Add(tableRow);
			}
		}

		protected void CreateOrder_Click(object sender, EventArgs e)
		{
			var sqlProvider = new DataBaseProvider();
			var userName = Request.Cookies["userName"];
			sqlProvider.CreateOrderLines(Session["OrdersList"] as List<Product> ?? new List<Product>(), userName == null ? null : userName.Value);
			Response.Redirect("~/Order.aspx");
		}
	}
}