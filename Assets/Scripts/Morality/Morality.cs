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
    protected GameObject character;
    protected Character charScript;
    public bool WasConverted = false;
    public Morality(GameObject character, Color color) {
        this.character = character;
        charScript = character.GetComponent<Character>();
        this.color = color;
        SetColor();
    }
    
    public Morality(GameObject character, Color color, bool converted) {
        WasConverted = converted;
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
