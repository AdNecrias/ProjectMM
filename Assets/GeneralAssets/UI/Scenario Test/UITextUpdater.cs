﻿using System;
using UnityEngine;
using AdNecriasMeldowMethod;
using UnityEngine.UI;

namespace UI {
    public class UITextUpdater : MonoBehaviour {
        public EntityType EntityType;
        public Text TotalsText;
        public Text RarityText;
        public Text ThreatText;

        private AMMPlayer Player;
        // Use this for initialization
        void Start() {
            Player = AMMPlayer.instance;
        }

        // Update is called once per frame
        void Update() {
            try {
                TotalsText.text = Player.EntityList[EntityType].T.ToString();
                RarityText.text = Player.EntityList[EntityType].R.ToString();
            } catch (Exception e) {

            }
        }
    }

}

