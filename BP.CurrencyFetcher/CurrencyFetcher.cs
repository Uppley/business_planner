using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BP.CurrencyFetcher
{
    public class CurrencyFetcher
    {
        public async Task<string> GetExchangeRate(string api,string from, string to)
        {
            Debug.WriteLine(api);
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://free.currconv.com");
                    var response = await client.GetAsync($"/api/v7/convert?q={from}_{to}&compact=ultra&apiKey={api}");
                    var stringResult = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(stringResult);
                    var dictResult = JObject.Parse(stringResult);
                    if(dictResult.ContainsKey("status"))
                    {
                        return "error";
                    }
                    else
                    {
                        return dictResult[$"{from}_{to}"].ToString();
                    }
                    
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(httpRequestException.StackTrace);
                    return "error";
                }
            }
        }
    }
}
