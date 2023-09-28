using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class UnitsBase : MonoBehaviour
{
    [SerializeField] private ScriptableObj ScriptableObj;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnBullet;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    
    protected float _distanceToEnemy;
    protected float _distanceAttack;
    protected float _damage;
    protected GameObject _enemy;
    protected string tagEnemy;
    
    public float _healthPoint;
    public List<GameObject> enemyObjects = new List<GameObject>();
    [FormerlySerializedAs("isDead")] public bool isAlive = true;
    public void OnEnable()
    {
        _healthPoint = ScriptableObj.healthPoint;
        _distanceAttack = ScriptableObj.distanceAttack;
        _damage = ScriptableObj.damage;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        SwitchTag();
    }

    public void Start()
    {
        FindNearestEnemy();
    }

    public void MoveUnit()
    {
        if (isAlive)
        {
            _distanceToEnemy = Vector3.Distance(_agent.transform.position, _enemy.transform.position);
            if (_distanceToEnemy <= _distanceAttack)
            {
                _agent.isStopped = true;
                _animator.SetBool("isAttacking", true);
            }
            else
            {
                _animator.SetBool("isMove", false);
                _agent.SetDestination(_enemy.transform.position);
                _agent.isStopped = false;
            }
        }
    }


    private float _minDistance = 99999;

    public void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagEnemy);
        
        foreach (GameObject enemy in enemies)
        {
            if(enemy.gameObject.GetComponent<UnitsBase>().isAlive)
            enemyObjects.Add(enemy);
        }
        
        for (int i = 0; i < enemyObjects.Count; i++)
        {
            float distance = Vector3.Distance(_agent.transform.position, enemyObjects[i].transform.position);
            
            if (distance < _minDistance)
            {
                _minDistance = distance;
                _enemy = enemyObjects[i];
            }
        }
    }
    private void Update()
    {
        Death();
        MoveUnit();
    }

    private void Death()
    {
        if (_healthPoint <= 0)
        {
            _animator.SetBool("isDead", true);
            _animator.SetBool("isAttacking",false);
            gameObject.GetComponent<Collider>().enabled = false;
            // gameObject.GetComponent<UnitsBase>().enabled = false;
            isAlive = false;
        }
    }
    public void TakeDamage(float enemyDamage)
    {
        _healthPoint -= enemyDamage;
    }
    public void SwitchTag()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            tagEnemy = "Alies";
        }
        else
        {
            tagEnemy = "Enemy";
        }
    }
    public void AttackCameraMan()
    {
        GameObject bulletPrefab = Instantiate(_bulletPrefab,
            _spawnBullet.position,
            Quaternion.identity);

        bulletPrefab.GetComponent<MoveBullet>().damage = _damage;
        bulletPrefab.GetComponent<MoveBullet>()._enemy = _enemy;
        bulletPrefab.GetComponent<MoveBullet>().tag = tagEnemy;
        
        if (_enemy.GetComponent<UnitsBase>()._healthPoint <= 0)
        {
            FindNearestEnemy();
        }
    }

    public void AttackToiletMan()
    {
        _enemy.GetComponent<UnitsBase>().TakeDamage(_damage);
        
        if (_enemy.GetComponent<UnitsBase>()._healthPoint <= 0)
        {
            FindNearestEnemy();
        }
    }
}