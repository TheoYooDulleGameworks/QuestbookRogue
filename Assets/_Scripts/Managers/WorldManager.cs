using UnityEngine;

public class WorldManager : Singleton<WorldManager>
{
    // World Assets //
    [SerializeField] public EnemyStatusSO enemyStatus;

    protected override void Awake()
    {
        base.Awake();

        ResetEnemyStatus();
    }

    private void ResetEnemyStatus()
    {
        enemyStatus.Lv.Value = 1;

        enemyStatus.maxHealth.Value = 0;
        enemyStatus.currentHealth.Value = 0;
        
        enemyStatus.maxArmor.Value = 0;
        enemyStatus.currentArmor.Value = 0;

        enemyStatus.maxDamage.Value = 0;
        enemyStatus.currentDamage.Value = 0;
    }
}
