using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    // Public variables
    public float earthRotationRate; // Angular rate of Earth in degrees/s (about y-axis)
    public float earthOrbitRate; // Orbital rate of Earth in degrees/s (about the Sun, positive = clockwise)
    public float earthOrbitRadius; // Orbital radius of the Earth in m
    public float earthTilt; // Angle by which the Earth tilts (spin axis) in degrees

    // Private variables
    private Vector3 earthPosition; // Earth position
    private Vector3 earthRotation; // Earth rotation (Euler angles)
    private float earthOrbitAngle = 0; // Angular position of Earth around the Sun in degrees
    private Transform sunTransform; // The Sun's Transform component

    // Start is called before the first frame update
    void Start()
    {
        sunTransform = GameObject.Find("Sun").transform; // Obtain Sun Transform component
        earthPosition = sunTransform.position + new Vector3(earthOrbitRadius, 0, 0); // Set Earth's initial position earthOrbitRadius away from the Sun
        earthRotation = new Vector3(0, 0, earthTilt); // Tilt the Earth by earthTilt degrees about the z-axis
        transform.position = earthPosition; // Set Earth initial position
        transform.eulerAngles = earthRotation; // Set Earth initial rotation
    }

    // Update is called once per frame
    void Update()
    {
        earthOrbitAngle += earthOrbitRate * Time.deltaTime; // Calculate next orbital angular position value
        if(earthOrbitAngle > 180)
        {
            earthOrbitAngle -= 360; // Keep earthOrbitAngle within -180 and 180 degrees
        }
        earthPosition = sunTransform.position + earthOrbitRadius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * earthOrbitAngle), 0, -Mathf.Sin(Mathf.Deg2Rad * earthOrbitAngle)); // Use basic trig to determine location of Earth around the circular orbit
        earthRotation.y += earthRotationRate * Time.deltaTime; // Calculate next rotation value
        transform.position = earthPosition; // Update Earth position
        transform.eulerAngles = earthRotation; // Update Earth rotation
    }
}
