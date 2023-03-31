using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateID
{
    targetPlayer
}

public interface AiState
{
    AiStateID GetStateID();
    void Enter(AiAgent agent);
    void Upadte(AiAgent agent);
    void Exit(AiAgent agent);

}
