using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  Attach this script to the root GameObject
 *  Hopefully that GameObject is your Teammate model/prefab
 */
[RequireComponent (typeof(NavMeshAgent))]
public class TeammateController : MonoBehaviour, Entity
{
    // At each interval, check what wave it is
    // If it reaches the next milestone, upgrade weapon
    private const float      CHECK_WAVE_TIME = 4f;

    // How many waves before upgrading guns
    private const int        WAVES_PER_UPGRADE = 3;

    [Header ("Customisations")]
    [SerializeField] [Range(100f, 500f)] private float health;
    [SerializeField] [Range(1f, 8f)]    private float moveSpeed;
    [SerializeField] [Range(7f, 14f)]   private float detectionRange;

    [Header ("References")]
    [SerializeField] private GameObject weaponController;

    public StateMachine      m_stateMachine { get; private set; }
    private NavMeshAgent     m_navMeshAgent;
    private PlayerInfo       m_playerInfo;
    public WeaponController  m_weaponController { get; private set; }
    public Zombie            m_enemy { get; private set; }
    private float            m_health;

    private WaveSystem       m_waveSystem;
    private int              m_lastWave;
    private int              m_currWave;
    private int              m_waveToUpgrade;
    private float            m_checkCurrentWaveTime;

    public float MoveSpeed      => moveSpeed;
    public float DetectionRange => detectionRange;

    public static event Action<Vector3> OnDeath;

    private void OnEnable()
    {
        PlayerInfo.OnPlayerDamaged += FollowPlayerState;
    }

    private void OnDisable()
    {
        PlayerInfo.OnPlayerDamaged -= FollowPlayerState;
    }

    private void Awake()
    {
        m_waveSystem = GameObject.Find("WaveManager").GetComponent<WaveSystem>();
    }

    private void Start()
    {
        m_health = health;
        m_playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();

        m_weaponController = weaponController.GetComponent<WeaponController>();

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.speed = moveSpeed;

        m_stateMachine = new StateMachine();
        m_stateMachine.AddState(new StateTeammatePatrol(this, m_playerInfo));
        m_stateMachine.AddState(new StateTeammateFollowPlayer(this, m_playerInfo));
        m_stateMachine.AddState(new StateTeammateShoot(this));
        m_stateMachine.AddState(new StateTeammateChaseEnemy(this));
        m_stateMachine.ChangeState("TeammatePatrol");

        m_lastWave             = 0;
        m_currWave             = 0;
        m_waveToUpgrade        = WAVES_PER_UPGRADE;
        m_checkCurrentWaveTime = CHECK_WAVE_TIME;
    }

    private void OnTriggerEnter(Collider other)
    {
        Fireball fireball = other.gameObject.GetComponent<Fireball>();
        if (fireball != null)
        {
            this.TakeDamage( fireball.Damage );
        }
    }

    private void Update()
    {
        m_stateMachine.Update();

        m_checkCurrentWaveTime -= Time.deltaTime;
        if (m_checkCurrentWaveTime <= 0f)
        {
            m_checkCurrentWaveTime = CHECK_WAVE_TIME;
            m_currWave = m_waveSystem.waveCount;

            // On wave change
            if (m_lastWave != m_currWave && m_currWave > 1)
            {
                m_lastWave = m_currWave;

                if (m_currWave >= m_waveToUpgrade)
                {
                    m_weaponController.UpgradeWeapon();
                    m_waveToUpgrade += WAVES_PER_UPGRADE;
                }
            }
        }

        // m_weaponController.UseWeapon();
    }

    public void TakeDamage(float dmg)
    {
        m_health -= dmg;

        if (m_health <= 0f)
        {
            OnDeath?.Invoke( transform.position );
            Destroy( this.gameObject );
        }
    }

    public float GetMaxHP()
    {
        return health;
    }

    public float GetCurrentHP()
    {
        return m_health;
    }

    public void SetEnemy(Zombie zombie)
    {
        m_enemy = zombie;
    }

    // If called, go into "follow player" state if in "patrol" state
    public void FollowPlayerState(float dummyVar)
    {
        float chance = 80f;
        float rand = UnityEngine.Random.Range(0f, 100f);

        if (chance <= rand)
        {
            if (m_stateMachine.GetCurrentState() == "TeammatePatrol")
            {
                m_stateMachine.ChangeState("TeammateFollowPlayer");
            }
        }
    }

    private void OnDrawGizmos()
    {
    /*      
       if (!Application.isPlaying)
            return;

        Vector3 pos = transform.position;
        pos.y = 0f;
        // Gizmos.DrawLine( transform.position, transform.position + transform.forward * 16f);
        Gizmos.DrawWireSphere(pos, this.DetectionRange);*/

    /*    if (!Application.isPlaying)
            return;

        int numRays = 40;
        Vector3 pos = transform.position;
        Vector3 dir = transform.forward;
        pos.y = 1f;
        float dTheta = 360f / numRays;

        for (int i = 0; i < numRays; ++i)
        {
            Gizmos.DrawRay( pos, dir * detectionRange );
            dir = Quaternion.Euler(0f, dTheta, 0f) * dir;
        }*/
    }
}
