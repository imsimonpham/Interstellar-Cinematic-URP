using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private float _timer;
    private bool _isPOVCamActive = false;

    [SerializeField] private GameObject _camContainer;
    [SerializeField] private CinemachineVirtualCamera _povCam;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;

    [SerializeField] private PlayableDirector _director;
    

    void Start()
    {
        _timer = 0f;
    }

    void Update()
    {
        if(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0 && !Input.anyKey)
        {
            _timer += Time.deltaTime;
            if(_timer >= 5f)
            {
                _director.gameObject.SetActive(true);
                _camContainer.SetActive(false);
                _director.GetComponent<PlayableDirector>().Play();
            }
        } else
        {
            _timer = 0f;
            _director.gameObject.SetActive(false);
            _camContainer.SetActive(true);
        }

        SwitchCam();
    }

   void SwitchCam()
   {
        if (_isPOVCamActive)
        {
            _povCam.Priority = 15;
            _thirdPersonCam.Priority = 10;
        }
        else
        {
            _povCam.Priority = 10;
            _thirdPersonCam.Priority = 15;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _isPOVCamActive = !_isPOVCamActive;
        }
   } 
}
