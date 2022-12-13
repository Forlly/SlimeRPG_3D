using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : IUnit
{
    public static CharacterController Instance;

    public void Init(GameModel gameModel)
    {
        AttackDamage = 1;
        AttackDistance = 5;
        SpeedAttack = 1f;
        SpeedMoving = 0.1f;
        StartHealth = 5;
        CurrentHealth = 5;

        CurrentPosition = gameModel.SpawnPositionCharacter;

        Instance = this;
    }


    public bool Attack(List<IUnit> enemies)
    {
        foreach (IUnit unit in enemies)
        {
            if (Vector3.Distance(unit.UnitView.transform.position, this.UnitView.transform.position) < AttackDistance)
            {
                unit.ReceiveDamage(AttackDamage);
                return true;
            }
        }
        return false;
    }
}
