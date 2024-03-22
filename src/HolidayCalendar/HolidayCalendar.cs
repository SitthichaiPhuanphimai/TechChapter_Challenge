using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualBasic;
using Newtonsoft.Json;


namespace HolidayCalendar;
public class HolidayCalendar : IHolidayCalendar
{
  private String token = "5a208add-a422-4609-a88a-3436f4645a74";


  public bool IsHoliday(DateTime date)
  {
    using (var client = new HttpClient())

    {
      client.BaseAddress = new Uri("https://api.sallinggroup.com/v1/holidays/");
      client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

      try
      {

        var response = client.GetAsync($"is-holiday?date={date:yyyy-MM-dd}").Result;
        response.EnsureSuccessStatusCode();
        var IsHoliday = response.Content.ReadAsStringAsync().Result;

        return bool.Parse(IsHoliday);

      }
      catch (HttpRequestException e)
      {
        Console.WriteLine($"HTTP request failed: {e.Message}");
        throw;
      }


    }
  }
  public ICollection<DateTime> GetHolidays(DateTime startDate, DateTime endDate)
  {
    using (var client = new HttpClient())

    {
      client.BaseAddress = new Uri("https://api.sallinggroup.com/v1/");
      client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

      try
      {
        var response = client.GetAsync($"holidays?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}").Result;

        var holidaysJson = response.Content.ReadAsStringAsync().Result;

        var holidays = JsonConvert.DeserializeObject<List<Holiday>>(holidaysJson);

        var holidayDates = new List<DateTime>();
        foreach (var holiday in holidays)
        {
          if (holiday.NationalHoliday)
          {
            holidayDates.Add(DateTime.Parse(holiday.Date));
          }
        }

        return holidayDates;
      }
      catch (HttpRequestException e)

      {
        Console.WriteLine($"HTTP request failed: {e.Message}");
        throw; 
      }

     
    }
  }
}