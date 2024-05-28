using UnityEngine;
using UnityEngine.Events;


public class TimeManager : MonoBehaviour
{

    [Header("Date & Time Settings")]
    [Range(1, 28)]
    public int dateInMonth;
    [Range(1, 4)]
    public int season;
    [Range(1, 99)]
    public int year;
    [Range(0, 24)]
    public int hour;
    [Range(0, 6)]
    public int minutes;

    private DateTime DateTime;

    [Header("Tick Settings")]
    public int TickSecondsIncrease = 10;
    public float TimeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;

    public static UnityAction<DateTime> OnDateTimeChanged;

    private void Awake()
    {
        DateTime = new DateTime(dateInMonth, season - 1, year, hour, minutes * 10);
    }
    private void Start()
    {
        OnDateTimeChanged?.Invoke(DateTime);
    }
    private void Update()
    {
        currentTimeBetweenTicks += Time.deltaTime;
        if (currentTimeBetweenTicks >= TimeBetweenTicks)
        {
            currentTimeBetweenTicks = 0;
            Tick();
        }
    }

    void Tick()
    {
        AdvanceTime();
    }

    void AdvanceTime()
    {
        DateTime.AdvanceMinutes(TickSecondsIncrease);

        OnDateTimeChanged?.Invoke(DateTime);
    }
}

[System.Serializable]
public struct DateTime
{
    private Days day;
    private int date;
    private int year;
    private int hour;
    private int minutes;
    private Season season;

    private int totalNumDays;
    private int totalNumWeeks;

    public Days Day => day;
    public int Date => date;
    public int Year => year;
    public int Hour => hour;
    public int Minute => minutes;
    public Season Season => season;
    public int TotalNumDays => totalNumDays;
    public int TotalNumWeeks => totalNumWeeks;
    public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;

    public DateTime(int date, int season, int year, int hour, int minutes)
    {
        this.day = (Days)(date % 7);
        if (day == 0) day = (Days)7;
        this.date = date;
        this.year = year;
        this.hour = hour;
        this.minutes = minutes;
        this.season = (Season)season;

        totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));

        totalNumWeeks = 1 + totalNumDays / 7;
    }

    #region Time Advance
    public void AdvanceMinutes(int SecondsToAdvanceBy)
    {
        if (minutes + SecondsToAdvanceBy >= 60)
        {
            minutes = (minutes + SecondsToAdvanceBy) % 60;
            AdvanceHour();
        }
        else
        {
            minutes += SecondsToAdvanceBy;
        }
    }

    private void AdvanceHour()
    {
        if ((hour + 1) == 24)
        {
            hour = 0;
            AdvanceDay();
        }
        else
        {
            hour++;
        }
    }

    private void AdvanceDay()
    {
        day++;
        if (day + 1 > (Days)7)
        {
            day = (Days)1;
            totalNumWeeks++;
        }
        date++;
        if (date % 29 == 0)
        {
            AdvanceSeason();
            date = 1;
        }

        totalNumDays++;
    }

    private void AdvanceSeason()
    {
        if (Season == Season.Winter)
        {
            season = Season.Spring;
            AdvanceYear();
        }
        else
        {
            season++;
        }
    }
    private void AdvanceYear()
    {
        date = 1;
        year++;
    }
    #endregion

    #region Bool Checks
    public bool IsNight()
    {
        return hour > 18 || hour < 6;
    }
    public bool IsMorning()
    {
        return hour >= 6 && hour <= 12;
    }
    public bool IsAfternoon()
    {
        return hour > 12 && hour < 18;
    }
    public bool IsWeekend()
    {
        return day > Days.Fri ? true : false;
    }
    public bool IsParticularDay(Days _day)
    {
        return day == _day;
    }
    #endregion

    #region Key Dates
    public DateTime NewYearsDay(int year)
    {
        if (year == 0) year = 1;
        return new DateTime(1, 0, year, 6, 0);
    }
    #endregion

    public override string ToString()
    {
        return $"Date: {DateToString()} Season: {season} Time: {TimeToString()} " + $"\nTotal Days: {totalNumDays} | Total Weeks: {totalNumWeeks}";
    }

    public string DateToString()
    {
        return $"{Day} {Date} {Year.ToString("D2")}";
    }

    public string TimeToString()
    {
        int adjustedHour = 0;
        if (hour == 0)
        {
            adjustedHour = 12;
        }
        else if (hour >= 13)
        {
            adjustedHour = hour - 12;
        }
        else
        {
            adjustedHour = hour;
        }

        string AmPm = hour == 0 || hour < 12 ? "AM" : "PM";
        return $"{adjustedHour.ToString("D2")} : {minutes.ToString("D2")} {AmPm}";
    }
}

[System.Serializable]
public enum Days
{
    NULL = 0,
    Mon = 1,
    Tue = 2,
    Wed = 3,
    Thu = 4,
    Fri = 5,
    Sat = 6,
    Sun = 7
}

[System.Serializable]
public enum Season
{
    Spring = 0,
    Summer = 1,
    Autumn = 2,
    Winter = 3
}
