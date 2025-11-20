using Newtonsoft.Json;

namespace CustomLoginThemeExtractor
{
    /// <summary>
    /// Json schema
    /// </summary>
    public class Output
    {
        [JsonProperty("custom_login_themes")]
        public List<CustomLoginTheme> CustomLoginThemes { get; set; } = new List<CustomLoginTheme>();
    }

    public class CustomLoginTheme
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("bgm")]
        public string Bgm { get; set; } = string.Empty;

        [JsonProperty("lvImageType")]
        public string LvImageType { get; set; } = string.Empty;
    }
}
