using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiftCalculator
{
    public class ShiftSample 
    {
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public bool IsDayOff { get; set; }
        public double Hours { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public double Break { get; set; }
    }

    public class GeneratedShift 
    {
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public double Hours { get; set; }
        public bool IsDayOff { get; set; }
        public double Break { get; set; }
    }

    public class MyFunctions 
    {
        private void GetShiftEndDate(DateTime Start, DateTime End, out DateTime ShiftEndDate) 
        {
            if(Start >= End) 
                End = End.AddDays(1);
            ShiftEndDate = End;
        }
        private GeneratedShift GenerateDayShift(int DayNumber, List<ShiftSample> Samples, DateTime DayDate)
        {
            ShiftSample sample = Samples.Where(x => x.DayNumber == DayNumber).First();
            DateTime startDate = DayDate;
            DateTime endDate = DayDate;
            if(sample.Start.HasValue && sample.End.HasValue) 
            {
                startDate = DayDate.Add(sample.Start.Value);
                endDate = DayDate.Add(sample.End.Value);
                GetShiftEndDate(startDate, endDate, out endDate);
            }                   
            return new GeneratedShift
            {
                Title = sample.Title,
                Start = startDate,
                End =  endDate,
                Hours = sample.Hours,
                Break = sample.Break,
                IsDayOff = sample.IsDayOff
            };
        }
        public List<GeneratedShift> GenerateShiftsForCalendar (int DayNumber, List<ShiftSample> Samples, DateTime CalendarStartDate, DateTime CalendarEndDate) 
        {
            List<GeneratedShift> shifts = new List<GeneratedShift> { };
            int lastDayNumberInSamples = Samples.Last().DayNumber;
            while (CalendarStartDate <= CalendarEndDate)
            {
                shifts.Add(GenerateDayShift(DayNumber, Samples, CalendarStartDate));
                DayNumber = (DayNumber == lastDayNumberInSamples) ? 1 : ++DayNumber;
                CalendarStartDate = CalendarStartDate.AddDays(1);
            }
            return shifts;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<GeneratedShift> shifts = new List<GeneratedShift> { };
            
            DateTime calendarStartDate = new DateTime(2020, 01, 01);
            DateTime calendarEndDate = new DateTime(2020, 12, 31);

            List<ShiftSample> samples = new List<ShiftSample> 
            {
                new ShiftSample { DayNumber = 1, IsDayOff = false, Start = new TimeSpan(7, 0, 0), End = new TimeSpan(19,0,0), Break = 1, Hours = 8, Title = "Day" },
                new ShiftSample { DayNumber = 2, IsDayOff = false, Start = new TimeSpan(7, 0, 0), End = new TimeSpan(19,0,0), Break = 1, Hours = 8, Title = "Day" },
                new ShiftSample { DayNumber = 3, IsDayOff = false, Start = new TimeSpan(19, 0, 0), End = new TimeSpan(7,0,0), Break = 1, Hours = 8, Title = "Night" },
                new ShiftSample { DayNumber = 4, IsDayOff = true, Start = null, End = null, Break = 1, Hours = 8, Title = "Rest" },
                new ShiftSample { DayNumber = 1, IsDayOff = true, Start = null, End = null, Break = 1, Hours = 8, Title = "Off" }
            };

            int dayNumberToStart = 1; //int sampleCounter = 0;             
            MyFunctions functions = new MyFunctions();
            functions.GenerateShiftsForCalendar(dayNumberToStart, samples.OrderBy(x => x.DayNumber).ToList(), calendarStartDate, calendarEndDate);

            shifts = shifts.Where(x => !x.IsDayOff).ToList();
            Console.ReadKey();

            //Console.WriteLine("Hello World!");
        }
    }
}
