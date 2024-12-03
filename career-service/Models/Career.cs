using System.ComponentModel.DataAnnotations;

namespace career_service.Models;

public class Career : BaseModel
{
    [StringLength(250)]
    public string Name { get; set; } = null!;
    
}