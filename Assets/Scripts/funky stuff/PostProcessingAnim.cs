using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor.PackageManager;

public class PostProcessingAnim : MonoBehaviour
{
    public PostProcessVolume _postProcessingVolumeHolder;
    public float _min, _max;

    public float _chromaValue = 1;
  

    [Header("EFFECTS")]
    private ChromaticAberration _ca;

    private void Start()
    {
        _postProcessingVolumeHolder.profile.TryGetSettings(out _ca);
        _ca.intensity.value = _min;
    }

    FloatParameter _fp = new FloatParameter();
    // Update is called once per frame
    void Update()
    {
        //if (!isPaused)
            _ca.intensity.value = _chromaValue;
    }
}
