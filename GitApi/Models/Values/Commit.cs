namespace GitApi.Models.Values
{
    public class Commit
    {
        public string message { get; set; }
        public Committer committer { get; set; }
    }
}
