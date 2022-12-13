using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    private IUnit unit;

    [SerializeField] private Image _totalHealth;
    [SerializeField] private Image _currentHealth;
    
    [SerializeField] private Text _receivedDamage;

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
            unit.ReceiveDamageEvent += ShowReceivedDamage;
        }
        get => unit;
    }


    public void Move(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
    
    public async void ShowReceivedDamage(string receivedDamage)
    {
        _receivedDamage.text = receivedDamage;
        _receivedDamage.gameObject.SetActive(true);
        await Task.Delay(300);
        _receivedDamage.gameObject.SetActive(false);
    }


    public void UpdateHealthView(int currentHP, int totalHP)
    {

        _totalHealth.enabled = true;
        _currentHealth.enabled = true;
        
        float percentCurrentHp = 100f * currentHP / totalHP;
        
        _currentHealth.fillAmount = percentCurrentHp/100f;
        
    }
    
}
