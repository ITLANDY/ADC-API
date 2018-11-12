using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using ADC.Stateful.DBService.Interfaces;
using ADC.Stateful.OnlineForm.Interface;
using ADC.Stateful.OnlineForm.Model;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Newtonsoft.Json;

namespace ADC.Stateful.OnlineForm
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class OnlineForm : StatefulService, IOnlineFormService
    {
        private readonly IDBService _dbService;

        public OnlineForm(StatefulServiceContext context)
            : base(context)
        {
            _dbService = ServiceProxy.Create<IDBService>(new Uri("fabric:/ADC/ADC.Stateful.DBService"), new ServicePartitionKey(0));

        }

        public async Task<FormContentModel> RetriveContentForAsync(int trustId)
        {
            //string sampleCOADataString = "{\"1556\": \"3632.77\", \"1557\": \"-7023.99\", \"1558\": \"88.89\", \"1559\": \"-333.24\", \"1560\": \"8168.60\", \"4000\": \"7871.64\", \"4002\": \"-2986.61\", \"4003\": \"503.84\", \"4004\": \"602.90\", \"4005\": \"27.74\", \"4006\": \"3260.90\", \"4007\": \"1.0\"}";

            //object mockRetrivedData = new FormContentModel()
            //{
            //    SubmissionId = submissionId,
            //    TrustId = trustId,
            //    DataSourceId = 1,
            //    CoAJsonString = sampleCOADataString
            //};

            object formData = await _dbService.GetFromStagingDBAsync(trustId);

            var dataString = JsonConvert.SerializeObject(formData);

            FormContentModel formContent = JsonConvert.DeserializeObject<FormContentModel>(dataString);

            return await Task.FromResult(formContent);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }

        /*
        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                    // discarded, and nothing is saved to the secondary replicas.
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
        */
    }
}
