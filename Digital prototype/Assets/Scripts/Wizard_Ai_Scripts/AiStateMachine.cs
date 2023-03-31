using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
    public AiState[] states;
    public AiAgent agent;
    public AiStateID currentState;

    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AiStateID)).Length;
        states = new AiState[numStates];
    }

    public void RegisterState(AiState state)
    {
        int i = (int)state.GetStateID();
        states[i] = state;
    }
   
    public AiState GetState(AiStateID stateID)
    {
        int i = (int)stateID;
        return states[i];
    }

    public void Update()
    {
        GetState(currentState)?.Upadte(agent);
    }

    public void ChangeState(AiStateID newStateID)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newStateID;
        GetState(currentState)?.Enter(agent);
    }
}
