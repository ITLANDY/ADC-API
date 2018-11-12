using System;
using System.Threading.Tasks;
using ADC.Stateful.AAR.DataModel;
using ADC.Stateful.AAR.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace ADC.Stateless.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AARController : ControllerBase
    {
        private readonly IAARService _arrService;

        public AARController()
        {
            _arrService = ServiceProxy.Create<IAARService>(new Uri("fabric:/ADC/ADC.Stateful.AAR"), new ServicePartitionKey(0));
        }

        [HttpPost]
        public async Task<IActionResult> AppendForm([FromBody] FormDataModel formData)
        {
            bool result = await _arrService.AppendFormAsync(formData);
            return Ok(result);
        }
    }
}
