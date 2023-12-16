using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager _GameManager;
    public CameraSC _Camera;
    public bool attackArena;
    public GameObject WaitingPoint;
    public Slider _Slider;
    public GameObject WayPoint;


    private void FixedUpdate()
    {
        if(!attackArena)
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }

    private void Start()
    {
        float Diff = Vector3.Distance(transform.position, WayPoint.transform.position);
        _Slider.maxValue = Diff;
    }

    void Update()
    {   

        if(attackArena)
        {
            transform.position = Vector3.Lerp(transform.position, WaitingPoint.transform.position, .015f);
            if (_Slider.value != 0)
                _Slider.value -= .002f;
        }
        else
        {
            float Diff = Vector3.Distance(transform.position, WayPoint.transform.position);
            _Slider.value = Diff;

            if (Input.GetKey(KeyCode.Mouse0))
            {

                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collection") || other.CompareTag("Extraction") || other.CompareTag("Impact") || other.CompareTag("Divide"))
        {
            int number1 = int.Parse(other.name);
            _GameManager.CopyPlayerManager(other.tag, number1, other.transform);
        }
        else if (other.CompareTag("AttackTrigger"))
        {
            _Camera.attackArena = true;
            _GameManager.EnemiesTrigger();
            attackArena = true;
        }
        
        else if (other.CompareTag("EmptyNinja"))
        {
            _GameManager.CopyPlayers.Add(other.gameObject);
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mast") || collision.gameObject.CompareTag("EnemyBox") || collision.gameObject.CompareTag("PropellerNeedles"))
        {
            if (transform.position.x > 0)
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            
            else
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z); 
            



        }
    }

}
