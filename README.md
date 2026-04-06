# Hệ thống Quản lý Phòng trọ (ASP.NET Core 8 MVC)

## Giới thiệu
Dư án được xây dựng để tối ưu hóa việc quản lý vận hành nhà trọ, giúp chủ trọ kiểm soát thông tin khách thuê và chỉ số điện nước một cách khoa học.

## Tính năng chính
- **Quản lý phòng:** Phân loại phòng, diện tích, giá thuê và trạng thái (Trống/Đã thuê).
- **Quản lý hợp đồng:** Lưu trữ thông tin khách hàng, ngày thuê, tiền cọc.
- **Tính tiền dịch vụ:** Tự động tính toán hóa đơn Điện (KWh), Nước (m3) và Rác theo tháng.
- **Bảo mật hệ thống:**
  - Mã hóa mật khẩu (BCrypt).
  - Phân quyền người dùng (Admin/Staff).

## Công nghệ sử dụng
- **Ngôn ngữ:** C# (ASP.NET Core 8 MVC).
- **Cơ sở dữ liệu:** SQL Server + Entity Framework Core.
- **Giao diện:** Razor Pages, JQuery.

## Hướng dẫn chạy dự án
- **Clone dự án:** Mở `CMD` gõ lệnh `git clone https://github.com/KhoaNguyen-IT2K4/room_management.git`.
- **Database:** Vào thư mục Database, chạy script `room_management.sql` trong SQL Server.
- **Cấu hình:** Cập nhật chuỗi kết nối trong `appsettings.json`.
- **Chạy:** Mở file `.sln` bằng Visual Studio và nhấn `F5`.

## Tài khoản Demo
- **Admin:** `admin@system.com` / `123456`.
- **Staff:** `staff@system.com` / `123456`.
