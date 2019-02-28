using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
    // store the name of the user here
    public string nameUser;
    // store the age of the user here
    public int ageUser;
    // answers the user gave to all the questions
    public List<string> StringList = new List<string>();

    // add the answer the user gave to the list
    // insert the name of the object the stage that the answers is given and the current level
    public void AddToListString(string nameObject, int stage, int level) {
        // add the answer to the correct row 
        StringList[stage - level] = nameObject;
    }
}
