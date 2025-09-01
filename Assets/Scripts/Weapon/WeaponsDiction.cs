using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class WeaponsDiction:MonoBehaviour
{
    private Dictionary<int, PlayerWeapon> weaponDictionary = new Dictionary<int, PlayerWeapon>();
    public Player player;

    private void Awake()
    {
        // Automatically gather all Playerweapon components from children
        PlayerWeapon[] weapons = GetComponentsInChildren<PlayerWeapon>(true);
        player = GetComponentInParent<Player>();

        foreach (PlayerWeapon weapon in weapons)
        {
            weaponDictionary[weapon.WeaponID] = weapon;
            weapon.DeactivateWeapon(); // Ensure all weapons start deactivated
            weapon.player = player; // Make player visible to all weapons
        }
    }
    public PlayerWeapon SearchWeapon(int id)
    {
        if (weaponDictionary.TryGetValue(id, out PlayerWeapon weapon))
        {
            return weapon;
        }
        Debug.LogWarning($"Weapon with ID {id} not found!");
        return null;
    }
}
