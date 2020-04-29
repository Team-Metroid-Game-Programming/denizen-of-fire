using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerReference;
    HealthController Source;
    SpriteRenderer HealthFill;
    SpriteRenderer MagicFill;
    // Start is called before the first frame update
    void Start()
    {
        Source = PlayerReference.GetComponent<HealthController>();  
        HealthFill = this.gameObject.transform.Find("Health Fill").GetComponent<SpriteRenderer>();
        MagicFill = this.gameObject.transform.Find("Magic Fill").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthFill.size = new Vector2(Source.currentHealth * 0.05f, 0.625f);
        MagicFill.size = new Vector2(Source.currentMagic * 0.05f, 0.5f);
    }
}