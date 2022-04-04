using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    [Header("Перевод будильника на стрелочный формат")]
    [SerializeField] private bool isArrowAlarm = false;
    [SerializeField] private InputField inputFieldHour, inputFieldMinute, inputFieldSecond;

    private int hour, minute, second;
    private int stepHour = -30;
    private float timer;
    private float angleArrowAlarm = 0;

    private GameObject currentArrowAlarm;

    private void Update()
    {
        if (isArrowAlarm)
        {
            SetAlarmArrow();
        }
    }

    /// <summary>
    /// будильник со стрелками сделан по аналогии будильника настольного с одной стрелкой. Можно переделать на три стрелки будильника
    /// </summary>
    private void SetAlarmArrow()
    {
        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    currentArrowAlarm = hit.collider.gameObject;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentArrowAlarm = null;
        }

        if (currentArrowAlarm != null && Input.GetMouseButton(0) && timer <= 0)
        {
            angleArrowAlarm += stepHour;
            hour++;

            currentArrowAlarm.transform.rotation = Quaternion.Euler(0f, 0f, angleArrowAlarm);

            timer = 0.5f;
        }
    }

    /// <summary>
    /// метод для кнопки, нажимать после ввода трёх чисел
    /// </summary>
    public void SetAlarmNumber()
    {
        if (!isArrowAlarm)
        {
            hour = 0;
            if (!int.TryParse(inputFieldHour.text, out hour))
            {
                Debug.Log("Incorrect input hour");
            }

            minute = 0;
            if (!int.TryParse(inputFieldMinute.text, out minute))
            {
                Debug.Log("Incorrect input minute");
            }

            second = 0;
            if (!int.TryParse(inputFieldSecond.text, out second))
            {
                Debug.Log("Incorrect input second");
            }
        }
    }

    public Vector3 GetTime()
    {
        return new Vector3(hour, minute, second);
    }
}
