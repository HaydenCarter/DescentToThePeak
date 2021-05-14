using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class InstantiateTimeline : MonoBehaviour
{
    public PlayableDirector timeline;

    // Update is called once per frame
        void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }
    public void PlayTimeline()
    {
        timeline.Play();
    }

    public void PauseTimeline()
    {
        timeline.Pause();
    }
}
