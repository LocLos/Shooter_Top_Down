using Zenject;
using UnityEngine;

public class BootstrapInstaller : MonoInstaller
{
    [Tooltip("0-startPont, 1-FinishPoint")]
    [SerializeField] Transform[] MovePoints;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Score _score;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] GameState _gameState;
    [SerializeField] LoadingLvl _loadingLvl;
    [SerializeField] UICharacteristics _uICharacteristics;

    public override void InstallBindings()
    {
        BindLoadingLvl();
        BindScore();
        BindEnemyFactory();
        BindSpawner();
        BindStateGame();
        BindMovePoints();
        BindUI();
        BindPlayer();
    }

    private void BindUI()
    {
        Container
            .Bind<UICharacteristics>()
            .FromInstance(_uICharacteristics)
            .AsSingle();
    }

    private void BindScore()
    {
        Container
            .Bind<Score>()
            .FromInstance(_score)
            .AsSingle();
    }

    private void BindMovePoints()
    {
        Container
            .Bind<Transform[]>()
            .FromInstance(MovePoints)
            .AsSingle();
    }

    private void BindSpawner()
    {
        Container
                .Bind<EnemySpawner>()
                .FromInstance(_enemySpawner)
                .AsSingle();
    }

    private void BindStateGame()
    {
        Container
                .Bind<GameState>()
                .FromInstance(_gameState)
                .AsSingle();
    }

    private void BindLoadingLvl()
    {
        Container
            .Bind<LoadingLvl>()
            .FromInstance(_loadingLvl)
            .AsSingle();
    }

    void BindEnemyFactory()
    {
        Container
            .Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();
    }


    void BindPlayer()
    {
        Player_Moving player = Container
              .InstantiatePrefabForComponent<Player_Moving>(playerPrefab);

        Container
              .Bind<Player_Moving>()
              .FromInstance(player)
              .AsTransient();
    }
}
 
 