using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLocker : MonoBehaviour
{
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public bool isLevelUnlocked;



    void Update()
    {
        if (isLevelUnlocked)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unlockedSprite;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = lockedSprite;
        }
    }
}
