using UnityEngine;
using UnityEngine.UIElements;

public class UIHandlerScript : MonoBehaviour
{
    // Reference to the root visual element of the UI document
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        // Get the root visual element of the UI document attached to this GameObject
        root = GetComponent<UIDocument>().rootVisualElement;

        // Find the button with the name "StartButton" and attach a click event handler
        Button startButton = root.Q<Button>("StartButton");
        startButton.clickable.clicked += OnStartButtonClick;
    }

    // Method to handle the button click
    void OnStartButtonClick()
    {
        // Close the UI panel
        gameObject.SetActive(false);
    }
}
