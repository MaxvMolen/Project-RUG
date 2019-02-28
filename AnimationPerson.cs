using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AnimationPerson : MonoBehaviour {
    public Animator animator;
    // the image of this person
    private Image m_Image;
    // default sprite
    public Sprite defaultS;
    // sprtie where person holds out its hand
    public Sprite outHand;
    // script used for moving the person on the stage
    public MoveToStage mts;
    // for checking animation state of the person
    private int an = 0;
    // for changing the sprites of the person
    private int hh = 0;
    // if this person has a animation after the walking one
    public bool ani = false;
    // used for dragging the object
    private DragObject drag;
    // for chaning playing the animation and changing sprite
    public bool animatorS = false;
    // indicate if hand is reaching out
    private bool hand = false;
    // indicate if the animation is finished playing
    private bool fin = true;
    // the list containing all the points
    public List<GameObject> objList = new List<GameObject>();

    // Use this for initialization
    void Start() {
        drag = GetComponent<DragObject>();
        // get the sprite of this object
        m_Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        // check if MoveToStage is enebled if it is turn grounded true if it isnt turn grounded false
        if (mts.enabled) {
            hh = 1;
            animator.enabled = true;
            animator.SetBool("Extra", false);
            animator.SetBool("Grounded", true);
            animator.SetBool("Idle", false);
            // will make an animation play once mts is disabled
            an = 0;
            if (animatorS == true) {
                ani = true;
            }
        }
        // set grounded to false and turn the sprite in the default
        else {
            // stop the animation and set default sprite
            if (an == 1) {
                animator.SetBool("Idle", true);
                animator.SetBool("Extra", false);
                // turn sprite back to default if it isn't already
                if (m_Image.sprite != defaultS && m_Image.sprite != outHand) {
                    m_Image.sprite = defaultS;
                }
            }
            // play animation only once
            if (an == 0) {
                animator.SetBool("Extra", true);
                // stop the object from moving until the time has passed
                if (animatorS == true) {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                    GetComponentInChildren<Collider2D>().enabled = false;
                    ani = true;
                    drag.enabled = false;
                    StartCoroutine(SetDrag(1));
                }
            }
            // will make sure the animation plays only once
            if (an < 1) {
                an++;
            }
        }
    }

    // change the image to the one where the the hand is held out
    public void HoldHand() {
        hand = true;
        // after walking has stopped
        if (hh == 1) {
            // if it has a transition animation do this
            if (ani == true) {
                if (gameObject.activeSelf && fin == true) {
                    fin = false;
                    StartCoroutine(SetTrue(1));
                }
            }
            // if it has no transition or the transition is already done
            else {
                hh = 2;
            }
        }
        // hold out hand sprite
        if (hh == 2) {
            animator.enabled = false;
            m_Image.sprite = outHand;
        }
    }

    // reset list with colliding objects
    public void ObjClear() {
        objList.Clear();
    }

    // change the image to the default one
    public void StopHand() {
        hand = false;
        // after walking has stopped
        if (hh == 1) {
            // if it has a transition animation do this
            if (ani == true) {
                if (gameObject.activeSelf && fin == true) {
                    fin = false;
                    StartCoroutine(SetTrue(1));
                }
            }
            // if it has no transition or the transition is already done
            else {
                hh = 2;
            }
        }
        // default sprite
        if (hh == 2) {
            animator.enabled = false;
            m_Image.sprite = defaultS;
        }
    }

    // wait until given time and then turn of animator and change the sprite to the one specified
    IEnumerator SetTrue(float time) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        fin = enabled;
        // change the current sprite to the default sprite
        if (hand == true) {
            m_Image.sprite = outHand;
            hh = 2;
            animator.enabled = false;
            ani = false;
        }
        // change the current sprite to the hold out hand sprite
        else if (hand == false) {
            m_Image.sprite = defaultS;
            hh = 2;
            animator.enabled = false;
            ani = false;
        }
    }

    // wait until the given time and then turn dragobject on again
    IEnumerator SetDrag(float time) {
        yield return new WaitForSeconds(time); // wait for the given amount of seconds
        GetComponentInChildren<Collider2D>().enabled = true;
        //unfreeze the object and then freeze rotation again
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        drag.enabled = true;
        ani = false;
    }
}
