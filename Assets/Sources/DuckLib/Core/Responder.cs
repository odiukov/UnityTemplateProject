using System;

namespace DuckLib.Core
{
    public class Responder : Responder<string>
    {
        public Responder(Action<string> result, Action<string> fault) : base(result, fault)
        {
        }

        public Responder(Action<string> result) : base(result)
        {
        }
    }

    public class Responder<T>
    {
        public readonly Action<T> Result;
        public readonly Action<T> Fault;

        protected Responder(Action<T> result, Action<T> fault)
        {
            Result = result;
            Fault = fault;
        }

        protected Responder(Action<T> result)
        {
            Result = result;
        }
    }
}