using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] [Range(10, 1000)] private float _dmg;
   [SerializeField] private float _bulletSpeed = 500f;
   [SerializeField] private GameObject _bulletImpactEffect;
   private GameObject _cloneContainer;
   void Start()
   {
      _cloneContainer = GameObject.Find("CloneContainer");
   }

   void Update()
   {
      transform.Translate(Vector3.forward * Time.deltaTime * _bulletSpeed);
      Destroy(this.gameObject, 10f);
   }
   
   void OnTriggerEnter(Collider other)
   {
     if (other.CompareTag("Enemy"))
     {
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.TakeDamage(_dmg);
        GameObject bulletEffect = Instantiate(_bulletImpactEffect, transform.position, Quaternion.identity);
        bulletEffect.transform.parent = _cloneContainer.transform;
        Destroy(this.gameObject);
     }
   }
}
