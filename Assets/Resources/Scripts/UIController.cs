using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public GameObject uiElement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void close()
    {
        if(uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    public void reloadApp()
    {
        SceneManager.LoadScene("cena1", LoadSceneMode.Single);
    }
}
