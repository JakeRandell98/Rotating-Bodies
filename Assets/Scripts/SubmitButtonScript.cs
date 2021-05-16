using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButtonScript : MonoBehaviour
{
    // Public variables
    public GameObject starsInputField; // Input Field GameObject (for entering number of stars)
    public StarsScript StarsScript; // Star controller script
    public Button submitButton; // Submit button object

    // Private variables
    private InputField numberOfStarsInputField; // Actual Input Field object (for entering number of stars)

    // Start is called before the first frame update
    void Start()
    {
        numberOfStarsInputField = starsInputField.GetComponent<InputField>(); // Retrieve Input Field component
        numberOfStarsInputField.text = StarsScript.numberOfStars.ToString(); // Set Input Field text to equal current numberOfStars value
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke(); // Allow button to be pressed via the "return" key
        }
    }

    // Set number of stars to value in Input Field 
    public void setNumberOfStars()
    {
        if(numberOfStarsInputField.text == "") // If Input Field is empty, set number of stars to 0
        {
            StarsScript.numberOfStars = 0;
        }
        else
        {
            StarsScript.numberOfStars = int.Parse(numberOfStarsInputField.text); // Convert Input Field text to int
        }
    }
}
