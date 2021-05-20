using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingAnim : MonoBehaviour
{
    public PostProcessVolume _postProcessingVolumeHolder;
    public float _ca_min, _ca_max, _cs_min, _cs_max;

    public float _chromaValue;
    public float _hueShiftValue;
    public float _tintValue;
    public float _vignetteValue;
    public float _sturationValue;
  

    [Header("EFFECTS")]
    public ChromaticAberration _ca;
    public ColorGrading _colourShift;
    public Vignette _vignette;

    private void Start()
    {

        _postProcessingVolumeHolder.profile.TryGetSettings(out _ca);
        _postProcessingVolumeHolder.profile.TryGetSettings(out _colourShift);
        _postProcessingVolumeHolder.profile.TryGetSettings(out _vignette);
        _chromaValue = _ca.intensity.value;
       // _hueShiftValue = _colourShift.hueShift.value;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isPaused)
        _ca.intensity.value = _chromaValue;

        _colourShift.hueShift.value = _hueShiftValue;

        _colourShift.tint.value = _tintValue;

        _colourShift.temperature.value = _tintValue * -1;

        _vignette.intensity.value = _vignetteValue;

        _colourShift.saturation.value = _sturationValue;
    }
}
