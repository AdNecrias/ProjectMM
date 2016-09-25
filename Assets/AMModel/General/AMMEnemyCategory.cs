using System;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMMEnemyCategory {

        public AMMEnemyCategory() {
            T = 0;
            R = 1;
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

        public AMMRarityLevel Rarity;

        public Action<AMMEnemyCategory> UpdateVisual_cb;

        private AMMRarityLevel ComputeRarity() {
            if (r <= 0.05) return AMMRarityLevel.unique;
            if (r <= 0.15) return AMMRarityLevel.legendary;
            if (r <= 0.20) return AMMRarityLevel.rare;
            if (r <= 0.25) return AMMRarityLevel.uncommon;
            return AMMRarityLevel.common;
        }

        //public override string ToString() {
        //    return "T - " + T + " | R - " + R;
        //}
    }

}