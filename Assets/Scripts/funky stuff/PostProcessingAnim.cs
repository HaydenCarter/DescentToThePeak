using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEditor.PackageManager;

public class PostProcessingAnim : MonoBehaviour
{
    public PostProcessVolume _postProcessingVolumeHolder;
    public float _ca_min, _ca_max, _cs_min, _cs_max;

    public float _chromaValue;
    public float _hueShiftValue;
    public float _tintValue;
  

    [Header("EFFECTS")]
    public ChromaticAberration _ca;
    public ColorGrading _colourShift;

    private void Start()
    {

        _postProcessingVolumeHolder.profile.TryGetSettings(out _ca);
        _postProcessingVolumeHolder.profile.TryGetSettings(out _colourShift);
        _chromaValue = _ca.intensity.value;
       // _hueShiftValue = _colourShift.hueShift.value;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isPaused)
        //_ca.intensity.value = _chromaValue;

        _colourShift.hueShift.value = _hueShiftValue;

        _colourShift.tint.value = _tintValue;

        _colourShift.temperature.value = _tintValue * -1;
    }
}
