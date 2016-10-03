using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour {

    public int HP = 100;
    public Object EquippedWeapon;

    public List<Object> AvailableItems;
    public Object SelectedItem;

    public QuestLog[] ArchivedQuestLogs;
    public List<QuestLog> ActiveQuestLogs;

    public GameObject QuestLogPanel;
    public GameObject QuestLogTextPrefab;


    // Use this for initialization
    void Start () {
        ActiveQuestLogs = new List<QuestLog>();
        AvailableItems = new List<Object>();

        Debug.LogError("HUDManager listening for key T. Remove me when not needed.");
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.T)) {
            AddEntry("Lorem Ipquaquelcoisa", 5);
        }
	}

    /// <summary>
    /// Adds a new entry to the quest log with the default color of white.
    /// </summary>
    /// <param name="text">Text to appear in text log.</param>
    /// <param name="lifetime">Lifetime to fade and delete itself. Value of 0 will disable deletion and fading, you are responsible for deleting the log yourself.</param>
    /// <returns>Returns the new text entry's GameObject, so you can delete it earlier or manipulate it. </returns>
    GameObject AddEntry(string text, float lifetime) {
        return AddEntry(text, Color.white, lifetime);
    }

    /// <summary>
    /// Adds a new entry to the quest log.
    /// </summary>
    /// <param name="text">Text to appear in text log.</param>
    /// <param name="color">Color the text should appear in.</param>
    /// <param name="lifetime">Lifetime to fade and delete itself. Value of 0 will disable deletion and fading, you are responsible for deleting the log yourself.</param>
    /// <returns>Returns the new text entry's GameObject, so you can delete it earlier or manipulate it. </returns>
    GameObject AddEntry(string text, Color color, float lifetime) {
        ActiveQuestLogs.Add(new QuestLog(text));
        GameObject go = (GameObject) Instantiate(QuestLogTextPrefab);
        go.name = text;
        go.transform.parent = QuestLogPanel.transform;

        Text textComponent = (Text) go.GetComponent<Text>();
        textComponent.text = text;
        textComponent.color = color;

        FadeTextOverLifetime fadeComponent = (FadeTextOverLifetime) go.GetComponent<FadeTextOverLifetime>();
        fadeComponent.Lifetime = lifetime;

        if (lifetime == 0) {
            Destroy(fadeComponent);
        } else {
            Destroy(go, lifetime);
        }
        return go;
        
    }
}

public class QuestLog {
    public string Text;
    public Color TextColor = Color.white;
    public float PostTime;

    public QuestLog(string text) {
        Text = text;
        PostTime = Time.time;
    }

    public QuestLog(string text, Color color) {
        Text = text;
        TextColor = color;
        PostTime = Time.time;
    }
}
