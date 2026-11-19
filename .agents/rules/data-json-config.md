<!-- activation: glob:**/*.json -->
# QUY TẮC CẤU HÌNH DỮ LIỆU & LƯU TRỮ JSON (DATA JSON CONFIG)
<!-- Cập nhật: 2026-11-19 | Sở hữu: Nguyễn Đình Huy & Nguyễn Công Huy -->

## 1. DỮ LIỆU HÓA 100% CẤU HÌNH TRÒ CHƠI
- Toàn bộ thông số 10 PCUs, 7 kiểu quái vật, 8 vật phẩm/trang bị, 5 kỹ năng và 5 phản ứng nguyên tố bắt buộc lưu trữ dưới dạng JSON trong thư mục `StreamingAssets/Data/`.
- CẤM hardcode chỉ số máu, sát thương, tốc độ hoặc chuỗi tên hiển thị trong mã C#.

## 2. QUY ƯỚC ĐƯỜNG DẪN TƯƠNG ĐỐI
- Khi nạp dữ liệu từ mã nguồn, bắt buộc dùng đường dẫn tương đối qua `Application.streamingAssetsPath` hoặc đường dẫn tương đối từ thư mục gốc dự án.
- CẤM tuyệt đối đường dẫn cứng cá nhân dạng `C:\Users\...` hoặc `D:\project\...`.

## 3. KIỂM CHỨNG TÍNH HỢP LỆ KHI NẠP DỮ LIỆU (VALIDATION)
- Khi đọc các file cấu hình màn chơi (`floors.json`) hoặc quái vật (`enemies.json`), bộ parser phải kiểm tra tính toàn vẹn (bắt buộc có id, name, stats không âm, danh sách room hợp lệ).
- Nếu phát hiện dữ liệu thiếu, sai định dạng hoặc không logic: Bắt buộc ném ngoại lệ tùy biến `InvalidLevelDataException` kèm thông báo chi tiết lỗi và vị trí tệp. Cấm nuốt lỗi hoặc trả về null âm thầm.

## 4. XỬ LÝ LỖI FILE LƯU TRỮ (SAVE / LOAD INTEGRITY)
- Dữ liệu lưu trạng thái màn chơi và kho đồ (`save_slot_*.json`) phải có trường kiểm tra phiên bản schema (`schemaVersion`) và checksum.
- Nếu người chơi sửa tay làm hỏng cấu trúc file save hoặc trường bắt buộc bị thiếu: Hệ thống bắt buộc ném `SaveCorruptedException`, đồng thời ghi log cảnh báo và đề xuất người chơi tạo màn chơi mới an toàn, không được làm crash game.
