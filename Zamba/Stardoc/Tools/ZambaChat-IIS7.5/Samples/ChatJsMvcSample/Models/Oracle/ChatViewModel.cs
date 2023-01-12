
namespace ChatJsMvcSample.Models.ViewModels
{
    public class ChatViewModel
    {
        /// <summary>
        /// Indicates whether the user is authenticated in the chat
        /// </summary>
        public bool IsUserAuthenticated { get; set; }
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
       // public byte[] Avatar { get; set; }
    }
}