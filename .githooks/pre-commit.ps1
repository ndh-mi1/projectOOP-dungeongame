# Pre-commit hook script (PowerShell)
Write-Host "Running pre-commit checks..." -ForegroundColor Cyan

# 1. Check Core purity
powershell -ExecutionPolicy Bypass -File tools/check-core-purity.ps1
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ABORT] Commit bị hủy do vi phạm tính thuần khiết của Game.Core!" -ForegroundColor Red
    exit 1
}

# 2. Check code smells
powershell -ExecutionPolicy Bypass -File tools/check-code-smells.ps1
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ABORT] Commit bị hủy do tồn tại lỗi code smell nghiêm trọng!" -ForegroundColor Red
    exit 1
}

Write-Host "[OK] Pre-commit hook kiểm tra thành công." -ForegroundColor Green
exit 0
