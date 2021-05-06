using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    void Update()
    {
        if (Universe.Instance.Stamina > Universe.Instance.MaxStamina)
        {
            Universe.Instance.Stamina = Universe.Instance.MaxStamina;
        }

        if (Universe.Instance.Stamina < 0)
        {
            Universe.Instance.Stamina = 0;
        }
    }


    bool _regenStamina = false;
    public void RegenStamina(bool isEnabled)
    {
         _regenStamina = isEnabled;
         Universe.Instance.Stamina = Universe.Instance.MaxStamina;
    }
}
