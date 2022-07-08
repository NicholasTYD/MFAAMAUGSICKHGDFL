using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int CurrentWave;

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float Attack { get; set; }
    public float AttackSpeed { get; set; }
    public float ParryDamageBonusDuration { get; set; }
    public float ParryDamageBonusMultiplier { get; set; }
    public float MovementSpeed { get; set; }

    /*
    public SaveData(int currentWave,
        float maxHealth,
        float currentHealth,
        float attack, 
        float attackSpeed,
        float parryDamageBonusDuration,
        float parryDamageBonusMultiplier,
        float movementSpeed)
    {
        this.CurrentWave = currentWave;
        this.MaxHealth = maxHealth;
        this.CurrentHealth = currentHealth;
        this.Attack = attack;
        this.AttackSpeed = attackSpeed;
        this.ParryDamageBonusDuration = parryDamageBonusDuration;
        this.ParryDamageBonusMultiplier = parryDamageBonusMultiplier;
        this.MovementSpeed = movementSpeed;
    }
    */
}
