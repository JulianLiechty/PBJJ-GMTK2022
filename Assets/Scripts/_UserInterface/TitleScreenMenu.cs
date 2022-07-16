using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interact with the title menu with multiple parts:
 * 
 * Title screen
 * Credits
 */
public class TitleScreenMenu : MonoBehaviour
{
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject creditsMenu;

    public void TurnOnTitleMenu()
    {
        titleMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void TurnOnCreditsMenu()
    {
        titleMenu.SetActive(false);
        creditsMenu.SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
        TurnOnTitleMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
