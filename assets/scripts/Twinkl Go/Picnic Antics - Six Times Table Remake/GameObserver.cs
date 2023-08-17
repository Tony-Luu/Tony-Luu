using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameObserver : MonoBehaviour
{
    [Header("Sprites for Emojis")]
    [SerializeField] private Sprite[] Emojis = new Sprite[4];
    [SerializeField] private Image SpeechBubbleImage;

    [Header("Buttons")]
    [SerializeField] private Button[] AnswerButton = new Button[4];
    [SerializeField] private Vector2[] ButtonPositions = new Vector2[4];
    [SerializeField] private RectTransform ButtonRectTransform;

    [Header("Sound")]
    [SerializeField] private AudioSource AntAudioSource;
    [SerializeField] private AudioClip AntMovingSound;
    [SerializeField] private AudioClip AntEatingSound;

    [Header("Other")]
    [SerializeField] private RectTransform EndScreenRectTransform;
    [SerializeField] private GameObject[] WorkerAnts = new GameObject[4];
    [SerializeField] private float MoveSpeed;
    [SerializeField] private RectTransform AntWithSpeechBubbleRectTransform;
    [SerializeField] private bool HasPlayerSelectedCorrectAnswer;

    private CanvasGroup PauseCanvasGroup;
    public delegate CanvasGroup PauseUI();
    public static PauseUI PauseMenu;

    //Delegate for MoverWorkerAnt script
    public delegate bool CheckIfWorkerAntsReachedDestination();
    public static CheckIfWorkerAntsReachedDestination AreWorkerAntsAtFoodItem;

    public delegate void CreateWorkerAntDestination();
    public static CreateWorkerAntDestination WorkerAntDestination;

    //Delegate for the speed value
    public delegate float ReturnMovement();
    public static ReturnMovement ReturnSpeedValue;

    //Delegates for GenerateAnswers script
    public delegate TextMeshProUGUI Display();
    public static Display ReturnSumDisplay;

    public delegate void ResetValues();
    public static ResetValues ReinitalizeDisplayedNumbers;

    public delegate int Answer();
    public static Answer CheckAnswer;

    public delegate List<int> NumbersList();
    public static NumbersList ReturnList;

    //Delegate for GenerateFoodItems script
    public delegate void ResetSprites();
    public static ResetSprites ResetFoodSprites;

    //Delegates for ScoreManager script
    public delegate void Score(int score);
    public static Score IncreaseScore;

    public delegate int ReadScore();
    public static ReadScore ReturnScore;

    public delegate void ScoreText();
    public static ScoreText UpdateScoreText;

    //Delegate for ChangeAntSprite script
    public delegate void AntSprites();
    public static AntSprites SwapAntSprites;


    CanvasGroup ReturnCanvasGroup()
    {
        return PauseCanvasGroup;
    }

    void ResetButtonPositions()
    {
        for (int i = 0; i < ButtonPositions.Length; i++)
        {
            AnswerButton[i].GetComponent<RectTransform>().anchoredPosition = ButtonPositions[i];
        }
    }

    float ReturnSpeed()
    {
        return MoveSpeed;
    }

    void SetStartingButtonPositions()
    {
        for (int i = 0; i < ButtonPositions.Length; i++)
        {
            ButtonPositions[i] = AnswerButton[i].GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void SetCurrentButtonRectTransform(RectTransform CurrentButtonRectTransform)
    {
        ButtonRectTransform = CurrentButtonRectTransform;
    }

    void SetButtonRectTransformWithIndex(int index)
    {
        ButtonRectTransform = AnswerButton[index].GetComponent<RectTransform>();
    }

    bool DoesNumberMatchCorrectAnswer(int Number)
    {
        HasPlayerSelectedCorrectAnswer = Number == CheckAnswer();
        return HasPlayerSelectedCorrectAnswer;
    }

    public void ToggleButtons(bool Toggle)
    {
        for (int i = 0; i < AnswerButton.Length; i++)
        {
            AnswerButton[i].interactable = Toggle;
        }
    }

    void AddListenerToButtons()
    {
        for (int i = 0; i < AnswerButton.Length; i++)
        {
            //Set the value from the for loop to the integer value
            int Index = i;

            //Set the value from the parsed text obtained from the answer button
            int ParsedAnswer = int.Parse(AnswerButton[i].GetComponentInChildren<TextMeshProUGUI>().text);

            //Add the listeners
            AnswerButton[i].onClick.AddListener(() => { SetButtonRectTransformWithIndex(Index); });
            AnswerButton[i].onClick.AddListener(() => { DoesNumberMatchCorrectAnswer(ParsedAnswer); });
            AnswerButton[i].onClick.AddListener(() => { ToggleAllButtons(false); });
        }
    }

    void RemoveListenerFromButtons()
    {
        for (int i = 0; i < AnswerButton.Length; i++)
        {
            //Remove all listeners from the button
            AnswerButton[i].onClick.RemoveAllListeners();
        }
    }

    void Start()
    {
        PauseCanvasGroup = GameObject.Find("PauseMenu").GetComponent<CanvasGroup>();
        PauseMenu = ReturnCanvasGroup;
        SetStartingButtonPositions();
        AddListenerToButtons();
        ReturnSpeedValue = ReturnSpeed;
        AreWorkerAntsAtFoodItem = HasAllWorkerAntsTouchedFoodItem;
        WorkerAntDestination = SetWorkerAntTargetDestinationToAntWithSpeechBubble;
        SwapAntSprites();
    }

    public bool HasAllWorkerAntsTouchedFoodItem()
    {
        for (int i = 0; i < WorkerAnts.Length; i++)
        {
            MoveWorkerAnt MoveWorkerAntScript = WorkerAnts[i].GetComponent<MoveWorkerAnt>();

            if (!MoveWorkerAntScript.HasAntArrivedAtFood())
            {
                return false;
            }
        }
        return true;
    }

    public void SetWorkerAntTargetDestinationToAntWithSpeechBubble()
    {
        if (HasAllWorkerAntsTouchedFoodItem())
        {
            RectTransform AntRectTransform = GameObject.Find("Canvas/Ant").GetComponent<RectTransform>();

            //Loop through the button array
            for (int i = 0; i < AnswerButton.Length; i++)
            {
                //Obtain the rect transform from the worker ants
                RectTransform WorkerAntRectTransform = WorkerAnts[i].GetComponent<RectTransform>();

                //Set the X and Y offsets
                float OffsetX = WorkerAntRectTransform.anchoredPosition.x - ButtonRectTransform.anchoredPosition.x;
                float OffsetY = WorkerAntRectTransform.anchoredPosition.y - ButtonRectTransform.anchoredPosition.y;

                //Set the ant target destination and direction
                Vector2 AntTargetDestination = AntRectTransform.TransformDirection(OffsetX, AntRectTransform.anchoredPosition.y + OffsetY, 0f);

                //Obtain the worker ant script and set the new target position
                MoveWorkerAnt MoveWorkerAntScript = WorkerAnts[i].GetComponent<MoveWorkerAnt>();
                MoveWorkerAntScript.SetNewTargetPosition(AntTargetDestination);

                //Obtain the animate button script and toggle the moving button
                AnimateButton AnimateButtonScript = ButtonRectTransform.GetComponent<AnimateButton>();
                AnimateButtonScript.ToggleMovingButton(true);
            }
        }
    }

    //Show emojis depending on a random value
    IEnumerator ShowEmojiFromIndexRange(int MinRange, int MaxRange, Action callback = null)
    {
        //Generate a unique random number
        int RandomIndex = UniqueRandomNumberGenerator.ReturnRandomValue(MinRange, MaxRange);

        //Set the speech bubble to active
        SpeechBubbleImage.transform.gameObject.SetActive(true);

        //Set the sprite to one of the emojis
        SpeechBubbleImage.sprite = Emojis[RandomIndex];

        //Wait for a few seconds
        yield return new WaitForSeconds(2);

        //If the action is not empty then call the method
        if (callback != null)
        {
            callback();
        }

        //Toggle all buttons to be activated true
        ToggleAllButtons(true);

        //Deactivate the speech bubble
        SpeechBubbleImage.transform.gameObject.SetActive(false);

        //Invoke the method to return the sum display and enable it
        ReturnSumDisplay.Invoke().enabled = true;
    }

    bool AreWorkerAntsActive()
    {
        WorkerAnts = GameObject.FindGameObjectsWithTag("WorkerAnt");

        if (WorkerAnts.Length == 0)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < WorkerAnts.Length; i++)
            {
                if (!WorkerAnts[i].activeInHierarchy)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void SetAllButtonsToActive()
    {
        for (int i = 0; i < AnswerButton.Length; i++)
        {
            if (!AnswerButton[i].gameObject.activeInHierarchy)
            {
                AnswerButton[i].gameObject.SetActive(true);
            }
        }
    }

    void ToggleAllButtons(bool Toggle)
    {
        for(int i = 0; i < AnswerButton.Length; i++)
        {
            AnswerButton[i].interactable = Toggle;
        }
    }

    void PlayAudioClip(AudioClip Clip, bool IsClipLooping)
    {
        //Store the previous clip
        AudioClip PreviousClip = Clip;

        //If the audio source exists
        if (AntAudioSource != null)
        {
            //Set if the clip loops or not
            AntAudioSource.loop = IsClipLooping;

            //If the audio source is not playing
            if (!AntAudioSource.isPlaying)
            {
                //Set the clip
                AntAudioSource.clip = Clip;

                //Play the clip
                AntAudioSource.Play();
            }
            else
            {
                //If the audio source is playing but the current clip is different than the previous one
                if (AntAudioSource.clip != PreviousClip)
                {
                    //Stop the audio source
                    AntAudioSource.Stop();

                    //Set the clip
                    AntAudioSource.clip = Clip;

                    //Play the clip
                    AntAudioSource.Play();
                }
            }            
        }
    }

    void CheckAnswerFromAnswerButton()
    {
        //If the food item button has been selected and the worker ants are active with all of them touching the food item
        if (ButtonRectTransform != null && AreWorkerAntsActive() && HasAllWorkerAntsTouchedFoodItem())
        {
            //If the distance is within proximity and if the food item button overlaps the ant with the speech bubble
            if (Vector2.Distance(ButtonRectTransform.anchoredPosition, AntWithSpeechBubbleRectTransform.anchoredPosition) <= 90f && 
                DetectOverlappingRectTransforms.IsFirstRectTransformOverlappingWithSecondRectTransform(ButtonRectTransform, AntWithSpeechBubbleRectTransform))
            {
                //Play the audio clip of the ant eating
                PlayAudioClip(AntEatingSound, false);

                //Deactivate the worker ants
                DeactivateWorkerAnts();

                //Deactive the selected food item button
                ButtonRectTransform.transform.gameObject.SetActive(false);

                //Deactive the sum display
                ReturnSumDisplay.Invoke().enabled = false;

                //If the player has selected the correct answer
                if (HasPlayerSelectedCorrectAnswer)
                {
                    //Add 1 to the the score
                    IncreaseScore(1);

                    //Update the score
                    UpdateScoreText();

                    //Start the coroutine of showing the emojos
                    StartCoroutine(ShowEmojiFromIndexRange(0, 2, ResetQuestion));
                }
                else
                {
                    //Start the coroutine of showing the emojos
                    StartCoroutine(ShowEmojiFromIndexRange(2, Emojis.Length));
                }
            }
        }
    }

    void ResetQuestion()
    {
        ReinitalizeDisplayedNumbers();
        ResetFoodSprites();
        RemoveListenerFromButtons();
        AddListenerToButtons();
        SetAllButtonsToActive();
        ResetButtonPositions();
        SwapAntSprites();
        ButtonRectTransform.transform.gameObject.SetActive(true);
        ButtonRectTransform = null;
        HasPlayerSelectedCorrectAnswer = false;

        //If the score is 12 then activate the end screen
        if (ReturnScore() == 12)
        {
            EndScreenRectTransform.gameObject.SetActive(true);
        }
    }

    void DeactivateWorkerAnts()
    {
        for (int i = 0; i < WorkerAnts.Length; i++)
        {
            WorkerAnts[i].transform.position = Vector3.zero;
            WorkerAnts[i].SetActive(false);
        }
    }

    void PlayAntMovingSound()
    {
        if (AreWorkerAntsActive())
        {
            PlayAudioClip(AntMovingSound, true);
        }
    }

    void Update()
    {
        CheckAnswerFromAnswerButton();
        PlayAntMovingSound();
    }

}