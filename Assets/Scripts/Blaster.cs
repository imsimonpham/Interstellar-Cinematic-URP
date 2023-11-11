using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] [Range(0f, 5f)] private float _fireRate = 2f;
    [SerializeField] private GameObject _cloneContainer;
    private float _canFire = 1f;
    [SerializeField] private AudioClip _laserSound;
    private AudioSource _audioSource;

    void Start()
    {
        _cloneContainer = GameObject.Find("CloneContainer");
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Audio Source is null");
        }
    }

    void Update()
    {
        if (_canFire <= 0f && Input.GetMouseButton(0))
        {
           Fire();
           _canFire = 1f / _fireRate;
        }
        _canFire -= Time.deltaTime;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.transform.position, transform.rotation);
        _audioSource.PlayOneShot(_laserSound);
        bullet.transform.parent = _cloneContainer.transform;
    }
}
