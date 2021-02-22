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
    private float TimeCount;

    void Update()
    {
        // Getters
        WaveCount = WaveSystem.GetComponent<WaveSystem>().waveCount;
        EnemiesLeftCount = WaveSystem.GetComponent<WaveSystem>().enemyAlive;
        TimeCount = WaveSystem.GetComponent<WaveSystem>().TimeCount;

        // Wave text
        WaveCountText.SetText("Wave: " + WaveCount);

        // Remaining Enemies left
        EnemiesLeftText.SetText("Enemies Left: " + EnemiesLeftCount.ToString());

        // Time till spawn next wave Display
        TimeTillSpawnText.SetText("Time Left: " + (int)TimeCount);
    }
}
