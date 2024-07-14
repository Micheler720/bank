
namespace Bank.Proposals.Worker.Domain.HttpService;

public interface IScoreHttpService
{
    Task<int> GetScore(string document);
}