public class BoomBonus : BonusBase
{
    // Update is called once per frame
    public override void BonusEffect()
    {
        var projectiles = FindObjectsOfType<Projectile>();

        foreach (var proj in projectiles)
        {
            Destroy(proj.gameObject);
        }
    }
}
