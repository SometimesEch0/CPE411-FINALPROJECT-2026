using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizQuestionManager : MonoBehaviour
{
    public QuizUIManager uiManager;

    public GameObject[] questionPanels;
    public TMP_Text resultText;
    public TMP_Text[] nextButtonTexts;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool answered = false;

    void Start()
    {
        for (int i = 0; i < questionPanels.Length; i++)
        {
            questionPanels[i].SetActive(false);
        }
    }

    public void StartQuizQuestions()
    {
        currentQuestionIndex = 0;
        score = 0;
        answered = false;

        uiManager.ShowQuizPanel();
        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int index)
    {
        for (int i = 0; i < questionPanels.Length; i++)
        {
            questionPanels[i].SetActive(false);
        }

        questionPanels[index].SetActive(true);
        answered = false;

        for (int i = 0; i < nextButtonTexts.Length; i++)
        {
            if (i == questionPanels.Length - 1)
                nextButtonTexts[i].text = "Submit";
            else
                nextButtonTexts[i].text = "Next";
        }

        Button[] buttons = questionPanels[index].GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            if (btn.CompareTag("AnswerButton"))
            {
                btn.interactable = true;

                if (btn.image != null)
                {
                    btn.image.color = Color.white;
                }
            }

            if (btn.CompareTag("NextButton"))
            {
                btn.gameObject.SetActive(false);
            }
        }
    }

    public void ChooseCorrect(Button clickedButton)
    {
        if (answered) return;

        answered = true;
        score++;

        if (clickedButton.image != null)
        {
            clickedButton.image.color = Color.green;
        }

        LockAnswersAndShowNext();
    }

    public void ChooseWrong(Button clickedButton)
    {
        if (answered) return;

        answered = true;

        if (clickedButton.image != null)
        {
            clickedButton.image.color = Color.red;
        }

        LockAnswersAndShowNext();
    }

    void LockAnswersAndShowNext()
    {
        Button[] buttons = questionPanels[currentQuestionIndex].GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            if (btn.CompareTag("AnswerButton"))
            {
                btn.interactable = false;
            }

            if (btn.CompareTag("NextButton"))
            {
                btn.gameObject.SetActive(true);
            }
        }
    }

    public void NextQuestion()
    {
        if (!answered) return;

        currentQuestionIndex++;

        if (currentQuestionIndex < questionPanels.Length)
        {
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResult();
        }
    }

    void ShowResult()
    {
        for (int i = 0; i < questionPanels.Length; i++)
        {
            questionPanels[i].SetActive(false);
        }

        uiManager.ShowResultPanel();
        resultText.text = "Your Score: " + score + " / " + questionPanels.Length;
    }

    public void RestartQuiz()
    {
    currentQuestionIndex = 0;
    score = 0;
    answered = false;

    for (int i = 0; i < questionPanels.Length; i++)
    {
        Button[] buttons = questionPanels[i].GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            if (btn.CompareTag("AnswerButton"))
            {
                btn.interactable = true;

                if (btn.image != null)
                {
                    btn.image.color = Color.white;
                }
            }

            if (btn.CompareTag("NextButton"))
            {
                btn.gameObject.SetActive(false);
            }
        }

        questionPanels[i].SetActive(false);
    }

    uiManager.ShowStartPanel();
    }
}