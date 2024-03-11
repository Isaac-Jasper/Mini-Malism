using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected Stats stats;
    [SerializeField]
    protected Color EvilColor, GoodColor, InnocentColor;
    protected Morality morality;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    CharacterType characterType;
    protected bool canMove = true;
    [Serializable]
    public enum MoveType {
        randomWander,
        targetWander,
        targetDirect,
        sillyWander
    }

    [Serializable]
    public enum CharacterType {
        INNOCENT,
        EVILDOER,
        GOOD
    }

    public void Awake() {
        Initialize();
    }
    void Update() {
        if (canMove) StartMove();
    }
    protected virtual void Initialize() {
        Convert(characterType);
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Squished() {
        morality.Death();
        Destroy(gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.transform.CompareTag("Character")) return;

        Character other = collision.gameObject.GetComponent<Character>();
        switch (other.characterType) {
            case CharacterType.EVILDOER: morality.EvilCollide(other);
            break;
            case CharacterType.INNOCENT: morality.InnocentCollide(other);
            break;
            case CharacterType.GOOD: morality.GoodCollide(other);
            break;
            default: morality.InnocentCollide(other);
            break;
        }
    }
    protected virtual IEnumerator RandomWander() {
        float dirR = Random.Range(0, (float) Math.PI*2);
        Vector2 dir = new Vector2((float) Math.Cos(dirR),(float) Math.Sin(dirR)).normalized;
        rb.velocity = dir*stats.moveSpeed;
        yield return new WaitForSeconds(Random.Range(stats.minMoveLength, stats.maxMoveLength));
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(stats.minWaitTime, stats.maxWaitTime));
        EndMove();
    }
    protected virtual IEnumerator TargetWander(Vector2 targetPos) {
        Vector2 targetDir = (Vector2) transform.position-targetPos;

        float range;
        if (targetDir.y > 0) {
            range = (float) Math.Acos(targetDir.x/targetDir.magnitude) + (float) Math.PI;
        } else {
            range = (float) Math.Acos(-targetDir.x/targetDir.magnitude);
        }

        float dirR = Random.Range(range + (float) Math.PI*(1f-stats.targetPercentLoss/100f), 
        range + (float) Math.PI*2 - (float) Math.PI*(1f-stats.targetPercentLoss/100f));

        Vector2 dir = new Vector2((float) Math.Cos(dirR),(float) Math.Sin(dirR)).normalized;
        rb.velocity = -dir*stats.moveSpeed;
        yield return new WaitForSeconds(Random.Range(stats.minMoveLength, stats.maxMoveLength));
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(stats.minWaitTime, stats.maxWaitTime));
        EndMove();
        EndMove();
    }
    public void Convert(CharacterType type) {
        morality = type switch
            {
                CharacterType.EVILDOER => new EvilMorality(gameObject, EvilColor),
                CharacterType.GOOD => new GoodMorality(gameObject, GoodColor),
                CharacterType.INNOCENT => new InnocentMorality(gameObject, InnocentColor),
                _ => new InnocentMorality(gameObject, InnocentColor),
            };
    }
    protected virtual void StartMove() {
        canMove = false;
        switch (stats.moveType) {
            case MoveType.randomWander: StartCoroutine(RandomWander());
            break;

            case MoveType.targetWander: StartCoroutine(TargetWander(Vector2.zero));
            break;
        }
    }
    protected virtual void EndMove() {
        canMove = true;
    }
    [Serializable]
    public class Stats {
        [SerializeField]
        public float moveSpeed, 
        minMoveLength, maxMoveLength,
        minWaitTime, maxWaitTime,
        targetPercentLoss;

        [SerializeField]
        public CharacterType target {get; set;}
        [SerializeField]
        public CharacterType characterType {get;}
        [SerializeField]
        public MoveType moveType {get; set;}
    }
}