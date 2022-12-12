using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    private IUnit unit;
    public Action<Vector3> MoveEvent;
    public Action<int, int> UpdateHealthViewEvent;

    [SerializeField] private Image _totalHealth;
    [SerializeField] private Image _currentHealth;
    
    public IUnit Unit
    {
        set
        {
            unit = value;
            unit.UnitView = this;
            MoveEvent = Move;
            UpdateHealthViewEvent = UpdateHealthView;
        }
        get => unit;
    }
    

    public void Move(Vector3 targetPosition)
    {
        transform.position = targetPosition;
        
    }
    

    public void UpdateHealthView(int currentHP, int totalHP)
    {
        _totalHealth.enabled = true;
        _currentHealth.enabled = true;
        
        if (currentHP <= 0)
        {
            _totalHealth.enabled = false;
            _currentHealth.enabled = false;
        }
        float percentCurrentHp = 100f * currentHP / totalHP;
        
        _currentHealth.fillAmount = percentCurrentHp/100f;
        
    }
}
