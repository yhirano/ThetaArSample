using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	private static UIController instance;
	
	public static UIController Instance {
		get { return instance; }
	}

	public Text messageBox;

	public string MessageBoxText {
		get { return messageBox.text; }
		set { messageBox.text = value; }
	}

	void Awake() {
		instance = this;
	}
}
