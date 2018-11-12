using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADC.Stateful.DBService.Models;

namespace ADC.Stateful.DBService.Interfaces
{
    public interface IDBService : IService
    {
        //Originaly built using IAsyncEnumerable but got "cannot be serialised" error
        Task<IEnumerable<SubmissionDataJson>> GetAllFromStagingDBAsync();
        
        Task<SubmissionDataJson> GetFromStagingDBAsync(int trustId);

        Task<int> AddToStagingDBAsync(SubmissionDataJson submission);

        //Task<bool> UpdateStagingDBAsync(string testString);
        //Task<bool> DeleteFromStagingDBAsync(string testString);
    }
}
