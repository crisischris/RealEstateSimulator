using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{

    public string currentScene;
    public GameObject helperMenu;
    public HandController controllerL;
    public HandController controllerR;

    // Use this for initialization
    void Start()
    {

        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "HomeSpace")
        {
            controllerL.helperMenuOn = false;
            controllerR.helperMenuOn = false;
            helperMenu.SetActive(false);
        }
    }
}
