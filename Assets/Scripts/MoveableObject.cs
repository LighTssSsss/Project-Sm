using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public float fuerzaEmpuje;
    private CheckerEnviroment checkeo;
    public MoveAndAnimatorController movimiento;
    [SerializeField] private AreaInteract areaInteraccion;
    [SerializeField] Vector3 direccionDeEmpuje;
    public bool estoyEmpujandolo;
    // [SerializeField] private CharacterController controlador;


    private void Awake()
    {
        checkeo = GetComponent<CheckerEnviroment>();
        movimiento = GetComponent<MoveAndAnimatorController>();
       // controlador = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody cuerpo = hit.collider.attachedRigidbody;


        if(cuerpo == null || cuerpo.isKinematic)
        {
            return;
        }

        if(hit.moveDirection.y < -0.3f)
        {
            return;
        }
        
        
        if (checkeo.estaEmpujando == true && movimiento.empujandoObjetos && areaInteraccion.loToma == false)
        {

            direccionDeEmpuje = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            Vector3 collisionPoint = hit.point;

            cuerpo.AddForceAtPosition(direccionDeEmpuje * fuerzaEmpuje, collisionPoint, ForceMode.Impulse);
            estoyEmpujandolo = true;
        }


        else
        {
            estoyEmpujandolo = false;
        }
        

       
    }
}
