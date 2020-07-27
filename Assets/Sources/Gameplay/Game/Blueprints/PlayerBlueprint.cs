using DuckLib.Core.Blueprints;

namespace Gameplay.Game.Blueprints
{
    public class PlayerBlueprint : IBlueprint<GameEntity>
    {
        public void Apply(GameEntity entity)
        {
            entity.isPlayer = true;
        }
    }
}