namespace Sources.InGame.BattleObject
{
    public interface IRequestDispose<T>
    {
        public void RequestDispose(T toDispose);
    }
}