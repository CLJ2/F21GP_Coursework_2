using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyAiState
{
    public EnemyAiStateID GetID()
    {
        return EnemyAiStateID.Dead;
    }

    public void Enter(EnemyAiAgent agent)
    {
        //add death handling here
    }

    public void Update(EnemyAiAgent agent)
    {

    }
    
    public void Exit(EnemyAiAgent agent)
    {
        
    }
}
