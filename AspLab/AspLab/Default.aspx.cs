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
		private readonly string CategoriesPath = @"/App_Data/Categories.txt";
		private readonly string ProductsPath = @"/App_Data/Products.txt";

		protected void Page_Load(object sender, EventArgs e)
		{
			#region InitWizard

			Wizard wizard = new Wizard() { ID = "wizard" };
			wizard.StepNavigationTemplate = null;

			wizard.StepNextButtonStyle.CssClass = "hidden";
			wizard.StartNextButtonStyle.CssClass = "hidden";
			wizard.StepPreviousButtonStyle.CssClass = "hidden";
			wizard.FinishPreviousButtonStyle.CssClass = "hidden";
			wizard.FinishCompleteButtonStyle.CssClass = "hidden";

			if (this.LoadCategories(wizard))
				this.LoadProducts(wizard);

			this.PlaceHolder.Controls.Add(wizard);

			#endregion


		}

		private bool LoadCategories(Wizard wizard)
		{
			var pathToFile = Server.MapPath("~") + this.CategoriesPath;
			if (File.Exists(pathToFile))
			{
				var streamReader = new StreamReader(pathToFile);
				while (!streamReader.EndOfStream)
				{
					var wizardStep = new WizardStep()
					{
						ID = "Wizard" + wizard.WizardSteps.Count.ToString(),
						Title = streamReader.ReadLine()
					};
					wizard.WizardSteps.Add(wizardStep);
				}
				streamReader.Close();
				return true;
			}
			return false;
		}

		private bool LoadProducts(Wizard wizard)
		{
			var pathToFile = Server.MapPath("~") + this.ProductsPath;
			if (File.Exists(pathToFile))
			{
				var streamReader = new StreamReader(pathToFile);
				var productsList = new List<string>();
				while (!streamReader.EndOfStream)
					productsList.Add(streamReader.ReadLine());
				streamReader.Close();

				foreach (var product in productsList)
				{
					int pos = product.IndexOf('@');
					if (pos >= 0)
					{
						var category = product.Substring(0, pos);

						WizardStep wizardStep = null;
						foreach (WizardStep locWizardStep in wizard.WizardSteps)
							if (locWizardStep.Title == category)
								wizardStep = locWizardStep;

						if (wizardStep != null)
						{
							wizardStep.Controls.Add(new CheckBox() { Text = product.Substring(pos + 1, product.Length - pos - 1) });
							wizardStep.Controls.Add(new LiteralControl("<br/>"));
						}
						else
							return CustomErrorPage("Products file is invalid: unknown category");
					}
					else
						return CustomErrorPage("Products file is invalid: invalid markup");
				}

				return true;
			}
			return false;
		}

		private bool CustomErrorPage(string Message)
		{
			Session["CurrentError"] = Message;
			Server.Transfer("ApplicationError.aspx");
			return false;
		}
	}
}