using System;
using System.Collections.Generic;
//using System.Runtime.Serialization;

namespace ADC.Stateful.DBService.Models
{

    //[DataContract]
    public partial class SubmissionDataJson
    {
        public int SubmissionDataId { get; set; }

        //[DataMember]
        public Guid SubmissionId { get; set; }

        //[DataMember]
        public int DataSourceId { get; set; }

        //[DataMember]
        public int TrustId { get; set; }

        //[DataMember]
        public string CoAJsonString { get; set; }
    }
}
