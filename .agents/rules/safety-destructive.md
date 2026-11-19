<!-- activation: always -->
# AN TOÀN THAO TÁC & CHỐNG PHÁ HỦY (SAFETY DESTRUCTIVE)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. LỆNH CẤM THỰC THI TỰ ĐỘNG
CẤM chạy các lệnh sau khi chưa hỏi và được sinh viên chấp thuận:
- `git reset --hard`, `git clean -fd`, `git checkout -- .` (mất mã nguồn).
- `git push --force` (ghi đè nhánh, mất commit đồng đội).
- Xóa file hoặc thư mục (`rm -rf`, `Remove-Item -Recurse`).
- Ghi đè file cấu hình khi chưa so sánh diff.

## 2. VÙNG DỮ LIỆU BẤT KHẢ XÂM PHẠM
- Thư mục `.git/`: Cấm can thiệp trực tiếp CSDL Git.
- Thư mục `docs/ref/`: Chứa đề bài, checklist gốc; cấm sửa/xóa.
- Thư mục `assets_src/`: Chứa asset pack gốc; cấm sửa/xóa.
- Tệp `.meta` của Unity: Cấm xóa lẻ làm gãy liên kết GUID trong Scene/Prefab.
- `ProjectSettings/`: Chỉ thay đổi khi có yêu cầu cấu hình rõ ràng trong phase.
