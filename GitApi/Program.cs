using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GitApi.Models.Values;
using GitApi.Interfaces;

namespace GitApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //simply di
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IApiShooter, ApiShooter>()
                .AddSingleton<IDatabase, Database>()
                .BuildServiceProvider();
            var db = serviceProvider.GetService<IDatabase>();
            var api = serviceProvider.GetService<IApiShooter>();

            //collect data from user
            Console.Clear();
            Console.WriteLine("User name:");
            var userName = Console.ReadLine();
            Console.WriteLine("Repo name:");
            var repoName = Console.ReadLine();

            //create url
            string url = $"https://api.github.com/repos/{userName}/{repoName}/commits";

            //shoot to api
            var apiResult = await api.CallGitHubWithGivenUrl(url);
            if (apiResult == null)
            {
                return;
            }

            //deserialize
            var deserializedResult = JsonConvert.DeserializeObject<List<Root>>(apiResult);
            
            //save result to db
            db.InsertEventLogsToDb(deserializedResult, repoName);

            //get results from db against repo name
            var dbResults = db.GetResultsAgainstGivenRepo(repoName);

            //result to console
            foreach (var item in dbResults)
            {
                Console.WriteLine($"{repoName}/{item.Sha}: {item.Message} by {item.User}");
            }

            Console.WriteLine("Good day to you mate!");
        }
    }
}
