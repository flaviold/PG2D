public interface IState
{
    void Update();
    StatesEnum StateTransition(StatesEnum nextState);
}
