using System.Collections.Generic;
using UnityEngine;

public class AIPlayerEnterAreaDetector : MoveController
{
    [field: SerializeField]

    public bool PlayerInArea { get; private set; }

    public bool AttackingPlayer { get; private set; }

    public Transform Player { get; private set; }

    [SerializeField]
    private float catchVelocity = 15f;

    [SerializeField]
    private string detectionTag = "Player";

    [SerializeField]
    private float idleVelocity = 0.3f;

    [SerializeField]
    private float attackDistance = 0.9f;

    private Vector3 lastPosition;

    private int lastDirection;

    BoxCollider2D m_Collider2D;

    private float timeSleeping = 0f;

    private float goToBed()
    {
        int isSleepingRandom = Random.Range(0, 9);

        if (isSleepingRandom < 1)
        {
            return Random.Range(10, 30);
        }
        return 0;
    }

    private void Awake()
    {
        // Enemy collider for not trespassing walls
        m_Collider2D = GetComponent<BoxCollider2D>();
    }

    private List<Vector2> directions = new List<Vector2> {
        new Vector2(0, 1f),
        new Vector2(0, -1f),
        new Vector2(-1f, 0),
        new Vector2(1f, 0),
        new Vector2(1f, 1f),
        new Vector2(-1f, 1f),
        new Vector2(1f, -1f),
        new Vector2(-1f, -1f)
    };

    // Player in range of the enemy 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            PlayerInArea = true;
            Player = collision.gameObject.transform;
        }
    }
    // Player out of range of the enemy 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            PlayerInArea = false;
            Player = null;
        }
    }

    private int getRandomDirection()
    {
        int newDirection = Random.Range(0, directions.Count);
        while (newDirection == lastDirection)
        {
            newDirection = Random.Range(0, directions.Count);
        }
        return newDirection;
    }

    private void FixedUpdate()
    {
        if (PlayerInArea)
        {
            UpdateMotor(new Vector2(Player.position.x - transform.position.x, Player.position.y - transform.position.y).normalized * catchVelocity * Time.deltaTime);

            // Enemy attacking or not
            if (Vector2.Distance(transform.position, Player.position) < attackDistance && !AttackingPlayer)
            {
                m_Collider2D.size = new Vector2(0, 0);
                AttackingPlayer = true;
                Debug.Log("Ataca");
            }
            else if (Vector2.Distance(transform.position, Player.position) > attackDistance && AttackingPlayer)
            {
                AttackingPlayer = false;
                Debug.Log("Ya no");
                m_Collider2D.size = new Vector2(0.5f, 0.8f);


            }

        }
        else
        {
            // Idle
            if (timeSleeping > 0)
            {
                Debug.Log("sleeping for " + timeSleeping);
                timeSleeping -= Time.deltaTime;
            }
            else
            {

                UpdateMotor(new Vector2(directions[lastDirection].x * idleVelocity, directions[lastDirection].y * idleVelocity));

                if (lastDirection < 4)
                {
                    if (lastPosition == transform.position)
                    {
                        lastDirection = getRandomDirection();
                        timeSleeping = goToBed();
                    }
                }
                else if (lastPosition.x == transform.position.x || lastPosition.y == transform.position.y)
                {

                    lastDirection = getRandomDirection();
                    timeSleeping = goToBed();
                }
            }

        }

        lastPosition = transform.position;
    }

}