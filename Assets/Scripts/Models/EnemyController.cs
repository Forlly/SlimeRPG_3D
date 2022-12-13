using UnityEngine;

public class EnemyController : IUnit
{
    public override bool Move()
    {
        if (UnitView != null)
        {
            CurrentPosition = Vector3.MoveTowards(CurrentPosition, TargetPosition, SpeedMoving);

            if (Vector3.Distance(CurrentPosition,TargetPosition) < AttackDistance)
            {
                IsMoving = false;
                return false;
            }

            MoveEvent?.Invoke(CurrentPosition);
        }
        
        return true;
    }
}
