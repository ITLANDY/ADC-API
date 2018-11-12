using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ADC.Stateful.DBService.Interfaces;
using ADC.Stateful.DBService.Models;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace ADC.Stateful.DBService
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class DBService : StatefulService, IDBService
    { 
        private readonly DFETestContext _dbContext;

        public DBService(StatefulServiceContext context, DFETestContext dbContext)
            : base(context)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SubmissionDataJson>> GetAllFromStagingDBAsync()
        {
            return await Task.FromResult(_dbContext.SubmissionDataJson.ToList());
        }

        public async Task<SubmissionDataJson> GetFromStagingDBAsync(int trustId)
        {
            return await Task.FromResult(_dbContext.SubmissionDataJson.Where(x => x.TrustId == trustId)?.LastOrDefault());
            //return await Task.FromResult(_dbContext.SubmissionDataJson.SingleOrDefault(x => x.SubmissionId == submissionId));
        }

        public async Task<int> AddToStagingDBAsync(SubmissionDataJson submission)
        {
            _dbContext.SubmissionDataJson.Add(submission);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            //For Service Remoting
            return this.CreateServiceRemotingReplicaListeners();

            //For HTTP
            //return new ServiceReplicaListener[]
            //{
            //    new ServiceReplicaListener(serviceContext =>
            //        new KestrelCommunicationListener(serviceContext, (url, listener) =>
            //        {
            //            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

            //            return new WebHostBuilder()
            //                        .UseKestrel()
            //                        .ConfigureServices(
            //                            services => services
            //                                .AddSingleton<StatefulServiceContext>(serviceContext)
            //                                .AddSingleton<IReliableStateManager>(this.StateManager))
            //                        .UseContentRoot(Directory.GetCurrentDirectory())
            //                        .UseStartup<Startup>()
            //                        .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
            //                        .UseUrls(url)
            //                        .Build();
            //        }))
            //};
        }
    }
}
