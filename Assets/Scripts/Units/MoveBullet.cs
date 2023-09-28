using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public GameObject _enemy;
    public float damage;
    public string tag;

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            _enemy.transform.position, 0.05f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.GetComponent<UnitsBase>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}