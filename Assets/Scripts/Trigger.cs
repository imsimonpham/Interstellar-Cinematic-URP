using UnityEngine.Playables;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector  _triggerDirector;
    [SerializeField] private ShipControls _shipControls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _shipControls.StopShip();
            _triggerDirector.Play();
        }
    }
}
