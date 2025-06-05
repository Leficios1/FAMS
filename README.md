# FAMS - Facility Asset Management System

FAMS là một nền tảng quản lý đào tạo toàn diện được thiết kế để tối ưu hóa việc tạo, triển khai và quản trị các chương trình giáo dục. Hệ thống cung cấp quản lý vòng đời đào tạo từ đầu đến cuối, từ phát triển giáo trình và lập kế hoạch chương trình đến lập lịch và triển khai lớp học. [1](#0-0) 

## Kiến trúc hệ thống

FAMS tuân theo mô hình kiến trúc phân lớp với sự tách biệt rõ ràng các mối quan tâm qua nhiều dự án trong solution:

- **FAMS.Api**: Web API controllers, service layer, authentication
- **FAMS.Core**: Entity Framework context, repository pattern, database migrations  
- **FAMS.Domain**: Entity models, DTOs, constants & enums
- **FAMS.Test**: Unit tests, mock implementations, NUnit framework [2](#0-1) 

## Công nghệ sử dụng

- **Web Framework**: ASP.NET Core 6.0
- **ORM**: Entity Framework Core 6.0
- **Database**: SQL Server
- **Authentication**: JWT Bearer tokens
- **Object Mapping**: AutoMapper
- **Excel Processing**: EPPlus
- **Testing**: NUnit, Moq

## Tính năng chính

### Quản lý giáo trình (Syllabus)
- Tạo và chỉnh sửa giáo trình với thông tin tổng quan, đề cương và sơ đồ đánh giá
- Import/Export Excel với validation và xử lý lỗi
- Tìm kiếm và lọc nâng cao theo mã, tên, ngày tạo
- Sao chép giáo trình hiện có với các chỉnh sửa

### Quản lý chương trình đào tạo
- Tạo chương trình liên kết nhiều giáo trình với thứ tự và validation
- Quản lý trạng thái: Draft, Active, Inactive
- Gán lớp học cho chương trình
- Import/Export Excel với giải quyết xung đột

### Triển khai lớp học
- Lập lịch dựa trên calendar với quản lý ngày/giờ
- Phân công trainer và admin cho lớp
- Quản lý địa điểm vật lý và ảo
- Theo dõi trạng thái: Planning, Scheduled, In-progress, Completed

### Quản lý người dùng & phân quyền
- Kiểm soát truy cập dựa trên vai trò: Super Admin, Class Admin, Trainer
- Xác thực JWT token-based
- Ma trận phân quyền chi tiết (Create, Read, Update, Delete, Import)

## Cơ sở dữ liệu

Hệ thống sử dụng Entity Framework Core với SQL Server, hỗ trợ workflow quản lý đào tạo toàn diện bao gồm tạo giáo trình, quản lý chương trình đào tạo, lập lịch lớp học và kiểm soát truy cập dựa trên vai trò người dùng. [3](#0-2) 

### Các entity chính:
- **Syllabus**: Định nghĩa giáo trình khóa học
- **TrainingProgram**: Tổ hợp giáo trình để triển khai
- **Class**: Instance triển khai đào tạo
- **User**: Người tham gia hệ thống
- **TrainingContent**: Các thành phần học tập riêng lẻ

Hệ thống bao gồm dữ liệu seed toàn diện với 28+ người dùng qua tất cả các cấp độ phân quyền, 8 mục tiêu học tập được định nghĩa trước, 6 loại phương thức giảng dạy và 12+ lớp học mẫu. [4](#0-3) 

## Cài đặt và chạy

1. Clone repository
2. Cấu hình connection string trong appsettings.json
3. Chạy migrations: `dotnet ef database update`
4. Khởi động ứng dụng: `dotnet run`

## Testing

Dự án bao gồm comprehensive unit tests sử dụng NUnit framework với mock implementations để đảm bảo chất lượng code và reliability.

**Notes**

FAMS được thiết kế như một giải pháp enterprise-grade cho các tổ chức giáo dục và đào tạo, với focus vào khả năng mở rộng, bảo mật và trải nghiệm người dùng. Hệ thống hỗ trợ đầy đủ quy trình từ thiết kế giáo trình đến triển khai và đánh giá kết quả đào tạo.

Wiki pages you might want to explore:
- [Overview (Leficios1/FAMS)](/wiki/Leficios1/FAMS#1)
- [Database Design (Leficios1/FAMS)](/wiki/Leficios1/FAMS#2.2)
