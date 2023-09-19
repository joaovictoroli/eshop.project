using respapi.eshop.Interfaces;
using respapi.eshop.Models;
using respapi.eshop.Models.Entities;
using Newtonsoft.Json;


namespace respapi.eshop.Services
{
    public class CepService : ICepService
    {
        static HttpClient client = new HttpClient();
        const string baseURL = "https://viacep.com.br/ws/";
        public async Task<CepApiResponse?> GetAdressByCep(string cep)
        {
            CepApiResponse? cepApi = new ();

            HttpResponseMessage response  = await client.GetAsync(GetApiUri(baseURL + cep));
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                cepApi = JsonConvert.DeserializeObject<CepApiResponse>(result);
            }

            return cepApi;
        }

        private string GetApiUri(string url) 
        {
            return url + "/json";
        }
    }
}
