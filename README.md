dotnet new webapi -n MyHttpCatApi

dotnet add package System.Net.Http

[Route("[controller]")] // Это означает, что базовый маршрут будет /cat, потому что имя контроллера - CatController
