using UnityEngine;

public class Clamp : MonoBehaviour {
    // indicate if this object is milo
    // this is used to prevent a bug that causes milo z axis to change
    public bool milo = false;
    public bool isObject = false;

    // keep the object with this script in the specified bounds
    void Update() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        // set the bounderies for the x axis of this object
        pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f);
        // set the bounderies for the y axis of this object
        if (isObject) {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        // set the bounderies for the y axis seperetely for each person
        else if (name == "Harold") {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        else if (name == "Milo") {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        else if (name == "Karin") {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        else if (name == "Sandra") {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        else if (name == "Berry") {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        else if (name == "Derk") {
            pos.y = Mathf.Clamp(pos.y, 0.20f, 0.95f);
        }
        // set the bounderies for the z axis of this object
        // this also will prevent a bug that causes milo to teleport 
        if (milo) {
            pos.z = Mathf.Clamp(pos.y, 100.0706f, 100.0706f);
        }
        // apply these bounderies to this object
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}

