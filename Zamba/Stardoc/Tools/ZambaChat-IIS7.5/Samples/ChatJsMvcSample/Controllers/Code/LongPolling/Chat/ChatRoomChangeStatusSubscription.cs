using System;
using ChatJs.Net;// quitar
using ChatJsMvcSample.Models; //Colocar

namespace ChatJsMvcSample.Code.LongPolling.Chat
{
    /// <summary>
    /// Allows for single time chatroom event subscription
    /// </summary>
    public class ChatRoomChangeStatusSubscription : IDisposable
    {
        public ChatRoom Room { get; private set; }
        public Action<int, int> Action { get; private set; }

        public ChatRoomChangeStatusSubscription(ChatRoom room, Action<int, int> action)
        {
            this.Room = room;
            this.Action = action;

            this.Room.ChangeStatusEvent += this.Action;
        }

        public void Dispose()
        {
            this.Room.ChangeStatusEvent -= this.Action;
        }
    }
}