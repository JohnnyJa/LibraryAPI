namespace Library.Common.Models;

public class ErrorDTO
{
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}