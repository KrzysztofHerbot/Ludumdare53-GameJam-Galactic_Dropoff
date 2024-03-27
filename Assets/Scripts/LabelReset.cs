using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabelReset : MonoBehaviour
{
    public void Show()
    {
        GetComponent<TextMeshProUGUI>().enabled = true;
    }

    public void Hide()
    {
        GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
