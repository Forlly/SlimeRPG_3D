using System;
using UnityEngine;

public abstract class IUnit
{
    public float SpeedMoving;
    public int SpeedAttack;
    public int AttackDamage;
    public float AttackDistance;
    
    public Action CameraMovementEvent;

    public int StartHealth;
    public int CurrentHealth;
    
    public Vector3 TargetPosition;
    public Vector3 CurrentPosition;
    
    public UnitView UnitView;
    
    public Action<Vector3> MoveEvent;
    public Action<int, int> UpdateHealthViewEvent;

    public bool IsMoving = false;

    public virtual void Init()
    {
        AttackDamage = 1;
        AttackDistance = 3;
        SpeedAttack = 800;
        SpeedMoving = 0.004f;
        StartHealth = 3;
        CurrentHealth = 3;
        
    }
    
    public virtual bool Move()
    {
        if (UnitView != null)
        {
            CurrentPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, SpeedMoving);
            
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
        Debug.Log(CurrentHealth);
        UpdateHealthViewEvent?.Invoke(CurrentHealth, StartHealth);
    }
}
