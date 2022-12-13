using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelView : MonoBehaviour
{
    [SerializeField] private Button _increaseAttackValue;
    [SerializeField] private Button _increaseSpeedOfAttack;
    [SerializeField] private Button _increaseHealthValue;
    
    [SerializeField] private Text _priceIncreaseAttackValue;
    [SerializeField] private Text _priceIncreaseSpeedOfAttack;
    [SerializeField] private Text _priceIncreaseHealthValue;
    
    [SerializeField] private TMP_Text _attackValueText;
    [SerializeField] private TMP_Text _speedOfAttackText;
    [SerializeField] private TMP_Text _healthValueText;
    
    [SerializeField] private Text _countOfSoftCurrencyText;
    private int _price;

    private CharacterController _character;

    public void Init(GameModel gameModel)
    {
        _increaseAttackValue.onClick.AddListener(IncreaseAttackValue);
        _increaseSpeedOfAttack.onClick.AddListener(IncreaseSpeedOfAttack);
        _increaseHealthValue.onClick.AddListener(IncreaseHealthValue);

        _character = gameModel.Character;
        _character.IncreaseSoftCurrencyEvent += IncreaseSoftCurrencyView;
    }


    private void IncreaseAttackValue()
    {
        if(int.TryParse(_priceIncreaseAttackValue.text, out _price))
            _character.IncreaseAttackValueEvent?.Invoke(_price);

        _attackValueText.text = _character.AttackDamage.ToString();
    }
    private void IncreaseSpeedOfAttack()
    {
        if (int.TryParse(_priceIncreaseSpeedOfAttack.text, out _price))
            _character.IncreaseSpeedOfAttackEvent?.Invoke(_price);

        _speedOfAttackText.text = _character.SpeedAttack.ToString();
    }
    private void IncreaseHealthValue()
    {
        if (int.TryParse(_priceIncreaseHealthValue.text, out _price))
            _character.IncreaseHealthValueEvent?.Invoke(_price);
        _healthValueText.text = _character.StartHealth.ToString();
    }
    
    private void IncreaseSoftCurrencyView(string softCurrency)
    {
        _countOfSoftCurrencyText.text = softCurrency;
    }
}
