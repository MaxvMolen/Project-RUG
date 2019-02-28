using UnityEngine;

public class KeepSteady : MonoBehaviour {
    // this script will not rotate with the given object but wil stay at the same position relative to the given object
    // the given object of focus
    public GameObject sobject;
    // used to keep the object steady
    private Vector3 offset;

    // Use this for initialization
    void Start() {
        // get the offset from this and other object
        offset = transform.position - sobject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate() {
        // follow object but dont rotate and keep distance as set from the start
        transform.position = sobject.transform.position + offset;
    }
}
