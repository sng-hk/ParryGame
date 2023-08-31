using UnityEngine;

public class TestSceneCameraSwitcher : MonoBehaviour
{
    private Animator animator;    

    private bool playerCamera = true;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Camera Switch");
            SwtichState();
        }
    }

    private void SwtichState()
    {
        if (playerCamera)
        {
            animator.Play("BossRoomCamera");
        }
        else
        {
            animator.Play("PlayerCamera");
        }
        playerCamera = !playerCamera; 
    }
}
