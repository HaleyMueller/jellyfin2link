using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jellyfin2link.DataEntities
{
    public class APIResponse<T>
    {
        public string DefaultSuccessMessage;

        public APIResponse(string defaultSuccessMessage)
        {
            DefaultSuccessMessage = defaultSuccessMessage;

            AddMessage(Notification.NotificationEnum.Success, DefaultSuccessMessage);
        }

        public T Data { get; set; }
        private List<Notification> _notifications { get; set; } = new List<Notification>();
        public List<Notification> Notifications
        {
            get
            {
                if (HasError)
                {
                    _notifications.RemoveAll(x => x.NotificationType == Notification.NotificationEnum.Success);
                }

                return _notifications;
            }
        }

        public bool HasError
        {
            get
            {
                if (_notifications.Any(x => x.NotificationType == Notification.NotificationEnum.Error))
                {
                    return true;
                }

                return false;
            }
        }

        public void AddExecption(Exception ex)
        {
            AddMessage(Notification.NotificationEnum.Error, ex.ToString(), "");
        }

        public void AddError(string fieldID, string errorMessage)
        {
            AddMessage(Notification.NotificationEnum.Error, errorMessage, fieldID);
        }

        public void AddMessage(Notification.NotificationEnum type, string message)
        {
            AddMessage(type, message, "");
        }

        public void AddMessage(Notification.NotificationEnum type, string message, string fieldName)
        {
            if (type == Notification.NotificationEnum.Success && message != DefaultSuccessMessage)
            {
                _notifications.RemoveAll(x => x.NotificationType == Notification.NotificationEnum.Success && x.Message == DefaultSuccessMessage);
            }

            this._notifications.Add(new Notification()
            {
                FieldName = fieldName,
                Message = message,
                NotificationType = type
            });
        }


    }
    public class Notification
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
        public NotificationEnum NotificationType { get; set; }

        public enum NotificationEnum
        {
            Info,
            Warning,
            Error,
            Success
        }
    }
}
