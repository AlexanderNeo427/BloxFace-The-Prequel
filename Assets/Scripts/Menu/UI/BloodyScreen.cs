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
    }

    private IEnumerator FadeOut()
    {
        Image bloodscreenImage = GetComponent<Image>();

        Color curretCOL = GetComponent<Image>().color;
        curretCOL.a = 1.0f;
        GetComponent<Image>().color = curretCOL;

        float targetAlpha = 0f;
        float timeElapsed = 0f;
        while (timeElapsed <= fadeOutTime)
        {
            timeElapsed += Time.deltaTime;

            Color currColor = bloodscreenImage.color;
            float ratio = timeElapsed / fadeOutTime;
            if (ratio > 0.85f)
            {
                currColor.a = 1.0f;
                break;
            }
            currColor.a = Mathf.Lerp(1.0f, targetAlpha, ratio);
            bloodscreenImage.color = currColor;

            yield return null;
        }

        yield return null;
    }
}
