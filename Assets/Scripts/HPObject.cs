using UnityEngine;
using Zenject;

public class HPObject : MonoBehaviour
{
    [SerializeField] int HP = 100;
    [SerializeField] GameObject bloodSprite;

    Score _score;
    UICharacteristics _uICharacteristics;

    [Inject]
    private void Construct(Score Score, UICharacteristics UICharacteristics)
    {
        _score = Score;
        _uICharacteristics = UICharacteristics;
    }
    public void ChangeHP(int value)
    {
        HP -= value;
        if (TryGetComponent(out Player_Moving player))
            _uICharacteristics.ChangeHealth(HP);
        if (HP <= 0)
        {
            if (TryGetComponent(out EnemyMoving enemy))
                _score.ChangeScore();
            Instantiate(bloodSprite, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
