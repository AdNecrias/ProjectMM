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
	}
	
	// Update is called once per frame
	void Update () {
        Debug.LogError("HUDManager listening for key T. Remove me when not needed.");
	    if(Input.GetKeyDown(KeyCode.T)) {
            AddEntry("Lorem Ipquaquelcoisa", 5);
        }
	}

    void AddEntry(string text, float lifetime) {
        AddEntry(text, Color.white, lifetime);
    }

    void AddEntry(string text, Color color, float lifetime) {
        ActiveQuestLogs.Add(new QuestLog(text));
        GameObject go = (GameObject) Instantiate(QuestLogTextPrefab);
        go.name = text;
        go.transform.parent = QuestLogPanel.transform;

        Text textComponent = (Text) go.GetComponent<Text>();
        textComponent.text = text;
        textComponent.color = color;

        FadeTextOverLifetime fadeComponent = (FadeTextOverLifetime) go.GetComponent<FadeTextOverLifetime>();
        fadeComponent.Lifetime = lifetime;

        Destroy(go, lifetime);
        
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
