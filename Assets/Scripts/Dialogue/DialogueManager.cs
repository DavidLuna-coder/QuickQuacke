using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using uLipSync;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public static DialogueManager Instance;
    public Queue<Sentence> sentences = new();
    public Image CharacterImage;
    public MeshRenderer CharacterMouth;
    public uLipSync.uLipSync uLipSync;
    private AudioSource audioSource;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
        sentences.Clear();
        foreach (Sentence sentence in dialogue.sentences)
        {
            Debug.Log("Enqueing: " + sentence.sentence);
            sentences.Enqueue(sentence);
        }

        CharacterMouth.material = dialogue.mouthSprite;
        dialogue.uLipSyncTexture.targetRenderer = CharacterMouth;
        uLipSync.onLipSyncUpdate.RemoveAllListeners();
        uLipSync.onLipSyncUpdate.AddListener(dialogue.uLipSyncTexture.OnLipSyncUpdate);

        DisplayNextSentence(dialogue, trigger);
    }

    public void DisplayNextSentence(Dialogue dialogue, DialogueTrigger trigger)
    {
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogueCoroutine(dialogue, trigger));
            return;
        }
        audioSource = null;
        nameText.text = dialogue.name;
        Sentence sentence = sentences.Dequeue();
        CharacterImage.sprite = dialogue.expressionsSprites.Find(x => x.expression == sentence.expression).sprite;
        

        Debug.Log(sentence.sentence);
        //StopAllCoroutines();
        audioSource = uLipSync.gameObject.GetComponent<AudioSource>();
        audioSource.clip = sentence?.audio;
        audioSource.Play();
        StartCoroutine(TypeSentence(sentence.sentence, dialogue, trigger));
    }

    IEnumerator TypeSentence(string sentence, Dialogue dialogue, DialogueTrigger trigger)
    {
        dialogueText.text = "";
        Debug.Log("Typing: " + sentence);
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(EndSentence(dialogue, trigger));
    }
    void EndDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        //animator.SetBool("IsOpen", false);
        if(trigger.nextDialogue!=null)
        {
            CharacterImage.sprite = null;
            Destroy(CharacterMouth.material);
            dialogue.uLipSyncTexture.targetRenderer = null;
        }
        trigger.TriggerNextDialogue();
        Debug.Log("End of conversation");
    }

    IEnumerator EndDialogueCoroutine(Dialogue dialogue, DialogueTrigger trigger)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        EndDialogue(dialogue, trigger);
    }

    IEnumerator EndSentence(Dialogue dialogue, DialogueTrigger trigger)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        DisplayNextSentence(dialogue, trigger);
    }
}
