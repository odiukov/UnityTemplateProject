using DuckLib.Core;
using DuckLib.Core.Commands;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public abstract class AuthenticatedCommand: Command
    {
        protected readonly ISocialPlatform SocialPlatform;

        protected AuthenticatedCommand(ISocialPlatform socialPlatform)
        {
            SocialPlatform = socialPlatform;
        }

        protected override void OnStart()
        {
            new AuthenticateCommand(SocialPlatform)
                .AddCallback(new Responder(OnAuthenticateResult, OnAuthenticateFault))
                .Execute();
        }

        protected abstract void OnAuthenticateFault(string message);

        protected abstract void OnAuthenticateResult(string message);
    }
}