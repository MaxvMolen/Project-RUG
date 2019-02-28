using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {
    // list with object of which the text will be updated
    public List<Text> TextList = new List<Text>();
    // the update you want to perform for those texts
    public List<string> StringList = new List<string>();

    // get the scripts for points and time
    public Points point;
    public TimerScript timeScript;

    // Use this for initialization
    void Start() {
        // update the text at startup
        UpdateText();
    }

    // update text
    void UpdateText() {
        // update text of onscreen objects
        for (int i = 0; i < TextList.Count; i++) {
            if (TextList[i] != null) {
                TextList[i].text = StringList[i];
            }
        }
        // update text that is set with code
        timeScript.time = StringList[StringList.Count - 2];
        point.point = StringList[StringList.Count - 1];
        // update display
        point.UpdateScore();
        timeScript.SetTimerText();
    }
}
