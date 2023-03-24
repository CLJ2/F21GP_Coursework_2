using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    public Transform agent;
    public Image foreground;
    public Image background;
    public Vector3 offset = new Vector3(0, 0.4f, 0);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(agent.position + offset);
    }

    public void SetHealthBarPecentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

    }
}
