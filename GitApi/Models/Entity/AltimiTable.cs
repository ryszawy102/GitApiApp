namespace GitApi.Models.Entity
{
    public class AltimiTable
    {
        public int Id { get; set; }
        public string Sha { get; set; }
        public string Repo { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
