using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image[] healthImages;

    public void TakeDamage(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            if (healthImages.Length > 0)
            {
                healthImages[healthImages.Length - 1].enabled = false;
                System.Array.Resize(ref healthImages, healthImages.Length - 1);
            }
        }
    }

    public int getHealthLenght()
    {
        return healthImages.Length;
    }
}
