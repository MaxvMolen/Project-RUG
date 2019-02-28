using UnityEngine;

public class CheckPoint : MonoBehaviour {
    // if you want to change to a new scene set true
    public bool nextScene;
    // if you want the object to bestroyed at the end
    public bool destroy = false;
    // the level you want to change to
    public string nextlevel;
    // scripts
    public NextScene buttons;
    public PointMovement point;
    private GameObject id;
    private Menu menu;

    void Start() {
        // this will only allow the curtain that goes down to use this code
        if (destroy == false) {
            // find the needed components close and reset the position of the ingamemenu
            id = GameObject.FindWithTag("BackAudio");
            menu = id.GetComponent<Menu>();
        }
    }

    // Update is called once per frame
    void Update() {
        // if object has stopped moving 
        if (point.stop == true) {
            // load the next level
            if (nextScene == true) {
                // close and reset position ingamemenu
                menu.InGameMenuClose();
                // reset menu int so the menu will not need 2 presses
                menu.menu = 0;
                buttons.LoadNextLevel(nextlevel);
            }
            // destroy the object
            if (destroy) {
                Destroy(gameObject);
            }
        }
    }
}
