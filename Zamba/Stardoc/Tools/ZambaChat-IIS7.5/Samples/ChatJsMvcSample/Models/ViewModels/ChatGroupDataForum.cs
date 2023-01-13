using ChatJsMvcSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatJsMvcSample.Models.ViewModels
{
    public class ChatGroupDataForum
    {

        public List<ChatUser> GroupList { get; set; }
        public DateTime LastMessage { get; set; }
        public string ChatName { get; set; }
        public decimal ChatId { get; set; }
        public int? DocId { get; set; }

        public ChatGroupDataForum(List<ChatUser> userList, DateTime lastMessage, string chatName, decimal chatId, int? docId)
        {
            GroupList = userList;
            LastMessage = lastMessage;
            ChatName = chatName;
            ChatId = chatId;
            DocId = docId;
        }
    }
    public class ChatExternalDataForum : ChatGroupDataForum
    {
        public ChatType ChatType { get; set; }
        public ChatExternalDataForum(List<ChatUser> userList, DateTime lastMessage, string chatName, decimal chatId, ChatType chatType,int? DocId) : base(userList, lastMessage, chatName, chatId, DocId)
        {
            ChatType = chatType;
        }
    }

}
