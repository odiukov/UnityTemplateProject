using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class PostScoreCommand : ICommand<PostScoreArgs, bool>
    {
        private readonly ISocialPlatform _socialPlatform;

        public PostScoreCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<bool> Execute(PostScoreArgs args)
        {
            return Observable.Create<bool>(observer =>
            {
                _socialPlatform.ReportScore(args.Score, args.LeaderboardId, result =>
                {
                    observer.OnNext(result);
                    observer.OnCompleted();
                });
                return Disposable.Empty;
            });
        }
    }

    public class PostScoreArgs
    {
        public string LeaderboardId;
        public long Score;
    }
}