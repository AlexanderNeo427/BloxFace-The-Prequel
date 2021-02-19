using UnityEngine;
using TMPro;

public class WaveDisplay : MonoBehaviour
{
    // Various Texts
    public TextMeshProUGUI WaveCountText;
    public TextMeshProUGUI EnemiesLeftText;
    public TextMeshProUGUI TimeTillSpawnText;

    public GameObject WaveSystem;

    private int WaveCount = 0;
    private int EnemiesLeftCount = 0;
    private float TimeCount = 30f;

    private float WaitTimer = 5f;
    private bool TriggerThis = true;

    void Update()
    {
        // Timer till next wave
        if (TimeCount > 0)
            TimeCount -= Time.deltaTime;
        // Spawn next wave
        else
        {
            WaitTimer = 0f;
            TimeCount = 30f;
            TriggerThis = true;
            WaveSystem.SetActive(true);
        }

        if (WaitTimer < 2f)
            WaitTimer += Time.deltaTime;
        else
        {
            if (TriggerThis == true)
            {
                if (EnemiesLeftCount == 0)
                {
                    if (TimeCount > 5f)
                    {
                        TimeCount = 5f;
                    }
                    TriggerThis = false;
                }
            }
        }
        // What wave it is
        WaveCount = WaveSystem.GetComponent<WaveSystem>().waveCount;
        WaveCountText.SetText("Wave: " + WaveCount);

        // How many enemies left
        EnemiesLeftCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        EnemiesLeftText.SetText("Enemies Left: " + EnemiesLeftCount.ToString());

        // Time Display
        TimeTillSpawnText.SetText("Time Left: " + (int)TimeCount);
    }

    void ReduceTimer()
    {
    }
}
