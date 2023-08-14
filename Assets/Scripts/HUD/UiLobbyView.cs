using System;
using UnityEngine;

public class UiLobbyView : MonoBehaviour
{
    public event Action OnPlayButtonClicked;

    public void OnPlayButtonClick()
    {
        OnPlayButtonClicked?.Invoke();
    }
}
