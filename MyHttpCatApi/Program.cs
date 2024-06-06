// Создаем билдера для приложения
var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку CORS и разрешаем все источники
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()     // Разрешаем запросы с любых источников
               .AllowAnyMethod()     // Разрешаем использование любых HTTP-методов (GET, POST, и т.д.)
               .AllowAnyHeader();    // Разрешаем любые заголовки в запросах
    });
});

// Регистрируем HttpClient как сервис для внедрения зависимостей
builder.Services.AddHttpClient();

// Добавляем контроллеры для приложения
builder.Services.AddControllers();

// Строим приложение
var app = builder.Build();

// Включаем отображение страницы для разработчиков в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Перенаправляем HTTP-трафик к HTTPS
app.UseHttpsRedirection();

// Включаем поддержку CORS для всех запросов
app.UseCors("AllowAll");

// Включаем авторизацию (может быть конфигурирована позже)
app.UseAuthorization();

// Выстраиваем маршруты для всех контроллеров
app.MapControllers();

// Запускаем приложение
app.Run();
