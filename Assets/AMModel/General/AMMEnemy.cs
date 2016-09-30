using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMMEnemy : MonoBehaviour {

        public EntityType Type;
        protected List<Renderer> RendererLstList;

        void Start() {
            RendererLstList = GetComponentsInChildren<Renderer>().ToList();
        }

        void OnEnable() {
            AMMPlayer.instance.RegisterUpdateVisualsCallback(Type, UpdateVisual);
        }

        void OnDisable() {
            AMMPlayer.instance.UnregisterUpdateVisualCallback(Type, UpdateVisual);
        }

        protected virtual void UpdateVisual(EnemyCategory R) { }
    }
}