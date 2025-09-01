using System;
using Unity.Behavior;

[BlackboardEnum]
public enum AiState
{
    Patrol,
	Chase,
	ChasePause,
	Attack,
	Dead
}
