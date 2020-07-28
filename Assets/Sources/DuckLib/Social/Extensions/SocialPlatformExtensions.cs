using System;
using DuckLib.Social.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Extensions
{
    public static class SocialPlatformExtensions
    {
        public static IObservable<bool> ContinueAfterAuthenticate(this ISocialPlatform socialPlatform,
            IObservable<bool> command)
        {
            var authCommand = new AuthenticateCommand(socialPlatform);
            return authCommand
                .Execute()
                .Where(x => x.Result)
                .ContinueWith(command);
        }
    }
}