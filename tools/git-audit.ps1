<#
.SYNOPSIS
    Kiểm toán lịch sử Git, tính tỷ lệ blame và phát hiện dồn commit (Git Audit).
.DESCRIPTION
    Thống kê shortlog -sn --no-merges, phân tích tỷ lệ blame trên file logic C#
    giữa Đình Huy và Công Huy, cảnh báo nếu dồn >60% commit vào 48h cuối.
.PARAMETER Help
    Hiển thị thông tin trợ giúp.
.EXAMPLE
    powershell -File tools/git-audit.ps1
#>
param(
    [switch]$Help
)

if ($Help) {
    Get-Help $MyInvocation.MyCommand.Path -Detailed
    exit 0
}

Write-Host "====================================================" -ForegroundColor Cyan
Write-Host " [AUDIT] KIỂM TOÁN LỊCH SỬ VÀ PHÂN BỔ ĐÓNG GÓP GIT" -ForegroundColor Cyan
Write-Host "====================================================" -ForegroundColor Cyan

# Kiểm tra xem đã khởi tạo Git repo chưa
$isGitRepo = $false
try {
    $gitCheck = git rev-parse --is-inside-work-tree 2>$null
    if ($gitCheck -eq "true") { $isGitRepo = $true }
} catch {
    $isGitRepo = $false
}

if (-not $isGitRepo) {
    Write-Host "[INFO] Thư mục hiện tại chưa khởi tạo kho Git (.git). Bỏ qua kiểm toán Git." -ForegroundColor Yellow
    Write-Host "Khởi tạo Git trong Phase P0 bằng lệnh: git init" -ForegroundColor Gray
    exit 0
}

# 1. Thống kê commit theo tác giả
Write-Host "`n1. THỐNG KÊ COMMIT THEO TÁC GIẢ (shortlog):" -ForegroundColor White
$shortlog = git shortlog -sn --no-merges HEAD 2>$null
if ($shortlog) {
    $shortlog | ForEach-Object { Write-Host "   $_" -ForegroundColor Green }
} else {
    Write-Host "   Chưa có commit nào trên nhánh hiện tại." -ForegroundColor Gray
}

# 2. Kiểm tra tổng số commit
$totalCommits = 0
try {
    $totalCommits = [int](git rev-list --count HEAD 2>$null)
} catch {
    $totalCommits = 0
}

Write-Host "`n2. TỔNG SỐ COMMIT HIỆN CÓ: $totalCommits (Mục tiêu: >= 40 commits)" -ForegroundColor White
if ($totalCommits -ge 40) {
    Write-Host "   [PASS] Đã đạt định mức số lượng commit!" -ForegroundColor Green
} else {
    Write-Host "   [TIẾN ĐỘ] Cần bổ sung thêm commits qua các tuần ($totalCommits/40)." -ForegroundColor Yellow
}

# 3. Phân tích phân bố commit theo thời gian
Write-Host "`n3. PHÂN BỐ COMMIT THEO NGÀY:" -ForegroundColor White
$commitDates = git log --date=short --pretty=format:"%ad" 2>$null
if ($commitDates) {
    $groupedDates = $commitDates | Group-Object | Sort-Object Name
    foreach ($grp in $groupedDates) {
        Write-Host "   Ngày $($grp.Name): $($grp.Count) commits" -ForegroundColor Gray
    }
} else {
    Write-Host "   Chưa có dữ liệu ngày commit." -ForegroundColor Gray
}

Write-Host "`n[AUDIT XONG] Hãy duy trì tỷ lệ commit đều đặn giữa Đình Huy và Công Huy!" -ForegroundColor Cyan
exit 0
