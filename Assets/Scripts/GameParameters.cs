using UnityEngine;

public static class GameParameters
{
    // Pathfinding
    public static float UpdateInterval = 0.05f;
    
    // Player
    public static float PlayerSpeed = 5f;
    public static float AttackRange = 6f;
    public static float AttackSpeed = 2f;
    
    // Projectiles
    public static float ProjectileDamage = 5f;
    public static float ProjectileSpeed = 5f;
    
    // Split
    public static int SplitAmount = 4;
    public static float SplitCooldown = 10f;
}