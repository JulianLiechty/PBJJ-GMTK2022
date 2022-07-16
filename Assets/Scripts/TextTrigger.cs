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

    private bool IsPaused = false;
    private int CurrPanel = 0;

    private void Awake()
    {
        TextField = GameObject.FindGameObjectsWithTag("MainText")[0].GetComponent<TextMeshProUGUI>();

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
            TextField.SetText(TextPanels[0]);
            Time.timeScale = 0;
        }
    }
}
