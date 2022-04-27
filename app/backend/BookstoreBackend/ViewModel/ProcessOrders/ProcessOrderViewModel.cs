﻿using System.Collections.Generic;
using BobsBookstore.Models.Orders;
using BookstoreBackend.ViewModel.ManageOrders;

namespace BookstoreBackend.ViewModel.ProcessOrders
{
    public class ProcessOrderViewModel
    {
        public long OrderId { get; set; }

        public long Status { get; set; }

        public long OldStatus { get; set; }

        public Order Order { get; set; }

        public List<OrderStatus> Statuses { get; set; }

        public PartialOrderViewModel FullOrder { get; set; }

        public string ErrorMessage { get; set; }
    }
}