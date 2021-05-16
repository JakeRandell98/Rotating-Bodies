using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    // Public variables
    public float moonRotationRate; // Angular rate of Moon in degrees/s (about y-axis)
    public float moonOrbitRate; // Orbital rate of Marth in degrees/s (about the Earth, positive = clockwise)
    public float moonOrbitRadius; // Orbital radius of the Moon
    public float moonOrbitInclination; // Orbital inclination of the Moon (degrees)

    // Private variables
    private Vector3 moonPosition; // Moon position
    private Vector3 moonRotation = Vector3.zero; // Moon rotation (Euler angles)
    private float moonOrbitAngle = 0; // Angular position of Moon around the Earth in degrees
    private Vector3 moonOrbitVector; // Moon's position w.r.t the Earth for an inclination angle of 0
    private Transform earthTransform; // The Earth's Transform component

    // Start is called before the first frame update
    void Start()
    {
        earthTransform = GameObject.Find("Earth").transform; // Obtain Earth Transform component
        moonPosition = earthTransform.position + new Vector3(moonOrbitRadius, 0, 0); // Set Moon's initial position moonOrbitRadius away from the Earth
        transform.position = moonPosition; // Set Moon initial position
        transform.eulerAngles = moonRotation; // Set Moon initial rotation
    }

    // Update is called once per frame
    void Update()
    {
        moonOrbitAngle += moonOrbitRate * Time.deltaTime; // Calculate next orbital angular position value
        if (moonOrbitAngle > 180)
        {
            moonOrbitAngle -= 360; // Keep moonOrbitAngle within -180 and 180 degrees
        }
        moonOrbitVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * moonOrbitAngle), 0, -Mathf.Sin(Mathf.Deg2Rad * moonOrbitAngle)); // Use basic trig to determine location of Moon w.r.t Earth if orbit has 0 inclination
        moonOrbitVector = (Quaternion.Euler(moonOrbitInclination, 0, 0) * moonOrbitVector).normalized; // Rotate moonOrbitVector about x-axis by the orbital inclination angle
        moonPosition = earthTransform.position + moonOrbitRadius * moonOrbitVector; // Determine Moon's position using the calculated orbital vector, and Earth's current position
        moonRotation.y += moonRotationRate * Time.deltaTime; // Calculate next rotation value
        transform.position = moonPosition; // Update Moon position
        transform.eulerAngles = moonRotation; // Update Moon rotation
    }
}
