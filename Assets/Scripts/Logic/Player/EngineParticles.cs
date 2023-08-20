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
    [SerializeField]
    private Transform ShipTransform;
    [SerializeField]
    private List<float> TrailsOnAngles = new List<float>() { 45f,315f,135f,225f };

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
        if (_inputService.zAxis != 0 || _inputService.xAxis != 0)
            EngineLight.enabled = true;
        else
            EngineLight.enabled = false;
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
        var shipTorque = ShipTransform.rotation.eulerAngles.y;
        return (shipTorque <= TrailsOnAngles[0] || shipTorque >= TrailsOnAngles[1]) ||
                (shipTorque >= TrailsOnAngles[2] && shipTorque <= TrailsOnAngles[3]);
    }

    private bool IsInputNotZero()
    {
        return _inputService.zAxis != 0 || _inputService.xAxis != 0;
    }

    private void SetTrails(bool emitting)
    {
        foreach (TrailRenderer trail in EngineTrails)
            trail.emitting = emitting;
    }
}
