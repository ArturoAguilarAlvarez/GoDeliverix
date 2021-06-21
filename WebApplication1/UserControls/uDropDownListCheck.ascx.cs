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

        public void SetDefault(string text, Guid value)
        {
            this.uDdl.Items.Clear();
            this.uDdl.Items.Add(new ListItem() { Value = value.ToString(), Text = text });
        }

        protected void uChk_CheckedChanged(object sender, EventArgs e)
        {
            uDdl.Enabled = uChk.Checked;

            if (uDdl.Items.Count > 0)
            {
                if (uChk.Checked)
                {
                    if (uDdl.Items[0].Value != Guid.Empty.ToString())
                    {
                        uDdl.SelectedIndex = 0;
                    }
                    else
                    {
                        if (uDdl.Items.Count > 1)
                            uDdl.SelectedIndex = 1;
                    }
                }
                else
                {
                    uDdl.SelectedIndex = 0;
                }

                if (this.SelectedIndexChanged != null)
                    this.SelectedIndexChanged(sender, e);
            }
        }

        protected void uDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(sender, e);
        }

        public string FindTextByValue(string value)
        {
            ListItem item = uDdl.Items.FindByValue(value);
            return item == null ? string.Empty : item.Text;
        }
    }
}