using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodyScreen : MonoBehaviour
{
    [SerializeField] [Range(0.8f, 8f)]
    private float fadeOutTime;

    private void OnEnable()
    {
        PlayerInfo.OnPlayerDamaged += BloodScreenEffect;
    }

    private void OnDisalbe()
    {
        PlayerInfo.OnPlayerDamaged -= BloodScreenEffect;
    }

    private void BloodScreenEffect(float damageTaken)
    {
        StartCoroutine( this.FadeOut(damageTaken) );
    }

    private IEnumerator FadeOut(float damageTaken)
    {
        Image bloodscreenImage = GetComponent<Image>();

        float maxDMG = 100f;
        Color curretCOL = GetComponent<Image>().color;
        curretCOL.a = Mathf.Min(damageTaken / maxDMG, 0.8f);
        float originalAlpha = curretCOL.a;

        GetComponent<Image>().color = curretCOL;

        float targetAlpha = 0f;
        float timeElapsed = 0f;
        while (timeElapsed <= fadeOutTime)
        {
            timeElapsed += Time.deltaTime;

            Color currColor = bloodscreenImage.color;
            float ratio = timeElapsed / fadeOutTime;
            if (ratio > 0.95f)
            {
                currColor.a = targetAlpha;
                bloodscreenImage.color = currColor;
                break;
            }
            currColor.a = Mathf.Lerp(0.6f, targetAlpha, ratio);
            bloodscreenImage.color = currColor;

            yield return null;
        }

        yield return null;
    }
}
