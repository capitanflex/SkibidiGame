using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToiletMan : UnitsBase
{
    public void Attack()
    {
        _enemy.GetComponent<UnitsBase>().TakeDamage(_damage);
    }
}
