using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDeathState : WitchAiState
{
    public Vector3 deathDirection;

    public WitchAiStateID GetID()
    {
        return WitchAiStateID.Dead;
    }

    public void Enter(WitchAiAgent agent)
    {
        agent.healthBar.gameObject.SetActive(false);
        if(agent.dead == false){
        agent.animator.CrossFade("dead", 0.5f);
        agent.healthBar.gameObject.SetActive(false);    //turn off health bar
        deathDirection.y = 0.5f;    //give direction of death a heigh to make the agent lift up at the start of death
        agent.dead =true;
        //Debug.Log("dead now yey");
        }

    }

    public void Update(WitchAiAgent agent)
    {

    }
    
    public void Exit(WitchAiAgent agent)
    {
        
    }
}
