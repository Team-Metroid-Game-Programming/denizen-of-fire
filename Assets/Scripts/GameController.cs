using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// global game logic goes in here
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToggleObsticles();
    }


    // temporary for Wednesday demo purposes
    [SerializeField] private GameObject groundObst;
    [SerializeField] private GameObject midAirObst;
    [SerializeField] private GameObject highAirObst;
    private void ToggleObsticles()
    {
        if (Input.GetKeyDown(KeyCode.G))
            groundObst.SetActive(!groundObst.activeSelf);
        if (Input.GetKeyDown(KeyCode.U))
            midAirObst.SetActive(!midAirObst.activeSelf);
        if (Input.GetKeyDown(KeyCode.H))
            highAirObst.SetActive(!highAirObst.activeSelf);
    }
}
