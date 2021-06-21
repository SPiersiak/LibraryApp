using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Request;
using LibraryApp.Mobile.Services.Reservations.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        public IRequestService _requestService => DependencyService.Get<IRequestService>();
        public async Task<bool> AddNewReservation(ReservationDto reservationDto)
        {
            var uri = $"{Settings.Server_Endpoint}Reservation";
            var result = await _requestService.PutAsync<ReservationDto>(uri, reservationDto);
            return true;
        }

        public async Task<bool> BookProlongate(long reservationId)
        {
            var uri = $"{Settings.Server_Endpoint}Reservation/ExtendReservation/" + reservationId;
            var result = await _requestService.PostAsync<long,string>(uri, reservationId);
            return true;
        }

        public async Task<bool> EndReservation(long reservationId)
        {
            var uri = $"{Settings.Server_Endpoint}Reservation/EndReservation/" + reservationId;
            var result = await _requestService.PostAsync<long, string>(uri, reservationId);
            return true;
        }

        public async Task<Reservation> GetActiveReservationForBook(long bookId)
        {
            var uri = $"{Settings.Server_Endpoint}Reservation/BookReservation/" + bookId;
            var result = await _requestService.GetAsync<Reservation>(uri);
            return result;
        }

        public async Task<IEnumerable<Reservation>> GetActiveReservationForUser(long userId)
        {
            var uri = $"{Settings.Server_Endpoint}Reservation/ActiveReservation/" + userId;
            var result = await _requestService.GetAsync<List<Reservation>>(uri);
            return result;
        }

        public async Task<IEnumerable<Reservation>> GetInActiveReservationForUser(long userId)
        {
            var uri = $"{Settings.Server_Endpoint}Reservation/InActiveReservation/" + userId;
            var result = await _requestService.GetAsync<List<Reservation>>(uri);
            return result;
        }
    }
}
