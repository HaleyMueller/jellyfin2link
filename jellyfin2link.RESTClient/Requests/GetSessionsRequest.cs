using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace jellyfin2link.RESTClient.Requests
{
    public class GetSessionsRequest
    {
        public Guid? ControllableByUserId { get; set; }
        public string? DeviceId { get; set; }
        public int? ActiveWithinSeconds { get; set; }

        public override string ToString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (ControllableByUserId.HasValue)
                query["controllableByUserId"] = ControllableByUserId.Value.ToString();

            if (!string.IsNullOrEmpty(DeviceId))
                query["deviceId"] = DeviceId;

            if (ActiveWithinSeconds.HasValue)
                query["activeWithinSeconds"] = ActiveWithinSeconds.Value.ToString();

            string queryString = query.ToString();
            return string.IsNullOrEmpty(queryString) ? string.Empty : "?" + queryString;
        }
    }
}
