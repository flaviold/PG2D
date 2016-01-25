using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine<T>
{
    #region State Transition
    public class StateTransition
    {
        public T currentState { get; set; }
        public T nextState { get; set; }

        public StateTransition(T currentState, T nextState)
        {
            this.currentState = currentState;
            this.nextState = nextState;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * currentState.GetHashCode() + 31 * nextState.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateTransition other = obj as StateTransition;
            return other != null && currentState.Equals(other.currentState) && nextState.Equals(other.nextState);
        }
    }
    #endregion

    #region BaseFsm Implementation
	private bool debug = true;
    protected Dictionary<StateTransition, T> transitions;
    public T currentState {
		get { return currentState; }
		set 
		{ 
			if(debug) Debug.Log("New current State: " + value);
			currentState = value;
		}
	}
    public T previousState;

    protected BaseStateMachine()
    {
        if (!typeof(T).IsEnum)
        {
            throw new Exception(typeof(T).FullName + " isn't an enum type!");
        }
    }

    private T GetNext(T next)
    {
        StateTransition transition = new StateTransition(currentState, next);
        T nextState;
        if (!transitions.TryGetValue(transition, out nextState)) throw new Exception("This transition is invalid: " + currentState + " -> " + next);
        Console.WriteLine("Next state " + nextState);
        return nextState;
    }

    public bool CanReachNext(T next)
    {
        StateTransition transition = new StateTransition(currentState, next);
        T nextState;
        if (!transitions.TryGetValue(transition, out nextState))
        {
            Console.WriteLine("This transition is invalid: " + currentState + "-> " + next);
            return false;
        }

        return true;
    }

    public T MoveNext(T next)
    {
        previousState = currentState;
        currentState = GetNext(next);
        Console.WriteLine("State changed from " + previousState + " to " + currentState);
        return currentState;
    }
    #endregion
}
