using System;
using UnityEngine;

public abstract class IUnit
{
    public float SpeedMoving;
    public float SpeedAttack;
    public int AttackDamage;
    public float AttackDistance;

    public int StartHealth;
    public int CurrentHealth;
    
    public Vector3 TargetPosition;
    public Vector3 CurrentPosition;
    
    public UnitView UnitView;
    
    public Action<Vector3> MoveEvent;
    public Action<int, int> UpdateHealthViewEvent;

    public bool IsMoving = false;
    
    public virtual bool Move()
    {
        if (UnitView != null)
        {
            CurrentPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, SpeedMoving);

            Debug.Log("POSITION");
            Debug.Log(CurrentPosition);
            Debug.Log(TargetPosition);
            MoveEvent?.Invoke(CurrentPosition);
            
            if (Vector3.Distance(CurrentPosition,TargetPosition) < 0.01f)
            {
                CurrentPosition = TargetPosition;
                IsMoving = false;
                return false;
            }
        }
        
        return true;
    }

    public virtual void Attack()
    {
        
    }

    public virtual void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("RECEIVE DAMAGE");
        UpdateHealthViewEvent?.Invoke(CurrentHealth, StartHealth);
    }
}
