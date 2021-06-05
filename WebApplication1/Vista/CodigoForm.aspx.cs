using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Vista
{
    public partial class CodigoForm : System.Web.UI.Page
    {
        #region Properties
        private IList<CodeRuleGrid> codeRules;
        public IList<CodeRuleGrid> CodeRules
        {
            get { return (IList<CodeRuleGrid>)ViewState["codeRules"]; }
            set { ViewState["codeRules"] = value; }
        }

        private int codeRuleValueType;
        public int CodeRuleValueType
        {
            get { return (int)ViewState["coderulevaluetype"]; }
            set { ViewState["coderulevaluetype"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        #region UI
        #endregion

        #region Implementation
        #endregion

        protected void Wizard1_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
        {
            e.Cancel = true;
        }
    }

    [Serializable]
    public class CodeRuleGrid
    {
        public Guid Uid { get; set; }
        public int Position { get; set; }
        public ComparisonOperator Operator { get; set; }
        public string OperatorText { get; set; }
        public CodeRuleValueType ValueType { get; set; }
        public string ValueTypeText { get; set; }
        public string Value { get; set; }
        public string ValueView { get; set; }
        public LogicalOperator Union { get; set; }
        public string UnionText { get; set; }
    }
}