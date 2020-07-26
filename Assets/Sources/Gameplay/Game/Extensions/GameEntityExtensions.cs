using DuckLib.Core.Services;

namespace Gameplay.Game.Extensions
{
    public static class GameEntityExtensions
    {
        public static void MarkCollided(this GameEntity entered, int by)
        {
            entered.isCollided = true;
            entered.ReplaceCollisionId(by);
        }

        public static GameEntity WithNewGeneralId(this GameEntity entity, IIdentifierService identifiers)
        {
            if (entity.hasId)
                return entity;

            entity.AddId(identifiers.Next(Identity.General));

            return entity;
        }
    }
}