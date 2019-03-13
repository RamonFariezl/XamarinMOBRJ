using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamarinMOBRJ.Models.ClassesAPI;

namespace XamarinMOBRJ.Services
{
    public interface IApiService
    {
        Task<RootObject> GetEstadosAsync();
    }
}
