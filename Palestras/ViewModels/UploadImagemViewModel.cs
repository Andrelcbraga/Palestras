using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Palestras.ViewModels
{
    public class UploadImagemViewModel
    {
        [Required]
        [Display(Name = "Foto")]
        public IFormFile PalestranteFoto { get; set; }
    }
}
