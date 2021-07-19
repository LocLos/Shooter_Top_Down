using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] GameObject sprite;

    void Update()
    {
        Vector3 direction = Input.mousePosition;
        direction = Camera.main.ScreenToWorldPoint(direction) - sprite.transform.position;
        sprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}
