using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    public Light DayNightCycle;

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
            DayOrNight = true;
        else if (DayNightCycle.intensity <= 0.1f)
            DayOrNight = false;

        DayNightCycle.intensity = IntensityValue;
    }
}
