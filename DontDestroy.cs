using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {
    // used for checking if this is a specific scene
    private string scene;
    public GameObject buttons;
    public Audio baudio;
    private static DontDestroy instance = null;

    void Update() {
        // get the camera
        GetComponent<Canvas>().worldCamera = Camera.main;
        // get the name of the currentscene
        scene = SceneManager.GetActiveScene().name;
        // check if this is the Credits scene
        if (scene == "Credits") {
            // zet de buttons uit
            buttons.SetActive(false);
            // stop the audio from playing // this will also make sure that the audio will start from the beginning again
            baudio.scourceAudio.Stop();
        }
        // check if this is the Main scene
        else if (scene == "Main") {
            // zet de buttons uit
            buttons.SetActive(false);
            // stop the audio from playing // this will also make sure that the audio will start from the beginning again
            baudio.scourceAudio.Stop();
        }
        // if it is not any of the above scenes then turn on the buttons
        else {
            buttons.SetActive(true);
        }
    }

    // on awake of this object
    void Awake() {
        // if there is another instance of this object in the scene destroy this instance
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        // if there is no other instance do not destroy this object
        else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
