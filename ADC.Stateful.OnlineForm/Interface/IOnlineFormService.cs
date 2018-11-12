using ADC.Stateful.OnlineForm.Model;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Threading.Tasks;

namespace ADC.Stateful.OnlineForm.Interface
{
    public interface IOnlineFormService: IService
    {
        Task<FormContentModel> RetriveContentForAsync(int trustId);
    }
}
