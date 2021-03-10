using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitApi.Interfaces;

namespace GitApi
{
    public class ApiShooter : IApiShooter
    {
        public async Task<string> CallGitHubWithGivenUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                try
                {
                    var contentsJson = await client.GetStringAsync(url);
                    return contentsJson;
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                    return null;
                }
                finally
                {
                    Console.WriteLine("Such user or repo does not exist");  
                }
            }
        }
    }
}
