namespace WebApplication2_AboutMe.Models
{
    public static class Repo
    {
        private static string _defaultLogoPath = "/local/storage/img/no-image.png";
        public static string DefaultLogoPath { 
            get 
            { 
                return _defaultLogoPath; 
            } 
            set 
            { 
                _defaultLogoPath = value; 
            } 
        }
        private static string _selectedLogoPath = string.Empty;
        public static string SelectedLogoPath 
        { 
            get 
            { 
                return _selectedLogoPath;
            } 
            set 
            { 
                _selectedLogoPath = value; 
            } 
        }
    }
}
