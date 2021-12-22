using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int _hoursPerDay = 6;//Ile godzin ma doba
    [SerializeField] private int _minutesPerDayMultipiler = 60;//mnożnik minut dla doby
    private int _minutesPerDay; //ile minut trwa doba
    private int _time = 0; //czas w minutach od północy
    private int _day = 1;//który dzień od początku rozgrywki (numeracja od 1)
    
    // Start is called before the first frame update
    void Start()
    {
        _minutesPerDay = _hoursPerDay * _minutesPerDayMultipiler;
    }

    public void PassTime(int minutesToPass)
    {
        _time += minutesToPass;
        if (_time >= _minutesPerDay)
        {
            _time -= _minutesPerDay;
            _day++;
        }
    }

    public string GetTimeAsString() //Wyświetlanie zegara
    {
        int hours = _time / 60;
        int minutes = _time % 60;

        if (minutes < 10) return $"{hours.ToString()}:0{minutes.ToString()}";
        else return $"{hours.ToString()}:{minutes.ToString()}";
    }

    public int GetTimeInMinutes()
    {
        return _time;
    }

    public int GetDayNumber()
    {
        return _day;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
