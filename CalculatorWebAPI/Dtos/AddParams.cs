using System.ComponentModel.DataAnnotations;

namespace CalculatorWebAPI.Dtos;

public class AddParams
{
    [Required]
    public int Start { get; set; }
    [Required]
    public int Amount { get; set; }
}