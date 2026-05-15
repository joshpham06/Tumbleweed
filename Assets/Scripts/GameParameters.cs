using UnityEngine;

public static class GameParameters
{
    // Pathfinding
    public static float UpdateInterval = 0.05f;
    
    // Player
    public static float PlayerSpeed = 5f;
    public static float PlayerAttackRange = 6f;
    public static float PlayerAttackSpeed = 1f;
    public static float PlayerAttackDamage = 5f;
    public static float PlayerMaxHealth = 100f;
    public static float PlayerLifestealMultiplier = 0.5f;
    
    //Burst
    public static float BurstDamage = 10f;
    public static float BurstCooldown = 3f;
    public static float BurstProjectileSpeed = 6.5f;
    
    // Split
    public static int SplitAmount = 4;
    public static float SplitCooldown = 10f;
    
    // Projectiles
    public static float ProjectileSpeed = 10f;
}