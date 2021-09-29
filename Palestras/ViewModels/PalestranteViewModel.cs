using System;
using System.ComponentModel.DataAnnotations;

namespace Palestras.ViewModels
{
    public class PalestranteViewModel : EditImagemViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Qualificação")]
        public string Qualificacao { get; set; }

        [Required]
        [Display(Name = "Experiência")]
        public int Experiencia { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime DataPalestra { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Hora")]
        public DateTime HoraPalestra { get; set; }

        [Required]
        public string Local { get; set; }
    }
}
