using Pewpew.Infrastructure.Services;
using Pewpew.Services.Inputs;
using System.Collections.Generic;
using UnityEngine;

public class EngineParticles
{
    private List<float> _trailsAngles;
    private List<TrailRenderer> _engineTrails;
    private Light _engineLight;
    private Transform _shipTransform;

    private IInputService _inputService;

    public EngineParticles(IInputService inputService, List<float> trailsAngles, List<TrailRenderer> engineTrails, Light engineLight, Transform shipTransform)
    {
        _inputService = inputService;
        _trailsAngles = trailsAngles;
        _engineTrails = engineTrails;
        _engineLight = engineLight;
        _shipTransform = shipTransform;
    }

    public void Execute()
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
        if (_inputService.zAxis != 0 || _inputService.xAxis != 0)
            _engineLight.enabled = true;
        else
            _engineLight.enabled = false;
    }

    private void ControlTrails()
    {
        
        if ((IsInputNotZero()) && (IsTorqueMatchTrailsAngles()))
            SetTrails(emitting: true);
        else
            SetTrails(emitting: false);

    }

    private bool IsTorqueMatchTrailsAngles()
    {
        var shipTorque = _shipTransform.rotation.eulerAngles.y;
        return (shipTorque <= _trailsAngles[0] || shipTorque >= _trailsAngles[1]) ||
                (shipTorque >= _trailsAngles[2] && shipTorque <= _trailsAngles[3]);
    }

    private bool IsInputNotZero()
    {
        return _inputService.zAxis != 0 || _inputService.xAxis != 0;
    }

    private void SetTrails(bool emitting)
    {
        foreach (TrailRenderer trail in _engineTrails)
            trail.emitting = emitting;
    }
}
