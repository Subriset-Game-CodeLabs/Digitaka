using System.Collections;
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

    private GameObject _player;
    private void Start()
    {
        if (_playOnStart)
        {
            PlayCutscene();
        }
    }
    void OnEnable()
    {
        GameEventsManager.Instance.CutsceneEvents.OnPlayCutscene += OnPlayCutscene;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.CutsceneEvents.OnPlayCutscene -= OnPlayCutscene;
    }

    private void OnPlayCutscene(string cutsceneId)
    {
        if (_cutsceneId == cutsceneId)
        {
            PlayCutscene();
        }
    }

    private void PlayCutscene()
    {
        if (CutsceneManager.Instance.HasPlayed(_cutsceneId))
        {
            gameObject.SetActive(false);
            return;
        }
        if (_binder)
            StartCoroutine(WaitForPlayer());
        else
        {
            _director.Play();
            CutsceneManager.Instance.MarkAsPlayed(_cutsceneId, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator WaitForPlayer()
    {

        while (_player == null)
        {
            _player = GameObject.FindWithTag("Player");
            yield return null;
        }
        _binder.BindTracks();
        _director.Play();
        CutsceneManager.Instance.MarkAsPlayed(_cutsceneId, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayCutscene();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
