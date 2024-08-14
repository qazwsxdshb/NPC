using System;
using System.Collections.Generic;
using CSharp.Model;
using CSharp.Util;

namespace CSharp.Controller {
    public class DramaEventArgs : EventArgs {
        public Drama Drama { get; }

        public DramaEventArgs(Drama drama) { Drama = drama; }
    }

    public class DramaController {
        private static DramaController instance;
        private static List<Node> _scenes;
        private static DramaPath _selectedDp;

        public Drama drama;

        // Events
        public event EventHandler<DramaEventArgs> OnDramaChanged;
        public event EventHandler<DramaEventArgs> OnSceneChanged;
        private DramaController() { }

        public static DramaController Instance {
            get { return instance ??= new DramaController(); }
        }

        public void Initialize(DramaPath dp) {
            _selectedDp = dp;
            LoadDrama(dp);

            // Notify subscribers that the scene has changed
            OnDramaChanged?.Invoke(this, new DramaEventArgs(drama));
        }

        private void LoadDrama(DramaPath dp) {
            drama = new Drama(
                XmlDeserializer.LoadDrama(dp)
            );
        }

        private void NextScene(string next) {
            drama.NextScene(next);
            OnSceneChanged?.Invoke(this, new DramaEventArgs(drama));
        }
        
        public void Restart() {
            LoadDrama(_selectedDp);
            OnDramaChanged?.Invoke(this, new DramaEventArgs(drama));
        }

        public void NextDrama() {
            drama.NextDrama();
            OnDramaChanged?.Invoke(this, new DramaEventArgs(drama));
        }

        public void MakeChoice(string choice) {
            drama.MakeChoice(choice);
            OnDramaChanged?.Invoke(this, new DramaEventArgs(drama));
        }
    }
}
