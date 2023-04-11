using GlowingNews.Client.DTOs.Theme.Enum;

namespace GlowingNews.Client.DTOs.Theme
{
    public class ThemeDto
    {
        public bool IsDarkMode { get; set; }
        public BackgroundType BackgroundType { get; set; }
        public BorderType BorderType { get; set; }
        public NavBorderType NavBorderType { get; set; }
        public VisibilityType VisibilityType { get; set; }
    }
}
