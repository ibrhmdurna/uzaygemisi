using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    [SerializeField] float Damage = 10f;

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return Damage;
    }
}
