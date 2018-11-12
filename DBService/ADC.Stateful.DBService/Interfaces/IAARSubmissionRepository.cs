using ADC.Stateful.DBService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADC.Stateful.DBService.Interfaces
{
    interface IAARSubmissionRepository
    {
        Task<IEnumerable<SubmissionDataJson>> GetAll();

        Task<SubmissionDataJson> Get(Guid submissionId);

        Task Add(SubmissionDataJson submissionDataJson);
    }
}
