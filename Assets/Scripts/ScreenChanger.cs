using UnityEngine;
using UnityEngine.UI;

public class ScreenChanger : MonoBehaviour
{
    // Textures to switch between
    public Texture blankTexture;
    public Texture normalTexture;

    // Track the state of the screen
    public bool isBlankScreen = false;

    // Reference to the RawImage component
    private RawImage rawImage;

    private void Awake()
    {
        // Get the RawImage component from the GameObject this script is attached to
        rawImage = GetComponent<RawImage>();
        if (rawImage == null)
        {
            Debug.LogError("RawImage component not found on the GameObject.");
        }
    }

    // Call this function to toggle the texture between blank and normal
    public void ChangeTexture()
    {
        if (rawImage != null)
        {
            if (isBlankScreen)
            {
                // If the screen is blank, change to the normal texture
                rawImage.texture = normalTexture;
                isBlankScreen = false;
            }
            else
            {
                // If the screen is not blank, change to the blank texture
                rawImage.texture = blankTexture;
                isBlankScreen = true;
            }
        }
    }
}