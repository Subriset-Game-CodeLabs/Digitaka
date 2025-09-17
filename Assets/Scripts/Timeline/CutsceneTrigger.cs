using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField]
    private string _cutsceneId;

    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private TimelineRuntimeBinder _binder;

    [SerializeField]
    private bool _playOnStart;


    private void Start()
    {
        if (_playOnStart)
        {
            if (CutsceneManager.Instance.HasPlayed(_cutsceneId))
            {
                gameObject.SetActive(false);
                return;
            }
            if (_binder)
                _binder.BindTracks();
            _director.Play();
            CutsceneManager.Instance.MarkAsPlayed(_cutsceneId);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CutsceneManager.Instance.HasPlayed(_cutsceneId))
            {
                gameObject.SetActive(false);
                return;
            }
            if (_binder)
                _binder.BindTracks();
            _director.Play();
            CutsceneManager.Instance.MarkAsPlayed(_cutsceneId);
        }
    }
}
