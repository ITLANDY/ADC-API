using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADC.Stateful.AAR.DataModel
{
    public class FormDataModel
    {
        public Guid SubmissionId { get; set; }

        public int DataSourceId { get; set; }

        public int TrustId { get; set; }

        [JsonIgnore]
        private Dictionary<string, object> CoAJson { get; set; }

        [JsonProperty("CoAJson")]
        private Dictionary<string,object> CoAJsonSetter
        {
            set { CoAJson = value; }
        }

        public string CoAJsonString
        {
            get
            {
                return JsonConvert.SerializeObject(CoAJson);
            }

            set
            {
                CoAJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
            }
        }
    }
}
