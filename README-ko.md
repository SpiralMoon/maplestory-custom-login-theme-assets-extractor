한국어 | [English](./README.md)
# MapleStory Custom Login Theme Assets Extractor

[![GitHub license](https://img.shields.io/github/license/SpiralMoon/maplestory-custom-login-theme-assets-extractor.svg)](https://github.com/SpiralMoon/maplestory-custom-login-theme-assets-extractor/blob/master/LICENSE)

메이플스토리의 클라이언트 데이터 파일(.wz)을 분석하여 캐릭터 선택창 에셋 정보를 추출하여 파일로 출력하는 스크립트입니다.

.wz 파일을 분석하기 위해 [WzComparerR2](https://github.com/Kagamia/WzComparerR2)의 [WzLib](https://github.com/Kagamia/WzComparerR2/tree/master/WzComparerR2.WzLib) 라이브러리를 사용합니다.

## Get Started

### Initialize SubModule

프로젝트를 시작하기 전, submodule을 초기화 해야합니다.

```bash
git submodule update --init --recursive
```

### Configuration

추출 대상이 될 데이터 파일(Base.wz)의 경로와 추출 결과물이 저장될 디렉토리 경로를 설정할 수 있습니다.

```json
{
  "Paths": {
    "WzFilePath": "C:\\Nexon\\Maple\\Data\\Base\\Base.wz",
    "OutputDirectory": "../output"
  }
}
```

설정 파일 경로는 `CustomLoginThemeExtractor/appsettings.json` 입니다.

### How to execute?

빠른 실행을 위해 즉시 실행 가능한 배치파일(run.bat)을 제공하고 있습니다.

```bash
$ run.bat
```
또는 CustomLoginThemeExtractor 디렉토리에서 직접 dotnet 명령어로 실행할 수 있습니다.
```bash
$ cd CustomLoginThemeExtractor
$ dotnet run
```

## Output Results
### Image File Output
- 출력 위치: `output/images/`
- 캐릭터 선택창 배경 이미지 파일
  <img width="912" height="539" alt="image" src="https://github.com/user-attachments/assets/e4af6f8d-54bf-4dc0-b48d-2bf7146ddd8a" />

### Sound File Output
- 출력 위치: `output/sounds/`
- 캐릭터 선택창 배경 음악 파일
  <img width="912" height="539" alt="image" src="https://github.com/user-attachments/assets/36e7fa68-df1b-4588-9235-685ebe2aad62" />

### Custom Login Theme Information
- 출력 위치: `output/custom_login_theme.json`

```json
{
  "custom_login_themes": [
    {
      "code": "0",
      "name": "세계수 아래(낮)",
      "bgm": "BgmUI/Title",
      "lvImageType": "light"
    },
    ... other custom login themes
  ]
}
```


## Dependencies

- [WzComparerR2](https://github.com/SpiralMoon/WzComparerR2)