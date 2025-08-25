using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI dialogueText;
    public float delayBeforeNextSentence = 2.0f;
    public float delayAfterLastSentence = 1.0f; // Additional delay after last sentence
    public float typingSpeed = 0.06f; // Delay between each character

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] textBlips;   // Assign 3 blip sounds in the inspector
    public float pitchRandomness = 0.05f; // Optional pitch variation

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
            StopAllCoroutines();
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

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i];

            // Play blip for all letters except the last two
            if (i < sentence.Length - 4 && textBlips.Length > 0 && audioSource != null)
            {
                int index = Random.Range(0, textBlips.Length);
                audioSource.clip = textBlips[index];
                audioSource.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
                audioSource.Play();
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait before next sentence
        yield return new WaitForSeconds(delayBeforeNextSentence);

        DisplayNextSentence();
    }

    IEnumerator WaitAndClearDialogue()
    {
        // Wait after last sentence
        yield return new WaitForSeconds(delayAfterLastSentence);

        dialogueText.text = "";

        // Stop any lingering blip immediately
        if (audioSource != null)
            audioSource.Stop();

        EndDialogue();
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }
}
