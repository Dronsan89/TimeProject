using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTimeArrow : IViewTime
{
    [SerializeField] private Transform arrowHourPosition, arrowMinutePosition, arrowSecondPosition;

    private int stepHours = -30;
    private int stepMinuteSecond = -6;

    public override void SetTime()
    {
        SetPositionArrow();
    }

    public override void GoTime()
    {
        GoTime2();
        SetPositionArrow();
    }

    private void SetPositionArrow()
    {
        arrowHourPosition.rotation = Quaternion.Euler(0, 0, stepHours * hour);
        arrowMinutePosition.rotation = Quaternion.Euler(0, 0, stepMinuteSecond * minute);
        arrowSecondPosition.rotation = Quaternion.Euler(0, 0, stepMinuteSecond * second);
    }
}
