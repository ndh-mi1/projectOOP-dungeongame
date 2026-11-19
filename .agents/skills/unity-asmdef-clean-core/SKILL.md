---
name: unity-asmdef-clean-core
description: Dựng Assembly Definition (.asmdef) cho Game.Core hoàn toàn độc lập Engine và cấu hình test EditMode headless.
---

# HƯỚNG DẪN DỰNG ASMDEF CHO CORE THUẦN C# (UNITY ASMDEF CLEAN CORE)

## 1. MỤC TIÊU VÀ TẦNG KIẾN TRÚC
Kỹ năng này hướng dẫn cách cấu hình các tệp Assembly Definition (`.asmdef`) của Unity để phân lập hoàn toàn tầng logic nghiệp vụ (`Game.Core`) khỏi Engine đồ họa Unity (`UnityEngine`), bảo đảm tuân thủ tiêu chí 4.3 Checklist K69.

```
[Game.Core.asmdef]  <────  [Game.Presentation.asmdef]
(0 references)                 (tham chiếu Game.Core, UnityEngine)
      ▲
      │
[Game.Core.Tests.asmdef]
(tham chiếu Game.Core, NUnit, EditMode)
```

## 2. CẤU HÌNH TỆP `Game.Core.asmdef`
Đặt tại: `src/Game.Core/Game.Core.asmdef`
```json
{
    "name": "Game.Core",
    "rootNamespace": "Game.Core",
    "references": [],
    "includePlatforms": [],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": false,
    "precompiledReferences": [],
    "autoReferenced": true,
    "defineConstraints": [],
    "versionDefines": [],
    "noEngineReferences": true
}
```
*Lưu ý quan trọng:* Cờ `"noEngineReferences": true` bắt buộc phải bật. Khi bật cờ này, bất kỳ lệnh `using UnityEngine;` nào gõ vào thư mục `src/Game.Core/` sẽ lập tức gây lỗi biên dịch đỏ ngay trên IDE.

## 3. CẤU HÌNH TỆP `Game.Core.Tests.asmdef`
Đặt tại: `tests/Game.Core.Tests/Game.Core.Tests.asmdef`
```json
{
    "name": "Game.Core.Tests",
    "rootNamespace": "Game.Core.Tests",
    "references": [
        "Game.Core"
    ],
    "optionalUnityReferences": [
        "TestAssemblies"
    ],
    "includePlatforms": [
        "Editor"
    ],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": false,
    "precompiledReferences": [],
    "autoReferenced": false,
    "defineConstraints": [
        "UNITY_INCLUDE_TESTS"
    ],
    "versionDefines": [],
    "noEngineReferences": true
}
```

## 4. CHẠY TEST HEADLESS CLI TRONG < 2 GIÂY
Để chạy toàn bộ unit tests mà không cần mở giao diện Unity Editor:
```powershell
Unity.exe -batchmode -runTests -testPlatform EditMode -testResults results.xml -projectPath .
```
Thời gian thực thi hoàn tất trong vòng dưới 2 giây.
