namespace GameZone.Settings
{
    public static class SettingsFile
    {
        public const string ImagePath = "/assets/images/games";
        public const string AllowedExtensions = ".jpeg,.jpg,.png,.webp";
        public const int MaxFileSizeInMG = 1;
        public const int MaxFileSizeInByte = MaxFileSizeInMG * 1024 * 1024;
    }
}
