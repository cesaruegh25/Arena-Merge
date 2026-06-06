using UnityEngine;
public enum EnemyType
{
    Basic,
    Elite,
    Boss
}

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType;
    public Sprite sprite;

    //animacion ver como ponerla

    public int vida = 100;
    public float velocidad = 0.01f;
    public int damage = 10;
    public int KnockBackForce = 10;
    public int scoreValue = 100;
}