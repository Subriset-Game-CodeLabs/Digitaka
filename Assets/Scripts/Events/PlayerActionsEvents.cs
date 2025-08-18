using System;

namespace Events
{
    public class PlayerActionsEvents
    {
        public event Action OnDashPerformed;
        public void DashPerformed()
        { 
            OnDashPerformed?.Invoke();
        }
        public event Action OnUltimatePerformed;
        public void UltimatePerformed()
        { 
            OnUltimatePerformed?.Invoke();
        }
        public event Action OnHealthPotionUsed;
        public void HealthPotionUsed()
        { 
            OnHealthPotionUsed?.Invoke();
        }
        public event Action OnManaPotionUsed;
        public void ManaPotionUsed()
        { 
            OnManaPotionUsed?.Invoke();
        }
    }
}