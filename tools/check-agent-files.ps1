<#
.SYNOPSIS
    Kiểm tra toàn bộ tính toàn vẹn của các tệp hạ tầng Agent (Agent Files Audit).
.DESCRIPTION
    Kiểm tra: trần ký tự (AGENTS.md, GEMINI.md, rules, workflows, memory, sessions),
    tổng nội dung always-on <= 9000 ký tự, tính hợp lệ của JSON, và dòng activation ở mỗi rule.
.PARAMETER Help
    Hiển thị thông tin trợ giúp.
.EXAMPLE
    powershell -File tools/check-agent-files.ps1
#>
param(
    [switch]$Help
)

if ($Help) {
    Get-Help $MyInvocation.MyCommand.Path -Detailed
    exit 0
}

Write-Host "====================================================" -ForegroundColor Cyan
Write-Host " [AUDIT] KIỂM TOÁN TÍNH TOÀN VẸN TỆP HẠ TẦNG AGENT" -ForegroundColor Cyan
Write-Host "====================================================" -ForegroundColor Cyan

$hasErrors = $false

# 1. Kiểm tra AGENTS.md (<= 6000 ký tự)
if (Test-Path "AGENTS.md") {
    $len = (Get-Content "AGENTS.md" -Raw).Length
    if ($len -gt 6000) {
        Write-Host "[FAIL] AGENTS.md vượt trần ký tự: $len/6000" -ForegroundColor Red
        $hasErrors = $true
    } else {
        Write-Host "[PASS] AGENTS.md đạt chuẩn: $len/6000 ký tự" -ForegroundColor Green
    }
} else {
    Write-Host "[FAIL] Thiếu tệp AGENTS.md!" -ForegroundColor Red
    $hasErrors = $true
}

# 2. Kiểm tra GEMINI.md (<= 2000 ký tự)
if (Test-Path "GEMINI.md") {
    $len = (Get-Content "GEMINI.md" -Raw).Length
    if ($len -gt 2000) {
        Write-Host "[FAIL] GEMINI.md vượt trần ký tự: $len/2000" -ForegroundColor Red
        $hasErrors = $true
    } else {
        Write-Host "[PASS] GEMINI.md đạt chuẩn: $len/2000 ký tự" -ForegroundColor Green
    }
} else {
    Write-Host "[FAIL] Thiếu tệp GEMINI.md!" -ForegroundColor Red
    $hasErrors = $true
}

# 3. Kiểm tra các quy tắc trong .agents/rules/ (mỗi rule <= 3500 ký tự, có dòng activation)
$rules = Get-ChildItem -Path ".agents/rules" -Filter *.md
Write-Host "`nKiểm tra $($rules.Count) quy tắc trong .agents/rules/:" -ForegroundColor White
foreach ($r in $rules) {
    $content = Get-Content $r.FullName -Raw
    $firstLine = (Get-Content $r.FullName)[0]
    $rLen = $content.Length

    if ($rLen -gt 3500) {
        Write-Host "  - [FAIL] $($r.Name) vượt trần ký tự: $rLen/3500" -ForegroundColor Red
        $hasErrors = $true
    }
    if ($firstLine -notmatch '^<!--\s*activation:\s*(always|glob:[^\s]+|model-decision|manual)\s*-->') {
        Write-Host "  - [FAIL] $($r.Name) thiếu dòng kích hoạt activation ở dòng 1!" -ForegroundColor Red
        $hasErrors = $true
    }
}

# 4. Kiểm tra tổng nội dung Always-On (<= 9000 ký tự)
$agentsLen = (Get-Content "AGENTS.md" -Raw).Length
$r1Len = (Get-Content ".agents/rules/session-protocol.md" -Raw).Length
$r2Len = (Get-Content ".agents/rules/ownership-integrity.md" -Raw).Length
$r3Len = (Get-Content ".agents/rules/safety-destructive.md" -Raw).Length
$totalAlways = $agentsLen + $r1Len + $r2Len + $r3Len

if ($totalAlways -gt 9000) {
    Write-Host "`n[FAIL] Tổng nội dung Always-On vượt trần: $totalAlways/9000 ký tự!" -ForegroundColor Red
    $hasErrors = $true
} else {
    Write-Host "`n[PASS] Tổng nội dung Always-On đạt chuẩn: $totalAlways/9000 ký tự." -ForegroundColor Green
}

# 5. Kiểm tra workflows (mỗi workflow <= 60 dòng)
$workflows = Get-ChildItem -Path ".agents/workflows" -Filter *.md
Write-Host "`nKiểm tra $($workflows.Count) workflow trong .agents/workflows/:" -ForegroundColor White
foreach ($wf in $workflows) {
    $lineCount = (Get-Content $wf.FullName).Count
    if ($lineCount -gt 60) {
        Write-Host "  - [FAIL] $($wf.Name) vượt quá 60 dòng ($lineCount dòng)!" -ForegroundColor Red
        $hasErrors = $true
    }
}

# 6. Kiểm tra MEMORY.md (<= 120 dòng)
if (Test-Path "docs/agent/memory/MEMORY.md") {
    $memLines = (Get-Content "docs/agent/memory/MEMORY.md").Count
    if ($memLines -gt 120) {
        Write-Host "`n[FAIL] MEMORY.md vượt quá 120 dòng ($memLines dòng)!" -ForegroundColor Red
        $hasErrors = $true
    } else {
        Write-Host "`n[PASS] MEMORY.md đạt chuẩn: $memLines/120 dòng." -ForegroundColor Green
    }
}

# 7. Kiểm tra tính hợp lệ của STATE.json
if (Test-Path "docs/agent/STATE.json") {
    try {
        $null = Get-Content "docs/agent/STATE.json" -Raw | ConvertFrom-Json
        Write-Host "[PASS] STATE.json định dạng JSON hợp lệ." -ForegroundColor Green
    } catch {
        Write-Host "[FAIL] STATE.json lỗi cú pháp JSON: $_" -ForegroundColor Red
        $hasErrors = $true
    }
}

Write-Host "====================================================" -ForegroundColor Cyan
if ($hasErrors) {
    Write-Host "[KẾT LUẬN] PHÁT HIỆN LỖI TRONG CÁC TỆP HẠ TẦNG AGENT!" -ForegroundColor Red
    exit 1
} else {
    Write-Host "[KẾT LUẬN] TOÀN BỘ HẠ TẦNG AGENT HOÀN TOÀN ĐẠT CHUẨN 100%!" -ForegroundColor Green
    exit 0
}
