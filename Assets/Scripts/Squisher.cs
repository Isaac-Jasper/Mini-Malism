using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squisher : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed, followSpeed;
    private float circleRadius;
    public void Awake() {
        circleRadius = GetComponent<CircleCollider2D>().radius;
    }
    public void OnEnable() {
        SubscribeToInputs();
    }
    public void OnDisable() {
        UnSubscribeToInputs();
    }
    private void SubscribeToInputs() {
        InputManager.Instance.OnGameplayClickStarted += OnClick;
    }
    private void UnSubscribeToInputs() {
        InputManager.Instance.OnGameplayClickStarted -= OnClick;
    }
    public void OnClick(Vector2 clickPos, float time) {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, circleRadius * transform.localScale.x, LayerMask.GetMask("Character"));
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i] == null) continue;
            hitColliders[i].GetComponent<Character>().Squished();
        }
    }
    public void Update() {
        Move();
        transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);
    }
    private void Move() {
        transform.position = Vector2.Lerp(transform.position, InputManager.Instance.GetClickWorldPosition(), followSpeed * Time.deltaTime);
    }
}