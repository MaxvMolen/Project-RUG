using UnityEngine.UI;
using UnityEngine;

public class Points : MonoBehaviour {
    // the text on screen
    public Text scoreDisplay;
    // the star objects
    public GameObject passedDisplay;
    public GameObject oneStars;
    public GameObject twoStars;
    public GameObject threeStars;
    //script
    public TimerScript timer;
    // score amounts
    static int score = 0;
    private int totalscore = 0;
    static int AddScore = 10;
    private int scoreGained;
    // the lowest score requered for 1 star
    public int lowScore;
    // the score inbetween is 2 stars
    // the highest score requered for 3 stars
    public int topScore;
    public string point;
    // text in which to display the score
    public Text scoreEnd;
    public Text scoreTotal;

    // Use this for initialization
    void Start() {
        score = 0;
        // set false
        passedDisplay.SetActive(false);
        HideStars();
        // update the score text
        UpdateScore();
    }

    // will always add the addscore amount
    public void Score() {
        score += AddScore;
        scoreGained += AddScore;
        UpdateScore();
    }

    // the amount of score you want to add can be specified
    public void SpecificScore(int scoreAmount) {
        score += scoreAmount;
        scoreGained += scoreAmount;
        UpdateScore();
    }

    // update de display met de current score
    public void UpdateScore() {
        // to get totalscore get the total time given for the whole scene
        totalscore = score;
        scoreDisplay.text = point + " " + score;
        scoreEnd.text = score.ToString(); // score
        scoreTotal.text = totalscore.ToString() + ""; // total score
    }

    //cleared alle score van de display
    public void ClearScore() {
        score = 0; // set score to 0
        scoreGained = 0;
        UpdateScore(); // update the score board
    }

    // give the player the stars that fit the score they have recieved
    public void ShowStars() {
        totalscore += timer.timeOverInt;
        scoreGained += timer.timeOverInt;
        scoreTotal.text = totalscore.ToString(); // total score
        // seconds passed
        passedDisplay.SetActive(true);
        // 1 star - bad
        if (scoreGained <= lowScore) {
            oneStars.SetActive(true);
        }
        // 2 stars - good
        else if (scoreGained >= lowScore + 1 && scoreGained <= topScore - 1) {
            oneStars.SetActive(true);
            twoStars.SetActive(true);
        }
        // 3 stars - perfect
        else if (scoreGained >= topScore) {
            oneStars.SetActive(true);
            twoStars.SetActive(true);
            threeStars.SetActive(true);
        }
    }

    // hide all the stars are displayed
    public void HideStars() {
        oneStars.SetActive(false);
        twoStars.SetActive(false);
        threeStars.SetActive(false);
    }
}
