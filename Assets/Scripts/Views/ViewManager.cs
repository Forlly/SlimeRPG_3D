using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance;
    public Transform SpawnPositionCharacter;
    
    [SerializeField] private GameObject _spawnCharacterUnitView;
    
    [SerializeField] private CameraMovement _camera;

    public Transform NextCharacterPoint;
    public Transform NextEnemiesPoint;

    [SerializeField] private UnitView _characterView;
    [SerializeField] public ObjectsPoolView _objectsPoolView;
    [SerializeField] public OptionsPanelView _optionsPanelView;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
        
    public void Init(GameModel gameModel)
    {
        gameModel.SpawnPositionCharacter = SpawnPositionCharacter.position;


        GameObject tmpUnitView =
            Instantiate(_spawnCharacterUnitView, SpawnPositionCharacter.position, Quaternion.identity);
        tmpUnitView.GetComponent<UnitView>().Init(gameModel);
        Debug.Log("ViewManager starting");
        _objectsPoolView.Init(gameModel);
        _optionsPanelView.Init(gameModel);

        _camera.Active = true;
        _camera.SetCharacter(tmpUnitView.GetComponent<UnitView>().Unit);
        Debug.Log(_camera.SetCharacter(tmpUnitView.GetComponent<UnitView>().Unit));
    }
}
