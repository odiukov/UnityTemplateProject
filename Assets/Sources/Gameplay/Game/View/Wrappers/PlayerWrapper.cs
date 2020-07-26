namespace Gameplay.Game.View.Wrappers
{
    public class PlayerWrapper : UnityViewController
    {
        protected override void OnStart()
        {
            Entity.isPlayer = true;
        }
    }
}