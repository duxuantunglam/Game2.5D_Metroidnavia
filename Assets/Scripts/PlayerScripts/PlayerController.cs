using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 5f;
    Rigidbody myRb;
    Animator myAnim;
    bool facingRight;

    public float jumpForce;
    public LayerMask groundLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    private void FixedUpdate() {
        float moveValue = Input.GetAxisRaw("Horizontal");

        myAnim.SetFloat("Speed", Mathf.Abs(moveValue));

        myRb.linearVelocity = new Vector3(moveValue * runSpeed, myRb.linearVelocity.y, 0);

        if(moveValue > 0 && !facingRight)
            Flip();
        else if(moveValue < 0 && facingRight)
            Flip();

        Jump();
    }

    public void Jump() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(IsGrounded()) {
                myRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                myAnim.SetTrigger("Jump");
            }
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    bool IsGrounded() {
        Gizmos.color = Color.red;
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f),Vector3.down),
        };

        for(int i = 0; i < rays.Length; i++) {
            if(Physics.Raycast(rays[i], 0.1f, groundLayerMask)) {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f),Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f),Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f),Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f),Vector3.down);
    }
}
