using Microsoft.Extensions.Configuration;
using CustomLoginThemeExtractor;
using WzComparerR2.WzLib;

Console.WriteLine("Start");

#region load configuration

// Load by appsettings.json
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .Build();

var settings = new AppSettings();
config.Bind(settings);

#endregion

#region initialize output directories

string outputDir = settings.Paths.OutputDirectory;
string imagesDir = Path.Combine(outputDir, "images");
string soundsDir = Path.Combine(outputDir, "sounds");

Directory.CreateDirectory(outputDir);
Directory.CreateDirectory(imagesDir);
Directory.CreateDirectory(soundsDir);

Console.WriteLine("Initialize output directories...");

#endregion

string wzFilePath = settings.Paths.WzFilePath; // Base.wz file path
Wz_Structure wz = new Wz_Structure(); // Root WZ tree

#region open wz (source from WzComparerR2)

Console.WriteLine("Trying open Base.wz");

if (string.Equals(Path.GetExtension(wzFilePath), ".ms", StringComparison.OrdinalIgnoreCase))
{
    wz.LoadMsFile(wzFilePath);
}
else if (wz.IsKMST1125WzFormat(wzFilePath))
{
    wz.LoadKMST1125DataWz(wzFilePath);
    if (string.Equals(Path.GetFileName(wzFilePath), "Base.wz", StringComparison.OrdinalIgnoreCase))
    {
        string packsDir = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(wzFilePath)), "Packs");
        if (Directory.Exists(packsDir))
        {
            foreach (var msFile in Directory.GetFiles(packsDir, "*.ms"))
            {
                wz.LoadMsFile(msFile);
            }
        }
    }
}
else
{
    wz.Load(wzFilePath, true);
}

Console.WriteLine("Base.wz open complete.");

#endregion

Wz_File wzFile = wz.WzNode.GetNodeWzFile(); // Root WZ file

#region load images

Wz_Node imgNodeCustomLoginTheme = wz.WzNode.FindNodeByPath("Etc\\customLoginTheme.img");
Wz_Image imgCustomLoginTheme = imgNodeCustomLoginTheme.GetNodeWzImage();

imgCustomLoginTheme.TryExtract();

Console.WriteLine("Etc\\customLoginTheme.img is Ready.");

#endregion

Output output = new Output(); // json output schema

#region parse custom login info and extract .png

Console.WriteLine("CustomLoginTheme parse start.");

try
{
    foreach (var nodeCustomLoginTheme in imgCustomLoginTheme.Node.Nodes)
    {
        string code = nodeCustomLoginTheme.Text;

        // icon node
        Wz_Node nodeIcon = nodeCustomLoginTheme.Nodes["icon"];
        Wz_Node nodeIconCanvas = nodeIcon.GetLinkedSourceNode(wzFile);
        Wz_Png pngIconRaw = nodeIconCanvas.GetValue<Wz_Png>();

        // Extract and save to .png file
        pngIconRaw.SaveToPng(Path.Combine(imagesDir, $"{nodeCustomLoginTheme.FullPath.Replace("\\", ".")}.icon.png"));

        // 0/image/back/0/0
        // 0/icon, name, lvImageType, bgm
        // bgm = BgmUI     (Sound/BgmUI.img)

        // custom login
        Wz_Node nodeBackground = nodeCustomLoginTheme.FindNodeByPath("image\\back\\0\\0");

        Wz_Node nodeBackgroundCanvas = nodeBackground.GetLinkedSourceNode(wzFile);
        Wz_Png pngBackground = nodeBackgroundCanvas.GetValue<Wz_Png>();

        pngBackground.SaveToPng(Path.Combine(imagesDir, $"{nodeBackground.FullPath.Replace("\\", ".")}.png"));

        // Extract and save to .mp3 file
        string bgm = nodeCustomLoginTheme.Nodes["bgm"].GetValue<string>(); // ex) BgmUI/Title
        string[] arr = bgm .Split("/");

        string bgmImgName = arr[0];
        string bgmName = arr[1];

        Wz_Node imgNodeBgm = wz.WzNode.FindNodeByPath($"Sound\\{bgmImgName}.img");
        Wz_Image imgBgm = imgNodeBgm.GetNodeWzImage();

        imgBgm.TryExtract();

        Wz_Node nodeBgm = imgBgm.Node.Nodes[bgmName];
        Wz_Sound soundBgm = nodeBgm.GetValue<Wz_Sound>();

        soundBgm.SaveToMp3(Path.Combine(soundsDir, $"{nodeBgm.FullPath.Replace("\\", ".")}.mp3"));

        string name = nodeCustomLoginTheme.Nodes["name"].GetValue<string>();
        string lvImageType = nodeCustomLoginTheme.Nodes["lvImageType"].GetValue<string>();

        CustomLoginTheme customLoginTheme = new CustomLoginTheme();
        customLoginTheme.Code = code;
        customLoginTheme.Name = name;
        customLoginTheme.Bgm = bgm;
        customLoginTheme.LvImageType = lvImageType;

        output.CustomLoginThemes.Add(customLoginTheme);

        Console.WriteLine($"CustomLoginTheme {code} parse completed.");
    }
}
catch (Exception e)
{
    Console.WriteLine("Exception occured.");
    Console.WriteLine(e);

    return;
}

Console.WriteLine("CustomLoginTheme parse finished.");

#endregion

#region write custom_login.json

string jsonOutputPath = Path.Combine(outputDir, "custom_login_theme.json");

using (var writer = new StreamWriter(jsonOutputPath))
{
    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(output, Newtonsoft.Json.Formatting.Indented);
    writer.Write(jsonStr);

    Console.WriteLine($"CustomLoginTheme data exported to {jsonOutputPath}");
}

#endregion

Console.WriteLine("Done");
