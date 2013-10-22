using AspLab.App_Code;
using AspLab_AppCode;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class Order : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var dataBaseProvider = new DataBaseProvider();
			var userName = Request.Cookies["userName"];

			List<Product> ordersList = dataBaseProvider.LoadOrdersList(userName == null ? "NULL" : userName.Value);
			foreach (var item in ordersList)
			{
				TableRow tableRow = new TableRow();
				tableRow.BorderWidth = 1;

				tableRow.Cells.Add(new TableCell() { Text = item.ProductName, BorderWidth = 1 });
				this.Table1.Rows.Add(tableRow);
			}
		}
	}
}