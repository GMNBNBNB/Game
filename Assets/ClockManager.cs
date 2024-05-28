using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;

public class ClockManager : MonoBehaviour
{
    public TextMeshProUGUI Date, Time, Season, Week;

    public Light2D sunlight;
    public float nightIntensity;
    public float dayIntensity;
    public AnimationCurve dayNightCurve;

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDateTime;
    }

    private void UpdateDateTime(DateTime dateTime)
    {
        Date.text = dateTime.DateToString();
        Time.text = dateTime.TimeToString();
        Season.text = dateTime.Season.ToString();
        Week.text = $"WK: {dateTime.CurrentWeek}";

        float t = (float)dateTime.Hour / 24f;
        float dayNightT = dayNightCurve.Evaluate(t);
        sunlight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, dayNightT);

    }
}
