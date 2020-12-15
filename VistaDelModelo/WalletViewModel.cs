using DBControl;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class WalletViewModel
    {
        private WalletDataAccess WalletData { get; }

        public WalletViewModel()
        {
            this.WalletData = new WalletDataAccess();
        }

        public Wallet GetUserWalletBalance(Guid uidUser)
        {
            Wallet result = null;
            DataTable data = this.WalletData.GetByUserId(uidUser);

            foreach (DataRow row in data.Rows)
            {
                result = new Wallet()
                {
                    Uid = (Guid)row["UidMonedero"],
                    UidUser = (Guid)row["UidUsuario"],
                    Amount = (Decimal)row["MMonto"],
                    CreatedDate = row.IsNull("DtmFechaDeCreacion") ? null : (DateTime?)row["DtmFechaDeCreacion"]
                };
            }

            return result;
        }
    }
}
