using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI[] headerFields;
    public TextMeshProUGUI[] contentFields;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string headerText, string[] content)
    {

        headerFields[0].text = headerText;
        for (int i = 0; i < contentFields.Length; i++)
        {
            if (content[i] == "")
            {
                headerFields[i].gameObject.SetActive(false);
                contentFields[i].gameObject.SetActive(false);
            }
            else
            {
                contentFields[i].text = content[i];
                headerFields[i].gameObject.SetActive(true);
                contentFields[i].gameObject.SetActive(true);
            }
        }

        layoutElement.enabled = Mathf.Max(
            headerFields[0].preferredWidth, headerFields[1].preferredWidth, headerFields[2].preferredWidth, headerFields[3].preferredWidth,
            contentFields[0].preferredWidth, contentFields[1].preferredWidth, contentFields[2].preferredWidth, contentFields[3].preferredWidth) >= 
            layoutElement.preferredWidth;
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;
        if (2.4f * LayoutUtility.GetPreferredWidth(rectTransform) + position.x > Screen.width)
        {
            position.x = Screen.width - 2.4f * LayoutUtility.GetPreferredWidth(rectTransform);
        }
        transform.position = position;
    }
}
