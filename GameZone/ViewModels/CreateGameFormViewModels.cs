

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModels :GameFormViewModel
    {
        [AllowedExtensions((SettingsFile.AllowedExtensions))]
        [MaxFileSize(SettingsFile.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; } = default!;

    }


}
