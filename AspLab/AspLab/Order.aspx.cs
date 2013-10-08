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

			var wizard = (Wizard)this.PreviousPage.FindControl("mainForm").FindControl("PlaceHolder").FindControl("wizard");
			foreach (WizardStep wizardStep in wizard.WizardSteps)
			{
				foreach (var control in wizardStep.Controls)
				{
					if (control is CheckBox)
					{
						var checkBox = control as CheckBox;
						if (checkBox.Checked)
						{
							TableRow tableRow = new TableRow();
							tableRow.BorderWidth = 1;
							tableRow.Cells.Add(new TableCell() { Text = wizardStep.Title, BorderWidth = 1 });
							tableRow.Cells.Add(new TableCell() { Text = checkBox.Text, BorderWidth = 1 });
							this.Table1.Rows.Add(tableRow);
						}
					}
				}
			}
		}
	}
}