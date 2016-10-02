using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMMPlayer : MonoBehaviour {
        public static AMMPlayer instance;

        //Not currently used
        public delegate void EntitySighted();
        public static event EntitySighted OnEntitySighted;

        public delegate void EntityInteracted(EntityType entityType, ThreatLevel threatLevel);
        public static event EntityInteracted OnEntityInteracted;

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
        public Dictionary<EntityType, EnemyCategory> EntityList = new Dictionary<EntityType, EnemyCategory>();

        private int TotalEntitiesEncountered = 0;

        public void RegisterUpdateVisualsCallback(EntityType type, Action<EnemyCategory> action) {
            if (!EntityList.ContainsKey(type)) EntityList.Add(type, new EnemyCategory());
            EntityList[type].UpdateVisual_cb += action;
        }

        public void UnregisterUpdateVisualCallback(EntityType type, Action<EnemyCategory> action) {
            EntityList[type].UpdateVisual_cb -= action;
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
            if (Utils.isEnemy(other.tag)) {
                //Updates total enemies encountered amount
                TotalEntitiesEncountered += 1;

                //Increment type total
                var type = other.GetComponentInParent<AMMEnemy>().Type;
                EntityList[type].T++;

                //Update Rarity
                foreach (var ec in EntityList) {
                    ec.Value.R = (float)ec.Value.T / TotalEntitiesEncountered;

                    //Set default values if previouse met
                    if (OnEntityInteracted != null) OnEntityInteracted(type, ec.Value.Threat);
                }
            }
        }

        #region Getters and Setters
        public float GetNS() { return NS; }
        public float GetHA() { return HA; }
        public float GetRD() { return RD; }
        public int GetTotalEnemiesEncountered() { return TotalEntitiesEncountered; }

        public void SetNS(float value) { NS = value; }
        public void SetHA(float value) { HA = value; }
        public void SetRD(float value) { RD = value; }
        #endregion

        private static void OnOnEntitySighted() {
            var handler = OnEntitySighted;
            if (handler != null) handler();
        }

        public static void ExecuteOnEntityInteracted(EntityType entityType, ThreatLevel threatLevel)
        {
            AMMPlayer.instance.EntityList[entityType].Threat = threatLevel;
            if (OnEntityInteracted != null) OnEntityInteracted(entityType, threatLevel);
        }
    }
}