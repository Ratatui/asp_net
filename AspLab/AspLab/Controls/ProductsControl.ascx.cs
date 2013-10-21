using AspLab.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class ProductsControl : System.Web.UI.UserControl
	{
		private List<Product> itemsSource;
		public List<Product> ItemsSource
		{
			get { return itemsSource; }
			set
			{
				itemsSource = value;
				this.SetCheckBoxList();
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.SetCheckBoxList();
		}

		private void SetCheckBoxList()
		{
			if (this.itemsSource == null)
				return;

			this.PlaceHolder.Controls.Clear();
			var orderList = Session["OrdersList"] as List<Product>;
			foreach (var item in this.itemsSource)
			{
				var checkBox = new CheckBox() { Text = item.ProductName, Checked = false, ID = item.ProductId.ToString(), AutoPostBack = true };
				if (orderList != null && orderList.Count(product => product.ProductId == int.Parse(checkBox.ID)) > 0)
					checkBox.Checked = true;
				checkBox.CheckedChanged += checkBox_CheckedChanged;
				this.PlaceHolder.Controls.Add(checkBox);
				this.PlaceHolder.Controls.Add(new LiteralControl("<br/>"));
			}
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			var checkBox = sender as CheckBox;
			List<Product> ordersList = Session["OrdersList"] as List<Product> ?? new List<Product>();

			if (checkBox.Checked)
				ordersList.Add(new Product() { ProductName = checkBox.Text, ProductId = int.Parse(checkBox.ID) });
			else
			{
				var pos = ordersList.FindIndex(item => item.ProductId == int.Parse(checkBox.ID));
				ordersList.RemoveAt(pos);
			}

			Session["OrdersList"] = ordersList;
		}
	}
}