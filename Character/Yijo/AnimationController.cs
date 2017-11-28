using UnityEngine;
using System.Collections;


public class YiAnimationController : MonoBehaviour
{
    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;
    private bool facingRight = true;

    private Vector3 pos;
    private Camera mainCamera;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private YiMovementController _controller;
    //private Animator _animator;
    public Animator _anim;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;
    private bool isAirJumped;
    private float inAirTime;


    void Awake()
    {
        //_animator = GetComponent<Animator>();
        //_anim = (Animator)transform.FindChild("root/pivot").GetComponent("Animator");
        _controller = GetComponent<YiMovementController>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;
        isAirJumped = false;
        inAirTime = 0.3f;
    }

    private void Start()
    {
        mainCamera = (Camera)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("Camera");
    }

    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        //Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        //Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion


    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void Update()
    {
        pos = mainCamera.WorldToScreenPoint(transform.position);

        if (_controller.isGrounded)
        {
            _velocity.y = 0;
            isAirJumped = false;
            inAirTime = 0.3f;
            _anim.SetBool("Grounded", true);
            _anim.SetBool("Jump", false);
        }
        if (Input.GetKey("d"))
        {
            facingRight = true;
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            if (_controller.isGrounded)
            {
                //_animator.Play( Animator.StringToHash( "Run" ) );

                float h = Input.GetAxis("Horizontal") / 3f;
                _anim.SetFloat("Speed", Mathf.Abs(h));
                _anim.SetBool("Jump", false);
            }
        }
        else if (Input.GetKey("a"))
        {
            facingRight = false;
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            if (_controller.isGrounded) {
                //_anim.Play( Animator.StringToHash("Run") );

                float h = Input.GetAxis("Horizontal");
                _anim.SetFloat("Speed", Mathf.Abs(h));
                _anim.SetBool("Jump", false);
            }
        }
        else
        {
            normalizedHorizontalSpeed = 0;
            if (_controller.isGrounded)
            {
                _anim.SetBool("Grounded", true);
                _anim.SetFloat("Speed", Mathf.Abs(0f));
                _anim.SetBool("Jump", false);
            }
        }

        // we can only jump whilst grounded
        if ((_controller.isGrounded || inAirTime > 0f)  && Input.GetButton("Jump") && isAirJumped == false)
		{
			_velocity.y = Mathf.Sqrt(0.8f * jumpHeight * -gravity );
            //_anim.Play( Animator.StringToHash( "Jump" ) );
            _anim.SetBool("Jump", true);
            inAirTime -= Time.deltaTime;
        }
        if(!_controller.isGrounded && Input.GetKeyDown("w") && isAirJumped == false)
        {
            _velocity.y = Mathf.Sqrt(1.5f * jumpHeight * -gravity);
            //_anim.Play( Animator.StringToHash( "Jump" ) );
            _anim.SetBool("Jump", true);
            isAirJumped = true;
        }


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if( _controller.isGrounded && Input.GetKey( "s" ) )
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

        // Look at cursor
        LookAtCursor();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void LookAtCursor()
    {
        if (Input.mousePosition.x < pos.x && facingRight)
        {
            Flip();
        }
        else if (Input.mousePosition.x > pos.x && !facingRight)
        {
            Flip();
        }
    }
}
