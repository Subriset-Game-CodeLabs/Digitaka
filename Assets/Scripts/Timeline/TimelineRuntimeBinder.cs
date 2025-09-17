using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineRuntimeBinder : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject player;
    public CinemachineCamera cinemachineCamera;

    public void BindTracks()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (cinemachineCamera)
            cinemachineCamera.Target.TrackingTarget = player.transform;

        foreach (var output in director.playableAsset.outputs)
        {
            // Look for the track you want to bind
            if (output.streamName == "Character Track")
            {
                var animator = player.GetComponent<Animator>();
                if (animator != null)
                {
                    director.SetGenericBinding(output.sourceObject, animator);
                }
                else
                {
                    Debug.LogWarning("Player has no Animator component to bind.");
                }
            }
            if (output.streamName == "Character Track Render")
            {
                GameObject renderer = player.transform.Find("Renderer").gameObject;
                var animator = renderer.GetComponent<Animator>();
                if (animator != null)
                {
                    director.SetGenericBinding(output.sourceObject, animator);
                }
                else
                {
                    Debug.LogWarning("Player has no Animator component to bind.");
                }
            }
        }
    }
}
