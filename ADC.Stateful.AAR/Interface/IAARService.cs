using ADC.Stateful.AAR.DataModel;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADC.Stateful.AAR.Interface
{
    public interface IAARService: IService
    {
        Task<bool> AppendFormAsync(FormDataModel formDataObject);
    }
}
