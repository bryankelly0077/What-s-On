﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsOn.Models
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }
        IEnumerable<Event> EventsOfTheWeek { get; }

        Event GetEventById(int eventId);
    }
}
