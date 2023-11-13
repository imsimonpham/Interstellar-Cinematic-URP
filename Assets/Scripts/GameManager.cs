using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    
    private bool _isPOVCamActive = false;
    [SerializeField] private CinemachineVirtualCamera _povCam;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;

    private float _timer;
    [SerializeField] private GameObject _mainSpaceship;
    [SerializeField] private PlayableDirector _noActivityDirector;
    
    [SerializeField] private GameObject _noActivityTimelineContainer;
    [SerializeField] private Trigger _trigger;
    private Vector3  _startPos = new Vector3(-417, 167, 0);
    private Quaternion _startRot = Quaternion.Euler(0f,-180, 0f);
    private Vector3  _currentPos;
    private Quaternion _currentRot;
    
    [SerializeField] private PlayableDirector[] _otherDirectors;
    private bool _anotherDirectorPlaying;


    void Start()
    {
        _timer = 0f;
        _noActivityTimelineContainer.SetActive(false);
        //Play intro cutscene
        _otherDirectors[0].Play();
        //_mainSpaceship.SetActive(false);
    }

    void Update()
    {
        SwitchToCinematic();
        CheckIfOtherDirectorsAreActive();
        if (_otherDirectors[0].state == PlayState.Paused)
        {
            _mainSpaceship.SetActive(true);
        }
        SwitchCam(); 
        QuitGame();
    }

    void SwitchToCinematic()
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0 && !Input.anyKey)
        {
            _timer += Time.deltaTime;
            if (_timer >= 5f && _anotherDirectorPlaying == false)
            {
                _noActivityTimelineContainer.SetActive(true);
                _mainSpaceship.SetActive(false);
                _noActivityDirector.Play();
            }
        } else 
        {
            _noActivityTimelineContainer.SetActive(false);
            _timer = 0f;
            _mainSpaceship.SetActive(true);
            _noActivityDirector.Stop();
        }
    }
    
     void CheckIfOtherDirectorsAreActive()
     {
         _anotherDirectorPlaying = false;
         foreach (PlayableDirector director in _otherDirectors)
         {
             if (director.state == PlayState.Playing)
             {
                 _timer = 0f;
                 _mainSpaceship.SetActive(false);
                 _anotherDirectorPlaying = true;
                 break;
             }
         } 
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

   void QuitGame(){
    if(Input.GetKeyDown(KeyCode.Escape)){
        Debug.Log("Quitting");
        Application.Quit();
    }
   }
}
