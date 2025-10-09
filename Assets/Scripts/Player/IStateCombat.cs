using UnityEngine;

namespace TwoDotFiveDimension
{
    public abstract class IStateCombat
    {
        protected float time { get; set; }
        protected float fixedtime { get; set; }
        protected float latetime { get; set; }
        public virtual void OnEnter(ComboCharacter owner)
        {
            // Reset timer setiap kali masuk state baru
            time = 0f;
            fixedtime = 0f;
        }

        public virtual void OnExit(ComboCharacter owner) { }

        public virtual void OnUpdate(ComboCharacter owner)
        {
            time += Time.deltaTime;
        }

        public virtual void OnFixedUpdate(ComboCharacter owner)
        {
            // Menggunakan Time.fixedDeltaTime untuk akurasi di FixedUpdate
            fixedtime += Time.fixedDeltaTime;
        }

        public virtual void OnLateUpdate(ComboCharacter owner) { }
    }
}