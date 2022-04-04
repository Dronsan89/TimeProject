using UnityEngine;

public class ControllerTime : MonoBehaviour
{
    [SerializeField] private IViewTime[] viewTimes;
    [SerializeField] private Alarm alarm;

    private float timerSecond;
    private float timerHour;

    void Start()
    {
        timerSecond = 1;
        timerHour = 3600;

        //метод вынесен в отдельный цикл, т.к. происходит рассинхрон по секундам
        for (int i = 0; i < viewTimes.Length; i++)
        {
            viewTimes[i].SetTimeFromNet();
        }

        for (int i = 0; i < viewTimes.Length; i++)
        {
            viewTimes[i].SetTime();
        }
    }

    void Update()
    {
        timerSecond -= Time.deltaTime;
        timerHour -= Time.deltaTime;

        if (timerSecond <= 0)
        {
            for (int i = 0; i < viewTimes.Length; i++)
            {
                viewTimes[i].GoTime();

                if (viewTimes[i].GetTime() == alarm.GetTime())
                {
                    GoAlarm();
                }
            }
            timerSecond = 1;
        }

        if(timerHour <=0)
        {
            for (int i = 0; i < viewTimes.Length; i++)
            {
                viewTimes[i].SetTimeFromNet();
            }
            timerHour = 3600;
        }
    }

    private void GoAlarm()
    {
        Debug.Log("Будильник звонит!");
    }
}
