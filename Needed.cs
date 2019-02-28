using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Needed : MonoBehaviour {
    // the list which contains all the objects needed to complete a stage
    public List<string> NeededList = new List<string>();
    // the list which contains all the objects that can be given and to who it can be given
    public List<string> NeededStringList = new List<string>();
    // used for adding gameobjects to the list
    private string addObject = null;
    // toolbox script used for checking the list which contains all objects and persons and for setting correct to true
    public Toolbox toolbox;

    // add object to the list
    // these items are requered to complete the stage
    public void AddToListString(string nameObject) {
        // find object with the given name
        addObject = toolbox.TotalList.Find(obj => obj.name == nameObject).name;
        // add this found object to the NeededList
        if (addObject != null) {
            NeededList.Add(addObject);
            addObject = null;
        }
    }

    // remove object from the list of items needed to complete this stage 
    // if the item can't be found it will print out Specified object not found
    public void RemoveFromList(string nameObject) {
        // find object with the given name
        addObject = toolbox.TotalList.Find(obj => obj.name == nameObject).name;
        // remove this found object from the NeededList
        if (addObject != null) {
            NeededList.Remove(addObject);
            addObject = null;
        }
    }

    // compare the 2 list, if both are equal do something
    public void CompareLists(List<string> needList, List<string> nameList) {
        // if the list contains the same contents and are the same size
        if (needList.All(nameList.Contains) == true && needList.Count == nameList.Count) {
            toolbox.listCheck++;
        }
    }
}
