using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using HuesarioApp.Models.Entities;

namespace HuesarioApp.Services.Messages;

public class sessionMessage(Sessions value)
    : ValueChangedMessage<Sessions>(value);