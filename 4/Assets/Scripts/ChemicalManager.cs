using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ChemicalManager : MonoBehaviour
{
    [Header("Detection Settings")]
    public float combinationDistance = 0.2f; 

    [Header("Hierarchy Objects")]
    public GameObject Xef2;
    public GameObject Krf2;
    public GameObject Rnf2;
    public GameObject XeO3;

    void Update()
    {
        GameObject xenon = FindAtomByCardName("Xenon");
        GameObject fluorine = FindAtomByCardName("Fluorine");
        GameObject krypton = FindAtomByCardName("Krypton");
        GameObject radon = FindAtomByCardName("Radon");
        GameObject oxygen = FindAtomByCardName("Oxygen");

        HandleReaction(xenon, fluorine, Xef2);
        HandleReaction(krypton, fluorine, Krf2);
        HandleReaction(radon, fluorine, Rnf2);
        HandleReaction(xenon, oxygen, XeO3);
    }

    GameObject FindAtomByCardName(string name)
    {
        ARTrackedImage[] images = GameObject.FindObjectsOfType<ARTrackedImage>();
        foreach (ARTrackedImage img in images)
        {
            if (img.referenceImage.name == name && img.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                return img.gameObject;
        }
        return null;
    }

    void HandleReaction(GameObject e1, GameObject e2, GameObject compound)
    {
        if (compound == null) return;

        if (e1 != null && e2 != null)
        {
            float dist = Vector3.Distance(e1.transform.position, e2.transform.position);

            if (dist < combinationDistance)
            {
                if (!compound.activeSelf) compound.SetActive(true);
                compound.transform.position = Vector3.Lerp(e1.transform.position, e2.transform.position, 0.5f);
                SetAtomsVisible(e1, false);
                SetAtomsVisible(e2, false);
            }
            else
            {
                if (compound.activeSelf) compound.SetActive(false);
                SetAtomsVisible(e1, true);
                SetAtomsVisible(e2, true);
            }
        }
        else if (compound.activeSelf)
        {
            compound.SetActive(false);
        }
    }

    void SetAtomsVisible(GameObject parent, bool state)
    {
        if (parent == null) return;
        ElementSwitcher switcher = parent.GetComponent<ElementSwitcher>();
        if (switcher != null) switcher.enabled = state;

        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}