using UnityEngine;

namespace TwoDotFiveDimension
{
    public enum TeamIndex
    {
        None = -1,
        Neutral = 0,
        Player,
        Enemy,
        Count
    }

    public class TeamComponent : MonoBehaviour
    {
        [SerializeField] private TeamIndex _teamIndex = TeamIndex.None;

        public TeamIndex TeamIndex
        {
            set
            {
                if (_teamIndex == value)
                {
                    return;
                }

                _teamIndex = value;
            }
            get
            {
                return _teamIndex;
            }
        }
    }
}