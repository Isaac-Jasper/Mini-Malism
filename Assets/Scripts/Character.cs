using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    [SerializeField]
    public Stats stats;
    [SerializeField]
    protected Color EvilColor, GoodColor, InnocentColor;
    protected Morality morality;
    [SerializeField]
    protected Rigidbody2D rb;
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
        Convert(stats.characterType);
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Squished() {
        morality.Death();
        Destroy(gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.transform.CompareTag("Character")) return;

        Character other = collision.gameObject.GetComponent<Character>();
        switch (other.stats.characterType) {
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
        if (GameController.Instance.IsTerminal()) {
            Stop();
            yield break;
        }
        float dirR = Random.Range(0, (float) Math.PI*2);
        Vector2 dir = new Vector2((float) Math.Cos(dirR),(float) Math.Sin(dirR)).normalized;
        rb.velocity = dir*stats.moveSpeed;
        yield return new WaitForSeconds(Random.Range(stats.minMoveLength, stats.maxMoveLength));
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(stats.minWaitTime, stats.maxWaitTime));
        EndMove();
    }
    protected virtual IEnumerator TargetWander() {
        if (GameController.Instance.IsTerminal()) {
            Stop();
            yield break;
        }
        Vector2 target = GetTarget();
        if (target == (Vector2) transform.position) {
            StartCoroutine(RandomWander());
            yield break;
        }

        Vector2 targetDir = (Vector2) transform.position-target;

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
    }

    protected void Stop() {
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }

    protected virtual Vector2 GetTarget() {
        Character[] potentialTargets = null;
        switch (stats.target) {
            case CharacterType.INNOCENT: potentialTargets = CharacterTracker.Instance.GetInnocentCharacters();
            break;
            case CharacterType.GOOD: potentialTargets = CharacterTracker.Instance.GetGoodCharacters();
            break;
            case CharacterType.EVILDOER: potentialTargets = CharacterTracker.Instance.GetEvilCharacters();
            break;
            default: CharacterTracker.Instance.GetInnocentCharacters();
            break;
        }
        Vector2 closestTarget = transform.position;
        float closestDirection = 1000000;
        for (int i = 0; i < potentialTargets.Length; i++) {
            if (potentialTargets[i] == null) break;
            float dist = (transform.position - potentialTargets[i].transform.position).magnitude;
            if (dist <= closestDirection) {
                closestDirection = dist;
                closestTarget = potentialTargets[i].transform.position;
            }
        }
        return closestTarget;
    }
    public void Convert(CharacterType type) {
        morality = type switch
            {
                CharacterType.EVILDOER => new EvilMorality(gameObject, EvilColor, true),
                CharacterType.GOOD => new GoodMorality(gameObject, GoodColor, true),
                CharacterType.INNOCENT => new InnocentMorality(gameObject, InnocentColor, true),
                _ => new InnocentMorality(gameObject, InnocentColor, true),
            };
        stats.characterType = type;
        morality.WasConverted = true;
    }
    protected virtual void StartMove() {
        canMove = false;
        switch (stats.moveType) {
            case MoveType.randomWander: StartCoroutine(RandomWander());
            break;

            case MoveType.targetWander: StartCoroutine(TargetWander());
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
        public CharacterType target;
        [SerializeField]
        public CharacterType characterType;
        [SerializeField]
        public MoveType moveType;
    }
}