namespace GameZone.ViewModels
{
    public class EditGameFormViewModel :GameFormViewModel
    {

        public int Id { get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExtensions((SettingsFile.AllowedExtensions))]
        [MaxFileSize(SettingsFile.MaxFileSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
