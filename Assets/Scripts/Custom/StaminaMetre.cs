using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaMetre : MonoBehaviour
{
    public Sprite stamina6;
    public Sprite stamina5;
    public Sprite stamina4;
    public Sprite stamina3;
    public Sprite stamina2;
    public Sprite stamina1;
    public Sprite stamina0;

    // Update is called once per frame
    void Update()
    {
        if(Universe.Instance.Stamina == 6)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina6;
        }

        if (Universe.Instance.Stamina == 5)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina5;
        }

        if (Universe.Instance.Stamina == 4)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina4;
        }

        if (Universe.Instance.Stamina == 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina3;
        }

        if (Universe.Instance.Stamina == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina2;
        }

        if (Universe.Instance.Stamina == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina1;
        }

        if (Universe.Instance.Stamina == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stamina0;
        }
    }
}
