using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Transform agent; //Stores transfrom of agent. In this case the head was used
    public Image foreground;    //Stores a reference to the foreground of the health bar
    public Image background;    //Stores a reference to the background of the health bar
    public Vector3 offset = new Vector3(0, 0.4f, 0);    //Creates an offset in the y-axis to show healthbar above agent instead of inside

    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(agent.position + offset);   //Display health bar only when player is looking at the agent
    }

    //Set the foreground of the health bar to be a percentage of the max size based on the amount of health the agent has left
    public void SetHealthBarPecentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;   //Get size of healthbar
        float width = parentWidth * percentage; //Calculate the requiered width of foreground to represent current health
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);   //set foreground to required width

    }
}
