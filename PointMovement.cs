using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointMovement : MonoBehaviour {
    // the index of the current point in the list
    private int i = 0;
    // the total amount of objects in the array
    private int total;
    // the speed at which the object moves at
    public float speed;
    // if you want to move back to the start point and start again
    public bool Loop = true;
    // set true if you want the object to stop at the specified point
    public bool stopPoint = false;
    // the point the object will stop at
    public int setStopPoint;
    // stops the movement
    public bool stop = false;
    // the list containing all the points
    public List<Transform> pointsList = new List<Transform>();
    private int move = 0;

    // Use this for initialization
    void Start() {
        // set the total points 
        total = pointsList.Count();
    }

    // Update is called once per frame
    void FixedUpdate() {
        // speed of the object
        float step = speed * Time.deltaTime;
        // stop at the end of the list or if there are no items in the list then stop 
        if (i == total && Loop == false || total == 0) {
            // will stop the loop
            stop = true;
        }
        // as long as object has not stopped execute this code 
        if (stop == false) {
            // move object from point to point
            transform.position = Vector3.MoveTowards(transform.position, pointsList[i].position, step);
            // if object reaches the point move to next point
            if (Vector3.Distance(transform.position, pointsList[i].position) < 0.1f) {
                i++;
            }
        }
        // restart from the beginning of the list
        if (i == total && Loop) {
            i = 0;
            // will keep the loop continueing
            stop = false;
        }
        // if the index is the same as the given point to stop then stop movement
        if (i == setStopPoint && stopPoint == true) {
            // will stop the loop
            stop = true;
        }
    }

    // add a point to the list
    public void AddToList(Transform point) {
        pointsList.Add(point);
        // update the total points
        total = pointsList.Count();
    }

    // remove a point from the list
    public void RemoveFromList(Transform point) {
        // search the list to see if its inside if it is remove it
        if (pointsList.Find(obj => obj.name == point.gameObject.name)) {
            pointsList.Remove(point);
        }
        // update the total points
        total = pointsList.Count();
    }

    // used to reset the object to the first point // also makes it start on the first point
    public void ResetPos() {
        // on first press set position to point 1 and en second pres make the object reset to start
        if (move == 1) {
            // reset the object with this script to its starting point and unfreeze it
            transform.position = pointsList[0].position;
            stop = false;
            i = 0;
            move = -1;
        }
        transform.position = pointsList[0].position;
        move++;
    }
}
