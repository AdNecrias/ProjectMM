using System;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class EnemyCategory {

        public EnemyCategory() {
            T = 0;
            R = 1;
            Threat = ThreatLevel.guarded;
        }

        //Total encounters with this category
        public int T;

        //Rarity value of this category
        private float r;
        public float R {
            get { return r; }
            set {
                r = Mathf.Clamp(value, -1, 1);
                Rarity = ComputeRarity();
                if (UpdateVisual_cb != null) UpdateVisual_cb(this);
            }
        }

        public RarityLevel Rarity;
        public ThreatLevel Threat;

        public Action<EnemyCategory> UpdateVisual_cb;

        private RarityLevel ComputeRarity() {
            if (r <= 0.05) return RarityLevel.unique;
            if (r <= 0.15) return RarityLevel.legendary;
            if (r <= 0.20) return RarityLevel.rare;
            if (r <= 0.25) return RarityLevel.uncommon;
            return RarityLevel.common;
        }
    }

}