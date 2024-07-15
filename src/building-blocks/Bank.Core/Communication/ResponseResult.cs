using System.Diagnostics.CodeAnalysis;

namespace Bank.Core.Communication;

[ExcludeFromCodeCoverage]
public class ResponseResult
{
    public ResponseResult()
    {
        Errors = new ResponseErrorMessages();
    }
    public string? Title { get; set; }
    public int Status { get; set; }
    public ResponseErrorMessages Errors { get; set; }
}

[ExcludeFromCodeCoverage]
public class ResponseErrorMessages
{
    public ResponseErrorMessages()
    {
        Messages = new List<string>();
    }
    public List<string> Messages { get; set; }
}