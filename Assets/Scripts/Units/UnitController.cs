using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public List<GameObject> pointsToSpawn = new List<GameObject>();
    private GameObject currentUnit;
    private UnitsBase _unitsBase;
    public Transform a;
    public Transform b;
    public ScriptableObj ToiletManParams;
    public ScriptableObj CameraManParams;

    private void Start()
    {
        // GameObject[] pointToSpawn = GameObject.FindGameObjectsWithTag("pointToSpawn");
        // foreach (GameObject point in pointToSpawn)
        // {
        //     pointsToSpawn.Add(point);
        // }
        
        _unitsBase.SwitchTag();
        _unitsBase.FindNearestEnemy();
    }

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _unitsBase.MoveUnit();
    }

    public void Attack()
    {
        
    }
}