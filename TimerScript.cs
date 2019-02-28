using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
    // the time the timer starts to countdown from
    public float timeLeft = 60.0f;
    private float resetTime = 60.0f;
    public float timeOvers = 0.0f;
    public int timeOverInt = 0;
    // if paused is true pause the timer
    private bool paused = false;
    // display time on screen
    public Text timer;
    public Text timePassedDisplay;
    // time countdown
    public string time;
    // total time passed
    public float totalTimePassed;
    // time passed
    private float timePassed;
    // toolbox script
    public Toolbox toolbox;

    // Use this for initialization
    void Start() {
        // set the time it needs to reset to after each question
        resetTime = timeLeft;
        // set timer text
        SetTimerText();
    }

    // Update is called once per frame
    void Update() {
        // count timer down
        if (paused == false) {
            Countdown();
        }
        // give certain amount of score if timer is over // also stops the timer and sets timeLeft to 0
        if (timeLeft <= 0) {
            paused = true;
            timeLeft = 0;
        }
    }

    // count the timer down
    void Countdown() {
        timeLeft -= Time.deltaTime;
        SetTimerText();
    }

    // pause the timer
    public void PauseTimer() {
        paused = true;
        SetTimerText();
    }

    // continue the timer
    public void ContinueTimer() {
        paused = false;
    }

    // time passed
    public void PassedTime() {
        timePassed = resetTime - timeLeft;
        totalTimePassed += timePassed; // add passed time to the total
        if (toolbox.correct == true) {
            timeOvers += (resetTime - timePassed);
        }
    }

    // reset the timeleft to what you set it as in the beginning
    public void ResetTimer() {
        timeLeft = resetTime;
    }

    // give score for the time reached
    public void ScoreTimer() {
        // pause the timer
        PauseTimer();
        PassedTime(); // time that has passed
        SetTimerText(); // update ui text
        ResetTimer();
    }

    // set score board text
    public void SetTimerText() {
        timeOverInt = Mathf.RoundToInt(timeOvers);
        timer.text = time + " " + timeLeft.ToString("F0");
        //timePassedDisplay.text = "Tijd: " + totalTimePassed.ToString("F0") + " Seconden";
        timePassedDisplay.text = totalTimePassed.ToString("F0");
    }
}
