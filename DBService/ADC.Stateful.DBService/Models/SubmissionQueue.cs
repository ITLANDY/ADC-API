using ADC.Stateful.DBService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADC.Stateful.DBService.Models
{
    public class SubmissionQueue : IAARSubmissionRepository
    {
        //This will be a queue!!!! ...but it's not yet one
#pragma warning disable 
        public async Task Add(SubmissionDataJson submissionDataJson)
        {
            throw new NotImplementedException();
        }

        public async Task<SubmissionDataJson> Get(Guid submissionId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SubmissionDataJson>> GetAll()
        {
            throw new NotImplementedException();
        }
#pragma warning restore
    }
}
