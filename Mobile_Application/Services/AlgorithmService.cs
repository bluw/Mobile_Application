using Mobile_Application.Exceptions;
using Mobile_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Application.Services
{
    class AlgorithmService
    {
        public async Task<Algorithm[]> GetAlgorithmsAsync()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://keyregisterweb.azurewebsites.net/api/algorithms/getAlgorithm");

                string json = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<Algorithm[]>(json);
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                string str = loader.GetString("noNetwork");
                throw new NoNetworkException(str);
            }
        }
    }
}
