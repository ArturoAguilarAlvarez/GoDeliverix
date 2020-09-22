using DBControl;
using Modelo;
using Modelo.Enums;
using Modelo.OneSignal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class NotificationViewModel
    {
        protected readonly string OneSignalApiKey = "MzM4YzhhYTktOTVmNC00MzUxLWFkOGMtZTYwYjE5MTVmN2I3";
        protected readonly string OneSignalApplicationId = "2964843e-3741-468d-a8be-8b6d78df6759";
        private NotificationDataAccess _NotificationDataAccess { get; set; }

        public NotificationViewModel()
        {
            this._NotificationDataAccess = new NotificationDataAccess();
        }

        public IEnumerable<Notification> ReadAllNotifications(NotificationTarget target, Guid uid)
        {
            DataTable data = this._NotificationDataAccess.ReadAllNotifications((int)target, uid);
            List<Notification> notifications = new List<Notification>();
            foreach (DataRow row in data.Rows)
            {
                notifications.Add(new Notification()
                {
                    Uid = row.IsNull("Uid") ? Guid.Empty : (Guid)row["Uid"],
                    Target = row.IsNull("Target") ? NotificationTarget.None : (NotificationTarget)row["Target"],
                    TargetUid = row.IsNull("TargetUid") ? Guid.Empty : (Guid)row["TargetUid"],
                    JsonTitle = row.IsNull("JsonTitle") ? string.Empty : (string)row["JsonTitle"],
                    JsonContent = row.IsNull("JsonContent") ? string.Empty : (string)row["JsonContent"],
                    Type = row.IsNull("Type") ? NotificationType.None : (NotificationType)row["Type"],
                    EntityTypeUid = row.IsNull("EntityTypeUid") ? Guid.Empty : (Guid)row["EntityTypeUid"],
                    CreatedDate = (DateTime)row["CreatedDate"],
                });
            }
            return notifications.AsEnumerable();
        }

        public bool CreateAndRegisterNotification(NotificationTarget target, Guid targetUid, NotificationTitle title, NotificationContent content, NotificationType type, Guid typeUid)
        {
            try
            {
                this._NotificationDataAccess.Add((int)target, targetUid, JsonConvert.SerializeObject(title), JsonConvert.SerializeObject(content), (int)type, typeUid);

                CreateNotification create = new CreateNotification()
                {
                    Title = title,
                    Content = content,
                    IncludeExternalUserIds = new List<string>() { targetUid.ToString().ToLower() }
                };

                this.CreateOneSignalNotification(create);

                return true;
            }
            catch (Exception ex)
            {
#if true
                Console.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #region One Signal
        public void CreateOneSignalNotification(CreateNotification create)
        {
            HttpWebRequest request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", $"Basic {OneSignalApiKey}");

            create.ApplicationId = this.OneSignalApplicationId;

            string json = JsonConvert.SerializeObject(create);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }

                CreateNotificationResult result = JsonConvert.DeserializeObject<CreateNotificationResult>(responseContent);
            }
            catch (WebException ex)
            {

            }
        }
        #endregion
    }
}
