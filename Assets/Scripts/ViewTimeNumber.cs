using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewTimeNumber : IViewTime
{
    [SerializeField] private Text textTime;

    public override void SetTime()
    {
        SetTextTime();
    }

    public override void GoTime()
    {
        GoTime2();
        SetTextTime();
    }

    private void SetTextTime()
    {
        textTime.text = $"{hour:d2}:{minute:d2}:{second:d2}";
    }
}
