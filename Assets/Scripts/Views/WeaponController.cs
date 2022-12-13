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

        float wspeed = (CharacterController.Instance.SpeedAttack * Time.deltaTime) /
                       Vector3.Distance(startPoint, unit.UnitView.transform.position);

        LifeTime = Vector3.Distance(startPoint, unit.UnitView.transform.position) /
                   (CharacterController.Instance.SpeedAttack *  Time.deltaTime);
        
        Vector3 center = (startPoint + unit.UnitView.transform.position)* 0.5F;
        center -= new Vector3(0, 1, 0);
        
        Vector3 startingPoint = startPoint - center;
        Vector3 endingPoint = unit.UnitView.transform.position - center;
        
        float progressFly = 0f;

        float currentTime = 0f;
        while (true)
        {
            yield return null;
            currentTime += Time.fixedDeltaTime;

            progressFly += wspeed;

            _bullet.transform.position =
                Vector3.Slerp(startPoint - center, unit.UnitView.transform.position - center, progressFly);
            _bullet.transform.position += center;

            if (currentTime >= LifeTime || progressFly > 1)
            {
                CharacterController.Instance.MakeDamageEvent?.Invoke(unit);
                _bullet.SetActive(false);
                yield break;
            }
        }
    }
}
