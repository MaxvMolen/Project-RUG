using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
    //begin scale
    public float startXScale;
    //increase scale
    public float scaleIncrease;
    // end scale
    public float fullXScale;
    //rotation speed
    public float rotationSpeed;
    public float fullRotation;
    public float rotationLimit;
    // rest
    public int rotatetype;
    public int waitTime;
    private Transform transformComp;

    // Use this for initialization
    void Start() {
        transformComp = this.GetComponent<Transform>();
        Setsize(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(ShowStars(waitTime));
    }

    // wait given amount of time before rotating resizing the object
    IEnumerator ShowStars(int time) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        if (rotatetype == 1) {
            Rotate();
        }
        if (rotatetype == 2) {
            RotateAlt();
        }
        Resize();
    }

    // resize the object
    void Resize() {
        if (fullXScale > startXScale) {
            transformComp.localScale = new Vector3(startXScale += scaleIncrease, startXScale += scaleIncrease, startXScale += scaleIncrease);
        }
    }

    // set the size of current object
    void Setsize(float setX, float setY, float setZ) {
        transformComp.localScale = new Vector3(setX, setY, setZ);
    }

    // rotate the object
    void Rotate() {
        if (fullRotation <= rotationLimit) {
            fullRotation += rotationSpeed;
            transformComp.Rotate(Vector3.back * rotationSpeed);
        }
    }

    // rotate the object alternate
    void RotateAlt() {
        if (fullRotation >= rotationLimit) {
            fullRotation += rotationSpeed;
            transformComp.Rotate(Vector3.back * rotationSpeed);
        }
    }
}
