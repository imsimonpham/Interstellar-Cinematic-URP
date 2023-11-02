using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _povCam;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;
    private bool _isPOVCamActive = false;

    void Update()
    {
        if (_isPOVCamActive)
        {
            _povCam.Priority = 15;
            _thirdPersonCam.Priority = 10;
        } else
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
