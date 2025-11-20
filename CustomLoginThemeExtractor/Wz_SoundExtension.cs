using WzComparerR2.WzLib;

namespace CustomLoginThemeExtractor;

public static class Wz_SoundExtension
{
    /// <summary>
    /// Save Wz_Sound to MP3 file
    /// </summary>
    public static void SaveToMp3(this Wz_Sound sound, string path)
    {
        if (sound == null)
            throw new ArgumentNullException(nameof(sound));

        // Ensure path has .mp3 extension
        if (!path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
        {
            path = Path.ChangeExtension(path, ".mp3");
        }

        // Extract sound data and save to MP3 file
        var soundData = sound.ExtractSound();
        if (soundData != null)
        {
            File.WriteAllBytes(path, soundData);
        }
    }
}
