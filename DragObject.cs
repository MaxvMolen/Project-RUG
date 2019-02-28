using UnityEngine;

public class DragObject : MonoBehaviour {
    private Vector3 screenPoint;
    // the rigid body of this object
    private Rigidbody2D rigidbody2d;
    // the gravity of this object
    private float gravity = 0;
    // zorgt er voor dat je het object pakt op het punt waar je klikt
    private Vector3 offset;

    // Use this for initialization
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        // get the set gravity from the object
        gravity = rigidbody2d.gravityScale;
    }

    // pickup the object
    public void OnMouseDown() {
        if (enabled == true) {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            rigidbody2d.gravityScale = 0;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    // freeze objects position and rotation when you release the object
    public void OnMouseUp() {
        if (enabled == true) {
            // stop the velocity of the object
            rigidbody2d.velocity = new Vector3(0, 0, 0);
            // set the gravity of the object
            rigidbody2d.gravityScale = gravity;
        }
    }

    // when you drag the object with your mouse
    public void OnMouseDrag() {
        if (enabled == true) {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            // get the current mouse position and add the offset of the object to it
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            // set this objects position to the position of the mouse cursor
            transform.position = curPosition;
        }
    }
}
