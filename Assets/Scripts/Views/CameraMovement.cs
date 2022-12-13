using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private IUnit _character;
    private Vector3 lastPos;
    private Vector3 distanceToMove;

    private bool active;
    public bool Active
    {
        set => active = value;
        get => active;
    }

    public bool SetCharacter(IUnit character)
    {
        _character = character;

        lastPos = _character.CurrentPosition;

        transform.position = new Vector3(lastPos.x + 5, transform.position.y, transform.position.z);
        SubscribeCharacterEvent(character);
        
        return character != null;
    }

    private void SubscribeCharacterEvent(IUnit character)
    {
        character.CameraMovementEvent += FollowCharacter;
    }
    
    public void FollowCharacter()
    {
        distanceToMove = _character.CurrentPosition - lastPos;
        transform.position = new Vector3(transform.position.x + distanceToMove.x, transform.position.y,
            transform.position.z);
        lastPos = _character.CurrentPosition;
    }
    
}
