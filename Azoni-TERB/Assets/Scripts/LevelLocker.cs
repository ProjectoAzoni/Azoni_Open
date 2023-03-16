using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLocker : MonoBehaviour
{
    [SerializeField] GameObject sceneManager;
    SceneController sceneController;
    public GameObject message;
    public Image star1;
    public Image star2;
    public Image star3;
    public float maxScore;
    public float score;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite activeStar;
    public Sprite inactiveStar;
    public float tiempo;
    public string Scene;
    public bool isLevelUnlocked;


    void Start()
    {
        sceneController= sceneController = sceneManager.GetComponent<SceneController>();
        message.SetActive(false);
        isLevelUnlocked = false;
    }
    void Update()
    {
        Score(maxScore, score);

        if (isLevelUnlocked)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unlockedSprite;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = lockedSprite;
        }
    }

    void Score(float maxScore,float score)
    {
        double thres1 = maxScore * 0.5;
        double thres2 = maxScore * 0.75;
        if (maxScore == score)
        {
            star1.sprite = activeStar;
            star2.sprite = activeStar;
            star3.sprite = activeStar;
            isLevelUnlocked = true;
        }else if (score >= thres2)
        {
            star1.sprite = activeStar;
            star2.sprite = activeStar;
            star3.sprite = inactiveStar;
        }else if (score >= thres1)
        {
            star1.sprite = activeStar;
            star2.sprite = inactiveStar;
            star3.sprite = inactiveStar;
        }
        else if (score < thres1)
        {
            star1.sprite = inactiveStar;
            star2.sprite = inactiveStar;
            star3.sprite = inactiveStar;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            message.SetActive(true);
            Debug.Log("si colisiona");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            message.SetActive(false);

            Debug.Log("No colisiona");
        }
    }
    private void OnMouseDown()
    {
        if (isLevelUnlocked)
        {
           StartCoroutine(Waitfor(tiempo));
        }
    }


    IEnumerator Waitfor(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        sceneController.GoToScene(Scene);
    }
}
