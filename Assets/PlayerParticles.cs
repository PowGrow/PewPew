using Pewpew.Infrastructure.Services;
using Pewpew.Services.Inputs;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject EngineParticles;

    private IInputService _inputService;

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Update()
    {
        OnPlayerMove();
    }

    private void OnPlayerMove()
    {
        if (_inputService.VerticalAxis != 0 || _inputService.Torque != 0)
            EngineParticles.SetActive(true);
        else
            EngineParticles.SetActive(false);
    }
}
