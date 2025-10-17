using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSphereData", menuName = "Game Data/Sphere")]
public class SphereData : ScriptableObject
{
    [Header("Visuals")]
    public Sprite mainSprite;

    [Header("Projectile")]
    public ProjectileData projectileData;
    public SoundData sound;
}
