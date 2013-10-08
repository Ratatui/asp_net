﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspLab
{
	public partial class ApplicationError : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ErrorLabel.Text = Session["CurrentError"] != null ? Session["CurrentError"].ToString() : "Unknown error";
		}
	}
}