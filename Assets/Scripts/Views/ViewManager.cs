using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;
    public Transform SpawnPositionCharacter;
    
    [SerializeField] private GameObject _spawnCharacterUnitView;

    public Transform NextCharacterPoint;

    [SerializeField] private UnitView _characterView;
    [SerializeField] public ObjectsPoolView _objectsPoolView;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
        
    public void Init(GameModel gameModel)
    {
        gameModel.SpawnPositionCharacter = SpawnPositionCharacter.position;
        gameModel.NextCharacterPoint = NextCharacterPoint.position;
        
        
        GameObject tmpUnitView =
            Instantiate(_spawnCharacterUnitView, SpawnPositionCharacter.position, Quaternion.identity);
        tmpUnitView.GetComponent<UnitView>().Init(gameModel);
        Debug.Log("ViewManager starting");
        _objectsPoolView.Init(gameModel);
    }
}
