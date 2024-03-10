using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{

    public GameObject arrow;
    //false = right true = left
    public bool site=false;
   
    
    
    void Start()
    {
      
        StartCoroutine(CreateObjectCoroutine());
        if (site == true)
        {
            
        }
    }

    IEnumerator CreateObjectCoroutine()
    {
        while (true)
        {
           
            GameObject newObject = Instantiate(arrow, this.transform.position,Quaternion.identity);
            if (site)
            {
                newObject.transform.Rotate(Vector3.forward, 180f);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
