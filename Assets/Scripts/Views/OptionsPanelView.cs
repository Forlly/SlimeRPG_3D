using System;
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
    }
    private void IncreaseSpeedOfAttack()
    {
        if (int.TryParse(_priceIncreaseSpeedOfAttack.text, out _price))
            _character.IncreaseSpeedOfAttackEvent?.Invoke(_price);
    }
    private void IncreaseHealthValue()
    {
        if (int.TryParse(_priceIncreaseHealthValue.text, out _price))
            _character.IncreaseHealthValueEvent?.Invoke(_price);
    }
    
    private void IncreaseSoftCurrencyView(string softCurrency)
    {
        _countOfSoftCurrencyText.text = softCurrency;
    }
}
