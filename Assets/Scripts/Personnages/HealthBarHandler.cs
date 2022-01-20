using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    private static Image HealthBarImage;

    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    /// 

    void Update()
    {
        GameObject Unit = GameObject.Find("Unit");

        Debug.Log(Unit.GetComponent<Unit>().HPPercentage());

        SetHealthBarValue(Unit.GetComponent<Unit>().HPPercentage());
    }
    public static void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value/100;
        if (HealthBarImage.fillAmount < 0.2f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (HealthBarImage.fillAmount < 0.4f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public static float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public static void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }
}