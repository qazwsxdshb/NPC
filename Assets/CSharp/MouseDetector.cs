using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDetector : MonoBehaviour {
    public void DialogueBoxClick() {
        SendMessageUpwards("GetPressed", "DialogueBox");
        BroadcastMessage("GetPressed", "DialogueBox");
    }
}
