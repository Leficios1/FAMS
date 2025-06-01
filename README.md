# FAMS - Fresher Academy Management System

## Một số quy tắc viết code và đặt tên branch 
1. Quy tắc đặt tên service và repo: 
   * Interface: I<tên-entity>Service/Repo
   * Class: <tên-entity>Service/Repo
2. Tên controller: <tên-entity>sController
    * Ví dụ: UsersController, UserPermissionsController
3. Tên branch tính năng: feature/<tên-chức-năng>
4. Tên branch sửa lỗi và resolve conflict
    * Sửa lỗi: hotfix/<vị trí>-<tên-chức-năng>
    * Resolve conflict: hotfix/<resolve-conflict>-<tên-chức-năng>

## CHÚ Ý QUAN TRỌNG
1. Không thay đổi code bên trong class BaseApiController
2. Không thay đổi code trong thư mục Migrations, class lib FAMS.Core
    * Khi chạy database chỉ cần dùng lệnh update-database

### © 2024 Group 4 - NET_05 