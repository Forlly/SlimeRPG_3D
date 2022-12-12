using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : IUnit
{
    public static CharacterController Instance;

    public void Init(GameModel gameModel)
    {
        AttackDamage = 1;
        SpeedAttack = 1f;
        SpeedMoving = 1f;
        StartHealth = 5;
        CurrentHealth = 5;
    
        Instance = this;
    }

    public void Attack(List<IUnit> enemies)
    {
        foreach (IUnit unit in enemies)
        {
            if (Vector3.Distance(unit.UnitView.transform.position, this.UnitView.transform.position) < AttackDistance)
            {
                unit.ReceiveDamage(AttackDamage);
                break;
            }
        }
    }
}
