using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogueManager : MonoBehaviour
{
    [SerializeField,TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private bool isPlayerInRange;
    private bool didDialougeStart;
    private int index;

    public float typingTime = 0.05f;
    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Jump"))
        {
            if (!didDialougeStart)
            {
                StartDialouge();
            }
            else if (dialogueText.text == dialogueLines[index])
            {
                NextDialogueLine();
            }
        }
    }
    private void StartDialouge()
    {
        didDialougeStart = true;
        dialoguePanel.SetActive(true);
        index = 0;
        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine()
    {
        index++;
        if (index < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialougeStart = false;
            dialoguePanel.SetActive(false);
        }

    }
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[index])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
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
