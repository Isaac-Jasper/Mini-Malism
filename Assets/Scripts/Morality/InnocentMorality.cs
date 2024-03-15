using UnityEngine;

public class InnocentMorality : Morality
{
    public InnocentMorality(GameObject character, Color color, bool WasConverted) : base(character, color) { }
    public InnocentMorality(GameObject character, Color color) : base(character, color) { }

    public override void Death() {
        UIScoreController.Instance.AddScore(-100);
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
