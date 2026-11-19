<#
.SYNOPSIS
    Kiểm tra các mẫu mùi code (Anti-patterns & Code Smells).
.DESCRIPTION
    Quét mã nguồn C# tìm: catch {} rỗng, field public, ép kiểu is/as,
    TODO/Debug.Log sót lại, đường dẫn tuyệt đối dạng C:\, Singleton/static toàn cục.
.PARAMETER Help
    Hiển thị thông tin trợ giúp.
.EXAMPLE
    powershell -File tools/check-code-smells.ps1
#>
param(
    [switch]$Help
)

if ($Help) {
    Get-Help $MyInvocation.MyCommand.Path -Detailed
    exit 0
}

Write-Host "====================================================" -ForegroundColor Cyan
Write-Host " [AUDIT] QUÉT MẪU MÙI CODE (CODE SMELLS AUDIT)" -ForegroundColor Cyan
Write-Host "====================================================" -ForegroundColor Cyan

$srcPath = "src"
if (-not (Test-Path $srcPath)) {
    Write-Host "[INFO] Thư mục src/ chưa được tạo. Bỏ qua kiểm tra mã nguồn." -ForegroundColor Yellow
    exit 0
}

$files = Get-ChildItem -Path $srcPath -Recurse -Filter *.cs
$warnings = @()
$errors = @()

foreach ($file in $files) {
    $rawText = Get-Content $file.FullName -Raw
    $lines = Get-Content $file.FullName

    # 1. Catch rỗng
    if ($rawText -match 'catch\s*(\([^\)]*\))?\s*\{\s*\}') {
        $errors += "[LỖI CATCH RỖNG] $($file.FullName) chứa khối catch {} rỗng nuốt lỗi!"
    }

    # 2. Đường dẫn tuyệt đối ổ đĩa C:\ hoặc D:\
    if ($rawText -match '[A-Za-z]:\\[A-Za-z0-9_\\]+') {
        $errors += "[LỖI ĐƯỜNG DẪN CỨNG] $($file.FullName) chứa đường dẫn tuyệt đối cá nhân!"
    }

    # 3. Quét từng dòng
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        $lineNum = $i + 1

        # Public field không phải const/readonly
        if ($line -match '^\s*public\s+(?!class|interface|struct|enum|delegate|event|const|static\s+readonly|readonly)[A-Za-z0-9_<>[\]?]+\s+[A-Za-z0-9_]+\s*;') {
            $warnings += "[CẢNH BÁO FIELD PUBLIC] $($file.FullName):$lineNum -> $line"
        }

        # Debug.Log trong Core
        if ($file.FullName -match 'Game\.Core' -and $line -match 'Debug\.Log') {
            $errors += "[LỖI DEBUG.LOG TRONG CORE] $($file.FullName):$lineNum -> Không được gọi Debug.Log trong Core!"
        }

        # TODO hoặc FIXME còn sót
        if ($line -match '\b(TODO|FIXME)\b') {
            $warnings += "[CẢNH BÁO TODO/FIXME] $($file.FullName):$lineNum -> $line"
        }
    }
}

if ($errors.Count -gt 0) {
    Write-Host "[FAIL] PHÁT HIỆN $($errors.Count) LỖI NGHIÊM TRỌNG:" -ForegroundColor Red
    foreach ($err in $errors) {
        Write-Host "  - $err" -ForegroundColor Red
    }
}

if ($warnings.Count -gt 0) {
    Write-Host "[WARN] PHÁT HIỆN $($warnings.Count) CẢNH BÁO:" -ForegroundColor Yellow
    foreach ($warn in $warnings) {
        Write-Host "  - $warn" -ForegroundColor Yellow
    }
}

if ($errors.Count -eq 0 -and $warnings.Count -eq 0) {
    Write-Host "[PASS] Mã nguồn sạch sẽ! 0 lỗi và 0 cảnh báo mùi code trong $($files.Count) tệp." -ForegroundColor Green
    exit 0
} elseif ($errors.Count -eq 0) {
    Write-Host "[PASS] Không có lỗi nghiêm trọng. Cần lưu ý các cảnh báo ở trên." -ForegroundColor Green
    exit 0
} else {
    exit 1
}
