namespace scsp.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public string? Title {get; set;}
    public string? Details {get; set;}

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}