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
        [BindProperty]
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [BindProperty]
        public int StartIntervalHours { get; set; }
        [BindProperty]
        public int StartIntervalMinutes { get; set; }
        [BindProperty]
        public int EndIntervalHours { get; set; }
        [BindProperty]
        public int EndIntervalMinutes { get; set; }

        public int StartInterval { get; set; }
        public int EndInterval { get; set; }
		public BookingModel()
        {
            _skoleService = new SkoleService();
        }
        public IActionResult OnGet(int year = 0, int month = 1, int day = 1, int startInterval = BookingService.EarliestAllowedBooking, int endInterval = BookingService.LatestAllowedBooking)
        {
            // The first time you visit the page, it just defaults to the current day.
            if (year != 0)
                date = new DateOnly(year, month, day);
            // making sure the intervals make sense
            startInterval = startInterval < BookingService.EarliestAllowedBooking ? BookingService.EarliestAllowedBooking : startInterval;
            endInterval = endInterval > BookingService.LatestAllowedBooking ? BookingService.LatestAllowedBooking : endInterval;
            endInterval = endInterval < startInterval ? startInterval : endInterval;
            System.Diagnostics.Debug.WriteLine($"Date: {date}, startInterval: {startInterval}, endInterval: {endInterval}");
            StartInterval = startInterval;
            StartIntervalHours = FromOnlyMinutesToHours(StartInterval);
            StartIntervalMinutes = FromOnlyMinutesToMinutes(StartInterval);

            EndInterval = endInterval;
            EndIntervalHours = FromOnlyMinutesToHours(EndInterval);
            EndIntervalMinutes = FromOnlyMinutesToMinutes(EndInterval);

            skole = _skoleService.Read(LoginManager.LoggedInUser.SkoleId).Location;
            _skoleId = LoginManager.LoggedInUser.SkoleId;
            return Page();
        }
        public IActionResult OnPost(int startIntervalHours, int startIntervalMinutes, int endIntervalHours, int endIntervalMinutes)
        {
            return RedirectToPage(null, new { year = date.Year, month = date.Month, day = date.Day, startInterval = FromHoursAndMinutesToOneInt(startIntervalHours, startIntervalMinutes), endInterval = FromHoursAndMinutesToOneInt(endIntervalHours, endIntervalMinutes) });
        }

        // converts from one int to represent time, to hours and minutes
        public static int FromOnlyMinutesToHours(int minutes)
        {
            return minutes / 60;
        }
        public static int FromOnlyMinutesToMinutes(int minutes)
        {
            return minutes % 60;
        }
        // converts from hours and minutes to one single int to represent time
        public int FromHoursAndMinutesToOneInt(int hours, int minutes)
        {
            return hours * 60 + minutes;
        }

        public Dictionary<int, List<Lokale>> GetRooms()
        {
            Dictionary<int, List<Lokale>> etageToRoomList = new Dictionary<int, List<Lokale>>();

            // get all bookings for the day
            List<Booking> bookings = new BookingService().ReadAll($"Dato='{date.Year+"-"+date.Month+"-"+date.Day}' AND SkoleId={_skoleId}").ToList();
            //List<Booking> bookings = new BookingService().ReadAll($"Dato='{date.Year + "-" + date.Month + "-" + date.Day}'").ToList();
            // go thorough all rooms
            foreach (var room in new LokaleService().ReadAll($"SkoleId={_skoleId}"))
            {
                // check if the room booked in the time interval
                bool[] timeAvailable = new bool[EndInterval - StartInterval];
                // we fill the timeAvailable array, with all the bookings that are connected to the room
                foreach (var booking in bookings.Where(book => book.LokaleId == room.Id))
                {
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
