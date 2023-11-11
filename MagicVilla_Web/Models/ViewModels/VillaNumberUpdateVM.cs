using MagicVilla_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels
{
    public class VillaNumberUpdateVM
    {
        public VillaNumberUpdateVM()
        {
            VillaNumber = new();
        }
        public VillaNumberUpdateDTO VillaNumber { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; } 
    }
}
