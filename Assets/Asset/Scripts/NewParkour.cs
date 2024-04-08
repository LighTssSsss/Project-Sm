using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Parkour Menu / Create New Parkour Action")]
public class NewParkour : ScriptableObject
{

    [Header("Checking Obstacle height")]   
    [SerializeField] public string animationName;
    [SerializeField] private string barrierTag;
    [SerializeField] private float minimumHeight;
    [SerializeField] private float maximumHeight;



    [Header("Rotating Player towards Obstacles")]
    [SerializeField] private bool lookAtObstacle;
    [SerializeField] private float parkourActionDelay;
    public Quaternion requiredRotation { get; set; }


    [Header("Target Matching")]
    [SerializeField] bool allowTargetMatching = true; // Permite la coincidencia de obstaculo con la animacion
    [SerializeField] AvatarTarget comparedBodyPart; //Comparo con que parte del cuerpo empieza la anmiacion para hacer coincidir con el objeto
    [SerializeField] float compareStartTime; //Comienza la coincidencia de la animacion
    [SerializeField] float compareEndTime;
    [SerializeField] Vector3 comparePositionWeight = new Vector3(0, 0, 0); // solo lo mueve la coincidencia en y


    public Vector3 comparePosition { get; set; }


    public bool CheckIfAvaible(ObstacleInfo hitData, Transform player)
    {
        if(!string.IsNullOrEmpty(barrierTag) && hitData.hitInfo.transform.tag != barrierTag)
        {
            return false;
        }

        float checHeight = hitData.heightInfo.point.y - player.position.y;
        
       

        if(checHeight < minimumHeight || checHeight > maximumHeight)
        {
            return false;
        }

        if (lookAtObstacle)
        {
            requiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
        }


        if (allowTargetMatching)
        {
            comparePosition = hitData.heightInfo.point; //Saltara un poco lejos del borde
        }

        return true;
    }

    //Forma de acceder a los valores
    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;
    public float ParkourActionDelay => parkourActionDelay;


    public bool AlloTargetMatching => allowTargetMatching;
    public AvatarTarget CompareBodyPart => comparedBodyPart;
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;
    public Vector3 ComparePositionWeight => comparePositionWeight;



}
