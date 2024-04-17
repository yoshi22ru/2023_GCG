using Sources.InGame.BattleObject;

namespace Sources.InGame.SecondEdition.Interface
{
    public interface IHealable
    {
        public void Heal(HealEntity healEntity);
    }

    public class HealEntity
    {
        public readonly int BaseHealValue;
        public readonly Team OwnerTeam;
    }
}