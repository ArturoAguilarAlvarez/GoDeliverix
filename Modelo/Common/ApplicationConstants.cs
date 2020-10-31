using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Common
{
    public static class ApplicationConstants
    {
        /// <summary>
        /// Firebase Data Base Server Url
        /// </summary>
        public static string FirebaseBaseUrl = "https://godeliverix-customers.firebaseio.com/";

        /// <summary>
        /// Firebase Secret Key
        /// <see cref="https://stackoverflow.com/questions/37418372/firebase-where-is-my-account-secret-in-the-new-console"/>
        /// </summary>
        public static string FirebaseSecretKey = "";

        /// <summary>
        /// Google Maps Direccions Api Url
        /// </summary>
        public static string GoogleDirectionApiBaseURl = "https://maps.googleapis.com/maps/";

        /// <summary>
        /// Google Maps Direccion Api Key
        /// </summary>
        public static string GoogleDirectionApiKey = "";
    }
}
