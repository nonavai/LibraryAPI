# LibraryAPI
3-Layer API for Library with: REST, EF, JWT, FLuentValidation

To Run the app you should:

1. Go to appsettings.json and change DataBase Server name to your own
2. Use Update-Databse in "package manager console", for example in Visual Studio
3. When it runs go to path /swagger/index.html to use inteface of Swagger

! If you have problems with building project its might be caused by missing "bin" and "obj" files. In that case go to Visual Studio Terminal and write "dotnet build".

On api level I decided to leave mapping as on BLL, as I thought that adding BLL dependency on API level is not desirable within the development on REST.

