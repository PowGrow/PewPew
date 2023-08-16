using Pewpew.Infrastructure.Services;
using Pewpew.Services.Inputs;
using System.Collections.Generic;
using UnityEngine;

public class EngineParticles : MonoBehaviour
{
    [SerializeField]
    private List<TrailRenderer> EngineTrails;
    [SerializeField]
    private Light EngineLight;

    private IInputService _inputService;

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
    }

    private void FixedUpdate()
    {
        OnPlayerMove();
    }

    private void OnPlayerMove()
    {
        ControlTrails();
        ControlLight();
    }

    private void ControlLight()
    {
        if (_inputService.VerticalAxis != 0 || _inputService.Torque != 0)
            EngineLight.enabled = true;
        else
            EngineLight.enabled = false;
    }

    private void ControlTrails()
    {
        if (_inputService.VerticalAxis > 0)
            SetTrails(emitting: true);
        else
            SetTrails(emitting: false);
    }

    private void SetTrails(bool emitting)
    {
        foreach (TrailRenderer trail in EngineTrails)
            trail.emitting = emitting;
    }
}
