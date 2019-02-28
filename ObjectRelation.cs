using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectRelation : MonoBehaviour {
    // object is set to never sleep since it will not constantly update the ontrigger stay function other wise
    public bool check = false;
    // toolbox for adding data/removing
    public Toolbox Toolbox = null;
    // animator of object
    public Animator anim;
    // default sprite and the image of the object
    public Image m_Image;
    public Sprite defaultS;
    // data which contains a object and to who it was given to 
    private string givenTo;
    // for moving the object
    public Transform point;
    // the person it is currently touching
    public GameObject pObject;
    // if the object wil be moving or not
    public bool move = false;
    // the speed at which the object moves
    public float speed = 5;
    // used to keep object from moveing from person to person
    public bool onTrigger = true;
    // keeps track of with how many persons its colliding
    public int collCount = 0;
    private float scale;

    void Start() {
        scale = GetComponent<Rigidbody2D>().gravityScale;
    }

    // fixed update will allow the object to move correctly even at low framerates
    void FixedUpdate() {
        if (check == true) {
            // move the object to the hand of the person
            if (move) {
                float step = speed * Time.deltaTime;
                // check if the given object exists
                if (point != null) {
                    // move the object towards the given object
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(point.position.x, point.position.y, transform.position.z), step);
                    //point.transform.parent.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                }
            }
        }
    }

    // reset the person it needs to move to
    public void SetNull() {
        point = null;
        pObject = null;
        collCount = 0;
        onTrigger = true;
    }

    // on triger enter with person
    void OnTriggerEnter2D(Collider2D other) {
        if (check == true) {
            // move the object to the hand of the person
            if (other.name != "floor" && other.name != "collider" && other.name != "collider (1)" && other.name != "floorS" && other.name != "Hand Positie") {
                // used to keep track of how many objects it is touching
                collCount++;
                // this will stop the object from changing from person to person
                if (onTrigger == true && collCount <= 1) {
                    point = other.gameObject.transform.GetChild(0);
                    pObject = other.gameObject;
                    move = true;
                    onTrigger = false;
                }
            }
            // if collider is hand positie send data
            if (other.name == "Hand Positie") {
                // send the name of the object and the object it is colliding with
                NameCollider(other.transform.parent.name, 1, name);
            }
        }
    }

    // on triger exit with person
    void OnTriggerExit2D(Collider2D other) {
        if (check == true) {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().gravityScale = scale;
            // check if other object is the floor or on of the objects 
            // then set grounded to false and disable the animator
            if (other.gameObject.name == "floorS" || other.transform.parent.gameObject.transform.parent.name == "kip" || other.transform.parent.gameObject.transform.parent.name == "bal"
          || other.transform.parent.gameObject.transform.parent.name == "ballon" || other.transform.parent.gameObject.transform.parent.name == "beer" || other.transform.parent.gameObject.transform.parent.name == "yoyo"
          || other.transform.parent.gameObject.transform.parent.name == "ijs" || other.transform.parent.gameObject.transform.parent.name == "boek" && move == false) {
                anim.SetBool("Grounded", false);
                anim.enabled = true;
            }
            if (other.name != "floor" && other.name != "collider" && other.name != "collider (1)" && other.name != "floorS" && other.name != "Hand Positie") {
                // used to keep track of how many objects it is touching
                collCount--;
                // stop the object from moving toward the hand
                move = false;
                onTrigger = true;
                // allow object to move freely again
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().gravityScale = scale;
                // when object leaves trigger of person turn on animator
                anim.enabled = true;
            }
            // if collider is hand positie send data and set hand sprite
            if (other.name == "Hand Positie") {
                // send the name of the object
                NameCollider(other.transform.parent.name, 2, name);
                // check if object is in list if not add it
                if (other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().objList.Find(obj => obj == gameObject)) {
                    // prevents collision issues with other objects
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = false;
                    other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().objList.Remove(gameObject);
                }
                // only change sprite if no other objects are in the persons hand
                if (other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().objList.Count == 0) {
                    other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().StopHand();
                }
            }
        }
    }

    // check if object touches a person // if it does freeze this object
    void OnTriggerStay2D(Collider2D other) {
        if (check == true) {
            // freeze objects
            if (other.transform.parent.name == "Harold" || other.transform.parent.name == "Milo" || other.transform.parent.name == "Karin"
                || other.transform.parent.name == "Sandra" || other.transform.parent.name == "Berry" || other.transform.parent.name == "Derk") {
                // change person to move to 
                if (collCount == 1 && other.name != "Hand Positie") {
                    pObject = other.gameObject;
                    point = other.gameObject.transform.GetChild(0);
                }
                move = true;
                // freeze object in place
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                anim.enabled = false;
                // if the sprite is not the same as the default
                DefaultSprite();
            }
        }

        // check if other object is the floor or on of the objects
        // then turn grounded to true en start the coroutine settrue 
        if (other.gameObject.name == "floorS" || other.transform.parent.gameObject.transform.parent.name == "kip" || other.transform.parent.gameObject.transform.parent.name == "bal"
            || other.transform.parent.gameObject.transform.parent.name == "ballon" || other.transform.parent.gameObject.transform.parent.name == "beer" || other.transform.parent.gameObject.transform.parent.name == "yoyo"
            || other.transform.parent.gameObject.transform.parent.name == "ijs" || other.transform.parent.gameObject.transform.parent.name == "boek" && move == false) {
            // stop playing the animation
            anim.SetBool("Grounded", true);
            // set the default sprite
            if (m_Image.sprite != defaultS) {
                // wait the given amount of time so the animation can finish
                StartCoroutine(SetTrue(1));
            }
        }
        // if collider is named hand positie send data and set hand sprite
        if (other.name == "Hand Positie") {
            // if object is given directly to the hand
            point = other.transform;
            // send the name of the object
            NameCollider(other.transform.parent.name, 3, name);
            // check if object is in list if not add it
            if (!other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().objList.Find(obj => obj == gameObject)) {
                // prevents collision issues with other objects
                other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().objList.Add(gameObject);
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
            }
            // only change sprite if only one object is in the persons hands
            if (other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().objList.Count == 1) {
                other.transform.parent.parent.gameObject.GetComponent<AnimationPerson>().HoldHand();
            }
        }
    }

    // the sprite of this object to the default
    public void DefaultSprite() {
        // check if the current sprite is the same as the default
        // if not change the current sprite into the default one
        if (m_Image.sprite != defaultS) {
            m_Image.sprite = defaultS;
        }
    }

    // wait the given amount of time and then turn the animator of and turn the sprite to default
    IEnumerator SetTrue(float time) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        // check if the object touches the ground if it does disable animator and set default sprite
        if (anim.GetBool("Grounded")) {
            anim.enabled = false;
            DefaultSprite();
        }
    }

    // this function will add or remove the input objects/persons depending on what method it uses
    // (name = name of other object | EE = indicator what method to use | collider = name of this object)
    public void NameCollider(string name, int EE, string collider) {
        // ignore colliders with the listed names
        if (name != "floor" && name != "collider" && name != "collider (1)" && name != "floorS" && name != "Hand Positie") {
            // who this object was given to
            givenTo = (collider + ">" + name);
            // enter trigger
            if (EE == 1) {
                // add this object to the list   
                Toolbox.StringList.Add(givenTo);
                // remove the object and colliding person from the list since it is now added to the stringlist
                Toolbox.RemoveFromList(name);
                Toolbox.RemoveFromList(collider);
            }
            // exit trigger
            if (EE == 2) {
                // remove this object from the list
                Toolbox.StringList.Remove(givenTo);
                // check if screenlist2 contains the object with this scripts then check if screenlist doesnt have it
                // then add this item to the list
                if (Toolbox.ScreenList2.Find(obj => obj.name == name) && Toolbox.ScreenList.Find(obj => obj == name) == null) {
                    Toolbox.AddToListString(name);
                }
                // check if screenlist2 contains the colliding person then check if screenlist doesnt have it
                // then add this person to the list
                if (Toolbox.ScreenList2.Find(obj => obj.name == collider) && Toolbox.ScreenList.Find(obj => obj == collider) == null) {
                    Toolbox.AddToListString(collider);
                }
            }
            // stay trigger
            if (EE == 3) {
                // if object is still on the object keep it and the object out of the onscreenlist
                Toolbox.RemoveFromList(name);
                Toolbox.RemoveFromList(collider);
            }
        }
    }
}
