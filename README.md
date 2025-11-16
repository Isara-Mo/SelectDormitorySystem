# SelectDormitory System

A new student dormitory selection system with work-rest schedule allocation functionality. Before selecting a dormitory, students are required to fill in their work-rest schedule, categorized as early sleepers or late sleepers. After statistical analysis, corresponding rooms are allocated based on gender, major, and work-rest schedule. Students can choose accommodation within their assigned range. Additionally, the system provides administrators with a one-click allocation feature that automatically assigns appropriate dormitories to students who have not yet filled in their work-rest schedule or selected a dormitory.

## Features

- **Student Login**: Secure login system with password hashing (SHA256)
- **Work-Rest Schedule**: Students can fill in their daily schedule preferences (early sleep/late sleep)
- **Smart Allocation**: Automatic room allocation based on:
  - Gender
  - Major
  - Work-rest schedule
- **Student Selection**: Students can choose dormitories within their assigned range
- **Admin Management**: Administrators can:
  - View all student information
  - One-click automatic allocation for unassigned students
  - Manage dormitory assignments

## Technology Stack

- **Language**: C# (.NET Framework)
- **UI Framework**: Windows Forms
- **Database**: SQL Server
- **Security**: SHA256 password hashing

## Project Structure

```
SelectDormitory/
├── SelectDormitory/
│   ├── Form1.cs          # Login form
│   ├── Form2.cs          # Admin management form
│   ├── Form3.cs          # Student main form
│   ├── Form3_1.cs        # Student schedule form
│   ├── Form3_2.cs        # Student dormitory selection form
│   ├── Form3_3.cs        # Student information form
│   ├── Form4.cs          # Additional form
│   ├── Dao.cs            # Database access layer
│   └── Program.cs        # Application entry point
└── original_v1.2.sql     # Database schema
```

## Database Configuration

The system uses SQL Server database. Update the connection string in `Dao.cs`:

```csharp
string str = "Data Source=YOUR_SERVER;Initial Catalog=SelectDormitory;Integrated Security=True;...";
```

## Installation

1. Clone the repository
2. Restore the database using `original_v1.2.sql`
3. Update the database connection string in `Dao.cs`
4. Build the project using Visual Studio
5. Run the application

## Usage

### For Students
1. Login with student credentials
2. Fill in work-rest schedule (early sleep/late sleep)
3. Select a dormitory from available options within your assigned range
4. View your dormitory assignment information

### For Administrators
1. Login with admin credentials
2. View all student information and dormitory assignments
3. Use one-click allocation to automatically assign dormitories to unassigned students

## Security

- Passwords are hashed using SHA256 algorithm
- Secure database connection with integrated security

## License

This project is for educational purposes.

---

<details>
<summary><strong>中文版本 / Chinese Version</strong></summary>

# 选宿系统

一个带有作息分配功能的新学生选宿系统。在选宿之前，学生需要填写作息情况，分为早睡和晚睡两类。经过统计分析后，系统会根据性别、专业和作息情况分配相应的房间。学生可以在自己分配的范围内选择住宿。此外，系统为管理员提供了一键分配功能，可以自动为尚未填写作息情况或选择宿舍的学生分配相应的宿舍。

## 功能特性

- **学生登录**：使用密码哈希（SHA256）的安全登录系统
- **作息安排**：学生可以填写日常作息偏好（早睡/晚睡）
- **智能分配**：基于以下条件的自动房间分配：
  - 性别
  - 专业
  - 作息安排
- **学生选择**：学生可以在分配的范围内选择宿舍
- **管理员管理**：管理员可以：
  - 查看所有学生信息
  - 一键自动分配未分配的学生
  - 管理宿舍分配

## 技术栈

- **编程语言**：C# (.NET Framework)
- **UI框架**：Windows Forms
- **数据库**：SQL Server
- **安全**：SHA256 密码哈希

## 项目结构

```
SelectDormitory/
├── SelectDormitory/
│   ├── Form1.cs          # 登录表单
│   ├── Form2.cs          # 管理员管理表单
│   ├── Form3.cs          # 学生主表单
│   ├── Form3_1.cs        # 学生作息表单
│   ├── Form3_2.cs        # 学生选宿表单
│   ├── Form3_3.cs        # 学生信息表单
│   ├── Form4.cs          # 其他表单
│   ├── Dao.cs            # 数据库访问层
│   └── Program.cs        # 应用程序入口点
└── original_v1.2.sql     # 数据库架构
```

## 数据库配置

系统使用 SQL Server 数据库。请在 `Dao.cs` 中更新连接字符串：

```csharp
string str = "Data Source=YOUR_SERVER;Initial Catalog=SelectDormitory;Integrated Security=True;...";
```

## 安装步骤

1. 克隆仓库
2. 使用 `original_v1.2.sql` 恢复数据库
3. 在 `Dao.cs` 中更新数据库连接字符串
4. 使用 Visual Studio 构建项目
5. 运行应用程序

## 使用方法

### 学生用户
1. 使用学生凭据登录
2. 填写作息安排（早睡/晚睡）
3. 从分配范围内的可用选项中选择宿舍
4. 查看宿舍分配信息

### 管理员用户
1. 使用管理员凭据登录
2. 查看所有学生信息和宿舍分配
3. 使用一键分配功能自动为未分配的学生分配宿舍

## 安全性

- 使用 SHA256 算法对密码进行哈希处理
- 使用集成安全性的安全数据库连接

## 许可证

本项目仅供教育用途。

</details>
