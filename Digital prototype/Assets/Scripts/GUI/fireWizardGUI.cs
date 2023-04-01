using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class fireWizardGUI : MonoBehaviour
{
    public ProgressBar fire_wizard_health_bar;
    public ProgressBar water_wizard_health_bar;
    public ProgressBar air_wizard_health_bar;

    private int fire_wizard_health;
    private int water_wizard_health;
    private int air_wizard_health;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        fire_wizard_health_bar = root.Q<ProgressBar>("FireWizardHealthBar");
        water_wizard_health_bar = root.Q<ProgressBar>("WaterWizardHealthBar");
        air_wizard_health_bar = root.Q<ProgressBar>("AirWizardHealthBar");


        fire_wizard_health=100;
        water_wizard_health=40;
        air_wizard_health=88;

    }

    void Update()
    {
       //TODO
       //m_health = get health (for each wizard)
       
       fire_wizard_health_bar.value = fire_wizard_health;
       water_wizard_health_bar.value = water_wizard_health;
       air_wizard_health_bar.value = air_wizard_health;

    //     //testing
    //    if (fire_wizard_health >=100){
    //         fire_wizard_health = 1;
    //    }
    //    else{
    //         fire_wizard_health=fire_wizard_health+1;
    //    }

    //     if (water_wizard_health >=100){
    //         water_wizard_health = 1;
    //     }
    //     else{
    //         water_wizard_health = water_wizard_health+2;
    //     }
        
    //     if (air_wizard_health <=0){
    //         air_wizard_health = 100;
    //     }
    //     else{
    //         air_wizard_health = air_wizard_health-3;
    //     }


    }
}
