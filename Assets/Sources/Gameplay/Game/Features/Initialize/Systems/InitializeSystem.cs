using DG.Tweening;
using Entitas;
using Gameplay.Common.Contexts;
using UnityEngine;

namespace Gameplay.Game.Features.Initialize.Systems
{
    public sealed class InitializeSystem : IInitializeSystem
    {
        private readonly IGameContext _context;

        public InitializeSystem(IGameContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            var player = _context.CreateEntity();
            player.AddAsset("Player");
            player.AddPosition(new Vector3(0, 0, 1));

            // Destroy object after 2 seconds
            // DOTween.Sequence()
            //     .AppendInterval(2f)
            //     .AppendCallback(() =>
            // {
            //     player.isDestruct = true;
            // });
        }
    }
}