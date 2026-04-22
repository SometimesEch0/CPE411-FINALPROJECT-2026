using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject startQuizPanel;
    public GameObject resultPanel;

    [Header("Question Panels")]
    public GameObject[] questionPanels;

    [Header("Quiz UI")]
    public TMP_Text feedbackText;
    public TMP_Text scoreText;

    [Header("Result UI")]
    public TMP_Text finalScoreText;
    public Button nextLevelButton;

    private int currentLevel = 1;
    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool answered = false;

    void Start()
    {
        ShowMainMenu();
        HideAllQuestionPanels();
    }

    public void OpenQuizStartPanel()
    {
        mainMenuPanel.SetActive(false);
        startQuizPanel.SetActive(true);
        resultPanel.SetActive(false);
        HideAllQuestionPanels();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        startQuizPanel.SetActive(false);
        resultPanel.SetActive(false);
        HideAllQuestionPanels();
    }

    public void StartQuiz()
    {
        startQuizPanel.SetActive(false);
        resultPanel.SetActive(false);

        currentQuestionIndex = 0;
        score = 0;

        ShowQuestion(currentQuestionIndex);
        UpdateScoreText();

        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    void ShowQuestion(int index)
    {
        HideAllQuestionPanels();

        if (index < questionPanels.Length)
        {
            questionPanels[index].SetActive(true);
            answered = false;
        }
        else
        {
            ShowResult();
        }
    }

    void HideAllQuestionPanels()
    {
        for (int i = 0; i < questionPanels.Length; i++)
        {
            questionPanels[i].SetActive(false);
        }
    }

    public void CorrectAnswer()
    {
        if (answered) return;

        answered = true;
        score++;
        UpdateScoreText();

        if (feedbackText != null)
        {
            feedbackText.text = "Correct!";
        }

        Invoke("NextQuestion", 1f);
    }

    public void WrongAnswer()
    {
        if (answered) return;

        answered = true;

        if (feedbackText != null)
        {
            feedbackText.text = "Wrong!";
        }

        Invoke("NextQuestion", 1f);
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (feedbackText != null)
        {
            feedbackText.text = "";
        }

        ShowQuestion(currentQuestionIndex);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void ShowResult()
    {
        HideAllQuestionPanels();
        resultPanel.SetActive(true);

        finalScoreText.text = "Level " + currentLevel + " Complete!\nScore: " + score + " / " + questionPanels.Length;

        if (nextLevelButton != null)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    public void ExitQuiz()
    {
        currentLevel = 1;
        ShowMainMenu();
    }
}