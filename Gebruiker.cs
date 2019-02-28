using UnityEngine.UI;
using UnityEngine;

public class Gebruiker : MonoBehaviour {
    // the inputfields for the user
    public InputField voornaamField;
    public InputField leeftijdField;

    // indicate if the name/ age is given
    public bool inputNaam;
    public bool inputLeeftijd;

    // get data // scripts
    public GameObject id;
    public Data data;
    public InsertData insertdata;

    // the button that shows the user input fields
    public Button StartGame;

    // Use this for initialization
    void Start() {
        // set all other buttons to false so the player is forced to input their name and age before they can start the game
        id = GameObject.FindWithTag("BackAudio");
        data = id.GetComponent<Data>();
        insertdata = id.GetComponent<InsertData>();
    }

    void Update() {
        // check if the player has entered his name and age // if so then turn the start button on
        if (inputNaam == true && inputLeeftijd == true) {
            StartGame.interactable = true;
        }
        else {
            StartGame.interactable = false;
        }
    }

    // set the name of the player
    public void Voornaam() {
        // check if the input field is empty
        if (voornaamField.text != "") {
            inputNaam = true;
            // send the given name
            data.nameUser = voornaamField.text;
        }
        else {
            inputNaam = false;
        }
    }

    // set the age of the player
    public void Leeftijd() {
        // cheeck if the input field is empty or says 0,00
        if (leeftijdField.text != "" && leeftijdField.text != "0" && leeftijdField.text != "00") {
            inputLeeftijd = true;
            // turn the given string into an int and then send the age of the user
            int.TryParse(leeftijdField.text.ToString(), out data.ageUser);
        }
        else {
            inputLeeftijd = false;
        }
    }
}
