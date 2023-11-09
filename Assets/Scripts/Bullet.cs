using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private Rigidbody _rigidbody;
   [SerializeField] [Range(5000f, 25000f)] private float _launchForce = 10000f;
   [SerializeField] [Range(10, 1000)] private int _dmg = 100;
   //[SerializeField] [Range(2, 10)] private float _range = 5f;
   [SerializeField] private float _bulletSpeed = 500f;

   void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
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
