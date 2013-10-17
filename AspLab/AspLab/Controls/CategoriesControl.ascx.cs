using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class CategoriesControl : System.Web.UI.UserControl
	{
		private List<string> itemsSource;
		public List<string> ItemsSource
		{
			get { return itemsSource; }
			set
			{
				itemsSource = value;
				this.SetCategoriesList();
			}
		}

		private string selectedItem;
		public string SelectedItem
		{
			get
			{
				if (selectedItem != null)
					return selectedItem;
				else if (this.itemsSource != null && this.itemsSource.Count > 0)
					return this.itemsSource[0];
				else
					return string.Empty;
			}
			set { selectedItem = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.SetCategoriesList();
		}

		private void SetCategoriesList()
		{
			if (this.itemsSource == null)
				return;

			this.PlaceHolder.Controls.Clear();
			foreach (var item in this.itemsSource)
			{
				var button = new Button();
				button.Text = item;
				button.PostBackUrl = "~/Default.aspx?CategoryName=" + item;
				if (item == this.SelectedItem)
					button.BorderColor = System.Drawing.Color.Red;

				this.PlaceHolder.Controls.Add(button);
				this.PlaceHolder.Controls.Add(new LiteralControl("<br />"));
			}
		}
	}
}