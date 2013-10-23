using AspLab_AppCode;
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
			set
			{
				selectedItem = value;
				ProductsControl.ItemsSource = SqlProvider.LoadProductsList(value);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.SetCategoriesList();
		}

		public DataBaseProvider SqlProvider { get; set; }
		public ProductsControl ProductsControl { get; set; }

		private void SetCategoriesList()
		{
			if (this.itemsSource == null)
				return;

			this.PlaceHolder.Controls.Clear();
			foreach (var item in this.itemsSource)
			{
				var button = new Button();
				button.Text = item;
				button.Click += button_Click;

				this.PlaceHolder.Controls.Add(button);
				this.PlaceHolder.Controls.Add(new LiteralControl("<br />"));
			}
		}

		void button_Click(object sender, EventArgs e)
		{
			System.Threading.Thread.Sleep(3000);

			foreach (Control button in this.Controls)
			{
				if (button is Button)
					(button as Button).BorderColor = System.Drawing.Color.Transparent;
			}

			Button Button = sender as Button;
			Button.BorderColor = System.Drawing.Color.Red;
			SelectedItem = Button.Text;
		}
	}
}