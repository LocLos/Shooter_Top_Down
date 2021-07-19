using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    //   максимальное и текущее количество патронов
    int max = 20;
    int current = 20;

    public float reloadTime = 3;
    public bool canShot = true;

    UICharacteristics _uICharacteristics;
    AudioSource audioSource;

    public AudioClip reloadClip;
    public AudioClip shotClip;
    public AudioClip emptyClip;

    [Inject]
    private void Construct(UICharacteristics UICharacteristics)
    {
        _uICharacteristics = UICharacteristics;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Reload();
    }

    public void Shot()
    {
        if (current > 1)
        {
            audioSource.PlayOneShot(shotClip);
            current--;
        }
        else
        {
            audioSource.PlayOneShot(emptyClip);
            current = 0;
            canShot = false;
        }
        _uICharacteristics.ChangeAmmo(current, max);
    }

    void Reload()
    {
        if (canShot)
        {
            return;
        }

        reloadTime -= Time.deltaTime;

        if (reloadTime <= 0)
        {
            audioSource.PlayOneShot(reloadClip);
            reloadTime = 3;
            canShot = true;
            current = max;
        }
    }
}