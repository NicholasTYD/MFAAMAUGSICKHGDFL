using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovementAI : EnemyMovementAI
{
    protected override bool StopCriteraFufilled()
    {
        return enemyMovement.playerDistanceWithin(5);
    }
}