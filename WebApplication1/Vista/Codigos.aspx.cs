using DataAccess.Models;
using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VistaDelModelo;

namespace WebApplication1.Vista
{
    public partial class Codigos : System.Web.UI.Page
    {
        #region Properties
        public Guid PromotionCodeUid
        {
            get => (Guid)ViewState["promotioncodeuid"];
            set => ViewState["promotioncodeuid"] = value;
        }

        private int SelectedCodeTab
        {
            get => (int)ViewState["selectedcodetab"];
            set => ViewState["selectedcodetab"] = value;
        }

        private readonly IList<ListboxViewInteger> _codeRewards = new List<ListboxViewInteger>()
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

        private readonly IList<ListboxViewInteger> _codeRuleValueTypes = new List<ListboxViewInteger>() {
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

        #region View Model

        private CodesViewModel VmCodes = new CodesViewModel("");
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlRewardType.DataSource = this._codeRewards;
                ddlRewardType.DataValueField = "Id";
                ddlRewardType.DataTextField = "Name";
                ddlRewardType.DataBind();

                //ddlValueType.DataSource = this._codeRuleValueTypes;
                //ddlValueType.DataValueField = "Id";
                //ddlValueType.DataTextField = "Name";
                //ddlValueType.DataBind();

                gvPromotionCodes.DataSource = VmCodes.ReadAllPromotionCodes().ToList();
                gvPromotionCodes.DataBind();
            }
        }

        #region UI
        protected void gvPromotionCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void gvPromotionCodes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    this.PromotionCodeUid = new Guid(e.CommandArgument.ToString());
                    this.FillPromotionCode();
                }
            }
            catch (Exception ex)
            {
                // TODO:
            }
        }

        protected void CodeTab_Clicked(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;

            this.SelectedCodeTab = int.Parse(e.CommandArgument.ToString());
            mvTabs.ActiveViewIndex = this.SelectedCodeTab;

            tab0.Attributes.Add("class", this.SelectedCodeTab == 0 ? "active" : "");
            tab1.Attributes.Add("class", this.SelectedCodeTab == 1 ? "active" : "");
            tab2.Attributes.Add("class", this.SelectedCodeTab == 2 ? "active" : "");
            tab3.Attributes.Add("class", this.SelectedCodeTab == 3 ? "active" : "");
            tab4.Attributes.Add("class", this.SelectedCodeTab == 4 ? "active" : "");
        }
        #endregion

        #region Implementation
        public void FillPromotionCode()
        {
            var code = this.VmCodes.GetPromotionCode(this.PromotionCodeUid);

            txtCode.Text = code.Code;
            ddlRewardType.SelectedValue = ((int)code.RewardType).ToString();
            ddlValueType.SelectedValue = ((int)code.ValueType).ToString();
            txtValue.Text = code.Value.ToString();
            lblValueType.Text = code.ValueType == DataAccess.Enum.CodeRewardValueType.Amount ? "$" : "%";
            ddlCodeStatus.SelectedValue = ((int)code.Status).ToString();
            ddlActivationType.SelectedValue = ((int)code.ActivationType).ToString();

            txtStartAt.Text = code.StartAt.ToString("yyyy-MM-dd");
            ddlCodeExpirationType.SelectedValue = ((int)code.ExpirationType).ToString();

            pnlExpirationDate.Visible = false;
            pnlDaysBeforeActivation.Visible = false;
            pnlActivationNumber.Visible = false;

            switch (code.ExpirationType)
            {
                case DataAccess.Enum.CodeExpirationType.None:
                    break;
                case DataAccess.Enum.CodeExpirationType.Date:
                    pnlExpirationDate.Visible = true;
                    txtExpirationDate.Text = code.ExpirationDate.Value.ToString("yyyy-MM-dd");
                    break;
                case DataAccess.Enum.CodeExpirationType.Activations:
                    pnlActivationNumber.Visible = true;
                    txtActivationsNumber.Text = code.Activations.ToString();
                    break;
                case DataAccess.Enum.CodeExpirationType.DaysBeforeActivations:
                    pnlDaysBeforeActivation.Visible = true;
                    txtDaysBeforeActivation.Text = code.DaysAfterActivation.ToString();
                    break;
            }
        }

        public string GetTabClass(object wStep)
        {
            if (wStep == null)
                return "";

            int stepIndex = (int)wStep;

            if (stepIndex > SelectedCodeTab)
            {
                return "active";
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}