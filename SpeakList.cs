using System.Collections.Generic;
using UnityEngine;

public class SpeakList : MonoBehaviour {
    // list containing all the speakers
    public List<GameObject> speakerList = new List<GameObject>();
    public GameObject id;
    public AudioSource asource;

    void Update() {
        if (asource != null) {
            // check if audio is muted
            if (asource.mute && asource.isPlaying) {
                for (int i = 0; i < speakerList.Count; i++) {
                    // stop the particles from playing if the object is muted
                    speakerList[i].GetComponentInChildren<ParticleSystem>().Stop();
                    // turn off the animation
                    speakerList[i].GetComponent<Animator>().enabled = false;
                }
            }
            // check if audio is muted
            else if (!asource.mute && asource.isPlaying) {
                for (int i = 0; i < speakerList.Count; i++) {
                    // check if particles are playing or not
                    if (!speakerList[i].GetComponentInChildren<ParticleSystem>().isPlaying) {
                        // if they are not playing then start playing them
                        speakerList[i].GetComponentInChildren<ParticleSystem>().Play();
                        // turn on the animation
                        speakerList[i].GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }
        // find the needed objects
        else {
            id = GameObject.FindWithTag("AB");
            asource = id.GetComponent<AudioSource>();
        }
    }
}
