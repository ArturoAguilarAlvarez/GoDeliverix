using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppPrueba.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";           
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStore.GetItemsAsync(true);
                IsBusy = false;
            }
            catch (Exception)
            {
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
