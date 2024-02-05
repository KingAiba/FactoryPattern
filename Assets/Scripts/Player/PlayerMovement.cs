using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public PlayerProperties PlayerProperties;
    [SerializeField] public VFXManager VFXManager;
    [HideInInspector] public CameraController CameraController { get; private set; }
    [HideInInspector] public InputHandler InputHandler { get; private set; }

    public Transform Orintation;
    public Transform WeaponHolder;
    public Rigidbody rigitBody;

    public bool isGrounded;
    private bool readyToJump = true;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    public List<IFactory> factories;
    public List<Weapon> weapons;

    private int _selectedWeapon = 0;

    private void Start()
    {
        InitializePlayerMovement();
        rigitBody.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerProperties.PlayerHeight * 0.5f + 0.2f, PlayerProperties.GroundLayerMask);
        SpeedControl();
        if(isGrounded)
        {
            rigitBody.drag = PlayerProperties.GroundDragForce;
        }
        else if(!isGrounded)
        {
            rigitBody.AddForce(moveDirection);
        }
        else
        {
            rigitBody.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void InitializePlayerMovement()
    {
        CameraController = new CameraController(Camera.main, Orintation);
        InputHandler = GameManager.Instance.InputHandler;

        InitFactories();
        PopulateWeapons();

        RemoveMovementListeners();
        AddMovementListeners();
    }

    private void InitFactories()
    {
        factories = new List<IFactory>();
        factories.Add(new FactoryA());
        factories.Add(new FactoryB());
        factories.Add(new FactoryC());
    }

    private void PopulateWeapons()
    {
        weapons = new List<Weapon>();
        foreach (IFactory factory in factories)
        {
            Weapon weapon = factory.CreateWeapon(WeaponHolder);
            weapon.gameObject.SetActive(false);

            weapon.OnShoot += PlayFlashEffect;
            weapon.OnProjectileHit += PlaySparkEffect;

            weapons.Add(weapon);
        }
        weapons[_selectedWeapon].gameObject.SetActive(true);
    }

    private void AddMovementListeners()
    {
        InputHandler.OnMousePositionChange += CameraController.OnMousePositionChange;
        InputHandler.OnMovementInput += GetMovementInput;
        InputHandler.OnJumpKey += OnJumpInput;
        InputHandler.OnMouseScroll += OnMouseScroll;
        InputHandler.OnMouseRightClick += ShootWeapon;

    }

    private void RemoveMovementListeners()
    {
        InputHandler.OnMousePositionChange -= CameraController.OnMousePositionChange;
        InputHandler.OnMovementInput -= GetMovementInput;
        InputHandler.OnJumpKey -= OnJumpInput;
        InputHandler.OnMouseScroll -= OnMouseScroll;
        InputHandler.OnMouseRightClick -= ShootWeapon;
    }

    public void Dispose()
    {
        foreach (var w in weapons)
        {
            w.OnProjectileHit -= PlaySparkEffect;
            w.OnShoot -= PlayFlashEffect; 
        }
        RemoveMovementListeners();
        CameraController = null;
        InputHandler = null;
    }

    private void GetMovementInput(float horizontal, float vertical)
    {
        horizontalInput = horizontal;
        verticalInput = vertical;

    }

    private void OnJumpInput()
    {
        if (readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), PlayerProperties.JumpCooldown);
        }
    }

    private void OnMouseScroll(Vector2 delta)
    {
        if (delta.magnitude > 0)
        {
            weapons[_selectedWeapon].gameObject.SetActive(false);
            _selectedWeapon += (int)delta.y;

            if (_selectedWeapon < 0) _selectedWeapon = weapons.Count - 1;

            _selectedWeapon = _selectedWeapon % weapons.Count;
            weapons[_selectedWeapon].gameObject.SetActive(true);
        }
    }

    private void ShootWeapon()
    {
        Debug.Log($"SHOOT WEAPON : {_selectedWeapon}");
        weapons[_selectedWeapon].Shoot();
    }

    private void MovePlayer()
    {
        moveDirection = Orintation.forward * verticalInput + Orintation.right * horizontalInput;
        if(isGrounded)
            rigitBody.AddForce(moveDirection * PlayerProperties.MovementSpeed, ForceMode.Force);
        else if(!isGrounded)
        {
            rigitBody.AddForce(moveDirection * PlayerProperties.MovementSpeed * PlayerProperties.AirMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rigitBody.velocity.x, 0, rigitBody.velocity.z);

        if(flatVelocity.magnitude > PlayerProperties.MovementSpeed)
        {
            Vector3 limitVel = flatVelocity.normalized * PlayerProperties.MovementSpeed;
            rigitBody.velocity = new Vector3(limitVel.x, limitVel.y, limitVel.z);
        }
    }
    private void Jump()
    {
        rigitBody.velocity = new Vector3(rigitBody.velocity.x, 0f, rigitBody.velocity.z);
        rigitBody.AddForce(transform.up * PlayerProperties.JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void PlayFlashEffect()
    {
        Instantiate(VFXManager.FlashParticle, weapons[_selectedWeapon].LaunchPoint);
    }

    private void PlaySparkEffect(Vector3 position)
    {
        Instantiate(VFXManager.SparkParticle, position, Quaternion.identity/*Quaternion.Euler(transform.position - position)*/);
    }

}
