using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Script to have a top-down camera that follows an object
 *  Attach to the main camera
 */
public class TopDownFollowCam : MonoBehaviour
{
    [Header("References")]

    [SerializeField] [Tooltip ("The object that the camera will follow")]
    private Transform playerTransform;

    [Header("Customisations")]

    [SerializeField] [Range (0.1f, 89.9f)] 
    [Tooltip ("0 -> Side view, 90 -> Topdown view")]
    private float cameraAngle = 40f;

    [SerializeField] [Range (10f, 250f)] 
    private float distanceFromPlayer = 100f;

    [SerializeField] [Range(1f, 50f)]
    private float fieldOfView = 12f;

    private Camera m_camera;

    private void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        // TODO : Once the settings are finalised,
        //        should probably stop setting FOV every frame

        m_camera.fieldOfView = fieldOfView;
        Vector3 dir = Quaternion.Euler(cameraAngle - 90f, 0f, 0f) * Vector3.up;

        transform.position = playerTransform.position + (dir * distanceFromPlayer);
        transform.LookAt( playerTransform );
    }
}