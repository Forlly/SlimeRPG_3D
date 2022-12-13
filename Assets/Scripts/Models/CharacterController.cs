using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : IUnit
{
    public static CharacterController Instance;

    private int _currentAttackDelay = 0;

    public void Init(GameModel gameModel)
    {
        AttackDamage = 1;
        AttackDistance = 9f;
        SpeedAttack = 800;
        SpeedMoving = 0.1f;
        StartHealth = 7;
        CurrentHealth = 5;

        CurrentPosition = gameModel.SpawnPositionCharacter;

        Instance = this;
    }


    public bool Attack(List<EnemyController> enemies, int msec)
    {
        _currentAttackDelay += msec;

        if (_currentAttackDelay > SpeedAttack)
        {
            _currentAttackDelay -= SpeedAttack;

            foreach (IUnit unit in enemies)
            {
                if (Vector3.Distance(unit.UnitView.transform.position, this.UnitView.transform.position) < AttackDistance)
                {
                    unit.ReceiveDamage(AttackDamage);
                    return true;
                }
            }
            
        }
        
        return false;
    }
}
