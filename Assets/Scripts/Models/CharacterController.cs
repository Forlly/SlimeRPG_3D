using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : IUnit
{
    public static CharacterController Instance;
    
    public Action<string> IncreaseSoftCurrencyEvent;
    public Action<int> IncreaseAttackValueEvent;
    public Action<int> IncreaseSpeedOfAttackEvent;
    public Action<int> IncreaseHealthValueEvent;
    public Action<IUnit> MakeDamageEvent;

    private int _currentAttackDelay = 0;
    private float _maxAttackSpeed;

    private int _softCurrency;

    public void Init(GameModel gameModel)
    {
        AttackDamage = 1;
        AttackDistance = 20f;
        AttackDelay = 1500;
        SpeedMoving = 0.008f;
        SpeedAttack = 11f;
        _maxAttackSpeed = 25f;
        StartHealth = 7;
        CurrentHealth = 7;
        _currentAttackDelay = 0;
        
        _softCurrency = 0;

        IsMoving = false;
        
        CurrentPosition = gameModel.SpawnPositionCharacter;

        Instance = this;
        SubscribeEvents();
    }

    public override bool Move(int msec)
    {
        if (UnitView != null)
        {
            CurrentPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, SpeedMoving * msec);
            
            MoveEvent?.Invoke(CurrentPosition);
            CameraMovementEvent?.Invoke();
            
            if (Vector3.Distance(CurrentPosition,TargetPosition) < 0.01f)
            {
                CurrentPosition = TargetPosition;
                IsMoving = false;
                return false;
            }
        }
        
        return true;
    }

    public bool Attack(List<EnemyController> enemies, int msec)
    {
        _currentAttackDelay += msec;
        Debug.Log(_currentAttackDelay);

        if (_currentAttackDelay >= AttackDelay)
        {
            _currentAttackDelay -= AttackDelay;

            foreach (IUnit unit in enemies)
            {
                if (Vector3.Distance(unit.UnitView.transform.position, this.UnitView.transform.position) < AttackDistance)
                {
                    Debug.Log("SHOT");
                    WeaponController.Instance.Fire(unit);
                    return true;
                }
            }
            
        }
        
        return false;
    }

    private void MakeDamage(IUnit unit)
    {
        unit.ReceiveDamage(AttackDamage);
        if (unit.CurrentHealth <= 0)
        {
            _softCurrency += 10;
            IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
        }

    }

    private void SubscribeEvents()
    {
        IncreaseAttackValueEvent += IncreaseAttackValue;
        IncreaseSpeedOfAttackEvent += IncreaseSpeedOfAttack;
        IncreaseHealthValueEvent += IncreaseHealthValue;
        MakeDamageEvent += MakeDamage;
    }
    
    
    private void IncreaseAttackValue(int price)
    {
        if (_softCurrency >= price)
        {
            _softCurrency -= price;
            AttackDamage += 1;
            
            IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
        }
    }
    private void IncreaseSpeedOfAttack(int price)
    {
        if (_softCurrency >= price)
        {
            _softCurrency -= price;
            if (SpeedAttack < _maxAttackSpeed)
            {
                SpeedAttack += 1f;
            }

            IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
        }
    }
    private void IncreaseHealthValue(int price)
    {
        if (_softCurrency >= price)
        {
            _softCurrency -= price;
            
            CurrentHealth += 1;
            StartHealth += 1;
            UpdateHealthViewEvent?.Invoke(CurrentHealth, StartHealth);
            
            IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
        }
    }
    
}
