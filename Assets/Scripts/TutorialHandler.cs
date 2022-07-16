using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string[] TextPanels = new string[6];

    private GameObject TextBox;
    private TextMeshProUGUI TextField;

    private bool[] ValuesHit = new bool[6];

    [SerializeField]
    [Tooltip("How long the tutorial stays up in seconds")]
    private float TutorialUpTime = 3f;

    [SerializeField]
    private bool CanTriggerMultipleTimes = false;

    private void Awake()
    {
        GameObject.FindGameObjectsWithTag("Dice")[0].GetComponent<DiceSolver>().OnDiceStop += DiceStop;

        TextBox = GameObject.FindGameObjectsWithTag("SideText")[0];
        TextBox.SetActive(false);

        TextField = TextBox.GetComponentInChildren<TextMeshProUGUI>();
        TextField.SetText("");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int DiceStop(int Val)
    {
        Debug.LogWarning(Val + "YOOOOOOO");

        if (Val < 1 || Val > 6)
            return 0;

        if (!ValuesHit[Val-1] || CanTriggerMultipleTimes)
        {
            ValuesHit[Val-1] = true;
            TextBox.SetActive(true);
            TextField.SetText(TextPanels[Val-1]);

            StartCoroutine(DisableMessage());
        }

        return 0;
    }

    IEnumerator DisableMessage()
    {
        yield return new WaitForSeconds(TutorialUpTime);

        TextField.SetText("");
        TextBox.SetActive(false);
    }
}
