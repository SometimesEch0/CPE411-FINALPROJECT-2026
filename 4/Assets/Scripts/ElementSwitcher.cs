using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ElementSwitcher : MonoBehaviour
{
    private ARTrackedImage trackedImage;

    void Awake()
    {
        trackedImage = GetComponent<ARTrackedImage>();
    }

    void Update()
    {
        if (trackedImage == null || trackedImage.referenceImage == null) return;

        string cardName = trackedImage.referenceImage.name;
        bool isTracking = trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(child.name == cardName && isTracking);
        }
    }

    void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}