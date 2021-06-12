using DataAccess.Enum;
using DataAccess.Models;
using Modelo.v2;
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
        protected PromotionCodeLevel CodeLevel
        {
            get => (PromotionCodeLevel)ViewState["promotioncodelevel"];
            set => ViewState["promotioncodelevel"] = value;
        }
        protected Guid? UidPromotionCode
        {
            get => (Guid?)ViewState["uidpromotioncode"];
            set => ViewState["uidpromotioncode"] = value;
        }

        public IList<PromotionCodeRuleView> CodeRules
        {
            get => (IList<PromotionCodeRuleView>)ViewState["codeRules"];
            set => ViewState["codeRules"] = value;
        }
        public PromotionCodeGeography PromotionCodeRegion
        {
            get { return (PromotionCodeGeography)ViewState["coderegion"]; }
            set { ViewState["coderegion"] = value; }
        }
        public PromotionCodeBusiness DeliveryCompany
        {
            get => (PromotionCodeBusiness)ViewState["deliverycompany"];
            set => ViewState["deliverycompany"] = value;
        }
        public PromotionCodeBusiness Company
        {
            get => (PromotionCodeBusiness)ViewState["company"];
            set => ViewState["company"] = value;
        }
        public PromotionCodeBase Code
        {
            get => (PromotionCodeBase)ViewState["code"];
            set => ViewState["code"] = value;
        }
        public PromotionCodeExpirationBase Expiration
        {
            get => (PromotionCodeExpirationBase)ViewState["expiration"];
            set => ViewState["expiration"] = value;
        }

        private readonly int[] _RegionCodeRewards = new int[2] { 6, 7 };
        private readonly int[] _CompanyCodeRewards = new int[2] { 4, 8 };
        private readonly int[] _DeliveryCompanyCodeRewards = new int[10] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        private readonly IList<ListboxViewInteger> _CodeRewards = new List<ListboxViewInteger>()
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

        private readonly int[] _RegionCodeRules = new int[5] { 2, 3, 7, 8, 9 };
        private readonly int[] _CompanyCodeRules = new int[6] { 0, 1, 2, 3, 4, 5 };
        private readonly int[] _DeliveryCompanyCodeRules = new int[3] { 0, 4, 5 };
        private readonly IList<ListboxViewInteger> _CodeRuleValueTypes = new List<ListboxViewInteger>() {
            new ListboxViewInteger(){ Id = 0, Name = "Subtotal (Orden)" },
            new ListboxViewInteger(){ Id = 1, Name = "Envió (Orden)" },
            new ListboxViewInteger(){ Id = 2, Name = "Subtotal (Compra)" },
            new ListboxViewInteger(){ Id = 3, Name = "Envió (Compra)" },
            new ListboxViewInteger(){ Id = 4, Name = "Producto" },
            new ListboxViewInteger(){ Id = 5, Name = "Cantidad de Productos" },
            new ListboxViewInteger(){ Id = 6, Name = "Giro" },
            new ListboxViewInteger(){ Id = 7, Name = "Categoria" },
            new ListboxViewInteger(){ Id = 8, Name = "Subcategoria" },
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
                this.PromotionCodeRegion = new PromotionCodeGeography();
                this.Company = null;
                this.DeliveryCompany = null;
                this.CodeRules = new List<PromotionCodeRuleView>();
                this.Code = new PromotionCodeBase();
                this.Expiration = new PromotionCodeExpirationBase();

                HideError();
                EnableCodeRuleFields(false);

                txtStartAt.Text = DateTime.Now.ToString("yyyy-MM-dd");

                if (Request.QueryString.HasKeys())
                {
                    var qUid = Request.QueryString["uid"];
                    if (qUid != null)
                    {
                        Guid tmp = Guid.Empty;
                        if (Guid.TryParse(qUid, out tmp))
                        {
                            this.UidPromotionCode = Guid.Parse(qUid);
                        }
                    }
                }
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

            if (e.CurrentStepIndex == 0) // Code Type
            {
                this.CodeLevel = (PromotionCodeLevel)int.Parse(ddlCodeLevel.SelectedValue);

                if (this.CodeLevel == PromotionCodeLevel.Region)
                {
                    FillCountries();
                }
                else if (this.CodeLevel == PromotionCodeLevel.DeliveryCompany)
                {
                    Wizard1.ActiveStepIndex = 3;
                    FillCompanies();
                    this.Company = new PromotionCodeBusiness();
                    this.DeliveryCompany = new PromotionCodeBusiness();
                }
                else if (this.CodeLevel == PromotionCodeLevel.Company)
                {
                    Wizard1.ActiveStepIndex = 2;
                    FillDeliveryCompanies();
                    this.DeliveryCompany = new PromotionCodeBusiness();
                }

                FillCodeCatalogs();
            }
            else if (e.CurrentStepIndex == 1) // Location
            {
                if (!this.PromotionCodeRegion.CountryUid.HasValue)
                {
                    DrawError("Seleccione un pais");
                    e.Cancel = true;
                }

                if (this.PromotionCodeRegion.CountryUid.HasValue && this.PromotionCodeRegion.CountryUid.Value == Guid.Empty)
                {
                    DrawError("Seleccione un pais");
                    e.Cancel = true;
                }

                Wizard1.ActiveStepIndex = 4;
            }
            else if (e.CurrentStepIndex == 2) // Delivery Company
            {
                if (!this.DeliveryCompany.UidCompany.HasValue)
                {
                    DrawError("Seleccione una distribuidora");
                    e.Cancel = true;
                }

                if (this.DeliveryCompany.UidCompany.HasValue && this.DeliveryCompany.UidCompany == Guid.Empty)
                {
                    DrawError("Seleccione una distribuidora");
                    e.Cancel = true;
                }
            }
            else if (e.CurrentStepIndex == 3) // Company
            {
                if (!this.Company.UidCompany.HasValue)
                {
                    DrawError("Seleccione una suministradora");
                    e.Cancel = true;
                }

                if (this.Company.UidCompany.HasValue && this.Company.UidCompany == Guid.Empty)
                {
                    DrawError("Seleccione una suministradora");
                    e.Cancel = true;
                }
            }
            else if (e.CurrentStepIndex == 4) // Code details
            {
                bool cancel = false;

                decimal value = 0;
                DateTime startAt = DateTime.Now;

                CodeExpirationType expirationType = (CodeExpirationType)int.Parse(ddlCodeExpirationType.SelectedValue);
                DateTime expirationDate = DateTime.Now;
                int expirationValue = 0;

                if (!decimal.TryParse(txtValue.Text, out value))
                {
                    DrawError("Ingrese un valor valido");
                    cancel = true;
                }

                if (!DateTime.TryParse(txtStartAt.Text.Trim(), out startAt))
                {
                    DrawError("Ingrese una fecha de inicio valida");
                    cancel = true;
                }

                if (expirationType == CodeExpirationType.Date)
                {
                    if (!DateTime.TryParse(txtExpirationDate.Text.Trim(), out expirationDate))
                    {
                        DrawError("Ingrese una fecha de expiracion valida");
                        cancel = true;
                    }
                }
                else if (expirationType == CodeExpirationType.Activations)
                {
                    if (!int.TryParse(txtActivationsNumber.Text.Trim(), out expirationValue))
                    {
                        DrawError("Ingrese un numero de activaciones valido");
                        cancel = true;
                    }
                }
                else if (expirationType == CodeExpirationType.DaysBeforeActivations)
                {
                    if (!int.TryParse(txtDaysBeforeActivation.Text.Trim(), out expirationValue))
                    {
                        DrawError("Ingrese un numero de activaciones valido");
                        cancel = true;
                    }
                }

                if (!cancel)
                {
                    this.Code.Code = txtCode.Text.Trim();
                    this.Code.RewardType = (CodeRewardType)int.Parse(ddlRewardType.SelectedValue);
                    this.Code.ValueType = (CodeRewardValueType)int.Parse(ddlValueType.SelectedValue);
                    this.Code.Value = value;
                    this.Code.ActivationType = (PromotionCodeActivationType)int.Parse(ddlActivationType.SelectedValue);

                    this.Expiration.StartAt = startAt;
                    this.Expiration.Type = expirationType;
                    this.Expiration.ActivationsLimit = expirationValue;
                    this.Expiration.ExpirationDate = expirationDate;
                    this.Expiration.DaysAfterActivation = expirationValue;
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

        protected void Wizard1_OnFinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                this.VmCodes.AddPromotionCode(
                    this.CodeLevel,
                    this.Code.Code,
                    this.Code.RewardType,
                    this.Code.ValueType,
                    this.Code.Value,
                    this.Code.ActivationType,
                    this.Expiration.Type,
                    this.Expiration.StartAt,
                    this.Expiration.ExpirationDate,
                    this.Expiration.Type == CodeExpirationType.Activations ? this.Expiration.ActivationsLimit : this.Expiration.DaysAfterActivation,
                    this.Company,
                    this.DeliveryCompany
                    );
            }
            catch (Exception exception)
            {
                DrawError(exception.Message);
            }
        }

        public string GetLinkStepClass(object wStep)
        {
            if (wStep == null)
                return "";

            int stepIndex = Wizard1.WizardSteps.IndexOf((WizardStep)wStep);

            if (stepIndex < Wizard1.ActiveStepIndex)
            {
                return "active";
            }
            else if (stepIndex > Wizard1.ActiveStepIndex)
            {
                return "";
            }
            else
            {
                return "active";
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
            this.PromotionCodeRegion.NeighborhoodUid = uid;
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
            if (this.Company == null)
                this.Company = new PromotionCodeBusiness();

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

            PromotionCodeRuleValueType type = (PromotionCodeRuleValueType)int.Parse(ddlCodeRuleValueType.SelectedValue.ToString());
            int valueType = 0;

            switch (type)
            {
                case DataAccess.Enum.PromotionCodeRuleValueType.Product:
                    valueType = 1;
                    break;
            }

            PromotionCodeRuleView rule = new PromotionCodeRuleView()
            {
                OperatorText = ddlCodeRuleOperator.SelectedItem.Text,
                ValueTypeText = ddlCodeRuleValueType.SelectedItem.Text,
                ValueText = valueType == 0 ? decimal.Parse(txtCodeRuleValue.Text).ToString("C2") : ddlCodeRuleValue.SelectedItem.Text
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
            ddlDeliveryCompanyBranch.SetDataSource(this.VmCodes.ReadAllCompanyBranches(this.DeliveryCompany.UidCompany.Value, "Seleccionar sucursal"), "Uid", "Name");
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
            ddlCompanyBranch.SetDataSource(this.VmCodes.ReadAllCompanyBranches(this.Company.UidCompany.Value, "Seleccionar sucursal"), "Uid", "Name");
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

        private void FillCodeCatalogs()
        {
            var rewards = new List<ListboxViewInteger>();
            var ruleTypes = new List<ListboxViewInteger>();

            if (this.CodeLevel == PromotionCodeLevel.Region)
            {
                rewards = this._CodeRewards.Where(r => _RegionCodeRewards.Contains(r.Id)).ToList();
                ruleTypes = this._CodeRuleValueTypes.Where(r => this._RegionCodeRules.Contains(r.Id)).ToList();
            }
            else if (this.CodeLevel == PromotionCodeLevel.DeliveryCompany)
            {
                rewards = this._CodeRewards.Where(r => this._CompanyCodeRewards.Contains(r.Id)).ToList();
                ruleTypes = this._CodeRuleValueTypes.Where(r => this._CompanyCodeRules.Contains(r.Id)).ToList();
            }
            else if (this.CodeLevel == PromotionCodeLevel.Company)
            {
                rewards = this._CodeRewards.Where(r => this._DeliveryCompanyCodeRewards.Contains(r.Id)).ToList();
                ruleTypes = this._CodeRuleValueTypes.Where(r => this._DeliveryCompanyCodeRules.Contains(r.Id)).ToList();
            }

            ddlRewardType.DataSource = rewards;
            ddlRewardType.DataTextField = "Name";
            ddlRewardType.DataValueField = "Id";
            ddlRewardType.DataBind();

            ddlCodeRuleValueType.DataSource = ruleTypes;
            ddlCodeRuleValueType.DataTextField = "Name";
            ddlCodeRuleValueType.DataValueField = "Id";
            ddlCodeRuleValueType.DataBind();
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
            DataAccess.Enum.PromotionCodeRuleValueType type = (DataAccess.Enum.PromotionCodeRuleValueType)int.Parse(ddlCodeRuleValueType.SelectedValue.ToString());
            bool isValid = true;
            int valueType = 0;

            switch (type)
            {
                case DataAccess.Enum.PromotionCodeRuleValueType.SubtotalOrder:
                    break;
                case DataAccess.Enum.PromotionCodeRuleValueType.ShipmentOrder:
                    break;
                case DataAccess.Enum.PromotionCodeRuleValueType.SubtotalPurchase:
                    break;
                case DataAccess.Enum.PromotionCodeRuleValueType.ShipmentPurchase:
                    break;
                case DataAccess.Enum.PromotionCodeRuleValueType.Product:
                    valueType = 1;
                    break;
                case DataAccess.Enum.PromotionCodeRuleValueType.ProductQuantity:
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
    public class PromotionCodeBase
    {
        public Guid Uid { get; set; }
        public Guid CodeUid { get; set; }
        public Guid? ExpirationUid { get; set; }
        public CodeRewardType RewardType { get; set; }
        public CodeRewardValueType ValueType { get; set; }
        public decimal Value { get; set; }
        public PromotionCodeActivationType ActivationType { get; set; }
        public int Activations { get; set; }
        public string Code { get; set; }
    }

    [Serializable]
    public class PromotionCodeExpirationBase
    {
        public Guid Uid { get; set; }
        public DateTime StartAt { get; set; }
        public CodeExpirationType Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? ActivationsLimit { get; set; }
        public int? DaysAfterActivation { get; set; }
    }
}