using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject helpPanel;

    public void ShowHelp()
    {
        helpPanel.SetActive(true);
    }

    public void HideHelp()
    {
        helpPanel.SetActive(false);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
