using System.Diagnostics.CodeAnalysis;
using Bank.Proposals.Worker.Domain.HttpService;

namespace Bank.Proposals.Worker.Infrastructure.HttpServices;

[ExcludeFromCodeCoverage]
public class ScoreHttpService : IScoreHttpService
{
    public Task<int> GetScore(string document)
    {
        var raizDocument = document.Substring(0, 2);
        var random = new Random();

        if (raizDocument == "11")
            return Task.FromResult(random.Next(0, 300));
        
        if (raizDocument == "22")
            throw new Exception("Erro ao buscar o score");

        return Task.FromResult(random.Next(301, 1000));
    }
}