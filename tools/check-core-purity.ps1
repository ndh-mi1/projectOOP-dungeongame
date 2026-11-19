<#
.SYNOPSIS
    Kiểm tra tính thuần khiết của tầng Game.Core (Core Purity Audit).
.DESCRIPTION
    Quét toàn bộ tệp .cs trong src/Game.Core/ để phát hiện các chỉ thị using bị cấm:
    UnityEngine, UnityEditor, System.Windows.Forms, System.Drawing, System.IO.
.PARAMETER Help
    Hiển thị thông tin trợ giúp.
.EXAMPLE
    powershell -File tools/check-core-purity.ps1
#>
param(
    [switch]$Help
)

if ($Help) {
    Get-Help $MyInvocation.MyCommand.Path -Detailed
    exit 0
}

Write-Host "====================================================" -ForegroundColor Cyan
Write-Host " [AUDIT] KIỂM TRA TÍNH THUẦN KHIẾT TẦNG GAME.CORE" -ForegroundColor Cyan
Write-Host "====================================================" -ForegroundColor Cyan

$corePaths = @("src/Game.Core", "src/MyGame.Core")
$targetPath = $null

foreach ($p in $corePaths) {
    if (Test-Path $p) {
        $targetPath = $p
        break
    }
}

if (-not $targetPath) {
    Write-Host "[INFO] Thư mục Core chưa được tạo ($($corePaths -join ' hoặc ')). Bỏ qua kiểm tra mã nguồn." -ForegroundColor Yellow
    exit 0
}

$forbiddenPatterns = @(
    "using\s+UnityEngine(\..+)?;",
    "using\s+UnityEditor(\..+)?;",
    "using\s+System\.Windows\.Forms;",
    "using\s+System\.Drawing;",
    "using\s+System\.IO;"
)

$violations = @()
$files = Get-ChildItem -Path $targetPath -Recurse -Filter *.cs

foreach ($file in $files) {
    $lines = Get-Content $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        foreach ($pattern in $forbiddenPatterns) {
            if ($line -match $pattern) {
                $violations += [PSCustomObject]@{
                    File    = $file.FullName
                    Line    = $i + 1
                    Content = $line.Trim()
                }
            }
        }
    }
}

if ($violations.Count -gt 0) {
    Write-Host "[FAIL] PHÁT HIỆN $($violations.Count) VI PHẠM TÍNH THUẦN KHIẾT TRONG CORE:" -ForegroundColor Red
    foreach ($v in $violations) {
        Write-Host "  - $($v.File):$($v.Line) -> $($v.Content)" -ForegroundColor Red
    }
    Write-Host "`nQuy tắc: Game.Core bắt buộc thuần C# 100%, không dính Engine!" -ForegroundColor Yellow
    exit 1
} else {
    Write-Host "[PASS] Tầng Game.Core hoàn toàn thuần khiết! (0 dòng using bị cấm trong $($files.Count) tệp)." -ForegroundColor Green
    exit 0
}
