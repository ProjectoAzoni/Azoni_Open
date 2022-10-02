using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogueManager : MonoBehaviour
{
    [SerializeField,TextArea(4,6)] private string[] dialogueLinesLocked;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLinesUnlocked;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] GameObject sceneManager;
    SceneController sceneController;
    [SerializeField] GameObject ControlCanvas;
    TouchControlManager2d touchControlManager2d;

    private bool isPlayerInRange;
    private bool didDialougeStart;
    public bool isLevelUnlocked;
    public bool isUiButtonPressed;
    public float tiempo;
    public string Scene;
    private int index;
    public float typingTime = 0.05f;

    void Start()
    {
        dialoguePanel.SetActive(false);
        sceneController = sceneManager.GetComponent<SceneController>();
    
        touchControlManager2d = ControlCanvas.GetComponent<TouchControlManager2d>();

    }


    void Update()
    {
        isUiButtonPressed = touchControlManager2d.GetIsActionButtonPressed();
        


        if (isPlayerInRange && IsBottonPushed() && !isLevelUnlocked)
        {
            if (!didDialougeStart)
            {
                StartDialouge(dialogueLinesLocked);
            }
            else if (dialogueText.text == dialogueLinesLocked[index])
            {
                NextDialogueLine(dialogueLinesLocked);
            }
        }
        else if (isPlayerInRange && IsBottonPushed() && isLevelUnlocked)
        {
            if (!didDialougeStart)
            {
                StartDialouge(dialogueLinesUnlocked);
            }
            else if (dialogueText.text == dialogueLinesUnlocked[index])
            {
                NextDialogueLine(dialogueLinesUnlocked);
            }
        } 
    }


    private bool IsBottonPushed()
    {
        bool pushed;
        if (Input.GetButtonDown("Jump") || touchControlManager2d.GetIsActionButtonPressed())
        {
            pushed = true;
            
        }else
        {
            pushed = false;
        }
        
        return pushed;
    }

  

    private void StartDialouge(string[] dialogue)
    {
        didDialougeStart = true;
        dialoguePanel.SetActive(true);
        index = 0;
        StartCoroutine(ShowLine(dialogue));
    }


    private void NextDialogueLine(string[] dialogue)
    {
        index++;

        if (index < dialogue.Length)
        {
            StartCoroutine(ShowLine(dialogue));
        }
        else 
        {
            didDialougeStart = false;
            dialoguePanel.SetActive(false);
            if (isLevelUnlocked)
            {
                StartCoroutine(Waitfor(tiempo));
            }
        }
        
    }

    private IEnumerator ShowLine(string[] dialogue)
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogue[index])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    IEnumerator Waitfor(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        sceneController.GoToScene(Scene);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("si colisiona");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("No colisiona");
        }
    }
}
