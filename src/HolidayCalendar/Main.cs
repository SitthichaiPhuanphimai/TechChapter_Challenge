using System;

class App
{
    static void Main(string[] args)
    {

        HolidayCalendar.HolidayCalendar calendar = new HolidayCalendar.HolidayCalendar();

        DateTime date = new DateTime(2025,12, 25);
        Console.WriteLine(calendar.IsHoliday(date));
  

        DateTime startDate = new DateTime(2023, 4, 1);
        DateTime endDate = new DateTime(2023, 4, 30);
        var holidays = calendar.GetHolidays(startDate, endDate);
        foreach (var holiday in holidays)
        {
            Console.WriteLine(holiday);
        }


    }
}