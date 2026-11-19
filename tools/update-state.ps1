<#
.SYNOPSIS
    Cập nhật tự động tệp docs/agent/STATE.json dựa trên số liệu thực tế.
.DESCRIPTION
    Đếm số lượng test trong tests/, số lượng lớp trong src/, số commit trên Git
    và cập nhật trường counts trong STATE.json một cách tự động.
.PARAMETER Help
    Hiển thị thông tin trợ giúp.
.EXAMPLE
    powershell -File tools/update-state.ps1
#>
param(
    [switch]$Help
)

if ($Help) {
    Get-Help $MyInvocation.MyCommand.Path -Detailed
    exit 0
}

$stateFile = "docs/agent/STATE.json"
if (-not (Test-Path $stateFile)) {
    Write-Host "[ERROR] Không tìm thấy tệp $stateFile!" -ForegroundColor Red
    exit 1
}

$state = Get-Content $stateFile -Raw | ConvertFrom-Json

# 1. Đếm Unit Tests và Integration Tests
$unitCount = 0
$integrationCount = 0

if (Test-Path "tests/Game.Core.Tests") {
    $unitFiles = Get-ChildItem -Path "tests/Game.Core.Tests" -Recurse -Filter *.cs
    foreach ($f in $unitFiles) {
        $matches = Select-String -Path $f.FullName -Pattern '\[Test\]'
        $unitCount += $matches.Count
    }
}

if (Test-Path "tests/Integration") {
    $intFiles = Get-ChildItem -Path "tests/Integration" -Recurse -Filter *.cs
    foreach ($f in $intFiles) {
        $matches = Select-String -Path $f.FullName -Pattern '\[Test\]'
        $integrationCount += $matches.Count
    }
}

$state.counts.unitTests = $unitCount
$state.counts.integrationTests = $integrationCount

# 2. Đếm số lớp C# thực tế trong src/Game.Core
$classCount = 0
if (Test-Path "src/Game.Core") {
    $csFiles = Get-ChildItem -Path "src/Game.Core" -Recurse -Filter *.cs
    foreach ($f in $csFiles) {
        $lines = Select-String -Path $f.FullName -Pattern '^\s*public\s+(abstract\s+)?(class|struct|interface|enum)\s+[A-Za-z0-9_]+'
        $classCount += $lines.Count
    }
}
$state.counts.entityClasses = $classCount
$state.counts.umlClasses = $classCount

# 3. Đếm số commit Git
try {
    $tv1Commits = (git log --grep="\[Đình Huy\]" --oneline 2>$null).Count
    $tv2Commits = (git log --grep="\[Công Huy\]" --oneline 2>$null).Count
    if (-not $tv1Commits) { $tv1Commits = 0 }
    if (-not $tv2Commits) { $tv2Commits = 0 }
    $state.counts.commits.TV1 = $tv1Commits
    $state.counts.commits.TV2 = $tv2Commits
} catch {
    # Giữ nguyên
}

# 4. Cập nhật thời gian
$state.updated = (Get-Date).ToString("yyyy-MM-ddTHH:mm:sszzz")

# Ghi lại vào STATE.json
$state | ConvertTo-Json -Depth 10 | Set-Content $stateFile -Encoding UTF8

Write-Host "[PASS] Đã cập nhật thành công $stateFile vào lúc $($state.updated)" -ForegroundColor Green
Write-Host "  - Unit Tests: $unitCount | Integration Tests: $integrationCount | Lớp C#: $classCount" -ForegroundColor Cyan
exit 0
