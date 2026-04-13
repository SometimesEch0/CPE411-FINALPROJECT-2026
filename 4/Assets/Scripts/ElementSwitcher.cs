using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ElementSwitcher : MonoBehaviour
{
    private ARTrackedImage trackedImage;

   void Awake()
	{
    trackedImage = GetComponent<ARTrackedImage>();
    // This kills the "Stuck" model on startup
    foreach (Transform child in transform) 
   		 { 
        child.gameObject.SetActive(false); 
    		}
	}

    void OnEnable()
    {
        // This runs the moment the card is detected
        UpdateElementVisibility();
    }

    public void UpdateElementVisibility()
    {
        if (trackedImage == null || trackedImage.referenceImage == null) return;

        // Get the name of the image from your Reference Library (e.g., "Xenon")
        string imageName = trackedImage.referenceImage.name;

        // Loop through all 3D children (the atoms your teammate made)
        foreach (Transform child in transform)
        {
            // Only turn ON the child that matches the card name exactly
            child.gameObject.SetActive(child.name == imageName);
        }
    }
}