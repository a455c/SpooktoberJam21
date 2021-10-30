using UnityEngine;

public class water_sink_script : MonoBehaviour
{
    public AudioSource sinking;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Run into river");
            sinking.Play();
            Destroy(collision.gameObject);
        }
    }
}
