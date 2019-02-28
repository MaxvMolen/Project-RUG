using UnityEngine;

public class ClampM : MonoBehaviour {
    // keep the object with this script in the specified bounds
    void Update() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.z = Mathf.Clamp(pos.y, 100.0f, 100.0f);
        // apply these bounderies to this object
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}