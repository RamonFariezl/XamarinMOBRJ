using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinMOBRJ.ViewModels;
using XamarinMOBRJ.Models.ClassesAPI;

namespace XamarinMOBRJ.Services
{
    public class ApiService : IApiService
    {
        public async Task<RootObject> GetEstadosAsync()
        {
            RootObject listaEstados = new RootObject();
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(Constantes.ApiBaseUrl);

                if (response.IsSuccessStatusCode)
                {
                    string conteudo =
                        response.Content.ReadAsStringAsync().Result;

                    dynamic resultado = JsonConvert.DeserializeObject<RootObject>(conteudo);
                    listaEstados = resultado;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return listaEstados;
        }
    }
}
