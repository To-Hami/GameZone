using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModels
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [AllowedExtensions((SettingsFile.AllowedExtensions))]
        [MaxFileSize(SettingsFile.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; } = default!;


        [Display(Name = "Category")]
        public int CategoryId { get; set; } = default!;
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();


        [Display(Name = "Selected Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();



    }


}
