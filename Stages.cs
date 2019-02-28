using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stages : MonoBehaviour {
    // contains the scripts for timer
    public TimerScript timer;
    // the amount of stages you want
    public int CurrentStageLevelInput;
    // current stage of the level
    public int currentStage = 1;
    // used for going through the list of buttons
    private int i;
    // next scene button
    public Button nextQuestion;
    // next check button
    public Button checkQuestion;
    // the audio replay button
    public Button replayAudio;
    // audio for all the question
    public AudioClip question1 = null;
    public AudioClip question2 = null;
    public AudioClip question3 = null;
    public AudioClip question4 = null;
    // scripts/components // needed for audio,lists and user data
    private AudioSource scourceAudio;
    private Needed need;
    private Toolbox toolbox;
    public Data data;
    public InsertData insertData;
    public GameObject id;
    // if the stage is completed or not
    bool stageCompleted = false;
    // used to stop the loop once its gone through the full list of button
    private int doneButtons = 0;

    // Use this for initialization
    void Start() {
        CurrentStageLevelInput = SceneManager.GetActiveScene().buildIndex; // get the current scene index
        // getcomponent
        need = GetComponent<Needed>();
        toolbox = GetComponent<Toolbox>();
        scourceAudio = GetComponent<AudioSource>();
        // set next question to false
        nextQuestion.interactable = false;
        // find background audio // this is used to get the data and insertdata scripts
        id = GameObject.FindWithTag("BackAudio");
        data = id.GetComponent<Data>();
        insertData = id.GetComponent<InsertData>();
        // set current level, set question answers and send data
        CurrentStageActive(CurrentStageLevelInput);
    }

    void Update() {
        // check if there is an object on the screen
        // if there is turn the check button on if there is not turn it off
        if (toolbox.ScreenList.Count != 0 || toolbox.StringList.Count != 0) {
            checkQuestion.interactable = true;
        }
        else {
            checkQuestion.interactable = false;
        }
    }
    /* sending data
     * insertData.UpdateData(1,1,data.StringList[0]);
     * insertData.UpdateData(the level number,question number, the given answer); */

    /* Creating the question
     *  use this if you want to add a person or object who you dont need to give or give away
     *      need.AddToListString("Sandra");
     *  use this to set the object en the person you need to give it to
     *  you will not need to add the first option if you want to do this with the object
     *      need.NeededStringList.Add("bal>Harold"); */

    public void CurrentStageActive(int CurrentStageLevel) {
        /*Level 1 #############*/
        if (CurrentStageLevel == 1) { // level 1
            if (currentStage == 1) {
                StartCoroutine(PlayAudio(question1, 3, 1)); // play the question once after the set amount of time has passed
                // who to give the object to
                need.NeededStringList.Add("bal>Harold");
                timer.PauseTimer();
                // InsertData to database // name and age of the user
                insertData.SetData(data.nameUser, data.ageUser);
            }
            if (currentStage == 2) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question2, 1, 2));
                StartCoroutine(SetTrue(question2.length));
                // who to give the object to
                need.NeededStringList.Add("yoyo>Berry");
                // InsertData to database // answers
                insertData.UpdateData(1, 1, data.StringList[0]);
            }
            if (currentStage == 3) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question3, 1, 3));
                StartCoroutine(SetTrue(question3.length));
                // who to give the object to
                need.NeededStringList.Add("ballon>Karin");
                // InsertData to database // answers
                insertData.UpdateData(1, 2, data.StringList[1]);
            }
            if (currentStage == 4) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question4, 1, 4));
                StartCoroutine(SetTrue(question4.length));
                // who to give the object to
                need.NeededStringList.Add("beer>Milo");
                // InsertData to database // answers
                insertData.UpdateData(1, 3, data.StringList[2]);
            }
            // final stage of the level
            if (currentStage == 5) {
                stageCompleted = true;
                nextQuestion.interactable = true;
                checkQuestion.interactable = false;
                replayAudio.interactable = false;
                // turn all the buttons non interactible
                for (int i = 0; i < toolbox.ButtonList.Count; i++) {
                    // add the collider to the screenlist
                    toolbox.ButtonList[i].GetComponent<Button>().interactable = false;
                }
                // InsertData to database // answers
                insertData.UpdateData(1, 4, data.StringList[3]);
                // show end score
                StartCoroutine(Stars(1));
            }
        }

        /*Level 2 #############*/
        if (CurrentStageLevel == 2) { // level 2
            if (currentStage == 6) {
                StartCoroutine(PlayAudio(question1, 3, 6)); // play the question once after the set amount of time has passed
                // add needed object to list
                need.AddToListString("Milo");
                // InsertData to database // name and age of the user
                insertData.SetData(data.nameUser, data.ageUser);
                timer.PauseTimer();
            }
            if (currentStage == 7) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question2, 1, 7));
                StartCoroutine(SetTrue(question2.length));
                // add needed object to list
                need.AddToListString("Milo");
                // InsertData to database // answers
                insertData.UpdateData(2, 5, data.StringList[4]);
            }
            if (currentStage == 8) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question3, 1, 8));
                StartCoroutine(SetTrue(question3.length));
                // add needed object to list
                need.AddToListString("Milo");
                // InsertData to database // answers
                insertData.UpdateData(2, 6, data.StringList[5]);
            }
            if (currentStage == 9) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question4, 1, 9));
                StartCoroutine(SetTrue(question4.length));
                // add needed object to list
                need.AddToListString("Milo");
                // InsertData to database // answers
                insertData.UpdateData(2, 7, data.StringList[6]);
            }
            // final stage of the level
            if (currentStage == 10) {
                stageCompleted = true;
                nextQuestion.interactable = true;
                checkQuestion.interactable = false;
                replayAudio.interactable = false;
                // turn all the buttons non interactible
                for (int i = 0; i < toolbox.ButtonList.Count; i++) {
                    // add the collider to the screenlist
                    toolbox.ButtonList[i].GetComponent<Button>().interactable = false;
                }
                // InsertData to database // answers
                insertData.UpdateData(2, 8, data.StringList[7]);
                // show end score
                StartCoroutine(Stars(1));
            }
        }

        /*Level 3 #############*/
        if (CurrentStageLevel == 3) { // level 3
            if (currentStage == 11) {
                StartCoroutine(PlayAudio(question1, 3, 11)); // play the question once after the set amount of time has passed
                // add needed object to list
                need.AddToListString("ballon");
                // who to give the object to
                //need.NeededStringList.Add("Yoyo>Karin");
                timer.PauseTimer();
            }
            if (currentStage == 12) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question2, 1, 12));
                StartCoroutine(SetTrue(question2.length));
                // add needed object to list
                need.AddToListString("ballon");
                // InsertData to database // answers
                insertData.UpdateData(3, 9, data.StringList[8]);
            }
            if (currentStage == 13) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question3, 1, 13));
                StartCoroutine(SetTrue(question3.length));
                // add needed object to list
                need.AddToListString("ballon");
                // InsertData to database // answers
                insertData.UpdateData(3, 10, data.StringList[9]);
            }
            if (currentStage == 14) {
                checkQuestion.interactable = false;
                StartCoroutine(PlayAudio(question4, 1, 14));
                StartCoroutine(SetTrue(question4.length));
                // add needed object to list
                need.AddToListString("ballon");
                // InsertData to database // answers
                insertData.UpdateData(3, 11, data.StringList[10]);
            }
            // final stage of the level
            if (currentStage == 15) {
                stageCompleted = true;
                nextQuestion.interactable = true;
                checkQuestion.interactable = false;
                replayAudio.interactable = false;
                // turn all the buttons non interactible
                for (int i = 0; i < toolbox.ButtonList.Count; i++) {
                    // add the collider to the screenlist
                    toolbox.ButtonList[i].GetComponent<Button>().interactable = false;
                }
                // InsertData to database // answers
                insertData.UpdateData(3, 12, data.StringList[11]);
                // show end score
                StartCoroutine(Stars(1));
            }
        }

        // set all objects inactive and play their particlesystem
        if (doneButtons <= toolbox.ButtonList.Count) {
            for (i = 0; i < toolbox.ButtonList.Count; i++) {
                toolbox.ButtonList[i].GetComponent<ToolBoxChoose>().SetOrigin();
                doneButtons++;
            }
        }
        doneButtons = 0;
        i = 0;
    }

    // on starting a new scene wait the set amount of seconds before playing the given audio file
    IEnumerator PlayAudio(AudioClip name, int time, int input) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        if (input == currentStage) {
            // play the given audio file
            scourceAudio.PlayOneShot(name, 0.7F);
            // set check button to true when clip ends
            StartCoroutine(SetTrue(name.length));
        }
    }

    // wait for audioclip to finish then continue the timer
    IEnumerator SetTrue(float time) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        timer.ContinueTimer();
    }

    // wait set amount of second to show stars
    IEnumerator Stars(int time) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        GetComponent<Points>().ShowStars(); // show the recieved stars
    }

    // replay audio function 
    public void ReplayAudio() {
        // check on what stage you are on and play the question that is needed for that stage
        // if the audio is already playing it wont play until after it has stopped and the button is pressed again
        // vraag 1
        if (currentStage == 1 || currentStage == 6 || currentStage == 11) {
            if (scourceAudio.isPlaying == false) {
                scourceAudio.PlayOneShot(question1, 0.7F);
            }
        }
        // vraag 2
        if (currentStage == 2 || currentStage == 7 || currentStage == 12) {
            if (scourceAudio.isPlaying == false) {
                scourceAudio.PlayOneShot(question2, 0.7F);
            }
        }
        // vraag 3
        if (currentStage == 3 || currentStage == 8 || currentStage == 13) {
            if (scourceAudio.isPlaying == false) {
                scourceAudio.PlayOneShot(question3, 0.7F);
            }
        }
        // vraag 4
        if (currentStage == 4 || currentStage == 9 || currentStage == 14) {
            if (scourceAudio.isPlaying == false) {
                scourceAudio.PlayOneShot(question4, 0.7F);
            }
        }
    }

    // if the stage is completed empty the list and move up to the next stage
    public void StageCompleted(int LevelStageList) {
        // stop audio that is already playing
        scourceAudio.Stop();
        need.NeededList.Clear(); // remove all objects from the list
        need.NeededStringList.Clear(); // remove all strings from the stringlist
        if (stageCompleted == false) {
            currentStage++; // move to next stage
        }
        CurrentStageActive(LevelStageList); // Set the stage and set the needed answers
    }
}
