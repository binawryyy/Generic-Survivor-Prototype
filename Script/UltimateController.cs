using UnityEngine;

public class UltimateController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.Instance == null) return;
        //transform.Translate(Vector3.forward * PlayerController.Instance.ultSpeed * Time.deltaTime, Space.Self);
    }
}
