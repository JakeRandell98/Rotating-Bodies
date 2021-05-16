using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScript : MonoBehaviour
{
    // Public variables
    public int numberOfStars; // User-defined number of stars to render
    public int starGenerationRate; // How many stars to instantiate/delete per frame
    public GameObject Star; // Star prefab
    public float minDistance; // Minimum distance from the Sun that stars can spawn
    public float maxDistance; // Maximum distance from the Sun that stars can spawn

    // Private variables
    private Transform sunTransform; // The Sun's Transform component
    private int prevNumberOfStars; // Stored previous value of the number of stars
    private bool generatingStars; // Boolean flag used to indicate whether stars are being generated
    private int numberOfStarsToCreate; // Copy of numberOfStars value to use during star instantitation/deletion
    private int starsCreated; // Counter for number of stars created (during star instantiation)
    private int starsDeleted; // Counter for number of stars deleted (during star deletion)
    private int starCount; // Number of stars to delete (set immediately before deletion process)


    // Start is called before the first frame update
    void Start()
    {
        sunTransform = GameObject.Find("Sun").transform; // Obtain Sun Transform component
        transform.position = sunTransform.position; // Set centre of StarCollection GameObject to equal Sun's position
        prevNumberOfStars = 0; // Set to 0 to trigger stars to render (unless numberOfStars = 0)
        generatingStars = false; // Stars not being generated initially, so set to false
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sunTransform.position; // Update StarCollection's position to match Sun's

        // Enter when stars are not already being generated, and the number of stars value changes
        if (!generatingStars && numberOfStars != prevNumberOfStars)
        {
            generatingStars = true; // Stars will now be generated
            numberOfStarsToCreate = numberOfStars; // Store the number of stars that need to be created
            starsCreated = 0; // Reset value to 0
            starsDeleted = 0; // Reset value to 0
            starCount = transform.childCount; // Store the number of stars taht need to be deleted
            StartCoroutine(GenerateStars()); // Begin star generation coroutine
        }
    }

    // Coroutine used to delete all stars, then generate a number of stars equal to numberOfStarsToCreate
    IEnumerator GenerateStars()
    {

        // Star deletion while loop
        while (starsDeleted < starCount)
        {
            GameObject.Destroy(transform.GetChild(starsDeleted % starGenerationRate).gameObject); // Delete next star within StarCollection
            starsDeleted++; // Increment number of stars deleted
            if ((starsDeleted % starGenerationRate) == 0) // If starsDeleted is a multiple of starGenerationRate, return from the coroutine
                yield return null; // Return from coroutine once every starGenerationRate number of loops
        }

        // Star generation while loop
        while (starsCreated < numberOfStarsToCreate)
        {
            Vector3 starPosition = RandomDistance(minDistance, maxDistance); // Locate a random position to place the new star
            Instantiate(Star, transform.position + starPosition, Quaternion.identity, transform); // Instantiate a new star at a location starPosition away from the Sun
            starsCreated++; // Increment number of stars created
            if ((starsCreated % starGenerationRate) == 0) // If starsCreated is a multiple of starGenerationRate, return from the coroutine
                yield return null; // Return from coroutine once every starGenerationRate number of loops
        }
        prevNumberOfStars = numberOfStarsToCreate; // Set previous number of stars to equal the number just created
        generatingStars = false; // No longer generating stars
    }

    // Function returns a random Vector3 with a magnitude between min and max
    private Vector3 RandomDistance(float min, float max)
    {
        return Random.Range(min, max) * Random.onUnitSphere;
    }
}
