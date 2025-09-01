using UnityEngine;

public class LanceState
{
    public LanceStateMachine stateMachine;
    public Lance lance;

    public LanceState(LanceStateMachine stateMachine, Lance lance)
    {
        this.stateMachine = stateMachine;
        this.lance = lance;
    }

    public virtual void Enter()
    {

    }
    public virtual void Exit() 
    {
        
    }
    public virtual void Update()
    {

    }
    public virtual void LateUpdate()
    {

    }
}
