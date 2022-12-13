using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController Instance;
    
    public GameObject bullet;
    public Transform spawnBulletPos;
    public float LifeTime;

    private void Awake()
    {
        Instance = this;
    }

    public void Fire(IUnit unit)
    {
        StartCoroutine(Shot(unit));
    }

    private IEnumerator Shot(IUnit unit)
    {
        Vector3 startPoint = spawnBulletPos.position;

        GameObject _bullet = BulletPoolView.Instance.GetPooledObject();
        if (_bullet != null)
        {
            _bullet.transform.position = startPoint;
        }

        Debug.Log("SPEED");
        Debug.Log(CharacterController.Instance.SpeedAttack);
        
        float wspeed = (CharacterController.Instance.SpeedAttack * Time.deltaTime) /
                       Vector3.Distance(startPoint, unit.UnitView.transform.position);

        LifeTime = Vector3.Distance(startPoint, unit.UnitView.transform.position) /
                   (CharacterController.Instance.SpeedAttack *  Time.deltaTime);
        float progressFly = 0f;

        float currentTime = 0f;
        while (true)
        {
            yield return null;
            currentTime += Time.fixedDeltaTime;

            progressFly += wspeed;
            Debug.Log("BULLET");
            Debug.Log(progressFly);
            Debug.Log(_bullet.transform.position);
            Debug.Log(unit.UnitView.transform.position);
            _bullet.transform.position = Vector3.Lerp(startPoint, unit.UnitView.transform.position, progressFly);

            Debug.Log("TIME");
            Debug.Log(currentTime);
            Debug.Log(LifeTime);
            if (currentTime >= LifeTime || progressFly > 1)
            {
                CharacterController.Instance.MakeDamageEvent?.Invoke(unit);
                _bullet.SetActive(false);
                yield break;
            }
        }
    }
}
