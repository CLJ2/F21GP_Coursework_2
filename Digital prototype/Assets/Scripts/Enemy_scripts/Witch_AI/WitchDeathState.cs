using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDeathState : WitchAiState
{
    public Vector3 deathDirection;
    bool dead =false;

    public WitchAiStateID GetID()
    {
        return WitchAiStateID.Dead;
    }

    public void Enter(WitchAiAgent agent)
    {
        agent.healthBar.gameObject.SetActive(false);
        if(dead == false){
        agent.animator.CrossFade("dead", 0.5f);
        agent.healthBar.gameObject.SetActive(false);    //turn off health bar
        deathDirection.y = 0.5f;    //give direction of death a heigh to make the agent lift up at the start of death
        dead =true;
        }

    }

    public void Update(WitchAiAgent agent)
    {

    }
    
    public void Exit(WitchAiAgent agent)
    {
        
    }
}
