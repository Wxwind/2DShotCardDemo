using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TMP_Text m_countdown;
    [SerializeField] private float m_gameLastTime=60;
    [SerializeField] private GameObject m_endGamePanel;
    [SerializeField] private PlayerController m_playerController;
    private Timer m_endGameTimer;
    private float m_remainingTime;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        instance = this;
        m_endGameTimer = new Timer(m_gameLastTime, EndGame, true);
        m_remainingTime = m_gameLastTime;
    }

    void Update()
    {
        m_endGameTimer.Tick(Time.deltaTime);
        m_remainingTime -= Time.deltaTime;
        UpdateUI();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0;
        m_endGamePanel.SetActive(true);
        AudioManager.instance.PauseBGMAudio();
        m_playerController.enabled = false;
    }

    void UpdateUI()
    {
        m_countdown.text = $"Countdown: {(int)m_remainingTime}";
    }
}
