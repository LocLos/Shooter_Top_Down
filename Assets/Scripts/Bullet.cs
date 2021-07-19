using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("bulletDestroy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyMoving enemy))
        {
            if (enemy.TryGetComponent(out HPObject hp))
            {
                hp.ChangeHP(damage);
            }
            Destroy(gameObject);
        }
    }
}
