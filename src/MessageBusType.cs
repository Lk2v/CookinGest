using System;
namespace CookinGest.src
{
    public class MessageBusType
    {
        public MessageBusType(MessageType viewType)
        {
            ViewType = viewType;
        }

        public MessageType ViewType { get; }
    }

    public enum MessageType
    {
        LoginSucess,
        SwitchRegisterView,
        SwitchLoginView,

        LogOut,
    }
}

