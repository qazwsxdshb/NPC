using CSharp.State;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CSharp.Scene {
    public class LoadingScene : MonoBehaviour {
        public Text loadingText;

        void Start() {
            loadingText.
                DOColor(Color.black, 1).
                SetLoops(Random.Range(3,6), LoopType.Yoyo).
                OnComplete(() => Util.Scene.SwitchScene(GameScene.MainScene));
        }
    }
}
