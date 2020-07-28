using System;
using System.Collections.Generic;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class LoadUsersCommand : ICommand<IUserProfile[], string[]>
    {
        private readonly ISocialPlatform _socialPlatform;

        public LoadUsersCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<IUserProfile[]> Execute(string[] userIds)
        {
            return Observable.Create<IUserProfile[]>(
                observer =>
                {
                    void OnLoadedUsers(IUserProfile[] achievements)
                    {
                        observer.OnNext(achievements);
                        observer.OnCompleted();
                    }

                    _socialPlatform.LoadUsers(userIds, OnLoadedUsers);
                    return Disposable.Empty;
                });
        }
    }
}