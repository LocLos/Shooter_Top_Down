using UnityEngine;
using Zenject;

public class EnemyMoving : MonoBehaviour
{
    public GameObject target;
    public float speed = 2;
    public float distance = 1;
    IAttack enemyAttack;

    Animator animator;
    AudioSource audioSource;
    LoadingLvl _loadingLvl;
    public AudioClip mainClip;

    [Inject]
    private void Construct(Player_Moving playerMoving, LoadingLvl LoadingLvl)
    {
        target = playerMoving.gameObject;
        _loadingLvl = LoadingLvl;
    }

    private void Start()
    {
        speed = _loadingLvl.level;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        InvokeRepeating(nameof(PlaySound), 0, 5f);

        if (TryGetComponent(out IAttack IAttack))
        {
            enemyAttack = IAttack;

        }
        else
            Debug.Log("IAttack was not found");
    }

    void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > distance)
            {
                animator.SetBool("isAttack", false);
                Moving();
            }
            else
            {
                animator.SetBool("isAttack", true);
            }
        }
        else
        {
            Win();
        }
    }
    void Moving()
    {
        Vector2 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    void Win()
    {
        animator.SetTrigger("Win");
    }

    void PlaySound()
    {
        audioSource.PlayOneShot(mainClip);
    }
}
