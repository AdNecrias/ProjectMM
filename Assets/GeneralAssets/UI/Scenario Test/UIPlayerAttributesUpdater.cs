using UnityEngine;
using AdNecriasMeldowMethod;
using UnityEngine.UI;

namespace UI {
    public class UIPlayerAttributesUpdater : MonoBehaviour {

        public Slider NsSlider;
        public Text NsSliderText;
        public Slider HaSlider;
        public Text HaSliderText;
        public Slider RdSlider;
        public Text RdSliderText;

        private AMMPlayer player;

        // Use this for initialization
        void Start() {
            player = AMMPlayer.instance;

            NsSlider.value = player.GetNS();
            HaSlider.value = player.GetHA();
            RdSlider.value = player.GetRD();
        }

        public void UpdatedNS() {
            NsSliderText.text = NsSlider.value.ToString();
            player.SetNS(NsSlider.value);
        }

        public void UpdateHaSlider() {
            HaSliderText.text = HaSlider.value.ToString();
            player.SetHA(HaSlider.value);
        }

        public void UpdateRdSlider() {
            RdSliderText.text = RdSlider.value.ToString();
            player.SetRD(RdSlider.value);
        }
    }
}
