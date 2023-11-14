using UnityEngine;
using UnityEngine.UI;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private float _vRotSpeed;
    [SerializeField] private float _hRotSpeed;
    [SerializeField] private float _currentSpeed;
    private float _vertical;
    private float _horizontal;
    private float _acceleratingRate;
    private float _maxSpeed = 10f;
    private float _minSpeed = 0f;
    [SerializeField] private GameObject[] _engines;
    [SerializeField] private Image _speedBar;
    [SerializeField] private ParticleSystem _spaceDust;
    [SerializeField] private GameObject _playerUIContainer;

    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = 1;
        _acceleratingRate = 1;
        ControlEngineFlames(_currentSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
        UpdateSpeedBar(_currentSpeed);
    }

    private void ShipMovement()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed += _acceleratingRate * Time.deltaTime;
            ControlEngineFlames(_currentSpeed);
            ControlSpaceDust(_currentSpeed);
            if (_currentSpeed > _maxSpeed)
            {
                _currentSpeed = _maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _currentSpeed -= _acceleratingRate * Time.deltaTime;
            ControlEngineFlames(_currentSpeed);
            ControlSpaceDust(_currentSpeed);
            if (_currentSpeed < _minSpeed)
            {
                _currentSpeed = _minSpeed;
            } 
        }
        

        Vector3 rotateH = new Vector3(0, _horizontal, 0);
        transform.Rotate(rotateH * _hRotSpeed * Time.deltaTime);

        Vector3 rotateV = new Vector3(_vertical, 0, 0);
        transform.Rotate(rotateV * - 1f * _vRotSpeed * Time.deltaTime);

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

    void ControlSpaceDust(float speed)
    {
        ParticleSystem.MainModule ps = _spaceDust.main;
        ps.startSpeed = new ParticleSystem.MinMaxCurve(speed * 5f);
    }

    public void StopShip()
    {
        _currentSpeed = 0;
        ControlEngineFlames( 0);
    }
    
    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
        ControlEngineFlames(speed);
    }

    void UpdateSpeedBar(float speed)
    {
        _speedBar.fillAmount = speed/_maxSpeed;
    }

    public void ShowPlayerUI()
    {
        _playerUIContainer.SetActive(true);
    }
    
    public void HidePlayerUI()
    {
        _playerUIContainer.SetActive(false);
    }
}
