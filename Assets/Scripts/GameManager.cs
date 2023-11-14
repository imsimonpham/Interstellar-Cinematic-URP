using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    //Cams
    private bool _isPOVCamActive = false;
    [SerializeField] private CinemachineVirtualCamera _povCam;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;
    [SerializeField] private CinemachineVirtualCamera _endingSceneCam;

    //Directors
    [SerializeField] private PlayableDirector _noActivityDirector;
    [SerializeField] private PlayableDirector _introDirector;
    [SerializeField] private PlayableDirector _triggerDirector;
    [SerializeField] private PlayableDirector _endingDirector;
    [SerializeField] private GameObject _noActivityTimelineContainer;
    private bool _introCutsceneHasFinished = false;
    
    //Sound
    [SerializeField] private AudioClip _dataRevealSound;
    private AudioSource _audioSource;
    
    //Text Bubbles
    [SerializeField] private GameObject _noActivityCutsceneText;
    
    //Others
    private float _timer;
    [SerializeField] private ShipControls _mainSpaceship;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private GameObject _lightOrb;
    private bool _showEnemyHealthBar;
    private bool _enemyDead;
    private bool _playEndingCutscene;
    [SerializeField] private Blaster[] _blasters;
    [SerializeField] private GameObject _pauseScreen;
    private bool _showPauseScreen = false;
    private string _menuScene = "Menu";
    
    
    void Start()
    {
        _timer = 0f;
        _noActivityTimelineContainer.SetActive(false);
        _enemyDead = false;
        _playEndingCutscene = false;
        _endingSceneCam.Priority = 10;
        _introDirector.Play();
        
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("Audio Source is null");
        }
    }

    void Update()
    {
        SwitchToCinematic();
        if (_enemyDead && _playEndingCutscene == false)
        {
            Invoke("PlayEndingCutscene", 3f);
            _playEndingCutscene = true;
        }
        if (_introDirector.state == PlayState.Playing || _endingDirector.state == PlayState.Playing || _triggerDirector.state == PlayState.Playing)
        {
            _timer = 0;
            _noActivityTimelineContainer.SetActive(false);
            if(_triggerDirector.state == PlayState.Playing)
            {
                _mainSpaceship.gameObject.SetActive(true);
                _mainSpaceship.StopShip();
                _mainSpaceship.enabled = false;
                foreach (var blaster in _blasters)
                {
                    blaster.enabled = false;
                }
            }
            else
            {
                _mainSpaceship.gameObject.SetActive(false);
            }
        }
        else if(_noActivityDirector.state == PlayState.Playing)
        {
            _mainSpaceship.gameObject.SetActive(false);
            if (_enemy != null && _enemy.activeSelf)
            {
                _enemy.GetComponent<MeshRenderer>().enabled = false;
                _enemy.GetComponent<Enemy>().enabled = false;
                _enemyHealthBar.GetComponent<Canvas>().enabled = false;
            }

            if (_lightOrb != null && _lightOrb.activeSelf)
            {
                _lightOrb.GetComponent<VisualEffect>().enabled = false;
            }
        }
        else
        {
            _mainSpaceship.gameObject.SetActive(true);
            _mainSpaceship.enabled = true;
            foreach (var blaster in _blasters)
            {
                blaster.enabled = true;
            }
            if (_enemy != null && _enemy.activeSelf)
            {
                _enemy.GetComponent<MeshRenderer>().enabled = true;
                _enemy.GetComponent<Enemy>().enabled = true;
                _enemyHealthBar.GetComponent<Canvas>().enabled = true;
            }
            if (_lightOrb != null && _lightOrb.activeSelf)
            {
                _lightOrb.GetComponent<VisualEffect>().enabled = true;
            }
        }
        
        SwitchCam();
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            _showPauseScreen = !_showPauseScreen;
        }
        ShowPauseScreen();
    }

    void PlayEndingCutscene()
    {
        _endingDirector.Play();
        _endingSceneCam.Priority = 15;
    }

    void SwitchToCinematic()
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0 && !Input.anyKey)
        {
            _timer += Time.deltaTime;
            if (_timer >= 5f)
            {
                _noActivityTimelineContainer.SetActive(true);
                _noActivityCutsceneText.SetActive(true);
                _noActivityDirector.Play();
            }
        } else 
        {
            _noActivityTimelineContainer.SetActive(false);
            _noActivityCutsceneText.SetActive(false);
            _timer = 0f;
            _noActivityDirector.Stop();
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
   
   public void CheckEnemyDeath()
   {
       _enemyDead = true;
   }
   
   void ShowPauseScreen()
   {
       if (_showPauseScreen)
       { 
           _pauseScreen.SetActive(true);
           Time.timeScale = 0f;
       }
       else
       {
           _pauseScreen.SetActive(false);
           Time.timeScale = 1f;
       }
   }

   public void ShowTextBubble(GameObject text)
   {
       text.SetActive(true);
       _audioSource.PlayOneShot(_dataRevealSound);
   }
   
   public void HideTextBubble(GameObject text)
   {
       text.SetActive(false);
   }

   public void LoadMenuScene()
   {
       SceneManager.LoadScene(_menuScene);
   }
}
