

using CommunityToolkit.Mvvm.Messaging.Messages;
using HuesarioApp.Models.Entities;
using System.Collections.ObjectModel;

namespace HuesarioApp.Services.Messages
{
    class ChangeInBrandMessage : ValueChangedMessage<ObservableCollection<Brands>>
    {
        public ChangeInBrandMessage(ObservableCollection<Brands> value) : base(value)
        {
        }
    }
}
