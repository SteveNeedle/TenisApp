using System;

namespace TennisApp.Models
{
    public class TrainingRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Name { get; set; }
    }
}
