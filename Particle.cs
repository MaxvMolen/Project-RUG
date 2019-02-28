using UnityEngine;

public class Particle : MonoBehaviour {
    // the particle you want to play
    public ParticleSystem paricleSystem;
    // the created particlesystem
    private ParticleSystem instantiated;

    public void PlayParticle(Vector3 location) {
        // createa particlesystem
        instantiated = Instantiate(paricleSystem, location, Quaternion.identity);
        // play the particle system
        instantiated.Play();
        // destroy the object when its done
        Destroy(instantiated.gameObject, 1.0f);
    }
}
