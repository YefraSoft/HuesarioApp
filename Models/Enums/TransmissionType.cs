using System.ComponentModel.DataAnnotations;

namespace HuesarioApp.Models.Enums;

public enum TransmissionType
{
    [Display(Name = "Transmisión Automática")]
    AUTOMATIC,
    
    [Display(Name = "Transmisión Manual")]
    STANDARD
}