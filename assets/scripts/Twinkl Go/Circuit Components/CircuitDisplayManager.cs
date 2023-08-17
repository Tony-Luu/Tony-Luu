using System.Collections.Generic;
using UnityEngine;

public class CircuitDisplayManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CircuitDiagrams = new List<GameObject>();

    [SerializeField]
    private ScoreManager ScoreManagerScript;



    void Start()
    {
        ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    //Change the circuit diagram when the player earns a score
    public void ChangeCircuitDiagram()
    {
        //Destroy the previous circuit diagram
        Destroy(transform.GetChild(0).gameObject);

        //Create a circuit diagram based on the score
        GameObject CircuitDiagramClone = Instantiate(CircuitDiagrams[ScoreManagerScript.ReturnScore()], Vector3.zero, Quaternion.identity);

        //Set the parent of the created circuit diagram to this gameobject
        CircuitDiagramClone.transform.SetParent(transform);

        //Set local position to zero
        CircuitDiagramClone.transform.localPosition = Vector3.zero;

        //Set local scale
        CircuitDiagramClone.transform.localScale = new Vector3(1f, 1f, 1f);

    }

}
