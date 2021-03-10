using System.Threading.Tasks;

namespace GitApi.Interfaces
{
    public interface IApiShooter
    {
        Task<string> CallGitHubWithGivenUrl(string url);
    }
}