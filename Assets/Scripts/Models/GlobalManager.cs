using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private ViewManager _viewManager;
    private GameModel _gameModel;
    
    public Transform SpawnPositionCharacter;
    
    public Transform SpawnPositionEnemies;
    private void Awake()
    {
        _gameModel = new GameModel();
        _gameModel.SpawnPositionCharacter = SpawnPositionCharacter.position;
        _gameModel.SpawnPositionEnemies = SpawnPositionEnemies.position;
        _gameModel.Init();
        _viewManager.Init(_gameModel);
        
        _gameModel.StartSimulation();
    }

    private void OnDisable()
    {
        _gameModel.EndModel();
    }
}
