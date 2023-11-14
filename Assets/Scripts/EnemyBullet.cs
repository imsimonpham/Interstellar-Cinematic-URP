using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   [SerializeField] [Range(10, 1000)] private float _dmg;
   [SerializeField] private float _bulletSpeed = 500f;
   [SerializeField] private GameObject _bulletImpactEffect;
   private GameObject _cloneContainer;
   private PlayerCameraShake _playerCameraShake;

   void Start()
   {
      _playerCameraShake = GameObject.FindWithTag("Player").GetComponent<PlayerCameraShake>();
      if (_playerCameraShake == null)
      {
         Debug.Log("Player Camera Shake is null!");
      }
      _cloneContainer = GameObject.Find("CloneContainer");
      if (_cloneContainer == null)
      {
         Debug.Log("Clone Container is null!");
      }
   }
   void Update()
   {
      transform.Translate(Vector3.forward * Time.deltaTime * _bulletSpeed);
      Destroy(this.gameObject, 10f);
   }
   
   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Enemy enemy = other.GetComponent<Enemy>();
         GameObject bulletEffect = Instantiate(_bulletImpactEffect, transform.position, Quaternion.identity);
         bulletEffect.transform.parent = _cloneContainer.transform;
         _playerCameraShake.Shake();
         Destroy(gameObject);
      }
   }
}
