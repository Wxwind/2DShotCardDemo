using UnityEngine;
using Sirenix.OdinInspector;


public class InputKeyManager : MonoBehaviour
{
    public static InputKeyManager instance;
    [Header("按键")] [ReadOnly] public KeyCode upKey;
    [ReadOnly] public KeyCode downKey;
    [ReadOnly] public KeyCode leftKey;
    [ReadOnly] public KeyCode rightKey;
    [ReadOnly] public KeyCode attackKey;
    [ReadOnly] public KeyCode interactKey;
    [ReadOnly] public KeyCode jumpKey;
    [ReadOnly] public KeyCode discardKey;
    [ReadOnly] public KeyCode switchCardKey;

    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        KeyInit();
    }

    private void KeyInit()
    {
        upKey = KeyCode.W;
        downKey = KeyCode.S;
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;
        attackKey = KeyCode.K;
        discardKey = KeyCode.L;
        jumpKey = KeyCode.J;
        switchCardKey = KeyCode.Q;
        interactKey = KeyCode.E;
    }
}