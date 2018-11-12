using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADC.Stateless.API.ResponseModel
{
    public class OnlineFormResponseModel
    {
        [JsonProperty("submissionId")]
        public Guid SubmissionId { get; set; }

        [JsonProperty("dataSourceId")]
        public int DataSourceId { get; set; }

        [JsonProperty("trustId")]
        public int TrustId { get; set; }

        [JsonIgnore]
        private string CoAJsonString { get; set; }

        [JsonProperty("CoAJsonString")]
        private string CoAJsonStringSetter
        {
            set { CoAJsonString = value; }
        }

        [JsonProperty("coaJson")]
        public object CoAJson
        {
            get
            {
                return JsonConvert.DeserializeObject(CoAJsonString);
            }
        }
    }
}
