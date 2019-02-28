using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject inGameMenu = null;
    // used for showing the ingamemenu
    public int menu = 0;
    public GameObject gordijn = null;
    // used to dinf the object with the tag BackAudio
    public GameObject id;
    // scripts
    public Data data;
    public InsertData insertdata;
    public PointMovement pointmovement;

    // Use this for initialization
    void Start() {
        // data
        id = GameObject.FindWithTag("BackAudio");
        insertdata = id.GetComponent<InsertData>();
        data = id.GetComponent<Data>();
        // turn ingamemenu of at the start of the scene
        if (inGameMenu != null) {
            inGameMenu.SetActive(false);
        }
        // set grodijn active at the start of the scene
        if (gordijn != null) {
            gordijn.SetActive(true);
        }
    }

    public void StartGameButton() {
        // reset given answers
        for (int i = 0; i < data.StringList.Count; i++) {
            data.StringList[i] = "";
        }
        insertdata.set = true;
        insertdata.send = false;
        SceneManager.LoadScene("Scene1");
    }

    public void CreditsGameButton() {
        SceneManager.LoadScene("Credits");
    }

    public void MenuGameButton() {
        SceneManager.LoadScene("Main");
    }

    public void InGameMenuOpen() {
        inGameMenu.SetActive(true);
        // close menu again if pressed once before
        if (menu == 1) {
            inGameMenu.SetActive(false);
            menu = -1;
        }
        menu++;
    }

    public void InGameMenuClose() {
        // close the menu and reset its positon to the default
        inGameMenu.SetActive(false);
        pointmovement.ResetPos();
        if (menu == 1) {
            menu = -1;
        }
        menu++;
    }
}
