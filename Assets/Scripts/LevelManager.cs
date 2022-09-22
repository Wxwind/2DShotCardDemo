using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemyLow EnemyLowPre;
    [SerializeField] private EnemyHigh EnemyHighPre;
    [SerializeField] private PlayerController m_playerController;
    private EnemyPool<EnemyLow> m_enemyLowPool;
    private EnemyPool<EnemyHigh> m_enemyHighool;
    private Timer m_generateEnemyTimer;
    [SerializeField] private float m_waveInternal = 5f;
    [SerializeField] private List<Vector3> spawnPoses;


    private void Awake()
    {
        m_enemyLowPool = new EnemyPool<EnemyLow>(5, EnemyLowPre, transform);
        m_enemyHighool = new EnemyPool<EnemyHigh>(5, EnemyHighPre, transform);
        m_generateEnemyTimer = new Timer(m_waveInternal, () =>
        {
            StartCoroutine(RandomGenerateEnemy());
            m_generateEnemyTimer.ReRun();
        }, true);
    }

    void Start()
    {
        AudioManager.instance.PlayBGMAudio("BGM_Quirky Companion");
        StartCoroutine(RandomGenerateEnemy());
    }

    private void Update()
    {
        if (!GameManager.instance.IsGameEnd)
        {
            m_generateEnemyTimer.Tick(Time.deltaTime);
        }
    }

    private void GenerateEnemy(bool isLow, Vector3 spawnPos, bool isBringCard = false)
    {
        if (isLow)
        {
            m_enemyLowPool.GetFromPool(m_playerController, spawnPos, isBringCard);
        }
        else m_enemyHighool.GetFromPool(m_playerController, spawnPos, isBringCard);
    }

    public void OnEndGame()
    {
        m_playerController.enabled = false;
    }

    private IEnumerator RandomGenerateEnemy()
    {
        Random random = new Random();
        var index = random.Next(spawnPoses.Count);
        if (index <= spawnPoses.Count / 2)
        {
            GenerateEnemy(true, spawnPoses[index], true);
        }
        else
        {
            GenerateEnemy(false, spawnPoses[index], true);
        }

        yield return new WaitForSeconds(1f);
        var index2 = random.Next(spawnPoses.Count);
        if (index2 <= spawnPoses.Count / 2)
        {
            GenerateEnemy(true, spawnPoses[index2]);
        }
        else
        {
            GenerateEnemy(false, spawnPoses[index2]);
        }

        yield return new WaitForSeconds(1f);
        var index3 = random.Next(spawnPoses.Count);
        if (index <= spawnPoses.Count / 2)
        {
            GenerateEnemy(true, spawnPoses[index3]);
        }
        else
        {
            GenerateEnemy(false, spawnPoses[index3]);
        }
    }
}