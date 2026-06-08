using System;
using System.Collections.Generic;
using System.Text;
using LibraryMembershipApp.Models;

namespace LibraryMembershipApp.Interfaces
{
    public interface INotificationService
    {
        void SendBorrowedNotification(string email, string bookTitle);
    }
}
