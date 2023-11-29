using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftPage : MonoBehaviour
{
    public TMP_Text page;

    // Start is called before the first frame update
    void Start()
    {
        page.text = "";
    }

    public void SetText(Component sender, object data) {
        page.text = (string) data;
    }
}
