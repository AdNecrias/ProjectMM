using UnityEngine;
using UnityEngine.UI;
using AdNecriasMeldowMethod;

namespace UI {
    public class UITextUpdaterTotals : MonoBehaviour {
        public Text TotalsText;

        // Update is called once per frame
        void Update() {
            try {
                TotalsText.text = AMMPlayer.instance.GetTotalEnemiesEncountered().ToString();
            } catch (System.Exception) { }
        }
    }
}
