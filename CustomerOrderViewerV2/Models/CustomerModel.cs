﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderViewerV2.Models
{
    class CustomerModel
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}