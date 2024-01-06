using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance;

    public List<IFactory> factories;
    public List<Weapon> weapons;

    private int _selectedWeapon = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);         
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        InitFactories();
        PopulateWeapons();
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
        foreach(IFactory factory in factories)
        {
            Weapon weapon = factory.CreateWeapon();
            weapon.gameObject.SetActive(false);

            weapons.Add(weapon);
        }
        weapons[_selectedWeapon].gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.magnitude > 0)
        {
            weapons[_selectedWeapon].gameObject.SetActive(false);
            _selectedWeapon += (int)Input.mouseScrollDelta.y;

            if (_selectedWeapon < 0) _selectedWeapon = weapons.Count - 1;

            _selectedWeapon = _selectedWeapon % weapons.Count;
            weapons[_selectedWeapon].gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log($"SHOOT WEAPON : {_selectedWeapon}");
            weapons[_selectedWeapon].Shoot();
        }
    }
}
