using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class WizardHealth : MonoBehaviour
{
    public float health;
    public AiAgent agent;
    public fireWizardGUI gui;

    void Start()
    {
        agent = GetComponent<AiAgent>();
        health = agent.config.maxHealth;
        gui = GameObject.Find("GUI").GetComponent<fireWizardGUI>();
        gui.SetUpHealthBar(agent.config.maxHealth);
        Debug.Log(agent);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        gui.UpdateHealthBars(agent, health);
        if(health < 0)
        {
            agent.stateMachine.ChangeState(AiStateID.Downed);
        }
    }
}
