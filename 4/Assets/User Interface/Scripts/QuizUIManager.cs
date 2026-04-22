using UnityEngine;

public class QuizUIManager : MonoBehaviour
{
    public GameObject startQuizPanel;
    public GameObject quizPanel;
    public GameObject resultPanel;

    void Start()
    {
        // Default state
        startQuizPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    // 🔹 SHOW FUNCTIONS
    public void ShowStartPanel()
    {
        startQuizPanel.SetActive(true);
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void ShowQuizPanel()
    {
        startQuizPanel.SetActive(false);
        quizPanel.SetActive(true);
        resultPanel.SetActive(false);
    }

    public void ShowResultPanel()
    {
        startQuizPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultPanel.SetActive(true);
    }

    // 🔹 HIDE FUNCTIONS (manual close)
    public void HideStartPanel()
    {
        startQuizPanel.SetActive(false);
    }

    public void HideQuizPanel()
    {
        quizPanel.SetActive(false);
    }

    public void HideResultPanel()
    {
        resultPanel.SetActive(false);
    }

    // 🔹 OPTIONAL: Hide everything
    public void HideAllPanels()
    {
        startQuizPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
    }
}
