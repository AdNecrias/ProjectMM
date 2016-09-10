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
                if (UpdateVisual_cb != null) UpdateVisual_cb(this);
            }
        }

        public Action<AMMEnemyCategory> UpdateVisual_cb;

        //public override string ToString() {
        //    return "T - " + T + " | R - " + R;
        //}
    }

}