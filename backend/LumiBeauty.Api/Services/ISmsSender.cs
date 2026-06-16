using LumiBeauty.Api.Models; namespace LumiBeauty.Api.Services; public interface ISmsSender{Task SendConfirmationAsync(Booking booking);}
