using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Page : MonoBehaviour
{
    public TMP_Text page;

    // Start is called before the first frame update
    void Awake()
    {
        page.SetText("");
        //page.fontSize = 11.5f;
    }

    public void SetText(Component sender, object data) {
        Debug.Log("Updating text to: " + (string)data);
        page.SetText((string)data);
        Debug.Log("Text should have updated");
    }
}
