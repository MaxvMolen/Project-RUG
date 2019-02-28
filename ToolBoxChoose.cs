using UnityEngine;

public class ToolBoxChoose : MonoBehaviour {
    public Toolbox toolbox;
    // the item you want to place
    public GameObject item = null;
    // extra object that belongs to item (bal,jojo)
    public GameObject extra = null;
    // the glow for around the buttons
    public GameObject shine = null;
    // indicate if this button wil remove on this pres
    public bool remove = false;
    // some settings for placing
    public bool freezeR = true;
    public bool isObject = false;
    // scripts / components
    private MoveToStage movetostage;
    private Clamp clamp;
    private ObjectRelation objectrelation;
    private Particle particle;
    private Rigidbody2D rigidbody2d;
    // spawn point
    public Transform target;
    private int i = 0;

    void Start() {
        // turn off the glow around the buttons
        shine.SetActive(false);
        // get all the needed components
        movetostage = item.GetComponent<MoveToStage>();
        rigidbody2d = item.GetComponent<Rigidbody2D>();
        clamp = item.GetComponent<Clamp>();
        objectrelation = item.GetComponent<ObjectRelation>();
        particle = item.GetComponent<Particle>();
    }

    // turn the object off and play particle on its position
    // needs to use GetComponent in this function otherwise it will cause errors
    public void SetOrigin() {
        remove = false;
        // if the removed item is an object change its settings
        if (isObject == true) {
            // reset settings of the object
            item.GetComponent<ObjectRelation>().check = false;
            item.GetComponent<ObjectRelation>().move = false;
            item.GetComponent<ObjectRelation>().SetNull();
            item.transform.GetChild(0).transform.GetChild(0).GetComponent<Collider2D>().isTrigger = false;
        }
        else {
            item.GetComponent<AnimationPerson>().ObjClear();
        }
        // will stop playing particles if the object is not active
        if (item.activeSelf) {
            if (i > 0) {
                item.GetComponent<Particle>().PlayParticle(item.transform.position);
            }
            i++;
        }
        item.SetActive(false);
        // this will cause the extra object to be on top of the item
        // check if the extra gameobject exists
        if (extra != null && i >= 2) {
            extra.SetActive(false);
        }
        item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        item.GetComponent<Clamp>().enabled = false;
        // turn off the glow around the buttons
        shine.SetActive(false);
    }

    // Place the given object
    public void PlaceItem() {
        if (remove == false) {
            // turn on the glow around the buttons
            shine.SetActive(true);
            rigidbody2d.constraints = RigidbodyConstraints2D.None;
            // freeze object its rotation
            if (freezeR == true) {
                rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            item.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, item.transform.position.z);
            item.SetActive(true);
            // check if the extra gameobject exists
            if (extra != null) {
                extra.SetActive(true);
            }
            // add object to list of items on screen
            toolbox.AddToListString(item.name);
            toolbox.AddToListString2(item);
            // if this is an object
            if (isObject == true) {
                // reset settings of the object
                objectrelation.check = true;
                objectrelation.SetNull();
                clamp.enabled = true;
                // check if the extra gameobject exists
                if (extra != null) {
                    extra.GetComponent<Animator>().enabled = true;
                }
                else {
                    item.GetComponent<Animator>().enabled = true;
                }
            }
            // if this is an person
            if (isObject == false) {
                item.GetComponent<AnimationPerson>().ObjClear();
                // move the person to its spawn point, activate the animation and move to the given point
                movetostage.ResetDistance();
                movetostage.enabled = true;
            }
            // check if the objects on screen are within the set limit. if not then the function calls the RemoveItem function
            toolbox.SizeCheck();
            remove = true;
            // reset the rotation
            item.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            // if place button has been pressed once call the removeitem function on a second pres down
            RemoveItem();
        }
    }

    // remove the placed item
    public void RemoveItem() {
        clamp.enabled = false;
        item.SetActive(false);
        // check if the extra gameobject exists
        if (extra != null) {
            extra.SetActive(false);
        }
        // turn on the glow around the buttons
        shine.SetActive(false);
        // play the particles for removing the object on the last position of the object
        particle.PlayParticle(item.transform.position);
        if (isObject == true) {
            // reset settings of the object
            objectrelation.check = false;
            objectrelation.SetNull();
            // disable and re enable the animator
            // check if the extra gameobject exists
            if (extra != null) {
                extra.GetComponent<Animator>().enabled = false;
            }
            else {
                item.GetComponent<Animator>().enabled = false;
            }
            // while doing that set the object to its default sprite
            objectrelation.DefaultSprite();
            // check if the extra gameobject exists
            if (extra != null) {
                extra.GetComponent<Animator>().enabled = enabled;
            }
            else {
                item.GetComponent<Animator>().enabled = enabled;
            }
        }
        if (isObject == false) {
            item.GetComponent<AnimationPerson>().ObjClear();
            // move the person to its spawn point
            movetostage.ResetDistance();
        }
        item.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, item.transform.position.z);
        // remove object to list of items on screen
        toolbox.RemoveFromList(item.name);
        toolbox.RemoveFromList2(item);
        // reset the rotation
        item.transform.rotation = Quaternion.Euler(0, 0, 0);
        // remove item
        remove = false;
    }
}
