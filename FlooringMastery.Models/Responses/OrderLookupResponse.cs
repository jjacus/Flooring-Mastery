﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
     public class OrderLookupResponse : Response
    {
        public List<Order> Orders { get; set; }

        public string Date { get; set; }
    }
}
