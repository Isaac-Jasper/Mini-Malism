using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    private static CharacterTracker instance;
    public static CharacterTracker Instance {
        get { return instance; }
    }
    private int evilDeathCount = 0;
    [SerializeField]
    private Character[] characterArr;

    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }

        InitializeCharacterArrays();
    }

    private void InitializeCharacterArrays() {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        characterArr = new Character[characters.Length];
        for (int i = 0; i < characters.Length; i++) {
            characterArr[i] = characters[i].GetComponent<Character>();
        }
    }

    public Character[] GetGoodCharacters() {
        Character[] goodArr = new Character[characterArr.Length];
        int index = 0;
        for (int i = 0; i < goodArr.Length; i++) {
            if (characterArr[i].stats.characterType == Character.CharacterType.GOOD) {
                goodArr[index] = characterArr[i];
                index++;
            }
        }
        return goodArr;
    }
    public Character[] GetInnocentCharacters() {
        Character[] innocentArr = new Character[characterArr.Length];
        int index = 0;
        for (int i = 0; i < innocentArr.Length; i++) {
            if (characterArr[i].stats.characterType == Character.CharacterType.INNOCENT) {
                innocentArr[index] = characterArr[i];
                index++;
            }
        }
        return innocentArr;
    }
    public Character[] GetEvilCharacters() {
        Character[] evilArr = new Character[characterArr.Length];
        int index = 0;
        for (int i = 0; i < evilArr.Length; i++) {
            if (characterArr[i] == null) continue;
            if (characterArr[i].stats.characterType == Character.CharacterType.EVILDOER) {
                evilArr[index] = characterArr[i];
                index++;
            }
        }
        return evilArr;
    }
    public int CountEvils() {
        int count = 0;
        for (int i = 0; i < characterArr.Length; i++) {
            if (characterArr[i].stats.characterType == Character.CharacterType.EVILDOER) {
                count++;
            }
        }
        return count;
    }
    public void EvilDeath() {
        evilDeathCount++;
    }
    public bool IsEvilsPurged() {
        if (CountEvils() == evilDeathCount) return true;
        else return false;
    }
}
