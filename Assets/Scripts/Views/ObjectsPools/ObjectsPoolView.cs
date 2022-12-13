using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolView : MonoBehaviour
{
    public static ObjectsPoolView Instance;

    private List<UnitView> _poolObjects = new List<UnitView>();
    [SerializeField] private int _amountPool = 32;
    [SerializeField] private GameObject _spawnObjct;
    

    public void Init(GameModel gameModel)
    {
        Instance = this;
        _amountPool = gameModel.AmountPool;

        for (int i = 0; i < _amountPool; i++)
        {
            GameObject tmpObj = Instantiate(_spawnObjct);
            tmpObj.SetActive(false);
            _poolObjects.Add(tmpObj.GetComponent<UnitView>());
        }

        SubscribePoolEvents(gameModel);
    }

    private void SubscribePoolEvents(GameModel gameModel)
    {
        gameModel.ObjectsPoolModel.getPooledObjectEvent += GetPooledObject;
        gameModel.ObjectsPoolModel.turnOfObjectEvent += TurnOfObject;
        gameModel.ObjectsPoolModel.createNewObjectEvent += CreateNewObject;
    }
    
    private void UnsubscribePoolEvents(GameModel gameModel)
    {
        gameModel.ObjectsPoolModel.getPooledObjectEvent -= GetPooledObject;
        gameModel.ObjectsPoolModel.turnOfObjectEvent -= TurnOfObject;
        gameModel.ObjectsPoolModel.createNewObjectEvent -= CreateNewObject;
    }


    public UnitView GetPooledObject(IUnit unit)
    {
        for (int i = 0; i < _amountPool; i++)
        {
            if (!_poolObjects[i].gameObject.activeInHierarchy)
            {
                _poolObjects[i].gameObject.SetActive(true);
                _poolObjects[i].Unit = unit;

                _poolObjects[i].Unit.CurrentHealth = _poolObjects[i].Unit.StartHealth;
                _poolObjects[i].UpdateHealthView(_poolObjects[i].Unit.CurrentHealth, _poolObjects[i].Unit.StartHealth);
                return _poolObjects[i];
            }
            
        }
        return null;
    }

    public void TurnOfObject(IUnit unit)
    {
       
        for (int i = 0; i < _amountPool; i++)
        {
            if (unit == _poolObjects[i].Unit)
            {
                _poolObjects[i].gameObject.SetActive(false);
            }

        }
    }

    private UnitView CreateNewObject(IUnit unit)
    {
        GameObject tmpObj = Instantiate(_spawnObjct);
        tmpObj.gameObject.SetActive(true);
        
        _amountPool++;
        
        tmpObj.GetComponent<UnitView>().Unit = unit;
        _poolObjects.Add(tmpObj.GetComponent<UnitView>());
        
        return tmpObj.GetComponent<UnitView>();
    }
}
