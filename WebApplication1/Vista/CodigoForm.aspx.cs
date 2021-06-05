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
                // Set default values
                ddlRewardType.SelectedValue = "0";
                ddlCodeExpirationType.SelectedValue = "0";

                ddlValueType.Enabled = false;
                txtValue.Enabled = false;

                pnlActivationNumber.Visible = false;
                pnlDaysBeforeActivation.Visible = false;
                pnlExpirationDate.Visible = false;

                txtStartAt.Text = DateTime.Now.ToString("yyyy-MM-dd");

                InitCodeRuleFields();
            }
        }

        #region UI
        protected void ddlRewardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataAccess.Enum.CodeRewardType rewardType = (DataAccess.Enum.CodeRewardType)int.Parse(ddlRewardType.SelectedValue);

            switch (rewardType)
            {
                case DataAccess.Enum.CodeRewardType.None:
                    ClearValueField();
                    ClearValueTypeField();
                    break;
                case DataAccess.Enum.CodeRewardType.WalletAmount:
                    ddlValueType.Enabled = true;
                    txtValue.Enabled = true;
                    break;
                case DataAccess.Enum.CodeRewardType.FreeDelivery:
                    ClearValueField();
                    ClearValueTypeField();
                    break;
                case DataAccess.Enum.CodeRewardType.Discount:
                    ddlValueType.Enabled = true;
                    txtValue.Enabled = true;
                    break;
                case DataAccess.Enum.CodeRewardType.DeliveryDiscount:
                    ddlValueType.Enabled = true;
                    txtValue.Enabled = true;
                    break;
                case DataAccess.Enum.CodeRewardType.SubtotalRefund:
                    ddlValueType.Enabled = true;
                    txtValue.Enabled = true;
                    break;
                case DataAccess.Enum.CodeRewardType.DeliveryRefund:
                    ddlValueType.Enabled = true;
                    txtValue.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        protected void ddlValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataAccess.Enum.CodeRewardValueType type = (DataAccess.Enum.CodeRewardValueType)int.Parse(ddlValueType.SelectedValue);
            lblValueType.Text = type == DataAccess.Enum.CodeRewardValueType.Amount ? "$" : "%";
        }

        protected void ddlCodeExpirationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataAccess.Enum.CodeExpirationType type = (DataAccess.Enum.CodeExpirationType)int.Parse(ddlCodeExpirationType.SelectedValue);
            pnlActivationNumber.Visible = false;
            pnlDaysBeforeActivation.Visible = false;
            pnlExpirationDate.Visible = false;

            switch (type)
            {
                case DataAccess.Enum.CodeExpirationType.None:
                    pnlActivationNumber.Visible = false;
                    break;
                case DataAccess.Enum.CodeExpirationType.Date:
                    pnlExpirationDate.Visible = true;
                    break;
                case DataAccess.Enum.CodeExpirationType.Activations:
                    pnlActivationNumber.Visible = true;
                    break;
                case DataAccess.Enum.CodeExpirationType.DaysBeforeActivations:
                    pnlDaysBeforeActivation.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void ClearValueTypeField()
        {
            ddlValueType.Enabled = false;
            ddlValueType.SelectedIndex = 0;
        }
        private void ClearValueField()
        {
            txtValue.Enabled = false;
            txtValue.Text = string.Empty;
        }

        private void InitCodeRuleFields()
        {
            this.CodeRules = new List<CodeRuleGrid>();

            gvCodeRules.DataSource = this.CodeRules;
            gvCodeRules.DataBind();

            EnableCodeRuleFields(false);
        }
        private void EnableCodeRuleFields(bool isEnabled)
        {
            ddlCodeRuleValueType.Enabled = isEnabled;
            ddlCodeRuleOperator.Enabled = isEnabled;
            txtCodeRuleValue.Enabled = isEnabled;
            ddlCodeRuleValue.Enabled = isEnabled;
        }
        protected void ddlCodeRuleValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataAccess.Enum.CodeRuleValueType type = (
            DataAccess.Enum.CodeRuleValueType)int.Parse(ddlCodeRuleValueType.SelectedValue.ToString());

            txtCodeRuleValue.Visible = false;
            ddlCodeRuleValue.Visible = false;

            switch (type)
            {
                case DataAccess.Enum.CodeRuleValueType.SubtotalOrder:
                    txtCodeRuleValue.Visible = true;
                    break;
                case DataAccess.Enum.CodeRuleValueType.ShipmentOrder:
                    txtCodeRuleValue.Visible = true;
                    break;
                case DataAccess.Enum.CodeRuleValueType.SubtotalPurchase:
                    txtCodeRuleValue.Visible = true;
                    break;
                case DataAccess.Enum.CodeRuleValueType.ShipmentPurchase:
                    txtCodeRuleValue.Visible = true;
                    break;
                case DataAccess.Enum.CodeRuleValueType.Product:
                    ddlCodeRuleValue.Visible = true;
                    break;
                case DataAccess.Enum.CodeRuleValueType.ProductQuantity:
                    txtCodeRuleValue.Visible = true;
                    break;
            }
        }
        protected void btnCodeRuleNew_Click(object sender, EventArgs e)
        {
            btnCodeRuleNew.Visible = false;
            btnCodeRuleEdit.Visible = false;
            btnCodeRuleSave.Visible = true;
            btnCodeRuleCancel.Visible = true;

            EnableCodeRuleFields(true);
        }
        protected void btnCodeRuleEdit_Click(object sender, EventArgs e)
        {
            btnCodeRuleNew.Visible = false;
            btnCodeRuleEdit.Visible = false;
            btnCodeRuleSave.Visible = true;
            btnCodeRuleCancel.Visible = true;

            EnableCodeRuleFields(true);
        }
        protected void btnCodeRuleSave_Click(object sender, EventArgs e)
        {
            bool isValid = this.VerifyCodeRuleFields();

            if (!isValid)
            {
                return;
            }

            CodeRuleValueType type = (CodeRuleValueType)int.Parse(ddlCodeRuleValueType.SelectedValue.ToString());
            int valueType = 0;

            switch (type)
            {
                case DataAccess.Enum.CodeRuleValueType.Product:
                    valueType = 1;
                    break;
            }

            CodeRuleGrid rule = new CodeRuleGrid()
            {
                OperatorText = ddlCodeRuleOperator.SelectedItem.Text,
                ValueTypeText = ddlCodeRuleValueType.SelectedItem.Text,
                ValueView = valueType == 0 ? decimal.Parse(txtCodeRuleValue.Text).ToString("C2") : ddlCodeRuleValue.SelectedItem.Text
            };

            btnCodeRuleNew.Visible = true;
            btnCodeRuleEdit.Visible = false;
            btnCodeRuleSave.Visible = false;
            btnCodeRuleCancel.Visible = false;

            EnableCodeRuleFields(false);

            this.CodeRules.Add(rule);
            gvCodeRules.DataSource = this.CodeRules;
            gvCodeRules.DataBind();
        }
        protected void btnCodeRuleCancel_Click(object sender, EventArgs e)
        {
            btnCodeRuleNew.Visible = true;
            btnCodeRuleEdit.Visible = false;
            btnCodeRuleSave.Visible = false;
            btnCodeRuleCancel.Visible = false;

            EnableCodeRuleFields(false);
        }
        #endregion

        #region Implementation
        public bool VerifyCodeRuleFields()
        {
            lblCodeRuleError.Text = string.Empty;
            DataAccess.Enum.CodeRuleValueType type = (DataAccess.Enum.CodeRuleValueType)int.Parse(ddlCodeRuleValueType.SelectedValue.ToString());
            bool isValid = true;
            int valueType = 0;

            switch (type)
            {
                case DataAccess.Enum.CodeRuleValueType.SubtotalOrder:
                    break;
                case DataAccess.Enum.CodeRuleValueType.ShipmentOrder:
                    break;
                case DataAccess.Enum.CodeRuleValueType.SubtotalPurchase:
                    break;
                case DataAccess.Enum.CodeRuleValueType.ShipmentPurchase:
                    break;
                case DataAccess.Enum.CodeRuleValueType.Product:
                    valueType = 1;
                    break;
                case DataAccess.Enum.CodeRuleValueType.ProductQuantity:
                    break;
            }

            if (valueType == 0)
            {
                decimal dValue = 0m;
                if (!decimal.TryParse(txtCodeRuleValue.Text.Trim(), out dValue))
                {
                    lblCodeRuleError.Text = "Ingrese un valor numerico valido";
                    isValid = false;
                }
            }

            return isValid;
        }
        #endregion

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