using System;
using UnityEngine;

public enum ItemCode
{
    NoItem = 0,
    Health = 1,
    AmmoBox = 2,
    MaxHP = 3,
    Raincoat = 4,
    Adventure = 5,
    Alien = 6,
    Cardboard = 7,
    CargoShorts = 8,
    Cowboy = 9,
    Damage = 10,
    AttackSpeed = 11,
}

public class ItemCodeParse
{
    public static ItemCode FromString(string itemName)
    {
        try
        {
            return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName);
        }
        catch(ArgumentException e)
        {
            Debug.Log(e.ToString());
            return ItemCode.NoItem;
        }
    }
}
