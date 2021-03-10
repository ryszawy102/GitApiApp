using System.Collections.Generic;
using GitApi.Models.Entity;
using GitApi.Models.Values;

namespace GitApi.Interfaces
{
    public interface IDatabase
    {
        void InsertEventLogsToDb(List<Root> listCommits, string repoName);
        List<AltimiTable> GetResultsAgainstGivenRepo(string repoName);
    }
}