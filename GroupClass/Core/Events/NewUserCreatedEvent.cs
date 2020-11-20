using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupClass.Core.Events
{
    public class NewUserCreatedEvent : INotification
    {
        private readonly string _username;

        public NewUserCreatedEvent(string username)
        {
            _username = username;
        }
    }
}
