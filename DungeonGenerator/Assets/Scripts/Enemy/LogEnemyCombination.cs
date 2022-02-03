using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemyCombination : Enemy {

    private Transform target;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    private Transform _homePosition;
    [SerializeField] private float idleVelocity = 0.3f;
    private Vector3 lastPosition;
    private int lastDirection;
    private float timeSleeping = 0f;
    private float timeAwakeMandatory = 10f;
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private float xSpeed = 4f;
    private float ySpeed = 4f;
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

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        CheckDistance();
    }

    void FixedUpdate() {
        
    }

    void CheckDistance() {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
            Debug.Log("Entró");
        }
        else {
            // Idle
            if (timeSleeping > 0) {
                timeSleeping -= Time.deltaTime;
            }
            else {
                UpdateMotor(new Vector2(directions[lastDirection].x * idleVelocity, directions[lastDirection].y * idleVelocity));

                if (lastDirection < 4) {
                    if (lastPosition == transform.position) {
                        lastDirection = getRandomDirection();
                        timeSleeping = goToBed();
                    }
                }
                else if (lastPosition.x == transform.position.x || lastPosition.y == transform.position.y) {
                    lastDirection = getRandomDirection();
                    timeSleeping = goToBed();
                }
            }
        }

        if (timeAwakeMandatory > 0) {
            timeAwakeMandatory -= Time.deltaTime;
        }
        if (timeSleeping < 0) {
            timeAwakeMandatory = 10f;
        }

        lastPosition = transform.position;
    }

    private int getRandomDirection() {
        int newDirection = Random.Range(0, directions.Count);
        while (newDirection == lastDirection) {
            newDirection = Random.Range(0, directions.Count);
        }
        return newDirection;
    }

    private float goToBed() {
        if (timeAwakeMandatory < 0) {
            int isSleepingRandom = Random.Range(0, 9);

            if (isSleepingRandom < 1) {
                // return Random.Range(10, 30);
                return 5;
            }
        }
        return 0;
    }

    void UpdateMotor(Vector3 input) {

        // Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Swap sprite direction wether right or left
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Add push vector if any
        // moveDelta += pushDirection;

        // // Reduce the push force every frame, based off recovery speed
        // pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Make sure we can move in this direction, by casting a box there first, if the box returns null, we're free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) {
            // Movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) {
            // Movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}