using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hp = 20;

    public int shield;

    public void Heal(int amount)
    {
        hp += amount;

        Debug.Log("Heal +" + amount);
    }

    public void AddShield(int amount)
    {
        shield += amount;

        Debug.Log("Shield +" + amount);
    }

    public void TakeDamage(int dmg)
    {
        if (shield > 0)
        {
            shield -= dmg;

            if (shield < 0)
            {
                hp += shield;
                shield = 0;
            }
        }
        else
        {
            hp -= dmg;
        }

        Debug.Log("HP: " + hp);
    }
}