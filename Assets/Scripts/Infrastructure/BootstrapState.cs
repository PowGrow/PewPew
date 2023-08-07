﻿using Pewpew.Infrastructure;
using Pewpew.Services.Inputs;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;

    public BootstrapState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    void RegisterServices()
    {
        Game.InputService = RegisterInputService();
    }
    public void Enter()
    {
        RegisterServices();    
    }

    public void Exit()
    {
        
    }

    private static IInputService RegisterInputService()
    {
        return new StandaloneInputService();
    }
}

