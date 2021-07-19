using UnityEngine;
using Zenject;

public class LoadingLvl : MonoBehaviour
{
    GameObject currentMap;
    [SerializeField] GameObject[] maps;
    public int level = 0;
    GameState _gameState;
    public int enemyCount => level * 2;

    public event OnSendState onSendState;
    public delegate void OnSendState(State state);

    public event OnCreateEnemy onCreateEnemy;
    public delegate void OnCreateEnemy(int numOfEnemy);

    [Inject]
    private void Construct(GameState GameState)
    {
        _gameState = GameState;
    }
    private void Start()
    {
        _gameState.onLoadNextLvl += StartLevel;
    }

    public void StartLevel()
    {
        level++;
        if (currentMap != null) Destroy(currentMap);
        if (level <= maps.Length)
        {
            onSendState?.Invoke(State.Start);
            currentMap = Instantiate(maps[level - 1]) as GameObject;
            onCreateEnemy?.Invoke(enemyCount);
        }
        else
        {
            onSendState?.Invoke(State.Win);
        }
    }
}
