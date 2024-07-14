using Bank.Proposals.Worker.Domain.HttpService;

namespace Bank.Proposals.Worker.Infrastructure.HttpServices;

public class ScoreHttpService : IScoreHttpService
{
    public Task<int> GetScore(string document)
    {
        var raizDocument = document.Substring(0, 2);
        var random = new Random();

        if (raizDocument == "11")
            return Task.FromResult(random.Next(0, 300));

        return Task.FromResult(random.Next(301, 1000));
    }
}