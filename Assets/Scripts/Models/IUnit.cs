using System;
using UnityEngine;

public abstract class IUnit
{
    public float SpeedMoving;
    public int AttackDelay;
    public float SpeedAttack;
    public int AttackDamage;
    public float AttackDistance;
    
    public Action CameraMovementEvent;
    
    public Action<String> ReceiveDamageEvent;

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
        SpeedAttack = 2;
        AttackDistance = 3;
        AttackDelay = 1000;
        SpeedMoving = 0.0015f;
        StartHealth = 3;
        CurrentHealth = 3;
        
    }
    
    public virtual bool Move(int msec)
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
        ReceiveDamageEvent?.Invoke(damage.ToString());
        UpdateHealthViewEvent?.Invoke(CurrentHealth, StartHealth);
    }
}
