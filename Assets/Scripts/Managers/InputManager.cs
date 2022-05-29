using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private PlayerControls playerControls;

    public static Vector2 PointerPosition { get; private set; }


    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Pointer.Position.performed += x => PointerPosition = x.ReadValue<Vector2>();
            playerControls.Selection.Plot1.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(0);
            playerControls.Selection.Plot2.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(1);
            playerControls.Selection.Plot3.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(2);
            playerControls.Selection.Plot4.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(3);
            playerControls.Selection.Plot5.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(4);
            playerControls.Selection.Plot6.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(5);
            playerControls.Selection.Plot7.performed += _ => GameManager.Instance.HeavenManager.SelectPlot(6);
            // Even writing this is making me cringe D:
            playerControls.Selection.Upgrade.performed += _ => UIManager.Instance.TryUpgradeSelectedPlot();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}


