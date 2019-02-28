using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextMenu : MonoBehaviour {
    // list with object of which the text will be updated
    public List<Text> TextList = new List<Text>();
    // the update you want to perform for those texts
    public List<string> StringList = new List<string>();

    // Use this for initialization
    void Start() {
        // update the text at startup
        UpdateText();
    }

    // update text
    void UpdateText() {
        // update text of the buttons
        for (int i = 0; i < TextList.Count; i++) {
            if (TextList[i] != null) {
                TextList[i].text = StringList[i];
            }
        }
    }
}
