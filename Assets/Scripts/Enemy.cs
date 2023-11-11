using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private AudioClip _explosionSound;
    private AudioSource _audioSource;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;
    [SerializeField] private HealthBarUI _healthBarCanvas;
    
    void Start()
    {
        _health = _maxHealth;
        _audioSource = GetComponent<AudioSource>();
        
        if (_audioSource == null)
        {
            Debug.Log("Audio Source is null");
        }

        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer == null)
        {
            Debug.Log("Mesh Collider is null");
        }
        
        _boxCollider = GetComponent<BoxCollider>();
        if (_boxCollider == null)
        {
            Debug.Log("Mesh Collider is null");
        }
    }
    public void TakeDamage(float amount)
    {
        _health -= amount;
        _healthBarCanvas.UpdateHealthBarUI(_health, _maxHealth);
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _audioSource.PlayOneShot(_explosionSound);
        Destroy(_meshRenderer);
        Destroy(_boxCollider);
        Destroy(this.gameObject, 2f);
    }

    public float GetEnemyHealth()
    {
        return _health;
    }
    
    public float GetEnemyMaxHealth()
    {
        return _health;
    }
}
