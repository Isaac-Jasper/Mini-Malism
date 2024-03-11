using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentMorality : Morality
{
    public InnocentMorality(GameObject character, Color color) : base(character, color) { }

    public override void Death() {
        return;
        //do nothing
    }

    public override void EvilCollide(Character other) {
        return;
        //do nothing
    }

    public override void GoodCollide(Character other) {
        return;
        //do nothing
    }

    public override void InnocentCollide(Character other) {
        return;
        //do nothing
    }
}
