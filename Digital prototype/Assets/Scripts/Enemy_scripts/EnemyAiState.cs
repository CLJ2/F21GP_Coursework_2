using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAiStateID  //possible states for enemy agents to have
{
    TargetPlayer,
    Dead,
    Idle,
    Hide
}
public interface EnemyAiState
{
    EnemyAiStateID GetID();
    void Enter(EnemyAiAgent agent);

    void Update(EnemyAiAgent agent);

    void Exit(EnemyAiAgent agent);
}
