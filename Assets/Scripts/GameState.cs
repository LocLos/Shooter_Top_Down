using UnityEngine;
using Zenject;

public partial class GameState : MonoBehaviour
{
    public event OnLoadNextLvl onLoadNextLvl;
    public delegate void OnLoadNextLvl();

    public event OnShowCurtain onShowCurtain;
    public delegate void OnShowCurtain(bool isShow);

    Score _score;
    LoadingLvl _loadingLvl;
    public State state;

    [Inject]
    private void Construct(Score Score, LoadingLvl LoadingLvl)
    {
        _score = Score;
        _loadingLvl = LoadingLvl;
    }

    private void Start()
    {
        _score.onSendState += ChangeState;
        _loadingLvl.onSendState += ChangeState;
        ChangeState(State.NextLevel);
    }

    public void ChangeState(State Gamestate)
    {
        state = Gamestate;
        switch (Gamestate)
        {
            case State.NextLevel: onLoadNextLvl?.Invoke(); break;
            case State.Start: onShowCurtain?.Invoke(false); break;
            case State.Finish: onShowCurtain?.Invoke(true); break;
        }
    }
}
