using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using static Character;
public abstract class Morality
{
    [SerializeField]
    protected Color color;
    [SerializeField]
    GameObject character;
    public Morality(GameObject character, Color color) {
        this.character = character;
        this.color = color;
        SetColor();
    }
    protected virtual void SetColor() {
        character.GetComponent<SpriteRenderer>().color = color;
    }
    public abstract void EvilCollide(Character other); 
    public abstract void InnocentCollide(Character other); 
    public abstract void GoodCollide(Character other); 
    public abstract void Death();
}
