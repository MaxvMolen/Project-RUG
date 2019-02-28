using UnityEngine;

public class Audio : MonoBehaviour {
    // audio source
    public AudioSource scourceAudio;
    // audioclip
    public AudioClip audioClip = null;

    // Update is called once per frame
    void Update() {
        // check if there is a audiofile and it sourceAudio is already playing or not
        // if the file is done it will directly stat up again
        if (audioClip != null && !scourceAudio.isPlaying) {
            PlayFile(audioClip);
        }
    }

    // play the given audio file once
    public void PlayFile(AudioClip clip) {
        if (scourceAudio.isPlaying == false) {
            scourceAudio.PlayOneShot(clip, 1.0F);
        }
    }
}
