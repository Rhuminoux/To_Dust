using System;
using UnityEngine;

public class TimeDayNightManager : MonoBehaviour
{
    public delegate void DayPassedEventHandler(EventArgs e);
    public static event DayPassedEventHandler DayPassed;

    public delegate void TimePassedEventHandler(EventArgs e);
    public static event TimePassedEventHandler TimePassed;

    private static readonly int I_SPEED_TIME = 25;
    public float F_ratioSpeedTime = 1;

    public enum Period { Morning = 720 /*End Morning*/, Afternoon = 1080 /*End Afternoon*/, Evening = 1440 /*End Evening*/};

    public int I_numberOfHours { get => I_actualTime / 60; }
    public int I_restOfMinutes { get => I_actualTime % 60; }

    public Period ActualPeriod;
    public int I_actualTime = (int)Period.Morning;
    public int I_numberOfDays = 0;

    void Start()
    {
        InvokeRepeating("AddTimeEachSecond", 1f, 1f);
    }

    void AddTimeEachSecond()
    {
        OnTimePassed();
        if (I_actualTime >= (int)Period.Evening)
        {
            OnDayPassed();
        }
        else if (I_actualTime >= (int)Period.Afternoon)
        {
            ActualPeriod = Period.Evening;
        }
        else if (I_actualTime >= (int)Period.Morning)
        {
            ActualPeriod = Period.Afternoon;
        }
    }

    void OnTimePassed()
    {
        I_actualTime += Convert.ToInt32(I_SPEED_TIME * F_ratioSpeedTime);
        TimePassed?.Invoke(EventArgs.Empty);
    }

    void OnDayPassed()
    {
        I_numberOfDays += 1;
        I_actualTime = 0;
        ActualPeriod = Period.Morning;
        DayPassed?.Invoke(EventArgs.Empty);
    }
}
