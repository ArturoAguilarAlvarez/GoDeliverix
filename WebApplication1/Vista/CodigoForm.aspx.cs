using DataAccess.Enum;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista
{
    public partial class CodigoForm : System.Web.UI.Page
    {
        #region Properties
        public int CodeLevel
        {
            get => (int)ViewState["codelevel"];
            set => ViewState["codelevel"] = value;
        }
        public IList<CodeRuleGrid> CodeRules
        {
            get { return (IList<CodeRuleGrid>)ViewState["codeRules"]; }
            set { ViewState["codeRules"] = value; }
        }
        public int CodeRuleValueType
        {
            get { return (int)ViewState["coderulevaluetype"]; }
            set { ViewState["coderulevaluetype"] = value; }
        }
        public CodeRegion PromotionCodeRegion
        {
            get { return (CodeRegion)ViewState["coderegion"]; }
            set { ViewState["coderegion"] = value; }
        }
        public CodeBusiness DeliveryCompany
        {
            get { return (CodeBusiness)ViewState["deliverycompany"]; }
            set { ViewState["deliverycompany"] = value; }
        }
        public CodeBusiness Company
        {
            get { return (CodeBusiness)ViewState["company"]; }
            set { ViewState["company"] = value; }
        }

        private int[] RegionCodeRewards = new int[2] { 6, 7 };
        private int[] CompanyCodeRewards = new int[2] { 4, 8 };
        private int[] DeliveryCompanyCodeRewards = new int[10] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private IList<ListboxViewInteger> CodeRewards = new List<ListboxViewInteger>()
        {
            new ListboxViewInteger(){ Id=0, Name="Ninguno" },
            new ListboxViewInteger(){ Id=1, Name="Monto a monedero" },
            new ListboxViewInteger(){ Id=2, Name="Envio gratis" },
            new ListboxViewInteger(){ Id=3, Name="Costo fijo en Envio" },
            new ListboxViewInteger(){ Id=4, Name="Descuento en la orden" },
            new ListboxViewInteger(){ Id=5, Name="Descuento en el envio de la orden" },
            new ListboxViewInteger(){ Id=6, Name="Descuento en la compra" },
            new ListboxViewInteger(){ Id=7, Name="Descuento en el envio de la compra" },
            new ListboxViewInteger(){ Id=8, Name="Reembolso del subtotal de la orden" },
            new ListboxViewInteger(){ Id=9, Name="Reembolso del costo de envio de la orden" },
            new ListboxViewInteger(){ Id=10, Name="Reembolso del subtotal de la compra" },
            new ListboxViewInteger(){ Id=11, Name="Reembolso del costo de envio de la compra" }
        };
        #endregion

        #region ViewModel
        public AddressViewModel VmAddress { get; set; }
        private CodesViewModel VmCodes { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VmAddress = new AddressViewModel();
            this.VmCodes = new CodesViewModel("");

            if (!Page.IsPostBack)
            {
                this.PromotionCodeRegion = new CodeRegion();
                this.Company = new CodeBusiness();
                this.DeliveryCompany = new CodeBusiness();
                this.CodeRules = new List<CodeRuleGrid>();

                HideError();
                EnableCodeRuleFields(false);
            }
        }

        #region UI
        protected void Wizard1_SideBarButtonClick(object sender, WizardNavigationEventArgs e)
        {
            e.Cancel = true;
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            HideError();

            if (e.CurrentStepIndex == 0)
            {
                this.CodeLevel = int.Parse(ddlCodeLevel.SelectedValue);

                if (this.CodeLevel == 0)
                {
                    FillCountries();
                }
                else if (this.CodeLevel == 1)
                {
                    Wizard1.ActiveStepIndex = 3;
                    FillCompanies();
                }
                else if (this.CodeLevel == 2)
                {
                    Wizard1.ActiveStepIndex = 2;
                    FillDeliveryCompanies();
                }

                FillCodeRewards();
            }
            else if (e.CurrentStepIndex == 1)
            {
            }
            else if (e.CurrentStepIndex == 2) // Delivery Company
            {
            }
            else if (e.CurrentStepIndex == 3) // Company
            {
            }
            else if (e.CurrentStepIndex == 4) // Code details
            {
                bool cancel = false;

                decimal value = 0;

                if (!decimal.TryParse(txtValue.Text, out value))
                {
                    DrawError("Ingrese un valor valido");
                    cancel = true;
                }

                e.Cancel = cancel;
            }
            else if (e.CurrentStepIndex == 5) // Rules
            {
            }
            else if (e.CurrentStepIndex == 6) // Summary
            {
            }


        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlCountry.SelectedValue.ToString());
            this.PromotionCodeRegion.CountryUid = uid;
            FillStates();
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlState.SelectedValue);
            this.PromotionCodeRegion.StateUid = uid;
            FillMunicipalities();
        }
        protected void ddlMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {

            Guid uid = Guid.Parse(ddlMunicipality.SelectedValue);
            this.PromotionCodeRegion.MunicipalityUid = uid;
            FillCities();
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            Guid uid = Guid.Parse(ddlCity.SelectedValue);
            this.PromotionCodeRegion.CityUid = uid;
            FillNeighborhoods();
        }
        protected void ddlNeighborhood_SelectedIndexChanged(object sender, EventArgs e)
        {

            Guid uid = Guid.Parse(ddlNeighborhood.SelectedValue);
            this.PromotionCodeRegion.NeighborgoodUid = uid;
        }

        protected void ddlDeliveryCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlDeliveryCompany.SelectedValue.ToString());
            this.DeliveryCompany.UidCompany = uid;
            this.FillDeliveryCompanyBranches();
        }
        protected void ddlDeliveryCompanyBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlDeliveryCompanyBranch.SelectedValue.ToString());
            this.DeliveryCompany.UidCompanyBranch = uid;
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlCompany.SelectedValue.ToString());
            this.Company.UidCompany = uid;
            this.FillCompanyBranches();
        }
        protected void ddlCompanyBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlCompanyBranch.SelectedValue.ToString());
            this.Company.UidCompanyBranch = uid;
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
        private void FillCountries()
        {
            ddlCountry.DataSource = this.VmAddress.ReadAllCountries("Seleccionar pais");
            ddlCountry.DataValueField = "Uid";
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataBind();
        }
        private void FillStates()
        {
            Guid? uid = PromotionCodeRegion.CountryUid;
            if (uid.HasValue)
            {
                ddlState.SetDataSource(this.VmAddress.ReadAllStates(uid.Value, "Seleccionar estado"), "Uid", "Name");
                ddlState.DataBind();
            }
            else
            {
                ddlState.SetDataSource(null, "Uid", "Name");
                ddlState.DataBind();
            }
        }
        private void FillMunicipalities()
        {
            Guid? uid = PromotionCodeRegion.StateUid;
            if (uid.HasValue)
            {
                ddlMunicipality.SetDataSource(this.VmAddress.ReadAllMunicipalities(uid.Value, "Seleccionar municipio"), "Uid", "Name");
                ddlMunicipality.DataBind();
            }
            else
            {
                ddlMunicipality.SetDataSource(null, "Uid", "Name");
                ddlMunicipality.DataBind();
            }
        }
        private void FillCities()
        {
            Guid? uid = PromotionCodeRegion.MunicipalityUid;
            if (uid.HasValue)
            {
                ddlCity.SetDataSource(this.VmAddress.ReadAllCities(uid.Value, "Seleccionar ciudad"), "Uid", "Name");
                ddlCity.DataBind();
            }
            else
            {
                ddlCity.SetDataSource(null, "Uid", "Name");
                ddlCity.DataBind();
            }
        }
        private void FillNeighborhoods()
        {
            Guid? uid = PromotionCodeRegion.CityUid;
            if (uid.HasValue)
            {
                ddlNeighborhood.SetDataSource(this.VmAddress.ReadAllNeighborhoods(uid.Value, "Seleccionar colonia"), "Uid", "Name");
                ddlNeighborhood.DataBind();
            }
            else
            {
                ddlNeighborhood.SetDataSource(null, "Uid", "Name");
                ddlNeighborhood.DataBind();
            }
        }

        private void FillDeliveryCompanies()
        {

            ddlDeliveryCompany.DataSource = this.VmCodes.ReadAllCompanies(2, defaultItem: "Seleccionar empresa");
            ddlDeliveryCompany.DataValueField = "Uid";
            ddlDeliveryCompany.DataTextField = "Name";
            ddlDeliveryCompany.DataBind();
        }
        private void FillDeliveryCompanyBranches()
        {
            ddlDeliveryCompanyBranch.SetDataSource(this.VmCodes.ReadAllCompanyBranches(this.DeliveryCompany.UidCompany, "Seleccionar sucursal"), "Uid", "Name");
            ddlDeliveryCompanyBranch.DataBind();
        }

        private void FillCompanies()
        {
            ddlCompany.DataSource = this.VmCodes.ReadAllCompanies(1, defaultItem: "Seleccionar empresa");
            ddlCompany.DataValueField = "Uid";
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataBind();
        }
        private void FillCompanyBranches()
        {
            ddlCompanyBranch.SetDataSource(this.VmCodes.ReadAllCompanyBranches(this.Company.UidCompany, "Seleccionar sucursal"), "Uid", "Name");
            ddlCompanyBranch.DataBind();
        }

        private void HideError()
        {
            pnlError.Visible = false;
        }
        private void DrawError(string error)
        {
            pnlError.Visible = true;
            lblError.Text = error;
        }

        private void FillCodeRewards()
        {
            var rewards = new List<ListboxViewInteger>();

            if (this.CodeLevel == 0)
            {
                rewards = this.CodeRewards.Where(r => this.RegionCodeRewards.Contains(r.Id)).ToList();
            }
            else if (this.CodeLevel == 1)
            {
                rewards = this.CodeRewards.Where(r => this.CompanyCodeRewards.Contains(r.Id)).ToList();
            }
            else if (this.CodeLevel == 2)
            {
                rewards = this.CodeRewards.Where(r => this.DeliveryCompanyCodeRewards.Contains(r.Id)).ToList();
            }

            ddlRewardType.DataSource = rewards;
            ddlRewardType.DataTextField = "Name";
            ddlRewardType.DataValueField = "Id";
            ddlRewardType.DataBind();
        }

        private void EnableCodeRuleFields(bool isEnabled)
        {
            ddlCodeRuleValueType.Enabled = isEnabled;
            ddlCodeRuleOperator.Enabled = isEnabled;
            txtCodeRuleValue.Enabled = isEnabled;
            ddlCodeRuleValue.Enabled = isEnabled;
        }
        public bool VerifyCodeRuleFields()
        {
            HideError();
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
                    DrawError("Ingrese un valor numerico valido");
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

    [Serializable]
    public class CodeRegion
    {
        public Guid? CountryUid { get; set; } = null;
        public Guid? StateUid { get; set; } = null;
        public Guid? MunicipalityUid { get; set; } = null;
        public Guid? CityUid { get; set; } = null;
        public Guid? NeighborgoodUid { get; set; } = null;
    }

    [Serializable]
    public class CodeBusiness
    {
        public Guid UidCompany { get; set; }
        public Guid UidCompanyBranch { get; set; }
    }
}