using UnityEngine;

[CreateAssetMenu(menuName = "Battle/BattleConfig")]
public class BattleConfig : ScriptableObject
{
    public EnemySpawnInfo[] enemiesToSpawn;
    public int intentos;
    public float timeLimit;
}