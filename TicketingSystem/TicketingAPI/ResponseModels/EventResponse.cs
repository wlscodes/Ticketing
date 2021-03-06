﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingAPI.DatabaseModels;

namespace TicketingAPI.ResponseModels
{
    public class EventResponse
    {
        public int EventId { get; set; }
        public int OrganizatorId { get; set; }
        public string EventName { get; set; }
        public string OrganizatorName { get; set; }
        public DateTime EventBeginDate { get; set; }
        public DateTime EventFinishDate { get; set; }
        public IList<EventSeats> Sections { get; set; }
        //public List<SectionResponse> Sections { get; set; }
    }
}
