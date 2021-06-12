using DataAccess;
using DataAccess.Entities;
using DataAccess.Enum;
using DataAccess.Models;
using DataAccess.Util;
using Modelo.DbContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.v2;
using VistaDelModelo.Services;

namespace VistaDelModelo
{
    public class CodesViewModel
    {
        protected string ServerPath { get; }
        public UserSignInRewardCodeDb SignInRewardCodeDb { get; }
        public SignInRewardCodesConfigDb SignInRewardCodeConfigDb { get; }
        private CodeDb _CodeDb { get; }
        private EmailService _EmailService { get; }

        public CodesViewModel(string serverPath)
        {
            this.SignInRewardCodeDb = new UserSignInRewardCodeDb();
            this.SignInRewardCodeConfigDb = new SignInRewardCodesConfigDb();
            this._CodeDb = new CodeDb();
            this._EmailService = new EmailService();

            this.ServerPath = serverPath;
        }

        #region Sign In Codes Config
        public SignInRewardCodesConfig GetSignInCodesConfig()
        {
            return this.SignInRewardCodeConfigDb.GetConfig();
        }
        #endregion

        #region Sign In Codes
        public int VerifySignInCode(string code)
        {
            return this.SignInRewardCodeDb.VerifyCode(code);
        }

        public UserSignInRewardCode GetSignInByUserUid(Guid uid)
        {
            return this.SignInRewardCodeDb.GetByUserUid(uid);
        }
        public int VerifyAndApplySignInCode(Guid uidUser, string code)
        {
            return this.SignInRewardCodeDb.VerifyAndApplyCode(uidUser, code);
        }

        public IEnumerable<UserSignInRewardCode> GetAllParentChildRedeems(Guid UidCode)
        {
            return this.SignInRewardCodeDb.ReadAllChildUserCodes(UidCode);
        }
        #endregion

