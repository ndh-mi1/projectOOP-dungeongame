---
name: roguelike-algorithms-reference
description: Tài liệu tham khảo 6 thuật toán phi tầm thường trong game: A*, Bresenham, Swept AABB, BSP, MST, Blob47.
---

# TỔNG QUAN 6 THUẬT TOÁN PHI TẦM THƯỜNG TRONG CRYSTAL DEPTHS

## 1. THUẬT TOÁN TÌM ĐƯỜNG A* (`Pathfinder.cs` - TV1)
- **Mục đích:** Tìm đường đi ngắn nhất cho quái vật đuổi theo người chơi trên lưới ô.
- **Độ phức tạp:** $\mathcal{O}(E \log V)$ với hàng đợi ưu tiên `PriorityQueue`.
- **Hàm Heuristic:** Khoảng cách Manhattan $h(A, B) = |x_A - x_B| + |y_A - y_B|$.
- **Ca biên:** Đích đến bị tường vây kín $\rightarrow$ Dừng an toàn khi tập `OpenSet` rỗng, trả về đường đi rỗng, không treo vòng lặp vô tận.

## 2. THUẬT TOÁN ĐƯỜNG NGẮM BRESENHAM (`LineOfSight.cs` - TV1)
- **Mục đích:** Kiểm tra đường thẳng nối giữa quái vật tầm xa và người chơi có bị tường che khuất không.
- **Độ phức tạp:** $\mathcal{O}(\max(\Delta x, \Delta y))$ chỉ dùng phép cộng/trừ số nguyên.

## 3. VA CHẠM QUÉT HỘP BAO SWEPT AABB (`CollisionDetector.cs` - TV1)
- **Mục đích:** Chống xuyên tường (tunneling) khi thực thể hoặc đạn di chuyển tốc độ cao.
- **Nguyên lý:** Tính thời gian va chạm trên trục X và Y: $t_{entry} = \max(t_{x\_entry}, t_{y\_entry})$, $t_{exit} = \min(t_{x\_exit}, t_{y\_exit})$. Va chạm xảy ra nếu $t_{entry} < t_{exit}$ và $t_{entry} \in [0, 1]$.
- **Tối ưu:** Kết hợp Spatial Hash lưới $4 \times 4$ ô để giảm số cặp kiểm tra từ $\mathcal{O}(N^2)$ xuống $\mathcal{O}(N)$.

## 4. PHÂN CHIA KHÔNG GIAN BSP & NỐI PHÒNG MST (`DungeonGenerator.cs` - TV2)
- **BSP (Binary Space Partitioning):** Đệ quy chia hình chữ nhật tầng hầm thành các phòng con với kích thước tối thiểu $8 \times 8$.
- **MST (Minimum Spanning Tree - Kruskal):** Nối các tâm phòng bằng cây khung nhỏ nhất bảo đảm mọi phòng đều liên thông; thêm ngẫu nhiên $15\%$ cạnh phụ tạo chu trình vòng tránh cụt đường.
- **Kiểm chứng BFS:** Chạy Breadth-First Search từ phòng Start để chứng minh $100\%$ đến được phòng Boss và Exit.

## 5. TỰ ĐỘNG NỐI NGÓI BLOB47 AUTOTILING (`Blob47Autotiler.cs` - TV2)
- **Nguyên lý:** Tính giá trị bitmask 8 hướng xung quanh ô gạch ($2^8 = 256$ trạng thái, quy về 47 mẫu chuẩn).
- **Quy tắc bit chéo (Diagonal Corner Constraint):** Bit góc chéo (Tây Bắc, Đông Bắc, Tây Nam, Đông Nam) CHỈ ĐƯỢC PHÉP TÍNH là 1 khi và chỉ khi CẢ HAI ô vuông góc kề cạnh nó đều có gạch! Quy tắc này ngăn chặn triệt để lỗi hiển thị ngói góc khi chỉ có tường chéo.
