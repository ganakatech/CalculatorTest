using System.ComponentModel.DataAnnotations;

namespace CalculatorWebAPI.Dtos;

public class MuliplyParams
{
    [Required]
    public int Start { get; set; }
    [Required]
    public int By { get; set; }
}