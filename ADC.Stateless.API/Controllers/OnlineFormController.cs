using System;
using System.Threading.Tasks;
using ADC.Stateful.OnlineForm.Interface;
using ADC.Stateless.API.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json;

namespace ADC.Stateless.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineFormController : ControllerBase
    {
        private readonly IOnlineFormService _onlineFormService;

        public OnlineFormController()
        {
            _onlineFormService = ServiceProxy.Create<IOnlineFormService>(new Uri("fabric:/ADC/ADC.Stateful.OnlineForm"), new ServicePartitionKey(0));
        }
        [HttpGet("{trustId}")]
        public async Task<ActionResult<OnlineFormResponseModel>> RetriveFormContentFor(int trustId)
        {
            object formContent = await _onlineFormService.RetriveContentForAsync(trustId);
            string formContentString = JsonConvert.SerializeObject(formContent);

            OnlineFormResponseModel formContentResponseModel = JsonConvert.DeserializeObject<OnlineFormResponseModel>(formContentString);

            return Ok(formContentResponseModel);
        }
    }
}
