using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour {

    public void close()
    {
        gameObject.SetActive(false);
    }
}
