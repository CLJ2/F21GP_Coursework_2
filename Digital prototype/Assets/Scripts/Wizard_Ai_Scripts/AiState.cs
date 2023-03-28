using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateID
{
    Idle,
    Downed,
    TargetEnemy,
    FollowPlayer,
    Unactive
}

public interface AiState
{
    AiStateID GetStateID();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);

}
