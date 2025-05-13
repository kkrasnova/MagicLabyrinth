using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleMaze : MonoBehaviour
{
    public GameObject cubePrefab; 
    public GameObject floorPrefab; 
    public GameObject playerPrefab;
    public GameObject relicPrefab; 
    public GameObject trapPrefab;
    public GameObject finishPrefab;
    public GameObject startPrefab;

    void Start()
    {
        CreateMaze();
        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
            playerController.canMove = true;
    }

    public void CreateMaze()
    {
        GameObject camObj = GameObject.FindWithTag("MainCamera");
        if (camObj != null)
        {
            var tpc = camObj.GetComponent<ThirdPersonCamera>();
            if (tpc != null)
            {
                tpc.target = null;
            }
        }

        var oldPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in oldPlayers)
        {
            Destroy(p);
        }
        
        int[,] maze = new int[12, 12]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,1,0,0,0,1,0,0,0,0,1},
            {1,0,1,0,1,0,1,0,1,1,0,1},
            {1,0,0,0,1,0,0,0,1,0,0,1},
            {1,1,1,0,1,1,1,0,1,0,1,1},
            {1,0,0,0,0,0,1,0,0,0,0,1},
            {1,0,1,1,1,0,1,1,1,1,0,1},
            {1,0,1,0,0,0,0,0,1,0,0,1},
            {1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,0,0,1,0,0,0,0,1,0,1},
            {1,1,1,0,1,0,1,1,0,1,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1}
        };

        for (int x = 0; x < 12; x++)
        {
            for (int z = 0; z < 12; z++)
            {
                Vector3 pos = new Vector3(x * 3, 0, z * 3);
                Instantiate(floorPrefab, pos, Quaternion.identity, transform);

                if (maze[z, x] == 1)
                {
                    Vector3 wallPos = new Vector3(x * 3, 1.5f, z * 3);
                    Instantiate(cubePrefab, wallPos, Quaternion.identity, transform);
                }
            }
        }

        CreateTrueRelic(3, 1); 
        CreateTrueRelic(7, 1); 
        CreateTrueRelic(10, 9); 

        CreateFakeRelic(2, 7);
        CreateFakeRelic(5, 5);
        CreateFakeRelic(9, 3);
        CreateFakeRelic(3, 8);

        CreateTrap(4, 1, Trap.TrapType.Slow);
        CreateTrap(7, 4, Trap.TrapType.Slow);
        CreateTrap(9, 7, Trap.TrapType.Slow);
        CreateTrap(10, 5, Trap.TrapType.Slow);

        CreateFinish(10, 10);

        Vector3 startPos = new Vector3(1 * 3, 0.5f, 1 * 3);
        Instantiate(startPrefab, startPos, Quaternion.identity);

        int playerX = 1;
        int playerZ = 1;
        Vector3 playerPos = new Vector3(playerX * 3, 1, playerZ * 3);
        GameObject player = Instantiate(playerPrefab, playerPos, Quaternion.identity);

        camObj = GameObject.FindWithTag("MainCamera");
        if (camObj != null)
        {
            var tpc = camObj.GetComponent<ThirdPersonCamera>();
            if (tpc != null)
            {
                tpc.target = player.transform;
            }
        }

        var pc = player.GetComponent<PlayerController>();
        if (pc != null) pc.canMove = false;

        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            eventSystem.gameObject.SetActive(false);
            eventSystem.gameObject.SetActive(true);
        }
    }

    void CreateTrueRelic(int x, int z)
    {
        Vector3 relicPos = new Vector3(x * 3, 0.5f, z * 3);
        GameObject relic = Instantiate(relicPrefab, relicPos, Quaternion.identity);
        relic.AddComponent<TrueRelic>();
    }

    void CreateFakeRelic(int x, int z)
    {
        Vector3 relicPos = new Vector3(x * 3, 0.5f, z * 3);
        GameObject relic = Instantiate(relicPrefab, relicPos, Quaternion.identity);
        relic.AddComponent<FakeRelic>();
    }

    void CreateTrap(int x, int z, Trap.TrapType type)
    {
        Vector3 trapPos = new Vector3(x * 3, 0.15f, z * 3);
        GameObject trap = Instantiate(trapPrefab, trapPos, Quaternion.identity);
        Trap trapScript = trap.AddComponent<Trap>();
        trapScript.trapType = type;
    }

    void CreateFinish(int x, int z)
    {
        if (finishPrefab != null)
        {
            Vector3 finishPos = new Vector3(x * 3, 0.5f, z * 3);
            Instantiate(finishPrefab, finishPos, Quaternion.identity);
        }
    }

    void CreateStart(int x, int z)
    {
        if (startPrefab != null)
        {
            Vector3 startPos = new Vector3(x * 3, 0.5f, z * 3);
            Instantiate(startPrefab, startPos, Quaternion.identity);
        }
    }
} 