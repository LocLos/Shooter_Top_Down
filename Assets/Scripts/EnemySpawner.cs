using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class EnemySpawner : MonoBehaviour
{
    IEnemyFactory _IEnemyFactory;
    LoadingLvl _loadingLvl;

    [Inject]
    void Constructor(IEnemyFactory Factory, LoadingLvl LoadingLvl)
    {
        _IEnemyFactory = Factory;
        _loadingLvl = LoadingLvl;
    }

    private void Start()
    {
        _loadingLvl.onCreateEnemy += StartCorutineSpawner;
    }

    public void StartCorutineSpawner(int enemyCount)
    {
        StartCoroutine(Spawner(enemyCount));
    }

    IEnumerator Spawner(int enemyCount)
    {
        yield return new WaitForSeconds(2f);
        _IEnemyFactory.Load();
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(2f);

            float X = Random.Range(0, 2) < 0.5f ? -10 : 10;
            float Y = Random.Range(-3, 3);
            Vector2 enemyPos = new Vector2(X, Y);

            _IEnemyFactory.Create(enemyPos);
        }
    }
}
