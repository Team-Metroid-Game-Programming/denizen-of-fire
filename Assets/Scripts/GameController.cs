using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// global game logic goes in here
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform startPoint;
    [SerializeField] private float respawnSeconds = 4f;

    private HealthController playerHealthController;

    private float timeDead = 0f;

    public Transform currentCheckPoint { get; set; }

    public static GameController Instance { get; private set; }
     
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private void Awake()
    {
        currentCheckPoint = startPoint;
        if (player != null)
        {
            playerHealthController = player.GetComponent<HealthController>();
        }
        playerHealthController.Spawn(currentCheckPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealthController.isDead)
        {
            // count up running total of time dead
            timeDead += Time.deltaTime;

            Debug.Log($"Respawning in {(int)(respawnSeconds - timeDead + 1)}");

            if (timeDead > respawnSeconds)
            {
                // respawn when timer hit
                playerHealthController.Spawn(currentCheckPoint);
                timeDead = 0;
                Debug.Log("");
            }
        }

        ToggleObsticles();
        HandleDamageToPlayer();
    }


    // temporary for Wednesday demo purposes
    [SerializeField] private GameObject groundObst;
    [SerializeField] private GameObject midAirObst;
    [SerializeField] private GameObject highAirObst;
    private void ToggleObsticles()
    {
        if (Input.GetKeyDown(KeyCode.G))
            groundObst.SetActive(!groundObst.activeSelf);
        if (Input.GetKeyDown(KeyCode.U))
            midAirObst.SetActive(!midAirObst.activeSelf);
        if (Input.GetKeyDown(KeyCode.H))
            highAirObst.SetActive(!highAirObst.activeSelf);
    }

    private void HandleDamageToPlayer()
    {
        if (Input.GetKeyDown(KeyCode.V))
            player.GetComponent<HealthController>().ModifyHealth(-10);
    }
}
