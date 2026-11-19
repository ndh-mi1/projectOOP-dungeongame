# CHƯƠNG [X]: [TÊN CHƯƠNG THEO MẠCH PDF V2026]
**Học phần:** Lập trình hướng đối tượng (MI4090) – K69  
**Dự án:** Crystal Depths – Hầm Ngục Pha Lê  
**Tác giả:** Nguyễn Đình Huy (MSSV: 202418918 - TV1) & Nguyễn Công Huy (MSSV: 202418914 - TV2)  
**Giai đoạn tương ứng:** Phase [N] – [Tên Phase]  

---

## 1. MỤC TIÊU & ĐẶC TẢ CHIỀU XUÔI [A → B]
*Trình bày bài toán nghiệp vụ, đặc tả yêu cầu, mô hình toán/hình học hoặc sơ đồ thiết kế trước khi cài đặt.*

- **Mục tiêu kỹ thuật**: 
- **Quy tắc / Luật chơi liên quan**: (Truy vết mã `R-xx`)
- **Use Case thực thi**: (Truy vết mã `UCxx`)
- **Sơ đồ phân tích / Thiết kế**: *(Nhúng PlantUML / Mermaid tương ứng)*

---

## 2. HIỆN THỰC MÃ NGUỒN & ĐỐI CHIẾU CHIỀU NGƯỢC [B → A]
*Trình bày chi tiết mã nguồn, vị trí tệp, vai trò các lớp, giải thích lý do thiết kế (không chỉ chép code).*

### 2.1 Cấu trúc Lớp & Trách nhiệm (SRP)
| Tên Lớp / Interface | Namespace / Tệp nguồn | Trách nhiệm cốt lõi (1 câu) | Thành viên phụ trách |
|---|---|---|:---:|
| `ClassExample` | `Game.Core.Module.ClassExample.cs` | ... | TV1 / TV2 |

### 2.2 Phân tích Đoạn mã Trọng tâm
```csharp
// Trích đoạn mã nguồn thực tế (tối đa 15-20 dòng, không dán lan man)
public void CoreMethod()
{
    // ...
}
```
- **Ý nghĩa các biến và điều kiện rẽ nhánh**: Giải thích vì sao dùng điều kiện này thay vì điều kiện khác.
- **Bảo vệ Bất biến (Invariants)**: Nêu rõ bất biến được duy trì tại đoạn mã này.

---

## 3. THỰC NGHIỆM ĐỘ HIỂU SÂU: NHIỄU LOẠN (Δ), ĐÁNH ĐỔI (W) & LIVE CODING

### 3.1 Thử nghiệm Nhiễu loạn (Δ - Perturbation Probe)
- **Tình huống giả định**: Nếu giảng viên yêu cầu đổi quy tắc X thành Y...
- **Vùng ảnh hưởng (Blast Radius)**: Chỉ đúng lớp nào, phương thức nào cần sửa.
- **Chứng minh tính đóng kín (OCP)**: Chứng minh các lớp khác không bị ảnh hưởng.

### 3.2 Phản biện Đánh đổi Thiết kế (W - Trade-off Defense)
- **Phương án lựa chọn**: Sử dụng Pattern/Cấu trúc X.
- **Phương án thay thế bị loại bỏ**: Tại sao không dùng Kế thừa đơn giản / switch-case / Singleton?
- **Ít nhất 2 nhược điểm cụ thể**: Chi phí phải trả về số lượng lớp, luồng gọi hàm, hoặc quản lý bộ nhớ.

---

## 4. KIỂM THỬ TỰ ĐỘNG & MINH CHỨNG HAI CHIỀU

### 4.1 Bảng Truy vết Ca kiểm thử
| Mã Test (TC-ID) | Luật kiểm tra | Phương thức Test | Trạng thái (EditMode) |
|---|---|---|:---:|
| `TC-MOD-01` | R-xx: ... | `Test_Class_Should_DoSomething_When_Condition()` | **PASS** |

### 4.2 Kết quả Kiểm chứng Mutation Check
- **Điểm đột biến giả định**: Sửa `<= ` thành `<` tại dòng ...
- **Ca test bắt lỗi**: Test `TC-MOD-01` chuyển **FAIL** đỏ ngay lập tức, chứng minh test không hình thức.

---

## 5. BẢNG PHÂN CÔNG THỰC TẾ & GIT COMMITS
- **Đóng góp của TV1**: ... (Các commit: `hash1`, `hash2`)
- **Đóng góp của TV2**: ... (Các commit: `hash3`, `hash4`)
- **Liên kết chéo**: PR #X do TV1 tạo, TV2 review và approve.
