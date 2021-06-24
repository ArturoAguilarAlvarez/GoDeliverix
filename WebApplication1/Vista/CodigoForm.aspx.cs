using DataAccess.Enum;
using DataAccess.Models;
using Modelo.v2;
using System;
using System.CodeDom;
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

        private readonly int[] _RegionCodeRules = new int[6] { 2, 3, 6, 7, 8, 9 };
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

        private readonly int[] _comboRuleOperators = new int[2] { 0, 5 };
        private readonly int[] _textRuleOperators = new int[6] { 0, 1, 2, 3, 4, 5 };
        private readonly IList<ListboxViewInteger> _codeRuleOperators = new List<ListboxViewInteger>()
        {
            new ListboxViewInteger(){Id =  0,Name = "Igual"},
            new ListboxViewInteger(){Id =  1,Name = "Menor"},
            new ListboxViewInteger(){Id =  2,Name = "Mayor"},
            new ListboxViewInteger(){Id =  3,Name = "Menor o igual"},
            new ListboxViewInteger(){Id =  4,Name = "Mayor o igual"},
            new ListboxViewInteger(){Id =  5,Name = "Diferente"}
        };
        #endregion

        #region ViewModel
        public AddressViewModel VmAddress { get; set; }
        private CodesViewModel VmCodes { get; set; }
        private ProductViewModel VmProduct { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VmAddress = new AddressViewModel();
            this.VmCodes = new CodesViewModel("");
            this.VmProduct = new ProductViewModel();

            if (!Page.IsPostBack)
            {
                this.PromotionCodeRegion = null;
                this.Company = null;
                this.DeliveryCompany = null;
                this.CodeRules = new List<PromotionCodeRuleView>();
                this.Code = new PromotionCodeBase();
                this.Expiration = new PromotionCodeExpirationBase();

                HideError();
                EnableCodeRuleFields(false);

                txtStartAt.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ddlState.SetDefault("Todos los estados", Guid.Empty);
                ddlMunicipality.SetDefault("Todos los municipios", Guid.Empty);
                ddlCity.SetDefault("Todos las ciudades", Guid.Empty);
                ddlNeighborhood.SetDefault("Todos las colonias", Guid.Empty);

                ddlCompanyBranch.SetDefault("Todos las sucursales", Guid.Empty);

                ddlDeliveryCompanyBranch.SetDefault("Todos las sucursales", Guid.Empty);

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

            // Fill Region
            //if (this.PromotionCodeRegion != null)
            //{
            //    if (this.PromotionCodeRegion.CountryUid.HasValue)
            //        ddlCountry.SelectedValue = PromotionCodeRegion.CountryUid.Value.ToString();

            //    if (this.PromotionCodeRegion.StateUid.HasValue)
            //        ddlState.SelectedValue = PromotionCodeRegion.StateUid.Value.ToString();

            //    if (this.PromotionCodeRegion.MunicipalityUid.HasValue)
            //        ddlMunicipality.SelectedValue = PromotionCodeRegion.MunicipalityUid.Value.ToString();

            //    if (this.PromotionCodeRegion.CityUid.HasValue)
            //        ddlCity.SelectedValue = PromotionCodeRegion.CityUid.Value.ToString();

            //    if (this.PromotionCodeRegion.NeighborhoodUid.HasValue)
            //        ddlNeighborhood.SelectedValue = PromotionCodeRegion.NeighborhoodUid.Value.ToString();
            //}

            // Fill Company
            //if (this.Company != null)
            //{
            //    if (this.Company.UidCompany.HasValue)
            //        ddlCompany.SelectedValue = this.Company.UidCompany.Value.ToString();

            //    if (this.Company.UidCompanyBranch.HasValue)
            //        ddlCompanyBranch.SelectedValue = this.Company.UidCompanyBranch.Value.ToString();
            //}

            // Fill Delivery Company
            //if (this.DeliveryCompany != null)
            //{
            //    if (this.DeliveryCompany.UidCompany.HasValue)
            //        ddlDeliveryCompany.SelectedValue = this.DeliveryCompany.UidCompany.Value.ToString();

            //    if (this.DeliveryCompany.UidCompanyBranch.HasValue)
            //        ddlDeliveryCompanyBranch.SelectedValue = this.DeliveryCompany.UidCompanyBranch.Value.ToString();
            //}
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
                lblSmCodeType.Text = ddlCodeLevel.SelectedItem.Text;

                if (this.CodeLevel == PromotionCodeLevel.Region)
                {
                    if (this.PromotionCodeRegion == null)
                        this.PromotionCodeRegion = new PromotionCodeGeography();

                    //if (this.DeliveryCompany == null)
                    //    this.DeliveryCompany = new PromotionCodeBusiness();

                    //if (this.Company == null)
                    //    this.Company = new PromotionCodeBusiness();

                    FillCountries();
                }
                else if (this.CodeLevel == PromotionCodeLevel.DeliveryCompany)
                {
                    Wizard1.ActiveStepIndex = 2;
                    FillDeliveryCompanies();

                    this.PromotionCodeRegion = null;

                    if (this.DeliveryCompany == null)
                        this.DeliveryCompany = new PromotionCodeBusiness();

                    if (this.Company == null)
                        this.Company = new PromotionCodeBusiness();
                }
                else if (this.CodeLevel == PromotionCodeLevel.Company)
                {
                    Wizard1.ActiveStepIndex = 3;
                    FillCompanies();

                    this.DeliveryCompany = null;

                    if (this.Company == null)
                        this.Company = new PromotionCodeBusiness();
                }

                FillCodeCatalogs();
            }
            else if (e.CurrentStepIndex == 1) // Location
            {
                if (this.PromotionCodeRegion == null)
                    this.PromotionCodeRegion = new PromotionCodeGeography();

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
                if (this.DeliveryCompany == null)
                    this.DeliveryCompany = new PromotionCodeBusiness();

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

                FillCompanies();

                if (this.Company == null)
                    this.Company = new PromotionCodeBusiness();
            }
            else if (e.CurrentStepIndex == 3) // Company
            {
                if (this.Company == null)
                    this.Company = new PromotionCodeBusiness();

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
                    this.Code.Status = int.Parse(ddlCodeStatus.SelectedValue);

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
                if (e.NextStepIndex == 6)
                {
                    pnlSmRegion.Visible = false;
                    pnlSmDeliveryCompany.Visible = false;
                    pnlSmCompany.Visible = false;
                    pnlSmCodeRules.Visible = false;

                    // Display Country Values
                    if (this.PromotionCodeRegion != null)
                    {
                        pnlSmRegion.Visible = true;

                        pnlSmState.Visible = this.PromotionCodeRegion.StateUid.HasValue;
                        pnlSmMunicipality.Visible = this.PromotionCodeRegion.MunicipalityUid.HasValue;
                        pnlSmCity.Visible = this.PromotionCodeRegion.CityUid.HasValue;
                        pnlSmNeighborhood.Visible = this.PromotionCodeRegion.NeighborhoodUid.HasValue;

                        lblSmCountry.Text = GetDropDownTextFromValue(ddlCountry, PromotionCodeRegion.CountryUid.ToString());

                        if (PromotionCodeRegion.StateUid.HasValue)
                            lblSmState.Text = ddlState.FindTextByValue(PromotionCodeRegion.StateUid.Value.ToString());

                        if (PromotionCodeRegion.MunicipalityUid.HasValue)
                            lblSmMunicipality.Text = ddlMunicipality.FindTextByValue(PromotionCodeRegion.MunicipalityUid.Value.ToString());

                        if (PromotionCodeRegion.CityUid.HasValue)
                            lblSmCity.Text = ddlCity.FindTextByValue(PromotionCodeRegion.CityUid.Value.ToString());

                        if (PromotionCodeRegion.NeighborhoodUid.HasValue)
                            lblSmNeighborhood.Text = ddlNeighborhood.FindTextByValue(PromotionCodeRegion.NeighborhoodUid.Value.ToString());
                    }

                    // Display Delivery Company Values
                    if (this.DeliveryCompany != null)
                    {
                        pnlSmDeliveryCompany.Visible = true;
                        pnlSmDeliveryCompanyBranch.Visible = this.DeliveryCompany.UidCompanyBranch.HasValue;

                        if (this.DeliveryCompany.UidCompany.HasValue)
                            lblSmDeliveryCompany.Text = GetDropDownTextFromValue(ddlDeliveryCompany, this.DeliveryCompany.UidCompany.ToString());

                        if (this.DeliveryCompany.UidCompanyBranch.HasValue)
                            lblSmDeliveryCompanyBranch.Text = ddlDeliveryCompanyBranch.FindTextByValue(this.DeliveryCompany.UidCompanyBranch.ToString());
                    }

                    // Display Company Values
                    if (this.Company != null)
                    {
                        this.pnlSmCompany.Visible = true;
                        pnlSmCompanyBranch.Visible = this.Company.UidCompanyBranch.HasValue;

                        if (this.Company.UidCompany.HasValue)
                            lblSmCompany.Text = GetDropDownTextFromValue(ddlCompany, this.Company.UidCompany.ToString());

                        if (this.Company.UidCompanyBranch.HasValue)
                            lblSmCompanyBranch.Text = ddlCompanyBranch.FindTextByValue(this.Company.UidCompanyBranch.ToString());
                    }

                    // Display Code rules
                    if (this.CodeRules.Count > 0)
                    {
                        pnlSmCodeRules.Visible = true;

                        gvSmCodeRules.DataSource = this.CodeRules;
                        gvSmCodeRules.DataBind();
                    }

                    // Display Code Values
                    lblSmCode.Text = string.IsNullOrEmpty(this.Code.Code) ? "(automatico)" : this.Code.Code;
                    lblSmRewardType.Text = GetDropDownTextFromValue(ddlRewardType, ((int)this.Code.RewardType).ToString());
                    lblSmValueType.Text = GetDropDownTextFromValue(ddlValueType, ((int)this.Code.ValueType).ToString());
                    lblSmValue.Text = $" {(this.Code.ValueType == CodeRewardValueType.Amount ? "$" : "")} {this.Code.Value} {(this.Code.ValueType == CodeRewardValueType.Percentage ? "%" : "")}";

                    lblSmActivationType.Text = GetDropDownTextFromValue(ddlActivationType, ((int)this.Code.ActivationType).ToString());
                    lblSmStatus.Text = GetDropDownTextFromValue(ddlCodeStatus, ((int)this.Code.Status).ToString());

                    lblSmStartAt.Text = this.Expiration.StartAt.ToString(("d"));
                    lblSmExpirationType.Text = GetDropDownTextFromValue(ddlCodeExpirationType, ((int)this.Expiration.Type).ToString());

                    pnlSmExpirationDate.Visible = this.Expiration.Type == CodeExpirationType.Date;
                    lblSmExpirationDate.Text = this.Expiration.ExpirationDate.HasValue ? this.Expiration.ExpirationDate.Value.ToString(("d")) : "";

                    pnlSmActivationNumber.Visible = this.Expiration.Type == CodeExpirationType.Activations;
                    lblSmActivationNumber.Text = this.Expiration.ExpirationDate.HasValue ? this.Expiration.ActivationsLimit.ToString() : "";

                    pnlSmDaysBeforeActivation.Visible = this.Expiration.Type == CodeExpirationType.DaysBeforeActivations;
                    lblSmDaysBeforeActivation.Text = this.Expiration.ExpirationDate.HasValue ? this.Expiration.DaysAfterActivation.ToString() : "";
                }
            }
            else if (e.CurrentStepIndex == 6) // Summary
            {
            }
        }

        protected void Wizard1_OnFinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                var success = this.VmCodes.AddPromotionCode(
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
                    this.DeliveryCompany,
                    this.PromotionCodeRegion,
                    this.CodeRules
                    );

                if (success) { Response.Redirect("Codigos.aspx"); } else { DrawError("Ocurrio un error al guardar el codigo"); }

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

        protected void ddlCodeExpirationType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CodeExpirationType type = (CodeExpirationType)int.Parse(ddlCodeExpirationType.SelectedValue);

            pnlExpirationDate.Visible = false;
            pnlActivationNumber.Visible = false;
            pnlDaysBeforeActivation.Visible = false;

            switch (type)
            {
                case CodeExpirationType.None:
                    break;
                case CodeExpirationType.Date:
                    pnlExpirationDate.Visible = true;
                    break;
                case CodeExpirationType.Activations:
                    pnlActivationNumber.Visible = true;
                    break;
                case CodeExpirationType.DaysBeforeActivations:
                    pnlDaysBeforeActivation.Visible = true;
                    break;
            }
        }

        protected void ddlValueType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CodeRewardValueType type = (CodeRewardValueType)int.Parse(ddlValueType.SelectedValue);
            lblValueType.Text = type == CodeRewardValueType.Amount ? "$" : "%";
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

            ddlMunicipality.Visible = true;
        }
        protected void ddlMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlMunicipality.SelectedValue);
            this.PromotionCodeRegion.MunicipalityUid = uid;
            FillCities();

            ddlCity.Visible = true;
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = Guid.Parse(ddlCity.SelectedValue);
            this.PromotionCodeRegion.CityUid = uid;
            FillNeighborhoods();

            ddlNeighborhood.Visible = true;
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

            string value = string.Empty;
            string valueText = string.Empty;

            switch (type)
            {
                case DataAccess.Enum.PromotionCodeRuleValueType.Product:
                    value = ddlCodeRuleValue.SelectedItem.Value;
                    valueText = ddlCodeRuleValue.SelectedItem.Text;
                    break;
                case PromotionCodeRuleValueType.Giro:
                    value = ddlRuleGiro.SelectedItem.Value;
                    valueText = ddlRuleGiro.SelectedItem.Text;
                    break;
                case PromotionCodeRuleValueType.Categoria:
                    value = ddlRuleCategoria.SelectedItem.Value;
                    valueText = ddlRuleCategoria.SelectedItem.Text;
                    break;
                case PromotionCodeRuleValueType.Subcategoria:
                    value = ddlRuleSubcategoria.SelectedItem.Value;
                    valueText = ddlRuleSubcategoria.SelectedItem.Text;
                    break;
                default:
                    value = txtValue.Text.Trim();
                    valueText = txtValue.Text.Trim();
                    break;
            }

            PromotionCodeRuleView rule = new PromotionCodeRuleView()
            {
                Operator = int.Parse(ddlCodeRuleOperator.SelectedItem.Value),
                ValueType = (int)type,
                Value = value,
                OperatorText = ddlCodeRuleOperator.SelectedItem.Text,
                ValueTypeText = ddlCodeRuleValueType.SelectedItem.Text,
                ValueText = valueText
            };

            btnCodeRuleNew.Visible = true;
            btnCodeRuleEdit.Visible = false;
            btnCodeRuleSave.Visible = false;
            btnCodeRuleCancel.Visible = false;

            ClearCodeRuleFields();

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

            ClearCodeRuleFields();

            EnableCodeRuleFields(false);
        }
        protected void ddlCodeRuleValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PromotionCodeRuleValueType type = (PromotionCodeRuleValueType)int.Parse(ddlCodeRuleValueType.SelectedValue);

            txtCodeRuleValue.Visible = false;
            ddlCodeRuleValue.Visible = false;
            pnlRuleValueType.Visible = false;

            pnlRuleGiroValueType.Visible = false;
            pnlRuleCategoria.Visible = false;
            pnlRuleSubcategoria.Visible = false;

            string fieldType = string.Empty;

            switch (type)
            {
                case PromotionCodeRuleValueType.SubtotalOrder:
                    pnlRuleValueType.Visible = true;
                    txtCodeRuleValue.Visible = true;
                    fieldType = "Text";
                    break;
                case PromotionCodeRuleValueType.ShipmentOrder:
                    pnlRuleValueType.Visible = true;
                    txtCodeRuleValue.Visible = true;
                    fieldType = "Text";
                    break;
                case PromotionCodeRuleValueType.SubtotalPurchase:
                    pnlRuleValueType.Visible = true;
                    txtCodeRuleValue.Visible = true;
                    fieldType = "Text";
                    break;
                case PromotionCodeRuleValueType.ShipmentPurchase:
                    pnlRuleValueType.Visible = true;
                    txtCodeRuleValue.Visible = true;
                    fieldType = "Text";
                    break;
                case PromotionCodeRuleValueType.Product:
                    pnlRuleValueType.Visible = true;
                    ddlCodeRuleValue.Visible = true;
                    FillCodeRuleProducts();
                    fieldType = "Combo";
                    break;
                case PromotionCodeRuleValueType.ProductQuantity:
                    pnlRuleValueType.Visible = true;
                    txtCodeRuleValue.Visible = true;
                    fieldType = "Text";
                    break;
                case PromotionCodeRuleValueType.Giro:
                    pnlRuleValueType.Visible = false;

                    pnlRuleGiroValueType.Visible = true;

                    fieldType = "Combo";

                    FillCodeRuleGiro();

                    ddlRuleCategoria.DataSource = new List<ListboxView>() { new ListboxView() { Uid = Guid.Empty, Name = "Todos" } };
                    ddlRuleCategoria.DataTextField = "Name";
                    ddlRuleCategoria.DataValueField = "Uid";
                    ddlRuleCategoria.DataBind();

                    ddlRuleSubcategoria.DataSource = new List<ListboxView>() { new ListboxView() { Uid = Guid.Empty, Name = "Todos" } };
                    ddlRuleSubcategoria.DataTextField = "Name";
                    ddlRuleSubcategoria.DataValueField = "Uid";
                    ddlRuleSubcategoria.DataBind();

                    break;
                case PromotionCodeRuleValueType.Categoria:
                    pnlRuleValueType.Visible = false;

                    pnlRuleGiroValueType.Visible = true;
                    pnlRuleCategoria.Visible = true;

                    fieldType = "Combo";

                    FillCodeRuleGiro();

                    ddlRuleCategoria.DataSource = new List<ListboxView>() { new ListboxView() { Uid = Guid.Empty, Name = "Todos" } };
                    ddlRuleCategoria.DataTextField = "Name";
                    ddlRuleCategoria.DataValueField = "Uid";
                    ddlRuleCategoria.DataBind();

                    ddlRuleSubcategoria.DataSource = new List<ListboxView>() { new ListboxView() { Uid = Guid.Empty, Name = "Todos" } };
                    ddlRuleSubcategoria.DataTextField = "Name";
                    ddlRuleSubcategoria.DataValueField = "Uid";
                    ddlRuleSubcategoria.DataBind();

                    break;
                case PromotionCodeRuleValueType.Subcategoria:
                    pnlRuleValueType.Visible = false;

                    pnlRuleGiroValueType.Visible = true;
                    pnlRuleCategoria.Visible = true;
                    pnlRuleSubcategoria.Visible = true;

                    fieldType = "Combo";

                    FillCodeRuleGiro();

                    ddlRuleCategoria.DataSource = new List<ListboxView>() { new ListboxView() { Uid = Guid.Empty, Name = "Todos" } };
                    ddlRuleCategoria.DataTextField = "Name";
                    ddlRuleCategoria.DataValueField = "Uid";
                    ddlRuleCategoria.DataBind();

                    ddlRuleSubcategoria.DataSource = new List<ListboxView>() { new ListboxView() { Uid = Guid.Empty, Name = "Todos" } };
                    ddlRuleSubcategoria.DataTextField = "Name";
                    ddlRuleSubcategoria.DataValueField = "Uid";
                    ddlRuleSubcategoria.DataBind();

                    break;
            }

            if (fieldType == "Text")
                ddlCodeRuleOperator.DataSource = this._codeRuleOperators.Where(r => this._textRuleOperators.Contains(r.Id)).ToList();
            else
                ddlCodeRuleOperator.DataSource = this._codeRuleOperators.Where(r => this._comboRuleOperators.Contains(r.Id)).ToList();

            ddlCodeRuleOperator.DataTextField = "Name";
            ddlCodeRuleOperator.DataValueField = "Id";
            ddlCodeRuleOperator.DataBind();

            ddlCodeRuleOperator.SelectedIndex = 0;
        }

        protected void ddlRuleGiro_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Guid uid = Guid.Parse(ddlRuleGiro.SelectedValue);
                this.FillCodeRuleCategoria(uid);
            }
            catch (Exception exception)
            {
                DrawError(exception.Message);
            }
        }
        protected void ddlRuleCategoria_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Guid uid = Guid.Parse(ddlRuleCategoria.SelectedValue);
                this.FillCodeRuleSubcategoria(uid);
            }
            catch (Exception exception)
            {
                DrawError(exception.Message);
            }
        }
        protected void ddlRuleSubcategoria_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Implementation
        private void FillCountries()
        {
            var source = this.VmAddress.ReadAllCountries("Seleccionar pais");

            ddlCountry.DataSource = source;
            ddlCountry.DataValueField = "Uid";
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataBind();
        }
        private void FillStates()
        {
            Guid? uid = PromotionCodeRegion.CountryUid;
            if (uid.HasValue)
            {
                var source = this.VmAddress.ReadAllStates(uid.Value, "Todos los estados");
                ddlState.SetDataSource(source, "Uid", "Name");
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
                var source = this.VmAddress.ReadAllMunicipalities(uid.Value, "Todos los municipios");
                ddlMunicipality.SetDataSource(source, "Uid", "Name");
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
                var source = this.VmAddress.ReadAllCities(uid.Value, "Todas las ciudades");
                ddlCity.SetDataSource(source, "Uid", "Name");
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
                var source = this.VmAddress.ReadAllNeighborhoods(uid.Value, "Todas las colonias");
                ddlNeighborhood.SetDataSource(source, "Uid", "Name");
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
            var source = this.VmCodes.ReadAllCompanies(2, defaultItem: "Seleccionar empresa");
            ddlDeliveryCompany.DataSource = source;
            ddlDeliveryCompany.DataValueField = "Uid";
            ddlDeliveryCompany.DataTextField = "Name";
            ddlDeliveryCompany.DataBind();
        }
        private void FillDeliveryCompanyBranches()
        {
            var source = this.VmCodes.ReadAllCompanyBranches(this.DeliveryCompany.UidCompany.Value, "Todas las sucursales");
            ddlDeliveryCompanyBranch.SetDataSource(source, "Uid", "Name");
            ddlDeliveryCompanyBranch.DataBind();
        }

        private void FillCompanies()
        {
            var source = this.VmCodes.ReadAllCompanies(1, defaultItem: "Seleccionar empresa");
            ddlCompany.DataSource = source;
            ddlCompany.DataValueField = "Uid";
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataBind();
        }
        private void FillCompanyBranches()
        {
            var source = this.VmCodes.ReadAllCompanyBranches(this.Company.UidCompany.Value, "Todas las sucursales");
            ddlCompanyBranch.SetDataSource(source, "Uid", "Name");
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
            else if (this.CodeLevel == PromotionCodeLevel.Company)
            {
                rewards = this._CodeRewards.Where(r => this._CompanyCodeRewards.Contains(r.Id)).ToList();
                ruleTypes = this._CodeRuleValueTypes.Where(r => this._CompanyCodeRules.Contains(r.Id)).ToList();
            }
            else if (this.CodeLevel == PromotionCodeLevel.DeliveryCompany)
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
                case PromotionCodeRuleValueType.SubtotalOrder:
                    break;
                case PromotionCodeRuleValueType.ShipmentOrder:
                    break;
                case PromotionCodeRuleValueType.SubtotalPurchase:
                    break;
                case PromotionCodeRuleValueType.ShipmentPurchase:
                    break;
                case PromotionCodeRuleValueType.Product:
                    valueType = 1;
                    break;
                case PromotionCodeRuleValueType.ProductQuantity:
                    break;
                case PromotionCodeRuleValueType.Giro:
                    valueType = 2;
                    break;
                case PromotionCodeRuleValueType.Categoria:
                    valueType = 3;
                    break;
                case PromotionCodeRuleValueType.Subcategoria:
                    valueType = 4;
                    break;
            }

            if (valueType == 0) // Monto
            {
                decimal dValue = 0m;
                if (!decimal.TryParse(txtCodeRuleValue.Text.Trim(), out dValue))
                {
                    DrawError("Ingrese un valor numerico valido");
                    isValid = false;
                }
            }
            else if (valueType == 1) // Producto
            {
                if (ddlCodeRuleValue.SelectedValue == Guid.Empty.ToString())
                {
                    DrawError("Seleccione un producto valido");
                    isValid = false;
                }
            }
            else if (valueType == 2) // Giro
            {
                if (ddlRuleGiro.SelectedValue == Guid.Empty.ToString())
                {
                    DrawError("Seleccione un giro valido");
                    isValid = false;
                }
            }
            else if (valueType == 3) // Categoria
            {
                if (ddlRuleCategoria.SelectedValue == Guid.Empty.ToString())
                {
                    DrawError("Seleccione una categoria valida");
                    isValid = false;
                }
            }
            else if (valueType == 4) // Subcategoria
            {
                if (ddlRuleSubcategoria.SelectedValue == Guid.Empty.ToString())
                {
                    DrawError("Seleccione una subcategoria valida");
                    isValid = false;
                }
            }

            return isValid;
        }
        private void ClearCodeRuleFields()
        {
            pnlRuleGiroValueType.Visible = false;
            ddlCodeRuleValueType.SelectedIndex = 0;
            ddlCodeRuleOperator.SelectedIndex = 0;

            ddlCodeRuleValue.DataSource = null;
            ddlCodeRuleValue.DataBind();

            ddlRuleGiro.DataSource = null;
            ddlRuleGiro.DataBind();

            ddlRuleCategoria.DataSource = null;
            ddlRuleCategoria.DataBind();

            txtCodeRuleValue.Text = string.Empty;
        }

        public void FillCodeRuleProducts()
        {
            if (this.Company == null)
            {
                DrawError("Seleccione una empresa para visualizar los productos");
                return;
            }


            IEnumerable<ListboxView> data = new List<ListboxView>();

            if (this.Company.UidCompanyBranch.HasValue)
                data = this.VmCodes.ReadAllCompanyBranchProducts(this.Company.UidCompanyBranch.Value);

            if (this.Company.UidCompany.HasValue)
                data = this.VmCodes.ReadAllCompanyProducts(this.Company.UidCompany.Value);

            if (data.Count() == 0)
                DrawError("No se encontraron productos");

            ddlCodeRuleValue.DataSource = data;
            ddlCodeRuleValue.DataTextField = "Name";
            ddlCodeRuleValue.DataValueField = "Uid";
            ddlCodeRuleValue.DataBind();
        }
        public void FillCodeRuleGiro()
        {
            if (ddlRuleGiro.Items.Count == 0)
            {
                var data = this.VmProduct.GetAllGiros();

                var tmp = data.ToList();
                tmp.Insert(0, new Modelo.ApiResponse.StoreGiro() { Uid = Guid.Empty, Name = "Todos" });
                data = tmp;

                ddlRuleGiro.DataSource = data;
                ddlRuleGiro.DataTextField = "Name";
                ddlRuleGiro.DataValueField = "Uid";
                ddlRuleGiro.DataBind();
            }
        }
        public void FillCodeRuleCategoria(Guid uid)
        {
            var data = this.VmProduct.GetCategorias(uid);

            var tmp = data.ToList();
            tmp.Insert(0, new Modelo.ApiResponse.CommonListBox() { Uid = Guid.Empty, Name = "Todos" });
            data = tmp;

            ddlRuleCategoria.DataSource = data;
            ddlRuleCategoria.DataTextField = "Name";
            ddlRuleCategoria.DataValueField = "Uid";
            ddlRuleCategoria.DataBind();

        }
        public void FillCodeRuleSubcategoria(Guid uid)
        {
            var data = this.VmProduct.GetSubcategorias(uid);

            var tmp = data.ToList();
            tmp.Insert(0, new Modelo.ApiResponse.CommonListBox() { Uid = Guid.Empty, Name = "Todos" });
            data = tmp;

            ddlRuleSubcategoria.DataSource = data;
            ddlRuleSubcategoria.DataTextField = "Name";
            ddlRuleSubcategoria.DataValueField = "Uid";
            ddlRuleSubcategoria.DataBind();
        }

        public string GetDropDownTextFromValue(DropDownList ddl, string value)
        {
            ListItem item = ddl.Items.FindByValue(value);
            return item == null ? string.Empty : item.Text;
        }
        #endregion

        protected void ddlRewardType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CodeRewardType type = (CodeRewardType)(int.Parse(ddlRewardType.SelectedValue));
            ddlValueType.Enabled = true;

            switch (type)
            {
                case CodeRewardType.None:
                    break;
                case CodeRewardType.WalletAmount:
                    this.Code.ActivationType = PromotionCodeActivationType.EndOfPurchase;
                    ddlValueType.Enabled = false;
                    ddlValueType.SelectedValue = ((int)CodeRewardValueType.Amount).ToString();
                    break;
                case CodeRewardType.FreeDelivery:
                    this.Code.ActivationType = PromotionCodeActivationType.OnPurchase;
                    ddlValueType.Enabled = false;
                    ddlValueType.SelectedValue = ((int)CodeRewardValueType.Amount).ToString();
                    break;
                case CodeRewardType.DeliveryFixedRate:
                    this.Code.ActivationType = PromotionCodeActivationType.OnPurchase;
                    break;
                case CodeRewardType.OrderDiscount:
                    this.Code.ActivationType = PromotionCodeActivationType.OnPurchase;
                    break;
                case CodeRewardType.DeliveryOrderDiscount:
                    this.Code.ActivationType = PromotionCodeActivationType.OnPurchase;
                    break;
                case CodeRewardType.PurchaseDiscount:
                    this.Code.ActivationType = PromotionCodeActivationType.OnPurchase;
                    break;
                case CodeRewardType.DeliveryPurchaseDiscount:
                    this.Code.ActivationType = PromotionCodeActivationType.OnPurchase;
                    break;
                case CodeRewardType.OrderRefund:
                    this.Code.ActivationType = PromotionCodeActivationType.EndOfPurchase;
                    break;
                case CodeRewardType.DeliveryOrderRefund:
                    this.Code.ActivationType = PromotionCodeActivationType.EndOfPurchase;
                    break;
                case CodeRewardType.PurchaseRefund:
                    this.Code.ActivationType = PromotionCodeActivationType.EndOfPurchase;
                    break;
                case CodeRewardType.DeliveryPurchaseRefund:
                    this.Code.ActivationType = PromotionCodeActivationType.EndOfPurchase;
                    break;
            }
        }
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
        public int Status { get; set; }
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