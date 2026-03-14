# CodeSphere

**CodeSphere** là một dự án ứng dụng web toàn diện và hiện đại được xây dựng trên nền tảng **ASP.NET Core (.NET 9)**. Dự án là sự kết hợp mạnh mẽ giữa mạng xã hội, hệ thống blog chuyên nghiệp, và tính năng nhắn tin thời gian thực. Dự án sử dụng lượng lớn các công nghệ tiên tiến, tích hợp Machine Learning và hàng loạt các dịch vụ của bên thứ ba, thể hiện khả năng xây dựng các hệ thống quy mô lớn, phức tạp và có tính ứng dụng cao.

## 🚀 Các Tính Năng Nổi Bật (Prominent Features)

Dự án này sở hữu những tính năng cực kỳ ấn tượng, là điểm cộng lớn trong mắt các nhà tuyển dụng:

- **Hệ Thống Mạng Xã Hội Sinh Động:** Bao gồm chức năng Follow/Unfollow, gợi ý kết bạn (Friend Recommendations), hệ thống nhóm người dùng (Groups), đánh giá người dùng (User Ratings), theo dõi xếp hạng và hoạt động chi tiết của từng cá nhân.
- **Hệ Thống Blog Hiện Đại & Tích hợp AI:** Hỗ trợ đăng bài (kèm ảnh), viết bình luận, phân loại (Categories) và gán nhãn (Tags). Các tính năng nâng cao bao gồm: danh sách bài viết yêu thích, hệ thống chờ duyệt/chặn bài viết, và **Tích hợp Machine Learning (ML.NET) để gợi ý bài viết (Post Recommendations)** cho người dùng.
- **Nhắn Tin Thời Gian Thực (Real-Time Chat):** Tính năng chat cá nhân và nhóm với hỗ trợ gửi ảnh, thả Emoji (hỗ trợ chọn màu da/skin tones), kho nhãn dán (Stickers & Favourite Stickers), thiết lập chủ đề chat (Chat Themes/Holiday Themes), và câu trả lời nhanh (Quick Chat Replies).
- **Trí Tuệ Nhân Tạo & Phân Tích Dữ Liệu:** Triển khai `Microsoft.ML` và thuật toán `LightGbm` để huấn luyện các model AI ngay trong hệ thống, cung cấp tính năng cá nhân hóa trải nghiệm người dùng.
- **Xác Thực & Bảo Mật Đa Tầng:** Xác thực qua ASP.NET Core Identity. Tích hợp tính năng đăng nhập bằng mạng xã hội (Google, Facebook) và xác minh bảo mật nâng cao qua SMS/Email (Twilio, SendGrid).
- **Xử Lý Tác Vụ Nền (Background Jobs):** Sử dụng **Hangfire** để đảm bảo ứng dụng chạy mượt mà khi xử lý các tác vụ ngầm, gửi mail, hoặc train model mà không ảnh hưởng tới trải nghiệm người dùng.
- **Quản Lý Lưu Trữ Đám Mây:** Tích hợp **Cloudinary** để quản lý và tối ưu hóa file phương tiện (hình ảnh, video) một cách chuyên nghiệp.
- **Bảo Vệ & Chống Spam:** Tích hợp Google ReCaptcha và Groq AI (tích hợp API Llama-3.1-8b) hỗ trợ phân tích và bảo vệ ứng dụng.

## 🛠 Cấu Hình & Công Nghệ Trọng Tâm (Tech Stack)
- **Framework & Ngôn ngữ:** C#, .NET 9.0 (ASP.NET Core Web SDK, MVC, Blazor Components)
- **Cơ Sở Dữ Liệu:** Microsoft SQL Server thông qua **Entity Framework Core 9.0** (hỗ trợ cả SQLite khi cần thiết).
- **Authentication/Authorization:** Identity Framework + OAuth2 (Google, Facebook).
- **Xử Lý Nền:** Hangfire & Hangfire.SqlServer.
- **Machine Learning:** ML.NET (`Microsoft.ML`, `Microsoft.ML.LightGbm`, `Microsoft.ML.Recommender`).
- **Thư Viện/Dịch Vụ Tích Hợp:** 
  - Gửi mail & SMS: SendGrid, Twilio
  - Lưu trữ ảnh: Cloudinary
  - Trình soạn thảo văn bản: TinyMCE kèm HtmlSanitizer để chống XSS.
  - Phân trang: X.PagedList
  - Xử lý Markdown: Markdig

## 🗄 Mô Hình & Cơ Sở Dữ Liệu (Database Models & Tables)
Mô hình dữ liệu của CodeSphere được tổ chức vô cùng logic và chuẩn hóa cao, bao gồm nhiều Object Domain phức tạp:

*   **Quản Lý Người Dùng & Tương Tác:** `ApplicationUsers`, `ApplicationRoles`, `UserActions`, `FollowUnfollows`, `RecommendedFriends`, `UserRatings`, `Groups`, `UserGroups`.
*   **Hệ Thống Blog & Content:** `Posts`, `PostImages`, `Comments` (hỗ trợ nested comments), `Categories`, `Tags`, `PostsTags`, `PostsLikes`, `FavouritePosts`, `PendingPosts`, `BlockedPosts`.
*   **Hệ Thống Chat & Cảm Xúc:** `ChatMessages`, `ChatImages`, `ChatThemes`, `QuickChatReplies`.
*   **Hệ Thống Stickers & Emojis:** `Emojis`, `EmojiSkins`, `Stickers`, `StickerTypes`, `FavouriteStickers`, `HolidayThemes`, `HolidayIcons`.
*   **Hệ Thống Phân Định Vị Trí Địa Lý:** `ZipCodes`, `Cities`, `States`, `Countries`, `CountryCodes`.

*(Cơ sở dữ liệu được map chặt chẽ qua File `ApplicationDbContext` với các cấu hình cascade/restrict delete logic, giúp bảo vệ toàn vẹn dữ liệu hệ thống).*

## ⚙ Hướng Dẫn Cài Đặt (Setup Required)
*(Vui lòng đảm bảo các file chứa API Key không được push lên public repos).*
1. Yêu cầu hệ thống phải cài đặt **.NET 9 SDK**.
2. Thiết lập nội dung file `appsettings.json` cho cấu hình ConnectionStrings, SendGrid, Twilio, Cloudinary, Groq, App ID Facebook/Google. (File `appsettings.json` đã được đưa vào `.gitignore` để đảm bảo an toàn khoá bí mật).
3. Chạy lệnh: `dotnet ef database update` để khởi tạo cấu trúc các bảng trên SQL Server.
4. Chạy dự án qua Visual Studio hoặc bằng lệnh `dotnet run`.