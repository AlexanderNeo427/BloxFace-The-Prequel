using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Sin Movement and rotation
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time) / 6, 0.0f);
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
