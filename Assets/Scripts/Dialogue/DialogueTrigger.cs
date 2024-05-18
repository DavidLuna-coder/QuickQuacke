using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueTrigger nextDialogue;
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue, this);
    }

    public void TriggerNextDialogue()
    {
        if (nextDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(nextDialogue.dialogue,this.nextDialogue);
        }
    }
}
