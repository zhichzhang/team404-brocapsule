using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Windows;

public class LanceStateMachine
{
    public LanceState currentState { get; private set; }
    public LanceState previousState { get; private set; }

    public Lance lance;

    public AttackInfo attackInfo;

    public bool stateLocked;


    public LanceStateMachine(Lance lance)
    {
        this.lance = lance;
    }

    public Deflectable trigger { get; set; }

    public void Initialize(LanceState _startState)
    {
        currentState = _startState;
        currentState.Enter();
        stateLocked = false;
        attackInfo = null;
    }

    public void ChangeState(LanceState _newState)
    {
        previousState = currentState;
        currentState.Exit();
        attackInfo = null;
        currentState = _newState;
        currentState.Enter();
        
    }
    public void ChangeState(LanceState _newState, AttackInfo ai)
    {
        previousState = currentState;
        currentState.Exit();
        attackInfo = ai;
        currentState = _newState;
        currentState.Enter();
    }

    public void ChangeToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }


}
