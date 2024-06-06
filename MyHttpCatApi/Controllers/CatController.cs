// Подключаем необходимые namespaces
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyHttpCatApi.Controllers
{
    [ApiController] // Этот атрибут указывает, что данный контроллер обрабатывает HTTP-запросы
    [Route("[controller]")] // Устанавливаем базовый маршрут для контрллера (например, /cat)
    public class CatController : ControllerBase
    {
        private readonly HttpClient _httpClient; // Определяем HttpClient для выполнения HTTP-запросов

        // Конструктор, в котором HttpClient внедряется через зависимость
        public CatController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet] // Указываем, что данный метод обрабатывает GET-запросы
        public async Task<IActionResult> Get([FromQuery] int code) // метод Get принимает параметр code из строки запроса
        {
            if (code < 100 || code > 599) // Проверка на валидность статус-кода
            {
                return BadRequest("Invalid status code."); // Если код некорректен, возвращаем ошибку 400
            }

            string url = $"https://http.cat/{code}"; // Формируем URL для запроса к http.cat

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url); // Выполняем GET-запрос
                response.EnsureSuccessStatusCode(); // Если запрос не успешен, выбрасываем исключение

                var responseStream = await response.Content.ReadAsStreamAsync(); // Читаем содержимое ответа как поток
                return File(responseStream, "image/jpeg"); // Возвращаем поток как image/jpeg
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request exception: {e.Message}"); // В случае ошибки возвращаем 500 статус
            }
        }
    }
}
