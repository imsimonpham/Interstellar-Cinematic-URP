using UnityEngine;

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
    [SerializeField] private GameManager _gameManager;
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject[] _firePoints;
    [SerializeField] [Range(0f, 5f)] private float _fireRate;
    [SerializeField] private GameObject _cloneContainer;
    private float _canFire = 1f;
    private bool _startFiring;
    [SerializeField] private AudioClip _laserSound;
    
    void Start()
    {
        _health = _maxHealth;
        _audioSource = GetComponent<AudioSource>();
        _startFiring = false;
        
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
    
    void Update()
    {
        if (_canFire <= 0f && _startFiring)
        {
            Fire();
            _canFire = 1f / _fireRate;
        }
        _canFire -= Time.deltaTime;
    }

     void Fire()
    {
        foreach (var firePoint in _firePoints)
        {
            GameObject bullet = Instantiate(_bulletPrefab, firePoint.transform.position, transform.rotation);
            bullet.transform.parent = _cloneContainer.transform;
        }
        //_audioSource.PlayOneShot(_laserSound);
    }
    public void TakeDamage(float amount)
    {
        _health -= amount;
        _healthBarCanvas.UpdateHealthBarUI(_health, _maxHealth);
        if (_health <= 0)
        {
            _gameManager.CheckEnemyDeath();
            Die();
        }
    }

    void Die()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _audioSource.PlayOneShot(_explosionSound);
        Destroy(_meshRenderer);
        Destroy(_boxCollider);
        _healthBarCanvas.gameObject.SetActive(false);
        Destroy(this.gameObject, 2f);
    }

    public void ShowHealthBar()
    {
        _healthBarCanvas.gameObject.SetActive(true);
    }

    public void StartFiring()
    {
        _startFiring = true;
    }
}
