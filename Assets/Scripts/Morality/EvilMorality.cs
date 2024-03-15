using UnityEngine;

public class EvilMorality : Morality
{
    public EvilMorality(GameObject character, Color color, bool WasConverted) : base(character, color) {
        charScript.stats.target = Character.CharacterType.INNOCENT;
        charScript.stats.moveType = Character.MoveType.targetWander;
        if (WasConverted) {
            UIScoreController.Instance.AddScore(-1000);
        }
    }
    public EvilMorality(GameObject character, Color color) : base(character, color) {
        charScript.stats.target = Character.CharacterType.INNOCENT;
        charScript.stats.moveType = Character.MoveType.targetWander;
    }
    public override void Death() {
        if (!WasConverted)
            UIScoreController.Instance.AddScore(1000);
        else UIScoreController.Instance.AddScoreNoMult(1500);
        CharacterTracker.Instance.EvilDeath();
        GameController.Instance.CheckIfWin();
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
    }
}
