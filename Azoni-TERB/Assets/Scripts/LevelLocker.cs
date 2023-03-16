using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLocker : MonoBehaviour
{
    [SerializeField] GameObject sceneManager;
    SceneController sceneController;
    public GameObject message;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public float tiempo;
    public string Scene;
    public bool isLevelUnlocked;


    void Start()
    {
        sceneController= sceneController = sceneManager.GetComponent<SceneController>();
    }
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
