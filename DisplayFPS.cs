using UnityEngine;

// Drop on any game object and fps will apear in top left corner of the screen
public class DisplayFPS : MonoBehaviour {
    float deltaTime = 0.0f;
    // set the color of the text
    public Color colorText;
    // set the font size of the text / default is 30
    public int fontsize = 30;
    // set the x position of the display
    public float posX = 0;
    // set the y position of the display
    public float posY = 0;

    void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI() {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(posX, posY, w, h);
        // set the alignment
        style.alignment = TextAnchor.UpperLeft;
        // set the fontSize
        style.fontSize = fontsize;
        // set font color
        style.normal.textColor = colorText;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        // what the text will say
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}