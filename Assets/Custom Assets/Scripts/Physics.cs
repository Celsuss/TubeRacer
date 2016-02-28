using UnityEngine;
using System.Collections;
using Assets.Custom_Assets.Scripts.Static;

public class Physics : MonoBehaviour
{
    #region Private fields

    private Vector3 _thrustDirection;
    private Vector3 _strafeDirection;
    private Vector3 _hoverDirection;

    private float _curThrust;
    private float _curStrafe;
    private float _velocity;

    private bool _isGrounded = false;
    private float _airTime = 0;

    private float _rotationZ = 0F;
    private float _sensitivityZ = 2F;
    private float _rotationY = 0F;
    private float _sensitivityY = 2F;

    private RigidbodyConstraints _currentConstraint;

    private GameObject _meshGameObject;

    #endregion

    #region Public fields

    public float gravity;

    public float thrust;
    public float strafe;
    public float hoverThrust;

    public float rotationSpeed;

    #endregion

    #region MonoBehaviour
    // Use this for initialization
    void Start()
    {
        _meshGameObject = GameObjectExtensions.getChildGameObject(this.gameObject, "Cube");
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        ApplyGravity();
        HoverThrust();
        ApplyThrust();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(this.transform.)

        //ApplyRotation();
    }

    // LateUpdate is called every frame, after all Update functions has been called 
    void LateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

            _currentConstraint = this.GetComponent<Rigidbody>().constraints;

            FreedomOfRotation();
        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            ResetRotation();

            

            _currentConstraint = this.GetComponent<Rigidbody>().constraints;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"));
        {
            _isGrounded = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"));
        {
            _isGrounded = false;
        }
    }

    #endregion

    void HoverThrust()
    {
        if(_isGrounded)
        {
            _airTime = 0;

            if (hoverThrust < 2)
            {
                //hoverThrust = 2;
                //hoverThrust += hoverThrust * Time.fixedDeltaTime;
            }

            _hoverDirection = Vector3.up;
            this.GetComponent<Rigidbody>().AddForce(_hoverDirection * (gravity * hoverThrust));
        }
        else if(!_isGrounded)
        {
            _airTime += Time.deltaTime;

            if (_airTime > 3)
            {
                //hoverThrust = 0;
            }
        }
    }

    void ApplyGravity()
    {
        this.GetComponent<Rigidbody>().AddForce(-Vector3.up * gravity);
    }

    void ApplyThrust()
    {
        _thrustDirection = transform.TransformDirection(Vector3.forward);
        _strafeDirection = transform.TransformDirection(Vector3.right);

        _curThrust = thrust * Input.GetAxis("Vertical");
        _curStrafe = strafe * Input.GetAxis("Horizontal");

        this.GetComponent<Rigidbody>().AddForce(_thrustDirection * _curThrust);
        this.GetComponent<Rigidbody>().AddForce(_strafeDirection * _curStrafe);

        _velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
    }

    void ApplyRotation()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
    }

    void FreedomOfRotation()
    {
        _rotationY += Input.GetAxis("Horizontal") * _sensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, -10, 10);

        ////_meshGameObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _rotationY, transform.localEulerAngles.z);

        _rotationZ += Input.GetAxis("Horizontal") * _sensitivityZ;
        _rotationZ = Mathf.Clamp(_rotationZ, -5, 5);

        _meshGameObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _rotationY, -_rotationZ);

        //_meshGameObject.transform.Rotate(new Vector3(transform.localEulerAngles.x, _rotationY, -_rotationZ));
    }

    

    void ResetRotation()
    {


        //if(_meshGameObject.transform.localRotation != this.GetComponent<Rigidbody>().transform.rotation)
        //{
        //    _meshGameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        ////}

        //if(_rotationZ != 0)
        //{
        //    _rotationZ--;
        //}
        //if(_rotationY != 0)
        //{
        //    _rotationY--;
        //}

        _rotationZ = _rotationZ != 0 ? _rotationZ * 0.01F : 0;
        _rotationY = _rotationY != 0 ? _rotationY * 0.01F : 0;

        //_rotationZ -= _rotationZ * 0.1F;
        //_rotationY -= _rotationY * 0.1F;

        _meshGameObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _rotationZ);
        _meshGameObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _rotationY, transform.localEulerAngles.z);
        //_meshGameObject.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //_meshGameObject.transform.rotation = this.GetComponent<Rigidbody>().transform.rotation;
    }
}
