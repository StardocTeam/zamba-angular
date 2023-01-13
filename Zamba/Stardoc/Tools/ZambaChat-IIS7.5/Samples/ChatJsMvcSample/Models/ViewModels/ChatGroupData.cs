using ChatJsMvcSample.Models;
using System;
using System.Collections.Generic;

namespace ChatJs.Net
{
    public class ChatGroupData
    {
        public List<ChatUser> GroupList { get; set; }
        public DateTime LastMessage { get; set; }
        public string ChatName { get; set; }
        public decimal ChatId { get; set; }


        public ChatGroupData(List<ChatUser> userList, DateTime lastMessage, string chatName, decimal chatId)
        {
            GroupList = userList;
            LastMessage = lastMessage;
            ChatName = chatName;
            ChatId = chatId;
   
        }
    }
    public class ChatExternalData : ChatGroupData
    {
        public ChatType ChatType { get; set; }
        public ChatExternalData(List<ChatUser> userList, DateTime lastMessage, string chatName, decimal chatId, ChatType chatType) : base(userList, lastMessage, chatName, chatId)
        {
            ChatType = chatType;
        }
    }
}