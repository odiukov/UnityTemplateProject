using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class PostScoreCommand : ICommand<bool, PostScoreArgs>
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
                void OnScoreReported(bool success)
                {
                    observer.OnNext(success);
                    observer.OnCompleted();
                }

                _socialPlatform.ReportScore(args.Score, args.LeaderboardId, OnScoreReported);
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