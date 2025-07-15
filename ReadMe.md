Step1.建立專案

Step2.Db First
- Install-Package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef dbcontext scaffold "你的連線字串" Microsoft.EntityFrameworkCore.SqlServer -o Models

Step3.先用建立訂單資料

Step4.建立專案透過API專案VIEW專案及API專案分離
