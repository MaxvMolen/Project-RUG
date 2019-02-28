using UnityEngine;

public class Relaties : MonoBehaviour {
    // indicator if the image will be shown or not (0 = show, 1 = dont show)
    public int show = 0;

    // give the image you want to show
    public void ShowImage(GameObject image) {
        // dont show the image
        if (show == 1) {
            image.SetActive(false);
            show = 0;
        }
        // show the image
        else {
            show++;
            image.SetActive(true);
        }
    }
}
