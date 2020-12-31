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

        public IEnumerable<WalletTransactionGrid> ReadAllTransactions(Guid uidUser, Guid? uidConcept = null, Guid? uidType = null)
        {
            List<WalletTransactionGrid> result = new List<WalletTransactionGrid>();
            DataTable data = this.WalletData.ReadAllUserTransactions(uidUser, uidConcept, uidType);

            foreach (DataRow row in data.Rows)
            {
                result.Add(new WalletTransactionGrid()
                {
                    Uid = (Guid)row["Uid"],
                    UidConcept = (Guid)row["UidConcept"],
                    UidType = (Guid)row["UidType"],
                    Amount = (decimal)row["Amount"],
                    Concept = (string)row["Concept"],
                    Date = (DateTime)row["Date"],
                    Folio = (long)row["Folio"],
                    Type = (string)row["Type"],
                    FolioOrdenSucursal = row.IsNull("FolioOrdenSucursal") ? null : (long?)row["FolioOrdenSucursal"]
                });
            }

            return result;
        }
    }
}
