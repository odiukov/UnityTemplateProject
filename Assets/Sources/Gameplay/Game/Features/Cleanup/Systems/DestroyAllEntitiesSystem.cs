using Entitas;
using Gameplay.Common.Contexts;

namespace Gameplay.Game.Features.Cleanup.Systems
{
    public sealed class DestroyAllEntitiesSystem : ITearDownSystem
    {
        private readonly IGameContext _context;

        public DestroyAllEntitiesSystem(IGameContext context)
        {
            _context = context;
        }

        public void TearDown()
        {
            foreach (var gameEntity in _context.GetEntities())
            {
                gameEntity.isDestruct = true;
            }
        }
    }
}