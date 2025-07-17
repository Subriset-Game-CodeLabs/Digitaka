using UnityEngine;

namespace TwoDotFiveDimension
{
    public abstract class IStateCombat
    {
        protected float time { get; set; }
        protected float fixedtime { get; set; }
        protected float latetime { get; set; }
        
        public  abstract void OnEnter();
        public abstract void OnExit();
        public virtual void OnUpdate()
        {
            time += Time.deltaTime;
        }
        public virtual void OnFixedUpdate()
        {
            fixedtime += Time.deltaTime;
        }
        public virtual void OnLateUpdate()
        {
            latetime += Time.deltaTime;
        }
    }
}