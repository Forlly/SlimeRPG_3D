using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameModel 
{
    public int AmountPool;
    public int TickTime;
    public ObjectsPoolModel ObjectsPoolModel;
    
    private bool _onSimulation;

    public CharacterController Character;
    public List<IUnit> Enemies = new List<IUnit>();

    public async void Init()
    {
        AmountPool = 128;
        TickTime = 2;
        
        ObjectsPoolModel = new ObjectsPoolModel();
        //ObjectsPoolModel.Init(this);

        Character = new CharacterController();
        Character.Init(this);
        
        await Tick(TickTime);
    }
    
    public async Task Tick(int msec)
    {
        _onSimulation = true;

        while (_onSimulation)
        {

            if (!Character.Move())
            {
                Character.Attack(Enemies);
            }
            

            await Task.Delay(msec);
        }
            
        EndModel();
    }
    
    public void EndModel()
    {
        _onSimulation = false;
    }
}
