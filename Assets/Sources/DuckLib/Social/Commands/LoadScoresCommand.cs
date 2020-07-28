using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class LoadScoresCommand : ICommand<IScore[], string>
    {
        private readonly ISocialPlatform _socialPlatform;

        public LoadScoresCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<IScore[]> Execute(string leaderboardId)
        {
            return Observable.Create<IScore[]>(
                observer =>
                {
                    void OnLoadedScores(IScore[] scores)
                    {
                        observer.OnNext(scores);
                        observer.OnCompleted();
                    }

                    _socialPlatform.LoadScores(leaderboardId, OnLoadedScores);
                    return Disposable.Empty;
                });
        }
    }
}