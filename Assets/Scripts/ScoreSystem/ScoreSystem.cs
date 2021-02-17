using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MultiplierText;

    private int ScoreValue = 0;
    private int ScoreMultiplier = 1;

    private float timer = 3.0f;

    // Update is called once per frame
    void Update()
    {
        // Timer for multiplier
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = 3.0f;

            if (ScoreMultiplier != 1)
            {
                MultiplierText.fontSize = 80f;
                DecreaseMultiplier();
            }
        }

        // Test for score
        if (Input.GetKeyDown("space"))
            AddScore();

        if (MultiplierText.fontSize > 36f)
            MultiplierText.fontSize -= Time.deltaTime * 15;

        ScoreText.SetText(ScoreValue.ToString("00000000000000"));
        MultiplierText.SetText(ScoreMultiplier.ToString() + "x");
    }

    void AddScore()
    {
        MultiplierText.fontSize = 80f;

        AudioManager.instance.Play("Multiplier");
        ScoreValue += 20 * ScoreMultiplier;

        // Increase multi
        if (ScoreMultiplier <= 128)
            ScoreMultiplier *= 2;

        // Reset timer
        timer = 3.0f;
    }

    void DecreaseMultiplier()
    {
        AudioManager.instance.Play("ReverseMultiplier");

        // Half the multiplier
        if (ScoreMultiplier > 1)
            ScoreMultiplier /= 2;
    }
}
