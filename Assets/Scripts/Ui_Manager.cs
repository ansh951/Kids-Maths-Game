using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
    [Header("Referces")]
    public InputField input;
    public Text questionText;
    public Text correctCountText;
    public Text incorrectCountText;
    public Text roundText;
    public Text averageTimeText;
    public GameObject gameFinishPanel;
    public Animator anim;
    public List<int> Options = new List<int>();
    public Text[] optionArray = new Text[4];

    private int numForMultiply;
    int correctAnswer;
    private int correctCount = 0;
    private int incorrectCount = 0;
    private int currentQuestion = 1;
    private int totalQuestions = 10;
    private int round = 1;
    private float questionStartTime;
    private float questionEndTime;
    private List<float> questionTimes = new List<float>();
    

    void Start()
    {
        gameFinishPanel.SetActive(false);
        GenerateQuestionAndOptions();
        RoundSystem();
    }
    private void Update()
    {
        
    }
    void GenerateQuestionAndOptions()
    {
        if (currentQuestion <= totalQuestions)
        {
            anim.SetBool("BannerIncoming", true);
            numForMultiply = Random.Range(1, 99);
            questionText.text = numForMultiply + "  X  2  =  ?";
            questionStartTime = Time.time;

            correctAnswer = numForMultiply * 2;
            Options.Add(correctAnswer);
            Debug.Log(correctAnswer);
            int wrongValuesForOptions;

            for (int i = 1; i <= 3; i++)
            {
                wrongValuesForOptions = Random.Range(correctAnswer - 10, correctAnswer + 10);
                Options.Add(wrongValuesForOptions);
                Debug.Log(wrongValuesForOptions);
            }
            StartCoroutine(BannerIncomingFalse());

        }
        else
        {
            ShowResults();
        }

    }

    public void StoreAnswerAndCheckAnswer()
    {

        if (int.TryParse(input.text, out int result))
        {
            questionEndTime = Time.time;
            float timeTaken = (questionEndTime - questionStartTime);
            questionTimes.Add(timeTaken);

            if (correctAnswer == result)
            {
                correctCount++;
                correctCountText.text = "Correct - " + correctCount;
            }
            else
            {
                incorrectCount++;
                incorrectCountText.text = "Incorrect - " + incorrectCount;
            }

            currentQuestion++;
            RoundSystem();
            GenerateQuestionAndOptions();
        }
    }

    void ShowResults()
    {
        for (int i = 0; i < questionTimes.Count; i++)
        {
            float roundedTime = Mathf.Round(questionTimes[i] * 10f) / 10f; 
            averageTimeText.text += "Time taken in question " + (i + 1) + " is " + roundedTime.ToString("F1") + "\n";
        }  
        gameFinishPanel.SetActive(true);
    }

    void RoundSystem()
    {
        if (round < 11)
        {
            roundText.text = "Round - " + round + " / 10";
            round++;
        }
    }
    IEnumerator BannerIncomingFalse()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("BannerIncoming", false);
    }
}

