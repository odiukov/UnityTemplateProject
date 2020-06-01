using System;

namespace DuckLib.Core.Commands
{
    public abstract class Command : IDisposable
    {
        private bool _running;
        public void Execute()
        {
            _running = true;
            OnStart();
        }

        protected abstract void OnStart();

        protected void FinishCommand()
        {
            if (!_running) return;
            Dispose();
        }

        public void Terminate()
        {
            if (_running)
            {
                FinishCommand();
            }
        }

        public virtual void Dispose()
        {
            _running = false;
            OnReleaseResources();
        }

        protected virtual void OnReleaseResources()
        {
        }
    }

    public abstract class CommandWithArgs<TArgs> : Command
    {
        protected TArgs Args;

        public CommandWithArgs<TArgs> SetArgs(TArgs args)
        {
            Args = args;
            return this;
        }

        public override void Dispose()
        {
            Args = default;
            base.Dispose();
        }
    }

    public abstract class CommandWithArgsAndCallback<TArgs, TResponse> : CommandWithArgs<TArgs>
    {
        private readonly Callback<TResponse> _callback;

        protected CommandWithArgsAndCallback()
        {
            _callback = new Callback<TResponse>();
        }

        private void OnFinishCommand(TResponse response, bool success)
        {
            if (success)
                _callback.FireResponse(response);
            else
                _callback.FireFault(response);
        }

        protected void FinishCommand(bool success, TResponse response = default)
        {
            OnFinishCommand(response, success);
            FinishCommand();
        }

        public CommandWithArgsAndCallback<TArgs, TResponse> AddCallback(Responder<TResponse> responder)
        {
            _callback.AddResponder(responder);
            return this;
        }

        public override void Dispose()
        {
            base.Dispose();
            _callback.Clear();
        }
    }

    public abstract class CommandWithCallback<TResponse> : Command
    {
        private readonly Callback<TResponse> _callback;

        protected CommandWithCallback()
        {
            _callback = new Callback<TResponse>();
        }

        private void OnFinishCommand(TResponse response, bool success)
        {
            if (success)
                _callback.FireResponse(response);
            else
                _callback.FireFault(response);
        }

        protected void FinishCommand(bool success, TResponse response = default)
        {
            OnFinishCommand(response, success);
            FinishCommand();
        }

        public CommandWithCallback<TResponse> AddCallback(Responder<TResponse> responder)
        {
            _callback.AddResponder(responder);
            return this;
        }

        public override void Dispose()
        {
            base.Dispose();
            _callback.Clear();
        }
    }
}