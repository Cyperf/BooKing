using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Pages.OurPages
{
    public class BookingModel : PageModel
    {
        public int _skoleId;
        private SkoleService _skoleService;
		public string skole;
        public DateOnly date;
        public int StartInterval { get; set; } = 0; // 0 minutes after midnight
        public int EndInterval { get; set; } = 1 * 60 * 24; // 24 hours after midnight (in minutes) (1 minute -> 1 hour -> 24 hours)
		public BookingModel()
        {
            _skoleService = new SkoleService();
            date = DateOnly.FromDateTime(DateTime.Now);
        }
        public void OnGet(DateOnly? date = null, int startInterval = 0, int endInterval = 1 * 60 * 60 * 24)
        {
            if (date != null)
                this.date = date.Value;
            StartInterval = startInterval;
            EndInterval = endInterval;
            if (endInterval < StartInterval)
                EndInterval = StartInterval;
            skole = _skoleService.Read(LoginManager.LoggedInUser.SkoleId).Location;
            _skoleId = LoginManager.LoggedInUser.SkoleId;


        }
        public Dictionary<int, List<Lokale>> GetRooms()
        {
            Dictionary<int, List<Lokale>> etageToRoomList = new Dictionary<int, List<Lokale>>();

            // get all bookings for the day
            List<Booking> bookings = new BookingService().ReadAll($"Dato='{date.Year+"-"+date.Month+"-"+date.Day}' AND SkoleId={_skoleId}").ToList();

            System.Diagnostics.Debug.WriteLine("Stuff amount: " + bookings.Count);
            // go thorough all rooms
            foreach (var room in new LokaleService().ReadAll($"SkoleId={_skoleId}"))
            {
                // check if the room booked in the time interval
                bool[] timeAvailable = new bool[EndInterval - StartInterval];
                // we fill the timeAvailable array, with all the bookings that are connected to the room
                foreach (var booking in bookings.Where(book => book.LokaleId == room.Id))
                {
                    System.Diagnostics.Debug.WriteLine("Stuff: " + booking);
                    for (int i = booking.TidFra > StartInterval ? booking.TidFra : StartInterval; i < (booking.TidTil < EndInterval ? booking.TidTil : EndInterval); i++)
                        timeAvailable[i - StartInterval] = true;
                }
                int j;
                // then we go through the timeAvailable array, if it a value is "false", it is available at at least one minut of time, in the given interval
                for (j = 0; j < timeAvailable.Length; j++)
                    if (!timeAvailable[j])
                        break;
                //if (room.Id == 1)
                //{
                //    System.Diagnostics.Debug.WriteLine("Stuff: " + j + " : " + timeAvailable.Length + " : " + timeAvailable[j]);
                //    for (j = 0; j < timeAvailable.Length; j++)
                //        if (!timeAvailable[j])
                //            System.Diagnostics.Debug.WriteLine("yes: " + j);
                //}
                // if j is equal to timeAvailable.Length, there wern't any available time
                if (j == timeAvailable.Length)
                    continue;
                // Make sure a key exists, for the floor
                if (!etageToRoomList.ContainsKey(room.Etage))
                    etageToRoomList.Add(room.Etage, new List<Lokale>());
                etageToRoomList[room.Etage].Add(room);
            }

            return etageToRoomList;
        }
    }
}
