using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

//statemachine used for determining enemy agent state
public class WitchAiStateMachine
{
    public WitchAiState[] states;   //stores possible states
    public WitchAiAgent agent;  //stores enemy agent
    public WitchAiStateID currentState; //stores current state

    public WitchAiStateMachine(WitchAiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(WitchAiStateID)).Length;
        states = new WitchAiState[numStates];
    }

    public void RegisterState(WitchAiState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public WitchAiState GetState(WitchAiStateID stateID)
    {
        return states[(int)stateID];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    //method changes state of the agent
    public void ChangeState(WitchAiStateID newStateID)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newStateID;
        GetState(currentState)?.Enter(agent); 
    }
}
