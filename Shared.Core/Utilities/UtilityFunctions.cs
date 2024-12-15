namespace Shared.Core.Utilities;

public static class UtilityFunctions
{
    public static DateTime? CreateDueDateTimeUTC(DateTime StartDateTime, double SLAHrs, double TotalShiftWorkingHrs, int StartShiftHour, int EndShiftHour, int StartShiftMiuntes, int EndShiftMinutes)
    {
        DateTime? DueDateFinal = null;
        try
        {
            int _DayAdded = 0;
            DateTime DayEndShiftDatetime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, EndShiftHour, EndShiftMinutes, 0).ToUniversalTime();

            if (StartDateTime > DayEndShiftDatetime)
            {
                StartDateTime = StartDateTime.AddDays(1);
                DateTime StartShiftTime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, StartShiftHour, StartShiftMiuntes, 0).ToUniversalTime();
                StartDateTime = StartShiftTime;
                DayEndShiftDatetime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, EndShiftHour, EndShiftMinutes, 0).ToUniversalTime();
            }

            if (StartDateTime.AddHours(SLAHrs) == DayEndShiftDatetime)
            {
                DueDateFinal = StartDateTime.AddHours(SLAHrs);
            }
            else if (StartDateTime.AddHours(SLAHrs) > DayEndShiftDatetime)
            {
                _DayAdded++;
                var Remaining = SLAHrs - TotalShiftWorkingHrs;
                if (Remaining < 0)
                {
                    TimeSpan TsEndShiftDiff = StartDateTime.AddHours(SLAHrs).Subtract(DayEndShiftDatetime);
                    Remaining = TsEndShiftDiff.TotalHours;
                }
                while (Remaining > TotalShiftWorkingHrs)
                {
                    _DayAdded++;
                    Remaining = Remaining - TotalShiftWorkingHrs;
                }
                if (Remaining == 0)
                {
                    DueDateFinal = StartDateTime.AddDays(_DayAdded);
                }
                else
                {
                    StartDateTime = StartDateTime.AddDays(_DayAdded);
                    DayEndShiftDatetime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, EndShiftHour, EndShiftMinutes, 0).ToUniversalTime();
                    if (StartDateTime.AddHours(Remaining) > DayEndShiftDatetime)
                    {
                        TimeSpan TsEndShiftDiff = StartDateTime.AddHours(Remaining).Subtract(DayEndShiftDatetime);
                        Remaining = TsEndShiftDiff.TotalHours;
                        StartDateTime = StartDateTime.AddDays(1);
                    }
                    DateTime StartShiftTime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, StartShiftHour, StartShiftMiuntes, 0).ToUniversalTime();
                    DueDateFinal = StartShiftTime.AddHours(Remaining);
                }
            }
            else
            {
                DueDateFinal = StartDateTime.AddHours(SLAHrs);
            }
            return DueDateFinal;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public static DateTime? CalculatePercentageTime(DateTime startDateTimeUTC, DateTime endDateTimeUTC, double percentage)
    {
        try
        {
            TimeSpan duration = endDateTimeUTC - startDateTimeUTC;
            TimeSpan percentageDuration = TimeSpan.FromTicks((long)(duration.Ticks * percentage));
            DateTime resultTime = startDateTimeUTC.Add(percentageDuration);
            return resultTime;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}
