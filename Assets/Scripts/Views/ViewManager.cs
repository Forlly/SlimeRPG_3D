using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;
        
    [SerializeField] private UnitView _characterView;
    [SerializeField] public ObjectsPoolView _objectsPoolView;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
        
    public void Init(GameModel gameModel)
    {
        _characterView.Unit = gameModel.Character;
        _objectsPoolView.Init(gameModel);
    }
}
