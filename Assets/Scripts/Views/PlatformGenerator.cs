using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public static PlatformGenerator Instance;
    [SerializeField] private GameObject _platform;

    [SerializeField] private float _platformSize = 20;
    [SerializeField] private GameObject _previousPosition;

    [SerializeField] private GameObject _currentPlatform;
    

    private void Awake()
    {
        Instance = this;
        _platformSize = _platform.GetComponent<Platform>().SizePlatform.localScale.x;
    }

    public void GenerateNewPlatform()
    {
        _previousPosition = _currentPlatform;
        Vector3 position = _previousPosition.transform.position;

        position = new Vector3(position.x + _platformSize, position.y, position.z);
        
        GameObject prevPlatform = Instantiate(_platform, position, Quaternion.identity);
        _currentPlatform = prevPlatform;
    }

    public Vector3 GetNextCharacterPosition()
    {
        return _currentPlatform.GetComponent<Platform>().NextCharacterPoint.position;
    }
    public Vector3 GetNextEnemyPosition()
    {
        return _currentPlatform.GetComponent<Platform>().NextEnemyPoint.position;
    }
    
    public void DeletePreviousPlatform()
    {
        Debug.Log(_currentPlatform);
        if (_currentPlatform == null) return;
        
        Destroy(_previousPosition.gameObject);
        Debug.Log(_previousPosition.gameObject);
    }
}
