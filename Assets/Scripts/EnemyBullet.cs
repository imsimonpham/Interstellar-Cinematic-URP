using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   [SerializeField] [Range(10, 1000)] private float _dmg = 20f;
   [SerializeField] private float _bulletSpeed = 500f;

   void Start()
   {

   }

   void Update()
   {
      transform.Translate(Vector3.forward * Time.deltaTime * _bulletSpeed);
      Destroy(this.gameObject, 10f);
   }

   void OnTriggerEnter(Collider other)
   {
     Debug.Log("Bullet hit " + other.name);
     if (other.CompareTag("Enemy"))
     {
        Destroy(this.gameObject);
     }
   }
}
