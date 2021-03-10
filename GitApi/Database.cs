using System;
using System.Collections.Generic;
using LiteDB;
using GitApi.Interfaces;
using GitApi.Models.Entity;
using GitApi.Models.Values;


namespace GitApi
{
    public class Database : IDatabase
    {
        public void InsertEventLogsToDb(List<Root> listCommits, string repoName)
        {
            try
            {
                using (var db = new LiteDatabase(@"Altimi.db"))
                {

                    var collection = db.GetCollection<AltimiTable>("AltimiTable");

                    foreach (var item in listCommits)
                    {

                        var results = collection.Query().Where(x => x.Sha.Equals(item.sha)).FirstOrDefault();
                        if (results != null)
                        {
                            continue;
                        }
                        //TODO Automapper to implement
                        var itemDb = new AltimiTable
                        {
                            Id = 0,
                            Sha = item.sha,
                            Repo = repoName,
                            Message = item.commit.message,
                            User = item.commit.committer.name
                        };

                        collection.Insert(itemDb);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO error logger
            }
        }
        public List<AltimiTable> GetResultsAgainstGivenRepo(string repoName)
        {
            try
            {
                using (var db = new LiteDatabase(@"Altimi.db"))
                {
                    var collection = db.GetCollection<AltimiTable>("AltimiTable");
                    var results = collection.Query().Where(x => x.Repo.StartsWith(repoName)).ToList();
                    return results;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //TODO error logger
            }
        }
    }
}

