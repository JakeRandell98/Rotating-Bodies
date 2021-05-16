using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    // Public variables
    public float sunRotationRate; // Angular rate of the Sun in degrees/s (about y-axis)
    public float sunTravelRate;  // Translational rate of the Sun in m/s (in x-direction)

    // Private variables
    private Vector3 sunPosition = Vector3.zero; // Sun position
    private Vector3 sunRotation = Vector3.zero; // Sun rotation (Euler angles)

    // Start is called before the first frame update
    void Start()
    {
        transform.position = sunPosition; // Set Sun initial position
        transform.eulerAngles = sunRotation; // Set Sun initial rotation
    }

    // Update is called once per frame
    void Update()
    {
        sunPosition.x += sunTravelRate*Time.deltaTime; // Calculate next position value
        sunRotation.y += sunRotationRate*Time.deltaTime; // Calculate next rotation value
        transform.position = sunPosition; // Update Sun position
        transform.eulerAngles = sunRotation; // Update Sun rotation
    }
}
