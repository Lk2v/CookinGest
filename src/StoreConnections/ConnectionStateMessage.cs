using System;

namespace CookinGest.src.StoreConnections
{
    public class ConnectionStateMessage
    {
        public ConnectionStateMessage(ConnectionStateEnum t, AccountData? d = null)
        {
            Type = t;
            Data = d;
        }

        public AccountData Data { get; }
        public ConnectionStateEnum Type { get; }
    }

    public enum ConnectionStateEnum
    {
        Login,
        FastLogin,
        LoginList,
    }
}

