using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public ScriptableObj parameters;
    private float _healthPoint;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

   
}