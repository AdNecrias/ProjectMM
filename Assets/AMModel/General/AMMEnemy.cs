using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace AdNecriasMeldowMethod {
    public class AMMEnemy : MonoBehaviour {

        public AMMEnemyType Type;
        protected List<Renderer> RendererLstList;
        protected AMMPlayer player;

        void Start() {
            RendererLstList = GetComponentsInChildren<Renderer>().ToList();
            player = AMManager.AMMPlayer;
        }

        void OnEnable() {
            AMManager.AMMPlayer.RegisterUpdateVisualsCallback(Type, UpdateVisual);
        }

        void OnDisable() {
            AMManager.AMMPlayer.UnregisterUpdateVisualCallback(Type, UpdateVisual);
        }

        protected virtual void UpdateVisual(AMMEnemyCategory R) { }
    }
}