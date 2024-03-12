using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMorality : Morality
{
    public EvilMorality(GameObject character, Color color, bool WasConverted) : base(character, color) {
        charScript.stats.target = Character.CharacterType.INNOCENT;
        charScript.stats.moveType = Character.MoveType.targetWander;
    }
    public EvilMorality(GameObject character, Color color) : base(character, color) {
        charScript.stats.target = Character.CharacterType.INNOCENT;
        charScript.stats.moveType = Character.MoveType.targetWander;
    }
    public override void Death() {
        if (!WasConverted)
            UIScoreController.Instance.AddScore(1000);
        else UIScoreController.Instance.AddScoreNoMult(1000);
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
        other.Convert(Character.CharacterType.EVILDOER);
        UIScoreController.Instance.AddScore(-1000);
    }
}
