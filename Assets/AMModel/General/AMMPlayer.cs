﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMMPlayer : MonoBehaviour {
        public static AMMPlayer instance;

        [Space(10)]
        [Header("Adnecrias-Meldow Personality Model")]

        [SerializeField]
        [Tooltip("Novelty Seeking Parameter.")]
        [Range(-1, 1)]
        private float NS;

        [SerializeField]
        [Tooltip("Harm Avoidance Parameter.")]
        [Range(-1, 1)]
        private float HA;

        [SerializeField]
        [Tooltip("Reward Dependence Parameter.")]
        [Range(-1, 1)]
        private float RD;

        [Space(10)]
        [Header("Other")]
        [Tooltip("List that records all the encounters the player had during the progression. This values should be stored when changing scenes.")]
        public Dictionary<AMMEnemyType, AMMEnemyCategory> NSList = new Dictionary<AMMEnemyType, AMMEnemyCategory>();

        [SerializeField]
        private int TotalEnemiesEncountered = 0;

        public void RegisterUpdateVisualsCallback(AMMEnemyType type, Action<AMMEnemyCategory> action) {
            if (!NSList.ContainsKey(type)) NSList.Add(type, new AMMEnemyCategory());
            NSList[type].UpdateVisual_cb += action;
        }

        public void UnregisterUpdateVisualCallback(AMMEnemyType type, Action<AMMEnemyCategory> action) {
            NSList[type].UpdateVisual_cb -= action;
        }

        public void singleton() {
            if (instance != null) {
                Destroy(this);
                return;
            }
            instance = this;
        }

        void Awake() {
            singleton();
        }

        void OnTriggerEnter(Collider other) {
            //Check if found an Enemy
            if (other.tag == AMMUtils.instance.EnemyTag) {

                //Updates total enemies encountered amount
                TotalEnemiesEncountered += 1;

                //Increment type total
                var type = other.GetComponentInParent<AMMEnemy>().Type;
                NSList[type].T++;

                //Update Rarity
                foreach (var ec in NSList) {
                    ec.Value.R = ec.Value.T / TotalEnemiesEncountered;

                    //Update own rarity
                    //if (ec.Key == type) {
                    //    //decrement ec.R based on ec.T
                    //    ec.Value.R += 0.1f;
                    //}
                    ////Update all others rarity
                    //else {
                    //    //increment ec.R based on ec.T
                    //    ec.Value.R += 0.1f;
                    //}
                }
            }
        }

        #region Getters and Setters
        public float GetNS() { return NS; }
        public float GetHA() { return HA; }
        public float GetRD() { return RD; }
        public float GetTotalEnemiesEncountered() { return TotalEnemiesEncountered; }

        public void SetNS(float value) { NS = value; }
        public void SetHA(float value) { HA = value; }
        public void SetRD(float value) { RD = value; }
        #endregion
    }
}