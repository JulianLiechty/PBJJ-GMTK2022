using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{

    [SerializeField]
    [TextArea]
    private string[] TextPanels;

    private TextMeshProUGUI TextField;
    private GameObject TextBox;

    private bool IsPaused = false;
    private int CurrPanel = 0;

    private void Awake()
    {
        TextBox = GameObject.FindGameObjectsWithTag("MainText")[0];
        TextBox.SetActive(false);

        TextField = TextBox.GetComponentInChildren<TextMeshProUGUI>();
        TextField.SetText("");

        if (TextPanels.Length == 0)
            Debug.LogError("Assign Text Panels to this trigger");
    }

    private void Update()
    {
        if (IsPaused && Input.GetKeyDown(KeyCode.Mouse0))
        {

            if(CurrPanel < TextPanels.Length - 1)
            {
                CurrPanel++;
                TextField.SetText(TextPanels[CurrPanel]);
            }
            else
            {
                TextField.SetText("");
                TextBox.SetActive(false);
                IsPaused = false;
                Time.timeScale = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Dice"))
        {
            IsPaused = true;
            Time.timeScale = 0;
           
            TextBox.SetActive(true);
            TextField.SetText(TextPanels[0]);
        }
    }
}
