using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScoreEvent : MonoBehaviour
{
    public GameObject ScoreSystem;
    public GameObject _WaveSystem;

    private void OnEnable()
    {
        WaveSystem.BonusScore += AddBonusScore;
    }

    private void OnDisable()
    {
        WaveSystem.BonusScore -= AddBonusScore;
    }
    void AddBonusScore()
    {
        if (_WaveSystem.GetComponent<WaveSystem>().waveCount != 0)
        {
            ScoreSystem.GetComponent<ScoreSystem>().ScoreValue += 50000;
        }
    }

}
