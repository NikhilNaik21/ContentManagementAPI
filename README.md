
Requirements: 
- CRUD apis to add/update/delete/get data from article and content table
User data can be manually added directly into db
- Article details call with list of all content items
- Paginated request for articles with
o sorting option on title (prioritize english title), default sorting will be
CreatedAt.
o Filtering option on article status
o List should show Title, Author, Status

<img width="806" height="270" alt="image" src="https://github.com/user-attachments/assets/2e1a1ffc-104b-444c-8134-4fb9ff692232" />


ArticleManagementApi/
├── ArticlesController.cs
├── ContentsController.cs
├── Data/
│   └── ArticleManagementDbContext.cs
├── DTO/
│   ├── ArticleDTO.cs
│   ├── ArticleListDTO.cs
│   ├── ArticlePaginatedResponseDTO.cs
│   ├── ArticlePaginationRequestDTO.cs
│   ├── ContentDTO.cs
│   └── UserDTO.cs
├── Enums/
│   ├── Language.cs
│   └── Status.cs
├── Migrations/
│   ├── 20260728125218_InitialCreate.cs
│   └── ArticleManagementDbContextModelSnapshot.cs
├── Models/
│   ├── Articles.cs
│   ├── Contents.cs
│   └── Users.cs
├── Services/
│   ├── Implementations/
│   │   ├── ArticleService.cs
│   │   └── ContentService.cs
│   └── Interfaces/
│       ├── IArticleService.cs
│       └── IContentService.cs
├── .gitignore
├── appsettings.json





