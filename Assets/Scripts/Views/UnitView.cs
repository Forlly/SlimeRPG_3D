using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    private IUnit unit;

    [SerializeField] private Image _totalHealth;
    [SerializeField] private Image _currentHealth;

    public void Init(GameModel gameModel)
    {
        Unit = gameModel.Character;
    }


    public IUnit Unit
    {
        set
        {
            unit = value;
            unit.UnitView = this;
            unit.MoveEvent += Move;
            unit.UpdateHealthViewEvent += UpdateHealthView;
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
        
        float percentCurrentHp = 100f * currentHP / totalHP;
        
        _currentHealth.fillAmount = percentCurrentHp/100f;
        
    }
    
}
