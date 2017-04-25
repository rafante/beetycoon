using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    // Create a public variable where we can assign the GUISkin
    public GUISkin guiSkin;

    // Apply the Skin in our OnGUI() function
    void OnGUI () {
        GUI.skin = guiSkin;

        // Now create any Controls you like, and they will be displayed with the custom Skin
        GUILayout.Toggle(true, "teste");

        // You can change or remove the skin for some Controls but not others
        GUI.skin = null;

        // Any Controls created here will use the default Skin and not the custom Skin
        GUILayout.Button ("This Button uses the default UnityGUI Skin");
    }
}
