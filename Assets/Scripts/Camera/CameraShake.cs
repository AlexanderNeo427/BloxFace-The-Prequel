using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Camera shake script with adjustable duration and shake magnitudeaaa
 *  Attach to the camera
 */
public class CameraShake : MonoBehaviour
{
    [Header("Customisations (settings adjustable during runtime)")]

    [SerializeField] [Range(0.1f, 1.5f)] private float duration;
    [SerializeField] [Range(0.1f, 3f)]   private float magnitude;

    private void OnEnable()
    {
        // Subscribe to events here
        ExplosionManager.OnExplosion += ShakeCamera;
    }

    private void OnDisable()
    {
        // Unsubscribe from events here
        ExplosionManager.OnExplosion -= ShakeCamera;
    }
    
    /*    
    private void Update()
    {
        // For testing
        if (Input.GetKeyDown(KeyCode.Z))
            StartCoroutine( this.Shake() );
    }*/

    private void ShakeCamera()
    {
        StartCoroutine( this.ShakeCoroutine() );
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float randX = Random.Range(-1f, 1f) * magnitude;
            float randY = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x + randX, originalPos.y + randY, originalPos.z);

            yield return null;
        }

        transform.localPosition = originalPos;
        yield return null;
    }
}
