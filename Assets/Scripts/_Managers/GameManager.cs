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

    public float freeCamSens { get; private set; }
    public float swingCamSens { get; private set; }

    void Start()
    {
        freeCamSens = 3f;
        swingCamSens = 0.06f;
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

    public void FreeCamSens(float val)
    {
        freeCamSens = val;
    }

    public void SwingCamSens(float val)
    {
        swingCamSens = val;
    }

}
