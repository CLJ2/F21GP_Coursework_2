using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAiStateID  //possible states for enmey agents to have
{
    targetPlayer,
    Dead,
    Idle
}
public interface EnemyAiState
{
    EnemyAiStateID GetID();
    void Enter(EnemyAiAgent agent);

    void Update(EnemyAiAgent agent);

    void Exit(EnemyAiAgent agent);
}
