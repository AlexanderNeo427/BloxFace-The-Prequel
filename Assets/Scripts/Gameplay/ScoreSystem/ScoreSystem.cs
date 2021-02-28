using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MultiplierText;
    public TextMeshProUGUI FinalScoreText;

    public int ScoreValue = 0;
    private int ScoreMultiplier = 1;

    private float timer = 3.0f;

    private void OnEnable()
    {
        RegularZombie.OnDeath += AddScoreRegular;
        SuicideBomberZombie.OnDeath += AddScoreSuicide;
        BossZombie.OnDeath += AddScoreBoss;
        RunnerZombie.OnDeath += AddScoreRunner;
        PlayerInfo.OnPlayerDamaged += OnHitDecreaseMultiplier;
    }

    private void OnDisable()
    {
        RegularZombie.OnDeath -= AddScoreRegular;
        SuicideBomberZombie.OnDeath -= AddScoreSuicide;
        BossZombie.OnDeath -= AddScoreBoss;
        RunnerZombie.OnDeath -= AddScoreRunner;
        PlayerInfo.OnPlayerDamaged -= OnHitDecreaseMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer for multiplier
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = 6.0f;

            if (ScoreMultiplier != 1)
            {
                MultiplierText.fontSize = 60f;
                DecreaseMultiplier();
            }
        }

        // "Bobbing" effect on multiplier text
        if (MultiplierText.fontSize > 36f)
            MultiplierText.fontSize -= Time.deltaTime * 8;

        ScoreText.SetText(ScoreValue.ToString("00000000000000"));
        MultiplierText.SetText(ScoreMultiplier.ToString() + "x");

        FinalScoreText.SetText("Final Score: " + ScoreValue.ToString());
    }

    // Add score and Add to multiplier
    void AddScoreRegular(Vector3 dummyVariable)
    {
        MultiplierText.fontSize = 60f;

        AudioManager.instance.Play("Multiplier");
        ScoreValue += 50 * ScoreMultiplier;

        // Increase multi
        if (ScoreMultiplier <= 128)
            ScoreMultiplier *= 2;

        // Reset timer
        timer = 6.0f;
    }

    void AddScoreRunner(Vector3 dummyVariable)
    {
        MultiplierText.fontSize = 60f;

        AudioManager.instance.Play("Multiplier");
        ScoreValue += 100 * ScoreMultiplier;

        // Increase multi
        if (ScoreMultiplier <= 128)
            ScoreMultiplier *= 2;

        // Reset timer
        timer = 6.0f;
    }

    void AddScoreSuicide(Vector3 dummyVariable)
    {
        MultiplierText.fontSize = 60f;

        AudioManager.instance.Play("Multiplier");
        ScoreValue += 150 * ScoreMultiplier;

        // Increase multi
        if (ScoreMultiplier <= 128)
            ScoreMultiplier *= 2;

        // Reset timer
        timer = 6.0f;
    }

    void AddScoreBoss(Vector3 dummyVariable)
    {
        MultiplierText.fontSize = 60f;

        AudioManager.instance.Play("Multiplier");
        ScoreValue += 400 * ScoreMultiplier;

        // Increase multi
        if (ScoreMultiplier <= 128)
            ScoreMultiplier *= 2;

        // Reset timer
        timer = 6.0f;
    }

    // Decrease Multiplier if player is hit or take too long to continue the killing streak
    void DecreaseMultiplier()
    {
        AudioManager.instance.Play("ReverseMultiplier");

        // Half the multiplier
        if (ScoreMultiplier > 1)
            ScoreMultiplier /= 2;
    }
    void OnHitDecreaseMultiplier(float dummyVariable)
    {
        AudioManager.instance.Play("Ugh");
        // Rest the multiplier when hit
        if (ScoreMultiplier > 1)
        {
            AudioManager.instance.Play("ReverseMultiplier");
            ScoreMultiplier = 1;
        }
    }
}
