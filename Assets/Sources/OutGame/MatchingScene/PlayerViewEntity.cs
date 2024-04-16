using UnityEngine;

namespace Sources.OutGame.MatchingScene
{
    public class PlayerViewEntity
    {
        public readonly PlayerIconView[] Views;

        public PlayerViewEntity(PlayerIconView view1, PlayerIconView view2, PlayerIconView view3, PlayerIconView view4)
        {
            Views = new PlayerIconView[] { view1, view2, view3, view4 };
            for (int i = 0; i < Views.Length; i++)
            {
                Views[i].SetOwnerNumber(i);
            }
        }
    }
}