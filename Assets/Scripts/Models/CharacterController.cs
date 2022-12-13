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

    private int _currentAttackDelay = 0;

    private int _softCurrency;

    public void Init(GameModel gameModel)
    {
        AttackDamage = 1;
        AttackDistance = 9f;
        SpeedAttack = 800;
        SpeedMoving = 0.1f;
        StartHealth = 7;
        CurrentHealth = 5;
        
        _softCurrency = 0;

        CurrentPosition = gameModel.SpawnPositionCharacter;

        Instance = this;
        SubscribeEvents();
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
                    if (unit.CurrentHealth <= 0)
                    {
                        _softCurrency += 10;
                        IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
                    }
                    Debug.Log(_softCurrency);
                    return true;
                }
            }
            
        }
        
        return false;
    }

    private void SubscribeEvents()
    {
        IncreaseAttackValueEvent += IncreaseAttackValue;
        IncreaseSpeedOfAttackEvent += IncreaseSpeedOfAttack;
        IncreaseHealthValueEvent += IncreaseHealthValue;
    }
    
    
    private void IncreaseAttackValue(int price)
    {
        if (_softCurrency >= price)
        {
            _softCurrency -= price;
            AttackDamage += 1;
            
            IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
            Debug.Log("IncreaseAttackValue");
        }
    }
    private void IncreaseSpeedOfAttack(int price)
    {
        if (_softCurrency >= price)
        {
            _softCurrency -= price;
            SpeedAttack += 100;
            
            IncreaseSoftCurrencyEvent?.Invoke(_softCurrency.ToString());
            Debug.Log("IncreaseSpeedOfAttack");
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
            Debug.Log("IncreaseHealthValue");
        }
    }
}
