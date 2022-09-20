
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShakeCameraManager : MonoBehaviour
{
    public static ShakeCameraManager instance;
    [SerializeField,ReadOnly]private CinemachineImpulseSource m_impulseSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        m_impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(Vector2 speed,float factor=1)
    {
        Debug.Log($"camera shake with speed:{speed}");
        m_impulseSource.GenerateImpulse(speed*factor);
    }
}
