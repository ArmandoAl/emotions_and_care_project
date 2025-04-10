using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Domain
{
    public class DummyUser : User
    {
        public string? Test { get; set; } = "";

        public BadgeCollection badgeCollection { get; set; } = new BadgeCollection();
    }
}