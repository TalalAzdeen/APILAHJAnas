﻿using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace AutoGenerator.Models
{
    public class Subscription : ITModel
    {
        [Key]
        public required string Id { get; set; }
        public required string CustomerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime CurrentPeriodStart { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }

        public required string Status { get; set; }
        //public required string BillingPeriod { get; set; } // interval of price like (day,week,month,year)
        public bool CancelAtPeriodEnd { get; set; } = false;
        public int NumberRequests { get; set; }
        public int AllowedRequests { get; set; }
        public int AllowedSpaces { get; set; } 
        public DateTime? CancelAt { get; set; }
        public DateTime? CanceledAt { get; set; }

        public string? PlanId { get; set; }
        public Plan? Plan { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<Request>? Requests { get; set; } = [];
        public ICollection<Space> Spaces { get; set; } = [];

    }
}
