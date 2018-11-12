using System;
using System.Collections.Generic;
using System.Text;

namespace ADC.Stateful.OnlineForm.Model
{
    public class FormContentModel
    {
        public Guid SubmissionId { get; set; }
        public int DataSourceId { get; set; }
        public int TrustId { get; set; }
        public string CoAJsonString { get; set; }
    }
}
