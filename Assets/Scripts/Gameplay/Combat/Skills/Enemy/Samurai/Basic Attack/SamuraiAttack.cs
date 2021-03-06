using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttack : EnemySkill
{
    [SerializeField] SamuraiBossMovementAI samuraiBossMovementAI;
    [SerializeField] float dashHealthPercentageThreshold;
    [SerializeField] float dashForce;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        if (enemyHealth.isHealthPercentageEqualOrBelow(dashHealthPercentageThreshold))
        {
            samuraiBossMovementAI.initiateDashes(enemy, player, dashForce, attackTimings);
        }
    }
}
