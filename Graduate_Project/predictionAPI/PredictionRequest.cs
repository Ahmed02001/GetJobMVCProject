using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class JobPredictionService
{
    private readonly string apiUrl = "https://predicted-job-model-d2cecbadfd31.herokuapp.com//predict";

    public async Task<string> GetJobPredictionAsync(List<string> Skills)
    {
        using (var client = new HttpClient())
        {
            var request = new
            {
                skill1 = Skills[0],
                skill2 = Skills[1],
                skill3 = Skills[2],
                skill4 = Skills[3]
            };

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);


            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}

