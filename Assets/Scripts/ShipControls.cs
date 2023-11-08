using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentSpeed;
    private float _vertical;
    private float _horizontal;
    private float _accelerator; 
    [SerializeField] private float _maxRotate;
    [SerializeField] private GameObject _shipModel;

    [SerializeField] private GameObject[] _engines;
    

    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = 1;
        _accelerator = 1;
        ControlEngineFlames(_currentSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
    }

    private void ShipMovement()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed += _accelerator * Time.deltaTime;
            ControlEngineFlames(_currentSpeed);
            if (_currentSpeed > 10)
            {
                _currentSpeed = 10;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _currentSpeed -= _accelerator * Time.deltaTime;
            ControlEngineFlames(_currentSpeed);
            if (_currentSpeed < 1)
            {
                _currentSpeed = 1;
            } 
        }
        

        Vector3 rotateH = new Vector3(0, _horizontal, 0);
        transform.Rotate(rotateH * _rotSpeed * Time.deltaTime);

        Vector3 rotateV = new Vector3(_vertical, 0, 0);
        transform.Rotate(rotateV * _rotSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, -_horizontal * 0.2f), Space.Self);

        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }

    void ControlEngineFlames(float speed)
    {
        foreach (GameObject engine in _engines)
        {
            ParticleSystem.MainModule ps = engine.GetComponent<ParticleSystem>().main;
            ps.startSize = new ParticleSystem.MinMaxCurve(speed/3);
            if (speed < 2)
            {
                ps.startSize = new ParticleSystem.MinMaxCurve(0);
            }
            ps.startSpeed = new ParticleSystem.MinMaxCurve(speed);
        }
    }
}
