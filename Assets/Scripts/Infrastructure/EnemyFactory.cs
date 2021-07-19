using UnityEngine;
using Zenject;

public partial class EnemyFactory : IEnemyFactory
{
    private const string Zomby = "EnemyZomby";
    readonly DiContainer _diContainer;
    Object _ZombyPrefab;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Load()
    {
        _ZombyPrefab = Resources.Load(Zomby);
    }

    public void Create(Vector2 pos)
    {
        _diContainer.InstantiatePrefab(_ZombyPrefab, pos, Quaternion.identity, null);
    }
}
 
 