using UnityEngine;

public class ParticlePlay : MonoBehaviour {
    // the particle you want to play
    public ParticleSystem pSystem;
    // the created particlesystem
    private ParticleSystem instantiated;

    // Use this for initialization
    void Start() {
        // create the particle system and make the particle the child of the object with this script
        instantiated = Instantiate(pSystem, transform.position, Quaternion.identity, gameObject.transform);
        // play particlesystem
        instantiated.Play();
        // set the object active
        instantiated.gameObject.SetActive(true);
    }
}
