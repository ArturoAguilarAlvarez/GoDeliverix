using DataAccess;
using Modelo.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class CodesViewModel
    {
        public UserSignInRewardCodeDb SignInRewardCodeDb { get; }
        public SignInRewardCodesConfigDb SignInRewardCodeConfigDb { get; }

        public CodesViewModel()
        {
            this.SignInRewardCodeDb = new UserSignInRewardCodeDb();
            this.SignInRewardCodeConfigDb = new SignInRewardCodesConfigDb();
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
        public int VerifyAndApplySignInCode(Guid uidUser,string code)
        {
            return this.SignInRewardCodeDb.VerifyAndApplyCode(uidUser, code);
        }

        public IEnumerable<UserSignInRewardCode> GetAllParentChildRedeems(Guid UidCode)
        {
            return this.SignInRewardCodeDb.ReadAllChildUserCodes(UidCode);
        }
        #endregion
    }
}
