using System;
using System.ComponentModel.DataAnnotations;

namespace Palestras.Models
{
    public class Palestrante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Qualificação")]
        public string Qualificacao { get; set; }

        [Required]
        [StringLength(200)]
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
        [StringLength(250)]
        public string Local { get; set; }

        [Required]
        [StringLength(300)]
        public string Foto { get; set; }
    }
}
