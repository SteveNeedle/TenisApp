using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TennisApp.Data;
using TennisApp.Models;

namespace TennisApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TennisContext _context;

        public HomeController(ILogger<HomeController> logger, TennisContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEventDetails(int day, string month, int year)
        {
            try
            {
                var eventDate = new DateTime(year, GetMonthNumber(month), day);
                var events = _context.TrainingRecords.Where(r => r.Date == eventDate).ToList();

                var eventDetails = events.Select(e => new { time = e.Time.ToString(@"hh\:mm"), name = e.Name });

                return Json(eventDetails);
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при получении информации о событии.");
            }
        }

        private int GetMonthNumber(string month)
        {
            // Преобразование названия месяца в числовой формат
            switch (month.ToLower())
            {
                case "январь": return 1;
                case "февраль": return 2;
                case "март": return 3;
                case "апрель": return 4;
                case "май": return 5;
                case "июнь": return 6;
                case "июль": return 7;
                case "август": return 8;
                case "сентябрь": return 9;
                case "октябрь": return 10;
                case "ноябрь": return 11;
                case "декабрь": return 12;
                default: throw new ArgumentException("Некорректное название месяца.");
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Trening()
        {
            var lessons = _context.Lessons.ToList();
            return View(lessons);
        }
        [HttpPost]
        public IActionResult AddLesson(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                // Добавление новых данных в базу данных
                _context.Lessons.Add(lesson);
                _context.SaveChanges();

                // Перенаправление на действие, которое отображает таблицу с обновленными данными
                return RedirectToAction("Trening");
            }

            // Если модель недействительна, вернуть представление с ошибками валидации
            return View(lesson);
        }

        [HttpGet]
        public IActionResult FilterLessons(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                var allLessons = _context.Lessons.ToList();
                return PartialView("_LessonsTable", allLessons);
            }

            var filteredLessons = _context.Lessons
                .Where(l => l.Name.ToLower().Contains(filter))
                .ToList();

            return PartialView("_LessonsTable", filteredLessons);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
