﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Summit.Controllers
{
    public class HomeController : Controller
    {
        public class LatLng
        {
            public string VenueLatitude;
            public string VenueLongitude;
            public LatLng(string Lat, string Lng)
            {
                this.VenueLatitude = Lat;
                this.VenueLongitude = Lng;
            }
        }
        public class EventSession
        {
            public string Time;
            public string Title;
            public string Extra;
            public string Source;
            public string Location;
            public string Attire;
            public EventSession(string Time, string Title)
            {
                this.Time = Time;
                this.Title = Title;
            }
            public EventSession(string Time, string Title, string Extra)
            {
                this.Time = Time;
                this.Title = Title;
                this.Extra = Extra;
            }
            public EventSession(string Time, string Title,string Extra,string Source,string Location)
            {
                this.Time = Time;
                this.Title = Title;
                this.Extra = Extra;
                this.Source = Source;
                this.Location = Location;
            }
        }
        public class EventDay
        {
            public string date;
            public string attire;
            public string location;
            public List<Groups> Groups;
        }
        public class Groups
        {
            public List<EventSession> Sessions;
        }
        
        
        public ActionResult Index(string Region, string Program)
        {
            ViewBag.Title = "Home Page";
            ViewBag.Region = Region = String.IsNullOrEmpty(Region) ? "US" : Region;

            ViewBag.Program = Program = String.IsNullOrEmpty(Program) ? "ITDP" : Program;

            ViewBag.Regions = new string[] { "India", "US", "Malaysia" };
            if(Region == "US")
                ViewBag.Programs = new string[] { "ITDP", "ITLP (FY16-FY18)", "LEADER (+ ITLP FY19)" };
            else
                ViewBag.Programs = new string[] { "ITDP", "ITLP"};
            if (Region == "India")
            {
                ViewBag.Date = "June 20-22, 2018";
                if (Program == "ITLP" || Program == "LEADER ( ITLP FY19)")
                {
                    ViewBag.Program = "ITLP";
                    Program = "ITLP";
                }
            }
            if (Region == "Malaysia")
            {
                ViewBag.Date = "June 26-27, 2018";
                if (Program == "ITLP (FY16-FY18)" || Program == "LEADER ( ITLP FY19)")
                {
                    ViewBag.Program = "ITLP";
                    Program = "ITLP";
                }
            }
            if (Region == "US")
                ViewBag.Date = "May 08-10, 2018";
            ViewBag.LatLngs = new KeyValuePair<string, string>[] {  new KeyValuePair<string, string>("US", "30.266620,-97.740375"),
                                                                    new KeyValuePair<string, string>("India", "28.580132,77.189365"),
                                                                    new KeyValuePair<string, string>("Malaysia", "3.153875,101.714669")
                                                                  };
            ViewBag.Agenda = GetAgenda(Region, Program);
            ViewBag.Sessions = GetSessions(Region, Program);
            ViewBag.TravelerNotes = GetTravelerNotes(Region, Program);
            ViewBag.Checklist = GetTravelerChecklist(Region, Program);
            return View();
        }

        private List<string> GetSessions(string region, string program)
        {
            List<string> sessions = new List<string>();
            if (region.ToLower() == "us")
            {
                sessions.Add("Opening Ceremony / Welcome");
                sessions.Add("Dell Strategy update(Ajaz Munsiff)");
                sessions.Add("Exec Panel – Digital Transformation");
                sessions.Add("Pivotal Overview(Kathy Burgess)");
                sessions.Add("Fireside Chat with Bask &Kellie");
                sessions.Add("CSR(Dell Children’s Hospital)");
                sessions.Add("Reception Dinner(Punchbowl Social)");
                if (program.ToLower() == "itdp")
                {
                    sessions.Add("Digital IT Simulation(Wronski)");
                }
                else if(program.ToLower() == "itlp (fy16-fy18)")
                {
                    sessions.Add("Influence Style Indicator(Goal Success)");
                    sessions.Add("Coaching(Goal Success)");
                    sessions.Add("Executive Challenge(Abilitie)");
                }
                else if(program.ToLower() == "leader (  itlp fy19)")
                {
                    sessions.Add("Influence / Executive Presence(Double Digit Sales)");
                    sessions.Add("Advancing Your Career(Christie Miller)");
                }
                sessions.Add("Graduation");
                sessions.Add("Farewell Lunch(Top Golf)");
            }
            return sessions;
        }

        public List<EventDay> GetAgenda(string Region, string Program)
        {
            string pro = Program.ToLower();
            List<EventDay> Agenda = new List<EventDay>();
            Groups group;
            EventDay day_one;
            switch (Region.ToLower())
            {
                case "us":
                    //First day
                    switch (Program.ToLower())
                    {
                        case "itlp (fy16-fy18)":
                            day_one = new EventDay();
                            day_one.date = "May 07";
                            day_one.attire = "smart biz casual / exec presence";
                            day_one.location = "Primrose AB";
                            day_one.Groups = new List<Groups>();
                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("07:30 AM - 08:30AM", "Registration & Breakfast"));
                            group.Sessions.Add(new EventSession("08:30 AM - 09:00AM", "Welcome/Agenda"));
                            group.Sessions.Add(new EventSession("09:00 AM - 10:00AM", "Keynote: The Parker Principles", "Mel Parker"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:30AM", "Class Pictures / Break"));
                            day_one.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("10:30 AM - 12:00PM", "Dell Finance & IT", "Tom Polosky, Parag Ved"));
                            group.Sessions.Add(new EventSession("12:00 PM - 01:00PM", "Lunch"));
                            day_one.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:00 PM - 03:00PM", "Next Level Leadership", "Sara Canaday"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 04:00PM", "Next Level Leadership- continued", "Sara Canaday"));
                            day_one.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("04:00 PM - 05:00PM", "Exec Panel - Next Level Leadership"));
                            group.Sessions.Add(new EventSession("05:00 PM - 05:15PM", "Wrap Up"));
                            day_one.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("05:15 PM - 07:00PM", "Exec Networking", "Westin Lobby Restaurant Private Dining Room"));
                            day_one.Groups.Add(group);

                            Agenda.Add(day_one);
                            break;
                    }
                    //Preparing day two with respect to programs
                    EventDay day_two = new EventDay();
                    day_two = new EventDay();
                    day_two.date = "May 08";
                    day_two.attire = "smart biz casual / exec presence";
                    day_two.location = "Main Ballroom";
                    day_two.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("07:30 AM - 08:30AM", "Registration & Breakfast"));
                    group.Sessions.Add(new EventSession("08:30 AM - 09:30AM", "Opening Ceremony/ Welcome/ Keynote or Team Activity"));
                    group.Sessions.Add(new EventSession("09:30 AM - 10:00AM", "Agenda"));
                    group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                    day_two.Groups.Add(group);

                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "FY19 Dell Strategy", "Ajaz Munsiff"));
                    group.Sessions.Add(new EventSession("12:00 PM - 01:00PM", "Lunch"));
                    day_two.Groups.Add(group);

                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("01:00 PM - 02:30PM", "Exec Panel - Digital Transformation"));
                    group.Sessions.Add(new EventSession("02:30 PM - 03:00PM", "Team Activity"));
                    group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                    day_two.Groups.Add(group);

                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("03:15 PM - 04:30PM", "Pivotal Overview", "Kathy Burgess, Emily Martinez"));
                    group.Sessions.Add(new EventSession("04:30 PM - 05:00PM", "Fireside chat", "Bask Iyer & Kellie Crantz"));
                    group.Sessions.Add(new EventSession("05:00 PM - 06:15PM", "Wrap Up or Raffle / ITDP class picture / (optional) CSR - onsite (Dell Children's Hospital)"));
                    day_two.Groups.Add(group);

                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("06:15 PM - 06:30PM", "Transition"));
                    group.Sessions.Add(new EventSession("06:30 PM - 08:30PM", "Welcome Reception Dinner", "Punchbowl Social"));
                    day_two.Groups.Add(group);
                    Agenda.Add(day_two);

                    //Preparing day three with a mix of common + program specific sessions
                    EventDay day_three = new EventDay();
                    day_three.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("07:00 AM - 08:00AM", "Breakfast"));
                    switch (Program.ToLower())
                    {
                        case "itdp":
                            day_three.location = "Main Ballroom";
                            day_three.date = "May 09";
                            day_three.attire = "jersey, jeans, comfortable shoes";
                            group.Sessions.Add(new EventSession("08:00 AM - 10:00AM", "Digital IT Future State Simulation", "Wronski"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                            group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "Digital IT Future State Simulation", "Wronski"));
                            day_three.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("12:00 PM - 12:30PM", "Lunch"));
                            group.Sessions.Add(new EventSession("12:30 PM - 01:15PM", "Team Activity"));
                            day_three.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:15 PM - 03:00PM", "Digital IT Future State Simulation", "Wronski"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 05:15PM", "Digital IT Future State Simulation", "Wronski"));
                            day_three.Groups.Add(group);
                            break;
                        case "itlp (fy16-fy18)":
                            day_three.location = "Primrose D";
                            day_three.date = "May 09";
                            day_three.attire = "jersey, jeans, comfortable shoes";
                            group.Sessions.Add(new EventSession("08:00 AM - 10:00AM", "Influence Style Indicator", "Goal Succcess"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                            group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "Influence Style Indicator", "Goal Succcess"));
                            day_three.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("12:00 PM - 12:45PM", "Lunch"));
                            group.Sessions.Add(new EventSession("12:45 PM - 01:15PM", "Team Contest/Activity","Main Ballroom"));
                            day_three.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:15 PM - 03:00PM", "Coaching", "Goal Succcess"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 05:00PM", "Coaching", "Goal Succcess"));
                            day_three.Groups.Add(group);

                            break;
                        case "leader (  itlp fy19)":
                            day_three.location = "Plumeria AB";
                            day_three.date = "May 09";
                            day_three.attire = "jersey, jeans, comfortable shoes";
                            group.Sessions.Add(new EventSession("08:00 AM - 10:00AM", "Influence/Presence", "DD Sales"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                            group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "Influence/Presence", "DD Sales"));
                            day_three.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("12:00 PM - 12:45PM", "Lunch"));
                            group.Sessions.Add(new EventSession("12:45 PM - 01:15PM", "Team Contest/Activity","Main Ballroom"));
                            day_three.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:15 PM - 03:00PM", "Influence/Presence", "DD Sales"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 05:15PM", "Influence/Presence", "DD Sales"));
                            day_three.Groups.Add(group);
                            break;
                    }
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("05:15 PM - 05:45PM", "Wrap Up/Raffle/ Group Photo","Main Ballroom"));
                    group.Sessions.Add(new EventSession("05:45 PM - 06:00PM", "transition"));
                    group.Sessions.Add(new EventSession("06:00 PM - 07:00PM", "Yoga (onsite)","optional"));
                    day_three.Groups.Add(group);
                    Agenda.Add(day_three);

                    EventDay day_four = new EventDay();
                    day_four.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("07:00 AM - 08:00AM", "Breakfast"));
                    switch (Program.ToLower())
                    {
                        case "itdp":
                            day_four.location = "Main Ballroom";
                            day_four.date = "May 10";
                            day_four.attire = "smart biz casual / exec presence";
                            group.Sessions.Add(new EventSession("08:00 AM - 10:00AM", "Digital IT Future State Simulation", "Wronski"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                            group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "Digital IT Future State Simulation", "Wronski"));
                            day_four.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("12:00 PM - 12:45PM", "Lunch"));
                            group.Sessions.Add(new EventSession("12:45 PM - 01:15PM", "Team Contest/Activity"));
                            day_four.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:15 PM - 03:00PM", "Digital IT Future State Simulation", "Wronski"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 04:15PM", "Simulation Judging", "IT Leaders"));
                            group.Sessions.Add(new EventSession("04:15 PM - 05:15PM", "Simulation Awards", "Wronski"));
                            day_four.Groups.Add(group);

                            break;
                        case "itlp (fy16-fy18)":
                            day_four.location = "Primrose D";
                            day_four.date = "May 10";
                            day_four.attire = "smart biz casual / exec presence";
                            group.Sessions.Add(new EventSession("08:00 AM - 10:00AM", "Exec Challenge", "Abilitie"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                            group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "Exec Challenge", "Abilitie"));
                            day_four.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("12:00 PM - 12:45PM", "Lunch"));
                            group.Sessions.Add(new EventSession("12:45 PM - 01:15PM", "Team Contest/Activity","Main Ballroom"));
                            day_four.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:15 PM - 03:00PM", "Exec Challenge", "Abilitie"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 04:15PM", "Exec Challenge", "Abilitie"));
                            group.Sessions.Add(new EventSession("03:15 PM - 05:15PM", "Exec Readouts","IT Leaders"));
                            day_four.Groups.Add(group);

                            break;
                        case "leader (  itlp fy19)":
                            day_four.location = "Plumeria AB";
                            day_four.date = "May 10";
                            day_four.attire = "smart biz casual / exec presence";
                            group.Sessions.Add(new EventSession("08:00 AM - 10:00AM", "Advancing your career", "Christi Miller, Angie Bickley"));
                            group.Sessions.Add(new EventSession("10:00 AM - 10:15AM", "Break"));
                            group.Sessions.Add(new EventSession("10:15 AM - 12:00PM", "Advancing your career", "Christi Miller, Angie Bickley"));
                            day_four.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("12:00 PM - 12:45PM", "Lunch"));
                            group.Sessions.Add(new EventSession("12:45 PM - 01:15PM", "Team Contest/Activity","Main Ballroom"));
                            day_four.Groups.Add(group);

                            group = new Groups();
                            group.Sessions = new List<EventSession>();
                            group.Sessions.Add(new EventSession("01:15 PM - 03:00PM", "Advancing your career", "Christi Miller, Angie Bickley"));
                            group.Sessions.Add(new EventSession("03:00 PM - 03:15PM", "Break"));
                            group.Sessions.Add(new EventSession("03:15 PM - 05:00PM", "Advancing your career", "Christi Miller, Angie Bickley"));
                            day_four.Groups.Add(group);
                            break;
                    }
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("05:15 PM - 07:00PM", "Graduate Recognition \r\n Wrap Up/Raffle/Team Awards \r\n Group Picture/Cocktail Reception Dinner","Main Ballroom"));
                    day_four.Groups.Add(group);
                    Agenda.Add(day_four);

                    break;
                case "india":
                    //First day
                    day_one = new EventDay();
                    day_one.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration india"));
                    group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Induction india"));
                    day_one.Groups.Add(group);
                    Agenda.Add(day_one);

                    //Preparing day two with respect to programs
                    day_two = new EventDay();
                    day_two.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    switch (Program.ToLower())
                    {
                        case "itdp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for india ITDPs"));
                            break;
                        case "itlp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for ITLPs"));
                            break;
                        case "leaders":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for Leaders"));
                            break;
                    }
                    day_two.Groups.Add(group);
                    Agenda.Add(day_two);
                    //Preparing day three with a mix of common + program specific sessions
                    day_three = new EventDay();
                    day_three.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for third day - Common"));
                    switch (Program.ToLower())
                    {
                        case "itdp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Specific for ITDPs"));
                            break;
                        case "itlp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Specific for ITLPs"));
                            break;
                        case "leaders":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Specific for Leaders"));
                            break;
                    }
                    day_three.Groups.Add(group);
                    Agenda.Add(day_three);
                    break;
                case "malaysia":
                    //First day
                    day_one = new EventDay();
                    day_one.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration malaysia"));
                    group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Induction malaysia"));
                    day_one.Groups.Add(group);
                    Agenda.Add(day_one);

                    //Preparing day two with respect to programs
                    day_two = new EventDay();
                    day_two.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    switch (Program.ToLower())
                    {
                        case "itdp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for malaysia ITDPs"));
                            break;
                        case "itlp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for ITLPs"));
                            break;
                        case "leaders":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for Leaders"));
                            break;
                    }
                    day_two.Groups.Add(group);
                    Agenda.Add(day_two);
                    //Preparing day three with a mix of common + program specific sessions
                    day_three = new EventDay();
                    day_three.Groups = new List<Groups>();
                    group = new Groups();
                    group.Sessions = new List<EventSession>();
                    group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Breakfast & Registration for third day - Common"));
                    switch (Program.ToLower())
                    {
                        case "itdp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Specific for ITDPs"));
                            break;
                        case "itlp":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Specific for ITLPs"));
                            break;
                        case "leaders":
                            group.Sessions.Add(new EventSession("08:00 AM - 09:00AM", "Specific for Leaders"));
                            break;
                    }
                    day_three.Groups.Add(group);
                    Agenda.Add(day_three);
                    break;
            }
            return Agenda;
        }

        public List<EventSession> GetTravelerNotes(string region, string Program)
        {
            List<EventSession> TEvents = new List<EventSession>();
            switch (region.ToLower())
            {
                case "us":
                    TEvents.Add(new EventSession("Hotels", "", "We will be hosting this year’s Austin summit at the Westin/Domain, please use this link to book your Hotel room", "https://www.starwoodmeeting.com/Book/DellSummit", "You should check into the hotel using your own corporate card, and then submit for reimbursement of these costs when you file your expense report."));
                    TEvents.Add(new EventSession("Meals", "", "Breakfast and Lunch will be provided to you on Tue May 8 thru Thu May 10, plus additional meals during social events after summit hours on Tue and Fri.\n Use the Registration Link above to tell us about any dietary restrictions.", "", "If you have any additional meal expenses outside of those provided, you can submit for reimbursement when you file your expense report (Daily limit of $75 per corporate policy)"));
                    TEvents.Add(new EventSession("Ground Transportation", "", "Upon arrival at the Austin airport, you will have many options for ground transportation to the hotel - Upper level provides options for Rental Cars; Lower level provides options for Taxi, Limo, Shuttle, and a number of transportation companies, such as: Lyft and Uber. The Westin hotel is also providing a discount for guests using Super Shuttle (use below link). Drive time will be approximately 45 minutes from the airport to the hotel, and some routes may have tolls.", "http://groups.supershuttle.com/westinaustinatthedomainguesttransportation.html", "You can submit for reimbursement of these costs when you file your expense report."));
                    //TEvents.Add(new EventSession("Weather", "", "", "https://weather.com/weather/tenday/l/Austin+TX+USTX0057:1:US", ""));
                    TEvents.Add(new EventSession("Expenses","","","",""));
                    break;
                case "india":
                    TEvents.Add(new EventSession("5/5/18 - 5/6/18", "Pecan Street Spring Arts Festival", "A free, family event, the Pecan Street Festival is the oldest and largest art festival in Central Texas. Musicians, food vendors, artists and crafts people turn Sixth Street - historically called Pecan Street into a lively street fair where there is something for people of all ages.", "www.pecanstreetfestival.org", "East Sixth Street"));
                    TEvents.Add(new EventSession("5/6/18 ( 7:30 am - 10:00 am)", "5th Annual Silicon Labs Sunshine Run", "The fifth annual Silicon Labs Sunshine Run will take place on Sunday, May 6 in the heart of downtown Austin on the certified course beginning at Vic Mathias Shores at Town Lake on 900 West Riverside Drive. Funds raised from the race will benefit Austin Sunshine Camps making a positive difference in the lives of Austin’s low-income youth.", "https://thingstodo.austin360.com/venue/auditorium-shores", "Auditorium Shores, 900 W Riverside Dr, Austin, TX 78704"));
                    break;
                case "malaysia":
                    TEvents.Add(new EventSession("5/5/18 - 5/6/18", "Pecan Street Spring Arts Festival", "A free, family event, the Pecan Street Festival is the oldest and largest art festival in Central Texas. Musicians, food vendors, artists and crafts people turn Sixth Street - historically called Pecan Street into a lively street fair where there is something for people of all ages.", "www.pecanstreetfestival.org", "East Sixth Street"));
                    TEvents.Add(new EventSession("5/6/18 ( 7:30 am - 10:00 am)", "5th Annual Silicon Labs Sunshine Run", "The fifth annual Silicon Labs Sunshine Run will take place on Sunday, May 6 in the heart of downtown Austin on the certified course beginning at Vic Mathias Shores at Town Lake on 900 West Riverside Drive. Funds raised from the race will benefit Austin Sunshine Camps making a positive difference in the lives of Austin’s low-income youth.", "https://thingstodo.austin360.com/venue/auditorium-shores", "Auditorium Shores, 900 W Riverside Dr, Austin, TX 78704"));
                    break;
                default:
                    TEvents.Add(new EventSession("5/5/18 - 5/6/18", "Pecan Street Spring Arts Festival", "A free, family event, the Pecan Street Festival is the oldest and largest art festival in Central Texas. Musicians, food vendors, artists and crafts people turn Sixth Street - historically called Pecan Street into a lively street fair where there is something for people of all ages.", "www.pecanstreetfestival.org", "East Sixth Street"));
                    break;
            }
            return TEvents;
        }
        public List<EventSession> GetTravelerChecklist(string region,string Program)
        {
            List<string> Checklist = new List<string>();
            List<EventSession> TEvents = new List<EventSession>();
            switch (region.ToLower())
            {
                case "us":
                    //TEvents.Add(new EventSession("What to Wear", "All sessions during the summit will be business casual (unless noted on your daily agenda), but jeans are perfectly fine at any time (after all, this is Austin, Texas!) as long as they are not ripped or frayed. Please no shorts, tank tops, or flip flops. Please refer to the article <a src='https://www.thebalance.com/business-casual-dress-code-1919379'>here</a> for further guidance."));
                    //TEvents.Add(new EventSession("What to Bring", "All sessions during the summit will be business casual (unless noted on your daily agenda), but jeans are perfectly fine at any time (after all, this is Austin, Texas!) as long as they are not ripped or frayed. Please no shorts, tank tops, or flip flops. Please refer to the article <a src='https://www.thebalance.com/business-casual-dress-code-1919379'>here</a> for further guidance."));
                    //TEvents.Add(new EventSession("Things to do", "All sessions during the summit will be business casual (unless noted on your daily agenda), but jeans are perfectly fine at any time (after all, this is Austin, Texas!) as long as they are not ripped or frayed. Please no shorts, tank tops, or flip flops. Please refer to the article <a src='https://www.thebalance.com/business-casual-dress-code-1919379'>here</a> for further guidance."));
                    //TEvents.Add(new EventSession("What to Wear", "All sessions during the summit will be business casual (unless noted on your daily agenda), but jeans are perfectly fine at any time (after all, this is Austin, Texas!) as long as they are not ripped or frayed. Please no shorts, tank tops, or flip flops. Please refer to the article <a src='https://www.thebalance.com/business-casual-dress-code-1919379'>here</a> for further guidance."));
                    //TEvents.Add(new EventSession("What to Wear", "All sessions during the summit will be business casual (unless noted on your daily agenda), but jeans are perfectly fine at any time (after all, this is Austin, Texas!) as long as they are not ripped or frayed. Please no shorts, tank tops, or flip flops. Please refer to the article <a src='https://www.thebalance.com/business-casual-dress-code-1919379'>here</a> for further guidance."));
                    break;
                //case "india":
                //    Checklist.Add(" Texas State Capital "); Checklist.Add(" Umlauf Sculpture Garden "); Checklist.Add(" Zilker Park "); Checklist.Add(" Lady Bird Lake "); Checklist.Add(" Texas State History Museum "); Checklist.Add(" 360 Bridge "); Checklist.Add(" Mount Bonnell "); Checklist.Add(" Blanton Museum of Art "); Checklist.Add(" University of Texas "); Checklist.Add(" Barton Creek "); Checklist.Add(" Zilker Botanical Garden "); Checklist.Add(" HOPE Outdoor Gallery");

                //    break;
                //case "malaysia":
                //    Checklist.Add(" Texas State Capital "); Checklist.Add(" Umlauf Sculpture Garden "); Checklist.Add(" Zilker Park "); Checklist.Add(" Lady Bird Lake "); Checklist.Add(" Texas State History Museum "); Checklist.Add(" 360 Bridge "); Checklist.Add(" Mount Bonnell "); Checklist.Add(" Blanton Museum of Art "); Checklist.Add(" University of Texas "); Checklist.Add(" Barton Creek "); Checklist.Add(" Zilker Botanical Garden "); Checklist.Add(" HOPE Outdoor Gallery");

                //    break;
                //default:
                //    Checklist.Add(" Texas State Capital "); Checklist.Add(" Umlauf Sculpture Garden "); Checklist.Add(" Zilker Park "); Checklist.Add(" Lady Bird Lake "); Checklist.Add(" Texas State History Museum "); Checklist.Add(" 360 Bridge "); Checklist.Add(" Mount Bonnell "); Checklist.Add(" Blanton Museum of Art "); Checklist.Add(" University of Texas "); Checklist.Add(" Barton Creek "); Checklist.Add(" Zilker Botanical Garden "); Checklist.Add(" HOPE Outdoor Gallery");
                //    break;
            }
            return TEvents;
        }

        public JsonResult GetLatitudeLongitude(string Region)
        {
            LatLng LL = null;
            switch (Region.ToLower())
            {
                case "US":
                    LL = new LatLng("30.266620", "-97.740375");
                    break;
                case "india":
                    LL = new LatLng("28.580132", "77.189365");
                    break;
                case "malaysia":
                    LL = new LatLng("3.153875", "101.714669");
                    break;
                default:
                    LL = new LatLng("0", "0");
                    break;
            }
            return Json(LL);
        }

    }


}
