using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    IEnumerator DeleteTimer()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
