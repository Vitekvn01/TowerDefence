using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _speed;

    private int _damage;
    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void Initialization(int damage)
    {
        _damage = damage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.ApplyDamage(_damage);
        }
        
        gameObject.SetActive(false);
    }
}
