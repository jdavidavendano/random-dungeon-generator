using System.Collections;
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
    private float idleVelocity = 0.2f;

    [SerializeField]
    private float attackDistance = 0.9f;

    private Vector3 lastPosition;

    private int lastDirection;

    BoxCollider2D m_Collider2D;

    private void Awake()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            Debug.Log("Entró");
            PlayerInArea = true;
            Player = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            Debug.Log("Salió");
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
            //transform.position = Vector2.MoveTowards(transform.position, Player.position, catchVelocity * Time.deltaTime);
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

            UpdateMotor(new Vector2(directions[lastDirection].x * idleVelocity, directions[lastDirection].y * idleVelocity));
            if (lastDirection < 4)
            {
                if (lastPosition == transform.position)
                {
                    lastDirection = getRandomDirection();
                }
            }
            else if (lastPosition.x == transform.position.x || lastPosition.y == transform.position.y)
            {

                lastDirection = getRandomDirection();
            }

            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 1), enemyVelocity * Time.deltaTime);


        }
        lastPosition = transform.position;


    }

}
