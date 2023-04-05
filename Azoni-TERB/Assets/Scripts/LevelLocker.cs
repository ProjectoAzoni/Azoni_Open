using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLocker : MonoBehaviour
{
    [SerializeField] GameObject sceneManager;
    SceneController sceneController;
    Animator anim;
    public GameObject message;
    public GameObject confirm;
    public GameObject difficultySelector;
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
    public int difficulty;


    void Start()
    {
        sceneController = sceneManager.GetComponent<SceneController>();
        message.SetActive(true);
        confirm.SetActive(false);
        difficultySelector.SetActive(false);
        isLevelUnlocked = false;
        anim = message.GetComponent<Animator>();
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
            isLevelUnlocked = false;
        }
        else if (score >= thres1)
        {
            star1.sprite = activeStar;
            star2.sprite = inactiveStar;
            star3.sprite = inactiveStar;
            isLevelUnlocked = false;
        }
        else if (score < thres1)
        {
            star1.sprite = inactiveStar;
            star2.sprite = inactiveStar;
            star3.sprite = inactiveStar;
            isLevelUnlocked = false;
        }
    }
 
    private void SetDifficulty(int x)
    {
        difficulty = x;
        Debug.Log(x);
    }
    private int GetDifficulty()
    {
        return difficulty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Cartel", true);
            //anim.SetTrigger("OPEN");
            //message.SetActive(true);
            Debug.Log("si colisiona");   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Cartel", false);
            //anim.SetTrigger("CLOSE");
            //message.SetActive(false);

            Debug.Log("No colisiona");
        }
    }
    private void OnMouseDown()
    {
        if (isLevelUnlocked)
        {
            anim.SetBool("Cartel", false);
            //anim.SetTrigger("CLOSE");
            confirm.SetActive(true);
        }
    }
   public void ButtonYes()
    {
        confirm.SetActive(false);
        difficultySelector.SetActive(true);
    }
    public void ButtonNo()
    {
        confirm.SetActive(false);

    }
    public void ButtonNormal()
    {
        StartCoroutine(Waitfor(tiempo));
        SetDifficulty(1);
    }
    public void ButtonHard()
    {
        StartCoroutine(Waitfor(tiempo));
        SetDifficulty(2);
    }
    IEnumerator Waitfor(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        sceneController.GoToScene(Scene);
    }
}
