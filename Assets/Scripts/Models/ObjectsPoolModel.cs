using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPoolModel
{
    public static ObjectsPoolModel Instance;
    public delegate UnitView GetPooledObjectEvent(EnemyController cubeController);
    public GetPooledObjectEvent getPooledObjectEvent;
    
    public delegate void TurnOfObjectEvent(EnemyController unit);
    public TurnOfObjectEvent turnOfObjectEvent;
    
    public delegate UnitView CreateNewObjectEvent(EnemyController unit);
    public CreateNewObjectEvent createNewObjectEvent;

    private List<PoolList> _poolObjects = new List<PoolList>();
    private int _amountPool = 32;

    private bool _isFull = false;

    public void Init(GameModel gameModel)
    {
        Instance = this;
        _amountPool = gameModel.AmountPool;

        for (int i = 0; i < _amountPool; i++)
        {
            PoolList tmpUnit = new PoolList();
            tmpUnit._unit.SpeedMoving = 0.05f;
            tmpUnit._unit.AttackDistance =2f;
            _poolObjects.Add(tmpUnit);
        }
    }

    public EnemyController GetPooledObject()
    {
        foreach (PoolList unit in _poolObjects)
        {
            if (unit._isFree)
            {
                unit._isFree = false;

                getPooledObjectEvent?.Invoke(unit._unit);
                return unit._unit;
            }
            _isFull = true;
        }

        if (_isFull)
        {
            EnemyController newUnit = CreateNewObject();
            createNewObjectEvent?.Invoke(newUnit);
            return newUnit;
        }

        return null;
    }

    public void TurnOfObject(EnemyController _unit)
    {
        foreach (PoolList unit in _poolObjects)
        {
            if (unit._unit == _unit)
            {
                turnOfObjectEvent?.Invoke(unit._unit);
                unit._isFree = true;
            }
        }
    }

    private EnemyController CreateNewObject()
    {

        PoolList tmpUnit = new PoolList();
        _amountPool++;
        _poolObjects.Add(tmpUnit);
        return tmpUnit._unit;
    }
}

public class PoolList
{
    public bool _isFree = true;
    public EnemyController _unit = new EnemyController();
}

