English | [한국어](./README-ko.md)
# MapleStory Custom Login Theme Assets Extractor

[![GitHub license](https://img.shields.io/github/license/SpiralMoon/maplestory-custom-login-theme-assets-extractor.svg)](https://github.com/SpiralMoon/maplestory-custom-login-theme-assets-extractor/blob/master/LICENSE)

A script that analyzes MapleStory client data files (.wz) to extract custom login theme asset information from the character selection screen, then outputs them as files.

Uses the [WzLib](https://github.com/Kagamia/WzComparerR2/tree/master/WzComparerR2.WzLib) library from [WzComparerR2](https://github.com/Kagamia/WzComparerR2) to analyze .wz files.

## Get Started

### Initialize SubModule

Before starting the project, you need to initialize the submodule.

```bash
git submodule update --init --recursive
```

### Configuration

You can configure the path to the data file (Base.wz) to be extracted and the directory path where the extraction results will be saved.

```json
{
  "Paths": {
    "WzFilePath": "C:\\Nexon\\Maple\\Data\\Base\\Base.wz",
    "OutputDirectory": "../output"
  }
}
```

The configuration file path is `CustomLoginThemeExtractor/appsettings.json`.

### How to execute?

A batch file (run.bat) is provided for quick execution.

```bash
$ run.bat
```
Alternatively, you can run it directly with dotnet commands from the CustomLoginThemeExtractor directory.
```bash
$ cd CustomLoginThemeExtractor
$ dotnet run
```

## Output Results
### Image File Output
- Output location: `output/images/`
- Character selection screen background image files
  <img width="912" height="539" alt="image" src="https://github.com/user-attachments/assets/e4af6f8d-54bf-4dc0-b48d-2bf7146ddd8a" />

### Sound File Output
- Output location: `output/sounds/`
- Character selection screen background music files
  <img width="912" height="539" alt="image" src="https://github.com/user-attachments/assets/36e7fa68-df1b-4588-9235-685ebe2aad62" />

### Custom Login Theme Information
- Output location: `output/custom_login_theme.json`

```json
{
  "custom_login_themes": [
    {
      "code": "0",
      "name": "Under the World Tree (Day)",
      "bgm": "BgmUI/Title",
      "lvImageType": "light"
    },
    ... other custom login themes
  ]
}
```


## Dependencies

- [WzComparerR2](https://github.com/SpiralMoon/WzComparerR2)
