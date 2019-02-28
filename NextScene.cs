using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    // next level
    public void SetNext(CheckPoint curtain) {
        curtain.gameObject.SetActive(true);
        curtain.nextScene = true;
    }

    // go to the next level
    public void LoadNextLevel(string nextlevel) {
        SceneManager.LoadScene(nextlevel);
    }

    // go back to the previous level
    public void PreviousLevel(string previouslevel) {
        SceneManager.LoadScene(previouslevel);
    }

    // restart the current level
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
