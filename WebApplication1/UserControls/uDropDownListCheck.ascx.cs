using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.UserControls
{
    public partial class uDropDownListCheck : System.Web.UI.UserControl
    {
        public string Label
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public bool IsEnabled { get { return this.uDdl.Enabled; } }

        public string SelectedValue
        {
            get => this.uDdl.SelectedValue;
            set => this.uDdl.SelectedValue = value;
        }

        [Browsable(true)]
        [Description("Invoked when user clicks dropdownlist")]
        public event EventHandler SelectedIndexChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uDdl.Enabled = false;
            }
        }

        public void SetDataSource(object data, string valueField, string textField)
        {
            this.uDdl.DataSource = data;
            this.uDdl.DataValueField = valueField;
            this.uDdl.DataTextField = textField;
            this.uDdl.DataBind();
        }

        protected void uChk_CheckedChanged(object sender, EventArgs e)
        {
            uDdl.Enabled = uChk.Checked;
        }

        protected void uDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(sender, e);
        }
    }
}