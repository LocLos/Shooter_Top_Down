using UnityEngine;
using Zenject;

public class ZombyAttack : MonoBehaviour, IAttack
{
    AudioSource audioSource;
    GameObject target;

    [SerializeField] AudioClip attackClip;
    int damage;

    [Inject]
    private void Construct(Player_Moving playerMoving, LoadingLvl LoadingLvl)
    {
        target = playerMoving.gameObject;
        damage = LoadingLvl.level * 2;
    }

    private void Start()
    {
        target = GetComponent<EnemyMoving>().target;
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack() // запускается по анимации 
    {
        audioSource.PlayOneShot(attackClip);
        if (target.TryGetComponent(out HPObject hp))
        {
            hp.ChangeHP(damage);
        }
    }
}
