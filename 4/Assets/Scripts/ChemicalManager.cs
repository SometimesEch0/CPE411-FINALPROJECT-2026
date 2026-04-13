using UnityEngine;

public class ChemicalManager : MonoBehaviour
{
    [Header("Distance Settings")]
    public float combinationDistance = 0.15f;

    [Header("Compound Models (From Hierarchy)")]
    public GameObject xef2Model;
    public GameObject krf2Model;
    public GameObject rnf2Model;
    public GameObject xeo3Model;

    void Update()
    {
        // 1. Find the active clones in the scene
        GameObject xenon = GameObject.Find("Xenon(Clone)");
        GameObject fluorine = GameObject.Find("Fluorine(Clone)");
        GameObject krypton = GameObject.Find("Krypton(Clone)");
        GameObject radon = GameObject.Find("Radon(Clone)");
        GameObject oxygen = GameObject.Find("Oxygen(Clone)");

        // 2. Logic for XeF2 (Xenon + Fluorine)
        HandleReaction(xenon, fluorine, xef2Model);

        // 3. Logic for KrF2 (Krypton + Fluorine)
        HandleReaction(krypton, fluorine, krf2Model);
        
        // 4. Logic for RnF2 (Radon + Fluorine)
        HandleReaction(radon, fluorine, rnf2Model);

        // 5. Logic for XeO3 (Xenon + Oxygen)
        HandleReaction(xenon, oxygen, xeo3Model);
    }

    void HandleReaction(GameObject elementA, GameObject elementB, GameObject compoundModel)
    {
        // If both cards are detected by AR Foundation
        if (elementA != null && elementB != null && elementA.activeInHierarchy && elementB.activeInHierarchy)
        {
            float dist = Vector3.Distance(elementA.transform.position, elementB.transform.position);

            if (dist < combinationDistance)
            {
                // Show the molecule
                compoundModel.SetActive(true);
                
                // Position it perfectly between the two cards
                compoundModel.transform.position = Vector3.Lerp(elementA.transform.position, elementB.transform.position, 0.5f);
                
                // Keep it facing the camera (optional but looks better)
                compoundModel.transform.LookAt(Camera.main.transform);

                // Hide the individual atoms so they "merged"
                // We access the child models to hide them without destroying the tracker
                SetChildrenActive(elementA, false);
                SetChildrenActive(elementB, false);
            }
            else
            {
                // Cards are too far apart
                compoundModel.SetActive(false);
                SetChildrenActive(elementA, true);
                SetChildrenActive(elementB, true);
            }
        }
        else
        {
            // One or both cards are missing/scanned away
            if (compoundModel != null) compoundModel.SetActive(false);
        }
    }

    // Helper function to hide the atoms inside the spawned prefab
    void SetChildrenActive(GameObject parent, bool state)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}