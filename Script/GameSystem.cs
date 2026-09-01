using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;
    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
