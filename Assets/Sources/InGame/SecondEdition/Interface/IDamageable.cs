using Sources.InGame.BattleObject;

namespace Sources.InGame.SecondEdition.Interface
{
    public interface IDamageable
    {
        public void Damage(DamageEntity damageEntity);
    }

    public class DamageEntity
    {
        public readonly int BaseDamage;
        public readonly Team OwnerTeam;
    }
}