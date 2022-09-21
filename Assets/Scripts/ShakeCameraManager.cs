using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

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
        m_impulseSource.GenerateImpulse(new Vector3(speed.x,speed.y, 0)*factor);
    }
}
