using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Toolbox : MonoBehaviour {
    // used in the SizeCheck function
    private GameObject overObject;
    private GameObject overButton;
    // list for for all objects that are on the screen
    public List<string> ScreenList = new List<string>();
    public List<GameObject> ScreenList2 = new List<GameObject>();
    // list with all placeable items/persons
    public List<GameObject> TotalList = new List<GameObject>();
    // list for for all the place buttons
    public List<GameObject> ButtonList = new List<GameObject>();
    // list for objects that have been given to a person
    public List<string> StringList = new List<string>();
    // the maximum amount of objects that are allowed on the screen
    public int objectLimit = 5;
    // is the given answere correct or not
    public bool correct = false;
    // used for setting correct true
    public int listCheck = 0;
    // the object you want to have added to the list
    private GameObject addObject = null;
    //getcomponent scripts
    private Needed needed;
    private Stages stages;
    public Data data;

    void Start() {
        // getcomponents
        needed = GetComponent<Needed>();
        stages = GetComponent<Stages>();
        data = stages.data;
    }

    // if the size of the list is more than the set limit remove a random item from the list and screen
    public void SizeCheck() {
        // max objects on the screen
        if (ScreenList2.Count() > objectLimit) {
            // Remove random object from the screen execpt for the last placed object.
            overObject = GameObject.Find(ScreenList2.ElementAt(Random.Range(1, ScreenList2.Count()) - 1).name);
            // if the object has a name in this list then stop here
            overButton = ButtonList.Find(obj => obj.name == "Place Button " + overObject.name);
            if (overButton) {
                // remove the randomly selected item to keep the list to its limit
                overButton.GetComponent<ToolBoxChoose>().RemoveItem();
            }
        }
    }

    // compare if both lists are equal
    public void Compare() {
        // compare if both lists are equal
        needed.CompareLists(needed.NeededList, ScreenList);
        // compare toolbox string list wth needed string list
        needed.CompareLists(needed.NeededStringList, StringList);
        // transform the contents of the list into a single string
        var s = string.Join(",", StringList.ToArray());
        var d = string.Join(",", ScreenList.ToArray());
        // if there is data in both vars then add both
        if (d != "" && s != "") {
            data.AddToListString(d + "," + s, stages.currentStage, stages.CurrentStageLevelInput);
        }
        // if there is no data in var s then only add the data in var d
        else if (d != "" && s == "") {
            data.AddToListString(d, stages.currentStage, stages.CurrentStageLevelInput);
        }
        // if there is no data in var d then only add the data in var s
        else if (d == "" && s != "") {
            data.AddToListString(s, stages.currentStage, stages.CurrentStageLevelInput);
        }
        // if both answers are correct then give the specified score
        if (listCheck == 2) {
            correct = true;
            GetComponent<Points>().SpecificScore(20);
            listCheck = 0;
        }
        // if there is one or no correct answer do nothing
        else {
            listCheck = 0;
            correct = false;
        }
        // clear list of current objects
        ScreenList.Clear();
        // clear list of current objects 2
        ScreenList2.Clear();
        // clear list of objects colliding with persons
        StringList.Clear();
        // move to next stage
        stages.StageCompleted(stages.CurrentStageLevelInput);
    }

    /********************************/
    /*** Used for the screen list ***/
    /********************************/
    // these items are requered to complete the stage
    public void AddToListString(string nameObject) {
        // find the object with the given name 
        addObject = TotalList.Find(obj => obj.name == nameObject);
        ScreenList.Add(addObject.name);
    }

    // remove object from the list of items needed to complete this stage 
    // if the item can't be found it will print out Specified object not found
    public void RemoveFromList(string nameObject) {
        // find object with the given name
        addObject = TotalList.Find(obj => obj.name == nameObject);
        ScreenList.Remove(addObject.name);
    }

    /**********************************/
    /*** Used for the screen list 2 ***/
    /**********************************/
    // use this to find the given object and then add it to the list
    public void AddToListString2(GameObject nameObject) {
        // find the object with the given name 
        addObject = TotalList.Find(obj => obj.name == nameObject.name);
        ScreenList2.Add(addObject);
    }

    // use this to find the given object and then remove it from the list
    public void RemoveFromList2(GameObject nameObject) {
        // find object with the given name
        addObject = TotalList.Find(obj => obj.name == nameObject.name);
        ScreenList2.Remove(addObject);
    }
}
