using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/New Enemy")]
public class ScriptableObj : ScriptableObject
{
    public float healthPoint;
    public float damage;
    public float distanceAttack;
    public GameObject unitPrefab;
    public GameObject bulletPrefab;
}
