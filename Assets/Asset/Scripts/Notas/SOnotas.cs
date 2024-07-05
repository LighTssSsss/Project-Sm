using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Nota Menu / Create nueva Nota")]
public class SOnotas : ScriptableObject
{

    [Header("Caracteristicas")]
    public string informacion;

    public string info => informacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
