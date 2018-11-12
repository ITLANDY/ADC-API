using System.Fabric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADC.Stateful.DBService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using ADC.Stateful.DBService.Models;

namespace ADC.Stateless.DBServiceAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AARController : ControllerBase
    {

        private readonly IDBService _dbService;

        public AARController()
        {
            _dbService = ServiceProxy.Create<IDBService>(new Uri("fabric:/ADC/ADC.Stateful.DBService"), new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<SubmissionDataJson>> Get()
        {
            return await _dbService.GetAllFromStagingDBAsync();
        }

        // GET api/aar/5
        [HttpGet("{trustId}")]
        public async Task<ActionResult<SubmissionDataJson>> Get(int trustId)
        {
            return await _dbService.GetFromStagingDBAsync(trustId);
            //return await _dbService.GetFromStagingDBAsync(new Guid("AAAAEBBC-EC22-4F78-82C7-39770680F92B"));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SubmissionDataJson newSub)
        {
            await _dbService.AddToStagingDBAsync(newSub);
            return new OkResult();

            /* EXAMPLE REQUEST BODY:
                {
	                "SubmissionId": "ab318f1d-775b-4a05-b0f8-8fc6b6310b18",
	                "DataSourceId": 1,
	                "TrustId": 11,
	                "coAJsonString": "{ \"valid\" : \"json object\" }"
                }
            */
        }

        /*
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;
        private readonly string reverseProxyBaseUri;
        private readonly StatelessServiceContext serviceContext;

        //public AARController(HttpClient httpClient, StatelessServiceContext context, FabricClient fabricClient)
        //{
        //    this.fabricClient = fabricClient;
        //    this.httpClient = httpClient;
        //    this.serviceContext = context;
        //    this.reverseProxyBaseUri = Environment.GetEnvironmentVariable("ReverseProxyBaseUri");
        //}

        // GET: api/aar
        //[HttpGet("")]
        [HttpGet]
        public async Task<ActionResult<List<KeyValuePair<string, int>>>> Get()
        {
            var ret = new List<KeyValuePair<string, int>>();

            ret.Add(new KeyValuePair<string, int>("hi", 123));

            return ret;
            /*
            Uri serviceName = DBServiceAPI.GetStatefulDBServiceName(this.serviceContext);
            Uri proxyAddress = this.GetProxyAddress(serviceName);

            ServicePartitionList partitions = await this.fabricClient.QueryManager.GetPartitionListAsync(serviceName);

            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();

            foreach (Partition partition in partitions)
            {
                string proxyUrl = $"{proxyAddress}/api/Values?PartitionKey={((Int64RangePartitionInformation)partition.PartitionInformation).LowKey}&PartitionKind=Int64Range";

                using (HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        continue;
                    }

                    result.AddRange(JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(await response.Content.ReadAsStringAsync()));
                }
            }

            return result;
            * /

            //foreach (Partition partition in partitions)
            //{
            //    string proxyUrl = $"{proxyAddress}/api/QueueAndStore?PartitionKey={((Int64RangePartitionInformation)partition.PartitionInformation).LowKey}&PartitionKind=Int64Range";

            //    using (HttpResponseMessage response = await this.httpClient.GetAsync(proxyUrl))
            //    {
            //        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //        {
            //            continue;
            //        }

            //        result.AddRange(JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(await response.Content.ReadAsStringAsync()));
            //    }
            //}


        }


        // GET api/aar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> Get(int id)
        {
            return new
            {
                SubmissionId = 1,
                DataSourceId = 2,
                COAJsonString = "{ '1554': { 'V': '65.12', 'breakdown': { 'partA': '62', 'partb': '20' } } }",
            };

        }
        */

            /*
            // PUT: api/Votes/name
            [HttpPut("{name}")]
            public async Task<IActionResult> Put(string name)
            {
                Uri serviceName = VotingWeb.GetVotingDataServiceName(this.serviceContext);
                Uri proxyAddress = this.GetProxyAddress(serviceName);
                long partitionKey = this.GetPartitionKey(name);
                string proxyUrl = $"{proxyAddress}/api/VoteData/{name}?PartitionKey={partitionKey}&PartitionKind=Int64Range";

                StringContent putContent = new StringContent($"{{ 'name' : '{name}' }}", Encoding.UTF8, "application/json");
                putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (HttpResponseMessage response = await this.httpClient.PutAsync(proxyUrl, putContent))
                {
                    return new ContentResult()
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };
                }
            }

            // DELETE: api/Votes/name
            [HttpDelete("{name}")]
            public async Task<IActionResult> Delete(string name)
            {
                Uri serviceName = VotingWeb.GetVotingDataServiceName(this.serviceContext);
                Uri proxyAddress = this.GetProxyAddress(serviceName);
                long partitionKey = this.GetPartitionKey(name);
                string proxyUrl = $"{proxyAddress}/api/VoteData/{name}?PartitionKey={partitionKey}&PartitionKind=Int64Range";

                using (HttpResponseMessage response = await this.httpClient.DeleteAsync(proxyUrl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return this.StatusCode((int)response.StatusCode);
                    }
                }

                return new OkResult();
            }
            */

            /// <summary>
            /// Constructs a reverse proxy URL for a given service.
            /// Example: http://localhost:19081/VotingApplication/VotingData/
            /// </summary>
            /// <param name="serviceName"></param>
            /// <returns></returns>
            //private Uri GetProxyAddress(Uri serviceName)
            //{
            //    return new Uri($"{this.reverseProxyBaseUri}{serviceName.AbsolutePath}");
            //}

            /// <summary>
            /// Creates a partition key from the given name.
            /// Uses the zero-based numeric position in the alphabet of the first letter of the name (0-25).
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            //private long GetPartitionKey(string name)
            //{
            //    return Char.ToUpper(name.First()) - 'A';
            //}
        }
    }
