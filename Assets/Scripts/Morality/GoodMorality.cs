using UnityEngine;

public class GoodMorality : Morality
{
    public GoodMorality(GameObject character, Color color, bool WasConverted) : base(character, color) { }
    public GoodMorality(GameObject character, Color color) : base(character, color) { }
    int MAX_GOOD_POINTS = 50;
    int pointsGiven = 0;
    public override void Death() {
        GameController.Instance.LoseGreenDeath();
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
        pointsGiven++;
        if (pointsGiven < MAX_GOOD_POINTS)
            UIScoreController.Instance.AddScore(10);
    }
}
