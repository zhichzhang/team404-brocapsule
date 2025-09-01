using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    public PlayerState previousState { get; private set; }

    public Player player;
    public PlayerInput input;
    public bool stateLocked;


    public PlayerStateMachine(Player player)
    {
        this.player = player;
    }


    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
        stateLocked = false;
    }

    public void ChangeState(PlayerState _newState)
    {
        if (!stateLocked)
        {
            previousState = currentState;
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
        // if there is a stateLock, abort current transition
    }

    public void ChangeToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }


}
