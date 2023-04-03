using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WitchAiStateID  //possible states for enmey agents to have
{
    TargetPlayer,
    Dead,
    Idle
}
public interface WitchAiState
{
    WitchAiStateID GetID();
    void Enter(WitchAiAgent agent);

    void Update(WitchAiAgent agent);

    void Exit(WitchAiAgent agent);
}
