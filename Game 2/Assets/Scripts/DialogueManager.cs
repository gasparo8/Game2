using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI dialogueText;
    public float delayBeforeNextSentence = 2.0f;
    public float delayAfterLastSentence = 1.0f; // Additional delay for the last sentence
    public float typingSpeed = .06f; // Delay between each character

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StartCoroutine(WaitAndClearDialogue());
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Delay between each letter
        }

        // Wait for the delay before displaying the next sentence
        yield return new WaitForSeconds(delayBeforeNextSentence);

        DisplayNextSentence();
    }

    IEnumerator WaitAndClearDialogue()
    {
        // Wait for the additional delay after the last sentence
        yield return new WaitForSeconds(delayAfterLastSentence);

        // Clear the dialogue text
        dialogueText.text = "";
        EndDialogue();
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }
}