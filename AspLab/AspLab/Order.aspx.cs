using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class Order : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.PreviousPage == null)
				return;

			List<string> ordersList = Session["OrdersList"] as List<string> ?? new List<string>();
			foreach (var item in ordersList)
			{
				TableRow tableRow = new TableRow();
				tableRow.BorderWidth = 1;

				tableRow.Cells.Add(new TableCell() { Text = item, BorderWidth = 1 });
				this.Table1.Rows.Add(tableRow);
			}

		}
	}
}