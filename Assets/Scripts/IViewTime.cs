using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IViewTime : MonoBehaviour
{
    protected int hour, minute, second;

    public abstract void SetTime();
    public abstract void GoTime();

    public void SetTimeFromNet()
    {
        hour = NetTime.GetNetworkTime().Hour;
        minute = NetTime.GetNetworkTime().Minute;
        second = NetTime.GetNetworkTime().Second;
    }

    public void GoTime2()
    {
        second++;
        if (second == 60)
        {
            second = 0;
            minute++;
        }
        if (minute == 60)
        {
            minute = 0;
            hour++;
        }
    }

    public Vector3 GetTime()
    {
        return new Vector3(hour, minute, second);
    }
}
