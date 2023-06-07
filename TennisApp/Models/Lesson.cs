using System;

namespace TennisApp.Models
{
    public class Lesson
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
    }
}
