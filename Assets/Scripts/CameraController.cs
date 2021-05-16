using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Public variables

    // Private variables
    private Vector3 originalPosition; // Original position of camera (set in editor)
    private Transform sunTransform; // The Sun's Transform component

    // Start is called before the first frame update
    void Start()
    {
        sunTransform = GameObject.Find("Sun").transform; // Obtain Sun Transform component
        originalPosition = transform.position; // Set original position
        transform.position = originalPosition + sunTransform.position; // Set new position w.r.t the Sun
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originalPosition + sunTransform.position; // Make camera follow the Sun
    }
}
