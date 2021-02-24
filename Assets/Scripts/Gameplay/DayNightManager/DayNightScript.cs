using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    public Light DayNightCycle;

    // Lerping values look at that!
    Color dayColor = new Color(1f, 1f, 0.9f);
    Color nightColor = new Color(0.2f, 0.3f, 0.8f);

    private float IntensityValue = 0f;
    private bool DayOrNight = false;
    // Update is called once per frame
    void Update()
    {
        // Changing intensity of daylight overtime
        // Increased and decrease at certain thresholds
        if (DayOrNight == false)
            IntensityValue += Time.deltaTime / 75;
        else
            IntensityValue -= Time.deltaTime / 75;
        
        if (DayNightCycle.intensity >= 0.8f)
        {
            DayOrNight = true;
            DayNightCycle.color = Color.red;
        }
        else if (DayNightCycle.intensity <= 0.3f)
            DayOrNight = false;

        float ratio = DayNightCycle.intensity / (0.8f - 0.3f);

        float R = Mathf.Lerp(nightColor.r, dayColor.r, ratio);
        float G = Mathf.Lerp(nightColor.g, dayColor.g, ratio);
        float B = Mathf.Lerp(nightColor.b, dayColor.b, ratio);

        DayNightCycle.intensity = IntensityValue;
        DayNightCycle.color = new Color(R, G, B);
    }
}
