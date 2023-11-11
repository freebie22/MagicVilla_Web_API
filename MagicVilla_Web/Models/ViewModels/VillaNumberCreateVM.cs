using MagicVilla_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels
{
    public class VillaNumberCreateVM
    {
        public VillaNumberCreateVM()
        {
            VillaNumber = new();
        }
        public VillaNumberCreateDTO VillaNumber { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; } 
    }
}