        #region Codes
        public async Task<CodeValidationResult> VerifyCodeAsync(string strCode)
        {
            CodeValidationResult result = CodeValidationResult.CodeNotFound;

            Codes code = await this._CodeDb.FindCodeAsync(strCode);

            if (code != null)
            {
                switch (code.Type)
                {
                    case CodeType.UserNetwork:
                        result = await this.ValidateUserNetworkCode(code.Uid);
                        break;
                    default:
                        result = CodeValidationResult.CodeNotFound;
                        break;
                }
            }

            return result;
        }
        public async Task<CodeValidationResult> VerifyCodeAsync(Guid uidUser, string strCode)
        {
            Codes code = await this._CodeDb.FindCodeAsync(strCode);

            if (code == null)
                return CodeValidationResult.CodeNotFound;

            CodeValidationResult result = CodeValidationResult.Invalid;

            switch (code.Type)
            {
                case CodeType.UserNetwork:
                    result = await ValidateUserNetworkCode(uidUser, code.Uid);
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion

        #region User Network Code
        public async Task<CodeValidationResult> ValidateUserNetworkCode(Guid uidCode)
        {
            CodeValidationResult result = CodeValidationResult.CodeNotFound;
            DateTime tNow = DateTime.Now;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime Now = TimeZoneInfo.ConvertTime(tNow, tzi);

            UserCodeNetworkConfig config = await this._CodeDb.GetConfigAsync();
            UserCodeNetworkGlobalStats stats = await this._CodeDb.GetGlobalStats();
            UserCodeNetwork userCode = await this._CodeDb.GetByCodeUid(uidCode);

            if (userCode == null)
            {
                return CodeValidationResult.CodeNotFound;
            }

            if (config.ExpirationDate.HasValue)
            {
                if (DateTime.Compare(Now, config.ExpirationDate.Value) < 0)
                    return CodeValidationResult.Expired;
            }

            if (config.GlobalShareLimit.HasValue)
            {
                if (config.GlobalShareLimit > stats.Activations)
                    return CodeValidationResult.Expired;
            }

            if (config.UserShareLimit.HasValue)
            {
                if (config.UserShareLimit.Value > userCode.Activations)
                {
                    return CodeValidationResult.Invalid;
                }
            }

            result = CodeValidationResult.Valid;

            return result;
        }
        public async Task<CodeValidationResult> ValidateUserNetworkCode(Guid uidUser, Guid uidCode)
        {
            DateTime Now = DateTimeUtils.GetTimeZoneDate();

            UserCodeNetworkConfig config = await this._CodeDb.GetConfigAsync();
            UserCodeNetworkGlobalStats stats = await this._CodeDb.GetGlobalStats();

            Codes code = await this._CodeDb.GetAsync<Codes>(uidCode);
            UserCodeNetwork guestCode = await this._CodeDb.GetByUserUid(uidUser);
            UserCodeNetwork ownerCode = await this._CodeDb.GetByCodeUid(uidCode);

            // Verify Guest Code
            if (guestCode == null)
            {
                this._CodeDb.BeginTransaction();

                try
                {
                    Guid? uid = await this._CodeDb.Add(CodeType.UserNetwork);
                    this._CodeDb.Insert(new UserCodeNetwork() { Activations = 0, CodeUid = uid.Value, UserUid = uidUser, CreatedDate = Now });
                    this._CodeDb.CommitTransaction();

                    guestCode = await this._CodeDb.GetByUserUid(uidUser);
                    if (guestCode == null)
                        throw new ArgumentNullException("Cant create user network code");
                }
                catch (Exception ex)
                {
                    try
                    {
                        this._CodeDb.RollbackTransaction();
                    }
                    catch (Exception exc) { /* Prevent Db Execution Error*/ }

                    return CodeValidationResult.Error;
                }
            }

            // verificar si existe el codigo
            if (ownerCode == null)
            {
                return CodeValidationResult.CodeNotFound;
            }

            // Verificar fecha de expiracion del codigo
            if (config.ExpirationDate.HasValue)
            {
                if (DateTime.Compare(Now, config.ExpirationDate.Value) < 0)
                    return CodeValidationResult.Expired;
            }

            // Verificar limite de activaciones globales
            if (config.GlobalShareLimit.HasValue)
            {
                if (config.GlobalShareLimit > stats.Activations)
                    return CodeValidationResult.Expired;
            }

            // Verificar limite de activaciones del usuario
            if (config.UserShareLimit.HasValue)
            {
                if (config.UserShareLimit.Value > ownerCode.Activations)
                {
                    return CodeValidationResult.Invalid;
                }
            }

            // Verificar si el usuario ingreso su propio codigo
            if (guestCode.UserUid.Equals(ownerCode.UserUid))
                return CodeValidationResult.Invalid;

            // Verificar si el usuario tiene un codigo asociado
            if (guestCode.OwnerCodeUid.HasValue)
                return CodeValidationResult.UserHasCode;

            // Iniciar proceso de activacion de codigo
            this._CodeDb.BeginTransaction();
            try
            {
                this._CodeDb.ReferenceUserNetworkCode(guestCode.Uid, ownerCode.Uid);
                await this._CodeDb.InsertAsync(new UserCodeNetworkLog()
                {
                    UserCodeNetworkUid = ownerCode.Uid,
                    Type = UserCodeNetworkLogType.Shared,
                    SharedCodeNetworkUid = guestCode.Uid,
                    CreatedDate = Now
                });

                await this._CodeDb.InsertAsync(new UserCodeNetworkLog()
                {
                    UserCodeNetworkUid = guestCode.Uid,
                    Type = UserCodeNetworkLogType.AssociateOwner,
                    SharedCodeNetworkUid = ownerCode.Uid,
                    CreatedDate = Now
                });
                this._CodeDb.CommitTransaction();
            }
            catch (Exception ex)
            {
                try
                {
                    this._CodeDb.RollbackTransaction();
                }
                catch (Exception) { /* Prevent Db Execution Error */ }

                return CodeValidationResult.Error;
            }

            try
            {
                this.SendOwnerActivationCodeNotification(ownerCode.UserUid);
            }
            catch (Exception ex) { /*Email exception*/ }

            return CodeValidationResult.Valid;
        }

        public async Task VerifyUserNetworkCode(Guid uidUser, Guid uidPurchase)
        {
            try
            {
                // obtener codigo del usuario
                UserCodeNetwork userCode = await this._CodeDb.GetByUserUid(uidUser);
                UserCodeNetworkPurchaseDetail purchase = null;
                UserCodeNetworkConfig config = null;

                // Verificar si existe el codigo del usuario
                if (userCode == null)
                    return;

                // Verificar si tiene una asociacion
                if (!userCode.OwnerCodeUid.HasValue)
                    return;

                purchase = await _CodeDb.GetPurchaseDetailAsync(uidPurchase);

                // verificar datos de la compra
                if (purchase == null)
                    return;

                // verificar si se completo la compra
                if (!purchase.IsCompleted)
                    return;

                // obtener configuracion de codigos
                config = await this._CodeDb.GetConfigAsync();

                // Realizar proceso de recompesa Owner
                if (config.OwnerRewardType != CodeRewardType.None)
                {
                    // obtener datos del codigo del owner
                    UserCodeNetwork userCodeOwner = await _CodeDb.GetAsync<UserCodeNetwork>(userCode.OwnerCodeUid.Value);
                    if (userCodeOwner == null)
                        return;

                    // verificar limite de recompensas
                    if (config.OwnerRewardLimit.HasValue)
                        if (config.OwnerRewardLimit.Value == userCodeOwner.Rewards)
                            return;

                    // verificar si monto monedero no es null
                    if (config.OwnerRewardType == CodeRewardType.WalletAmount && !purchase.AvailableToRefundOwner.HasValue)
                        return;

                    // verificar si monto monedero no es 0
                    if (config.OwnerRewardType == CodeRewardType.WalletAmount && purchase.AvailableToRefundOwner.Value == 0)
                        return;

                    bool success = await this._CodeDb.RegistryUserCodeNetworkReward(
                        userCodeOwner.Uid,
                        userCode.Uid,
                        userCodeOwner.UserUid,
                        config.OwnerRewardType,
                        purchase.AvailableToRefundOwner.Value);

                    if (config.OwnerRewardType == CodeRewardType.WalletAmount)
                    {
                        this.SendUserNetworkCodeRewardedAmount(userCodeOwner.UserUid, purchase.AvailableToRefundOwner.Value);
                    }
                }

                // Realizar proceso de recompesa usuario
                if (config.GuestRewardType != CodeRewardType.None)
                {
                    // verificar limite de recompensas
                    if (config.GuestRewardLimit.HasValue)
                        if (config.GuestRewardLimit.Value == userCode.Rewards)
                            return;

                    // verificar si monto monedero no es null
                    if (config.GuestRewardType == CodeRewardType.WalletAmount && !purchase.AvailableToRefundGuest.HasValue)
                        return;

                    // verificar si monto monedero no es 0
                    if (config.GuestRewardType == CodeRewardType.WalletAmount && purchase.AvailableToRefundGuest.Value == 0)
                        return;

                    bool success = await this._CodeDb.RegistryUserCodeNetworkReward(
                        userCode.Uid,
                        null,
                        userCode.UserUid,
                        config.GuestRewardType,
                        purchase.AvailableToRefundGuest.Value);

                    if (config.GuestRewardType == CodeRewardType.WalletAmount)
                    {
                        this.SendUserNetworkCodeRewardedAmount(userCode.UserUid, purchase.AvailableToRefundGuest.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                // TODO: Save this
            }
        }

        public void SendOwnerActivationCodeNotification(Guid uidUser)
        {
            UserDetails user = this._CodeDb.GetUser(uidUser);
            if (user == null)
                return;

            string templatePath = $@"{this.ServerPath}Resources\EmailTemplate\UserCodeNetwork\gd_code_activated_{user.Lang.ToLower()}.html";

            string template = File.ReadAllText(templatePath);

            string subject = string.Empty;
            switch (user.Lang)
            {
                case "en":
                    subject = "GoDeliverix - A new friend activated your user code";
                    break;
                case "es":
                    subject = "GoDeliverix - Un nuevo amigo activó tu código de usuario";
                    break;
                default:
                    subject = "";
                    break;
            }

            this._EmailService.SendEmail(template, user.Email, subject, lang: user.Lang);
        }

        public void SendUserNetworkCodeRewardedAmount(Guid uidUser, decimal amount)
        {
            UserDetails user = this._CodeDb.GetUser(uidUser);
            if (user == null)
                return;

            string templatePath = $@"{this.ServerPath}Resources\EmailTemplate\UserCodeNetwork\gd_rewarded_{user.Lang.ToLower()}.html";

            string template = File.ReadAllText(templatePath);

            template = template.Replace("{{amount}}", amount.ToString("C2"));

            string subject = string.Empty;
            switch (user.Lang)
            {
                case "en":
                    subject = "GoDeliverix - You have received a reward in the wallet";
                    break;
                case "es":
                    subject = "GoDeliverix - Has recibido una recompensa en el monedero";
                    break;
                default:
                    subject = "";
                    break;
            }

            this._EmailService.SendEmail(template, user.Email, subject, lang: user.Lang);
        }

        public ApplyPurchaseReward GetPurchaseInfoToApplyReward(Guid uidOrden)
        {
            return this._CodeDb.GetPurchaseInfoToApplyReward(uidOrden);
        }
        #endregion

        #region View Resources
        public IEnumerable<ListboxView> ReadAllCompanies(int type, Guid? uid = null, string defaultItem = "")
        {
            var data = this._CodeDb.ReadAllCompanies(type, uid);
            if (defaultItem != string.Empty)
            {
                var tmp = data.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultItem });
                data = tmp;
            }
            return data;
        }
        public IEnumerable<ListboxView> ReadAllCompanyBranches(Guid uid, string defaultItem = "")
        {
            var data = this._CodeDb.ReadAllCompanyBranches(uid);
            if (defaultItem != string.Empty)
            {
                var tmp = data.ToList();
                tmp.Insert(0, new ListboxView() { Uid = Guid.Empty, Name = defaultItem });
                data = tmp;
            }
            return data;
        }
        #endregion

        #region Promotion Codes

        public bool AddPromotionCode(
            PromotionCodeLevel level,
            string code,
            CodeRewardType rewardType,
            CodeRewardValueType valueType,
            decimal value,
            PromotionCodeActivationType activationType,
            CodeExpirationType expirationType,
            DateTime startAt,
            DateTime? expiredAt = null,
            int? expirationValue = null,
            PromotionCodeBusiness company = null,
            PromotionCodeBusiness deliveryCompany = null,
            PromotionCodeGeography geography = null,
            IEnumerable<PromotionCodeRuleView> rules = null)
        {
            return this._CodeDb.AddPromotionCode(level ,code, rewardType, valueType, value, activationType, expirationType, startAt, expiredAt, expirationValue, company, deliveryCompany, geography, rules);
        }
        #endregion
    }
}
