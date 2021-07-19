using UnityEngine;
using Zenject;

public class Score : MonoBehaviour, IScore
{
    public event OnSendState onSendState;
    public delegate void OnSendState(State state);

    public int score = 0;
    LoadingLvl _loadingLvl;

    [Inject]
    private void Construct(LoadingLvl LoadingLvl)
    {
        _loadingLvl = LoadingLvl;
    }
    public void ChangeScore()
    {
        score++;
        if (score >= _loadingLvl.enemyCount)
        {
            score = 0;
            onSendState?.Invoke(State.Finish);
        }
    }
}
