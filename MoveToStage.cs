using UnityEngine;

public class MoveToStage : MonoBehaviour {
    // the speed at which this object will move
    public float speed = 5;
    // the starting point of the object
    public GameObject start;
    // the point the object will move towards
    public GameObject point;
    // getcomponent
    private Rigidbody2D rigidbody2d;
    private Clamp clam;

    // Use this for initialization
    void Start() {
        // getcomponents
        clam = GetComponent<Clamp>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        // turn components off
        GetComponent<DragObject>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        float step = speed * Time.deltaTime;
        // move the object towards the given object
        transform.position = Vector3.MoveTowards(transform.position, point.transform.position, step);
        // check if the object is within the specified distance of the point
        if (Vector3.Distance(transform.position, point.transform.position) > 0.1f) {
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        // if it is closer than the specified distance
        else {
            // stops the player when it is at the destination
            rigidbody2d.velocity = new Vector3(0, 0, 0);
            // make the player able to drag the object again
            GetComponent<DragObject>().enabled = true;
            GetComponentInChildren<Collider2D>().enabled = true;
            //unfreeze the object and then freeze rotation again
            rigidbody2d.constraints = RigidbodyConstraints2D.None;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            // enable object again and the bounderies
            clam.enabled = true;
            enabled = false;
        }
    }

    // return the object to the start position and disable some components
    public void ResetDistance() {
        GetComponent<DragObject>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
        // set the position of the person
        transform.position = start.transform.position;
    }
}
