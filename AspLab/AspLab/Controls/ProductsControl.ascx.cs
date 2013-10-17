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
		private List<string> itemsSource;
		public List<string> ItemsSource
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
			var orderList = Session["OrdersList"] as List<string>;
			foreach (var item in this.itemsSource)
			{
				var checkBox = new CheckBox() { Text = item, Checked = false, ID = item, AutoPostBack = true };
				if (orderList != null && orderList.Contains(item))
					checkBox.Checked = true;
				checkBox.CheckedChanged += checkBox_CheckedChanged;
				this.PlaceHolder.Controls.Add(checkBox);
				this.PlaceHolder.Controls.Add(new LiteralControl("<br/>"));
			}
		}

		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			var checkBox = sender as CheckBox;
			List<string> ordersList = Session["OrdersList"] as List<string> ?? new List<string>();

			if (checkBox.Checked)
				ordersList.Add(checkBox.Text);
			else
				ordersList.Remove(checkBox.Text);

			Session["OrdersList"] = ordersList;
		}
	}
}