using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask allButPlayerMasks;

    private bool spaceDown;
    private float horzInput;
    private Rigidbody rigidbodyComponent;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

            horzInput = Input.GetAxis("Horizontal");
    }

    //  Called once every physics update (default 100fps (faster than Update))
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horzInput * 3, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.01f, allButPlayerMasks).Length == 0) //Checking if a coordinate at the player's feet (groundCheckTransform) overlaps any mask other than that of the player. If there are no overlaps with anything but the player then the length of the array will be zero and we assume the player is in the air.
        {
           // spaceDown = false;
            return;
        }

        if (spaceDown)
        {
            // Debug.Log("Space Key Pressed Down.");S
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            spaceDown = false;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
