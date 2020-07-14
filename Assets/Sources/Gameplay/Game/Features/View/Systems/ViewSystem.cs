using System.Collections.Generic;
using DuckLib.Core.View;
using Entitas;
using Gameplay.Common.Contexts;
using Gameplay.Game.View;

namespace Gameplay.Game.Features.View.Systems
{
    public sealed class ViewSystem : ReactiveSystem<GameEntity>
    {
        private readonly IViewService<GameEntity> _viewService;

        public ViewSystem(IGameContext context, IViewService<GameEntity> viewService) : base(context)
        {
            _viewService = viewService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Asset.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAsset;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameEntity in entities)
            {
                _viewService.LoadView<UnityViewController>(gameEntity, gameEntity.asset.Name);
                gameEntity.RemoveAsset();
            }
        }
    }
}