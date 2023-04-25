using System.ComponentModel.DataAnnotations;

namespace Application.Core.Parameters;

public class PhotoQuery
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string To { get; set; }
}
