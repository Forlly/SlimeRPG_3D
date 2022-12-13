using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameModel
{
    public static GameModel Instance;
    
    public int AmountPool;
    public int TickTime;
    public ObjectsPoolModel ObjectsPoolModel;
    
    public Vector3 SpawnPositionCharacter;
    public Vector3 NextCharacterPoint;
    
    public Vector3 SpawnPositionEnemies;
    public Vector3 NextEnemiesPoint;
    
    private bool _onSimulation;

    public CharacterController Character;
    public List<EnemyController> Enemies = new List<EnemyController>();
    public int TargetCountOfEnemiesOnScreen;

    private int _currentCharacterAttackDelay;
    private int _currentEnemyAttackDelay;

    public void Init()
    {
        Instance = this;
        
        AmountPool = 32;
        TickTime = 2;
        
        ObjectsPoolModel = new ObjectsPoolModel();
        ObjectsPoolModel.Init(this);

        Character = new CharacterController();
        Character.Init(this);

        TargetCountOfEnemiesOnScreen = 3;
        
        Debug.Log("GameModel starting");
        
    }

    public async void StartSimulation()
    {
        SpawnEnemies(TargetCountOfEnemiesOnScreen);
        await Tick(TickTime);
    }
    public async Task Tick(int msec)
    {
        _onSimulation = true;

        while (_onSimulation)
        {

            if (!Character.IsMoving)
            {
               
                if (!Character.Attack(Enemies, msec))
                {
                    GoToNextPoint();
                }
            }
            
            foreach (EnemyController enemy in Enemies)
            {
                enemy.TargetPosition = Character.UnitView.transform.position;
 
                if (!enemy.Move())
                {
                    enemy.Attack(Character, msec);
                }
            }


            await Task.Delay(msec);
        }
            
        EndModel();
    }

    public void SpawnEnemies(int countOfEnemies)
    {
        for (int i = 0; i < countOfEnemies; i++)
        {
            EnemyController unit = ObjectsPoolModel.GetPooledObject();

            unit.CurrentPosition = new Vector3(SpawnPositionEnemies.x + i*2, SpawnPositionEnemies.y,
                SpawnPositionEnemies.z + i*2);
            unit.UnitView.transform.position = new Vector3(SpawnPositionEnemies.x + i*2, SpawnPositionEnemies.y,
                SpawnPositionEnemies.z+ i*2);

            unit.TargetPosition = Character.CurrentPosition;
            unit.Move();
            Enemies.Add(unit);
        }
    }

    public void DleteEnemyFromList(EnemyController enemy)
    {
        Enemies.Remove(enemy);
    }
    
    public void GoToNextPoint()
    {
        Character.TargetPosition = NextCharacterPoint;
    }
    
    public void EndModel()
    {
        _onSimulation = false;
    }
}
