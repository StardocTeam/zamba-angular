using System.Collections.Generic;
using ChatJsMvcSample.Models;

namespace ChatJs.Net
{
    public interface IChatHub
    {
        /// <summary>
        /// Returns the message history between the current user and another user
        /// </summary>
        List<ChatMessage> GetMessageHistory(List<decimal> otherUserId, ChatType chatType);//, int cant);
        //List<ChatMessage> GetMsgHistoryGroup(List<int> usersIds);
        /// <summary>
        /// Sends a message to a another user
        /// </summary>
        //void SendMessage(int otherUserId, string message, string clientGuid);

        void SendMsgToUsers(decimal usersIds, decimal  chatId,string message, ChatType chatType, string clientGuid);

        /// <summary>
        /// Sends a typing signal to a another user
        /// </summary>
        void SendTypingSignal(int otherUserId);

        /// <summary>
        /// When a new client connects
        /// </summary>
        System.Threading.Tasks.Task OnConnected();

        /// <summary>
        /// When a client disconnects
        /// 
        /// </summary>
        System.Threading.Tasks.Task OnDisconnected(bool stopCalled);

       //System.Threading.Tasks.Task OnChangeStatus();
    }
}