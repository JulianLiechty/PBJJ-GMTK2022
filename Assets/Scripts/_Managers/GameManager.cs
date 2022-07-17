using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Instance of the menu manager that remains alive the entire life of the application.
    public static GameManager instance;
    public GameObject instanceObject;

    private GameObject winState;


    void Start()
    {

    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.instanceObject);
        }

        instance = this;
    }

    /// <summary>
    /// Loads the one level we have.
    /// </summary>
    public void LoadPlayableLevel()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Loads the Main Menu.
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
