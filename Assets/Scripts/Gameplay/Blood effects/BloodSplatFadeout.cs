using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatFadeout : MonoBehaviour
{
    [SerializeField] [Range (5f, 12f)]
    private float fadeOutTimeMin;

    [SerializeField] [Range(18f, 25f)]
    private float fadeOutTimeMax;

    private float m_fadeOutTime;

    private void Start()
    {
        m_fadeOutTime = Random.Range( fadeOutTimeMin, fadeOutTimeMax );
        StartCoroutine( this.BloodSplatFadeOut(m_fadeOutTime) );
    }

    private IEnumerator BloodSplatFadeOut(float _fadeOutTime)
    {
        float timeElapsed   = 0f;
        Color originalColor = GetComponent<Renderer>().material.color;
        float originalAlpha = originalColor.a;
        float targetAlpha   = 0f;

        while (timeElapsed < _fadeOutTime)
        {
            timeElapsed += Time.deltaTime;

            float ratio = timeElapsed / _fadeOutTime;
            float newAlpha = Mathf.Lerp(originalAlpha, targetAlpha, ratio);
            Color newColor = GetComponent<Renderer>().material.color;
            newColor.a = newAlpha;
            GetComponent<Renderer>().material.color = newColor;

            if (ratio >= 0.95)
            {
                newColor.a = targetAlpha;
                GetComponent<Renderer>().material.color = newColor;
                break;
            }

            yield return null;
        }

        yield return null;
        Destroy( this );
    }
}
