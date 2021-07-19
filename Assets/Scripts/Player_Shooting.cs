using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    public Transform bulletPoint;
    public Bullet bullet;

    public float reloadTime;
    float shotTime = Mathf.Infinity;
    bool canShot = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShot)
        {
            Shot();
        }
        else if (!canShot) CanShot();
    }
    void Shot()
    {
        Weapon weapon = gameObject.GetComponentInChildren<Weapon>();

        if (weapon.canShot)
        {
            weapon.Shot();
            canShot = false;
            Bullet bul = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        }
    }

    void CanShot()
    {
        if (canShot)
        {
            return;
        }
        shotTime += Time.deltaTime;
        if (shotTime > reloadTime)
        {
            shotTime = 0;
            canShot = true;
        }
    }
}
