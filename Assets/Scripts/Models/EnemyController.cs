using UnityEngine;

public class EnemyController : IUnit
{
    private int _currentAttackDelay = 0;
    public override bool Move()
    {
        if (UnitView != null)
        {
            CurrentPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, SpeedMoving);

            if (Vector3.Distance(CurrentPosition,TargetPosition) < AttackDistance - 0.5f)
            {
                IsMoving = false;
                return false;
            }

            MoveEvent?.Invoke(CurrentPosition);
        }
        
        return true;
    }
    
    public override void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;
        UpdateHealthViewEvent?.Invoke(CurrentHealth, StartHealth);

        if (CurrentHealth <= 0)
        {
            GameModel.Instance.DleteEnemyFromList(this);
            ObjectsPoolModel.Instance.TurnOfObject(this);
        }
        
    }
    
    public bool Attack(IUnit _unit, int msec)
    {
        _currentAttackDelay += msec;
        
        Debug.Log(_unit);
        Debug.Log(_currentAttackDelay);

        if (_currentAttackDelay > SpeedAttack)
        {
            _currentAttackDelay -= SpeedAttack;

            Debug.Log("DISTANCE 5");
            Debug.Log(Vector3.Distance(_unit.UnitView.transform.position, this.UnitView.transform.position));
            
            if (Vector3.Distance(_unit.UnitView.transform.position, this.UnitView.transform.position) < AttackDistance)
            {
                Debug.Log("CHARACTER RECEIVE DAMAGE 5");
                _unit.ReceiveDamage(AttackDamage);
                return true;
            }

        }
        
        return false;
    }
}
