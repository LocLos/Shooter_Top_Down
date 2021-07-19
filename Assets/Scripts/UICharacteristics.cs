using UnityEngine;
using UnityEngine.UI;

public class UICharacteristics : MonoBehaviour
{
    [SerializeField] Text ammo_txt;
    [SerializeField] Text health_txt;

    public void ChangeAmmo(int current, int max)
    {
        ammo_txt.text = current.ToString() + "/" + max.ToString();
    }
    public void ChangeHealth(int health)
    {
        health_txt.text = health.ToString();
    }
}
