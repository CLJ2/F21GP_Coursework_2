using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

//statemachine used for determining enemy agent state
public class EnemyAiStateMachine
{
    public EnemyAiState[] states;   //stores possible states
    public EnemyAiAgent agent;  //stores enemy agent
    public EnemyAiStateID currentState; //stores current state

    public EnemyAiStateMachine(EnemyAiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(EnemyAiStateID)).Length;
        states = new EnemyAiState[numStates];
    }

    public void RegisterState(EnemyAiState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public EnemyAiState GetState(EnemyAiStateID stateID)
    {
        return states[(int)stateID];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    //method changes state of the agent
    public void ChangeState(EnemyAiStateID newStateID)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newStateID;
        GetState(currentState)?.Enter(agent); 
    }
}
