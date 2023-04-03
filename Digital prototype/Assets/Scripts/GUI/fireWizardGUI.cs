using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class fireWizardGUI : MonoBehaviour
{
    public ProgressBar fire_wizard_health_bar;
    public ProgressBar water_wizard_health_bar;
    public ProgressBar air_wizard_health_bar;


    public void SetUpHealthBar(float maxhealth)
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        fire_wizard_health_bar = root.Q<ProgressBar>("FireWizardHealthBar");
        water_wizard_health_bar = root.Q<ProgressBar>("WaterWizardHealthBar");
        air_wizard_health_bar = root.Q<ProgressBar>("AirWizardHealthBar");

        //Debug.Log(maxhealth);
        //Set the health bars max value to the maxhealth
        fire_wizard_health_bar.highValue = maxhealth;
        water_wizard_health_bar.highValue = maxhealth;
        air_wizard_health_bar.highValue = maxhealth;

        //Set the health bars current value to the max health of the wizard
        fire_wizard_health_bar.value = maxhealth;
        water_wizard_health_bar.value = maxhealth;
        air_wizard_health_bar.value = maxhealth;
    }

    public void UpdateHealthBars(AiAgent agent, float health)
    {
        if (agent.name == "FireWizard") fire_wizard_health_bar.value = health;
        if (agent.name == "WaterWizard") water_wizard_health_bar.value = health;
        if (agent.name == "AirWizard") air_wizard_health_bar.value = health;


        if (air_wizard_health_bar.value <= 0 && water_wizard_health_bar.value <=0 && fire_wizard_health_bar.value <= 0) {
            SceneManager.LoadScene(0);

        }
    }
}


       