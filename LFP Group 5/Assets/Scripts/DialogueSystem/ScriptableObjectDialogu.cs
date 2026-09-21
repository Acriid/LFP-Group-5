using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Dialogue/Dialogue")]
public class ScriptableObjectDialogu : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public Sprite _characterIcon;

        [TextArea(3,10)]
        public string line;
    }

    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
