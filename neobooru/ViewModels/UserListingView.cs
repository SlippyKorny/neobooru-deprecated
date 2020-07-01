using Microsoft.AspNetCore.Identity;
using neobooru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neobooru.ViewModels
{
    public class UserListingView
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string Role { get; set; }

        public UserListingView(string id, string username, string email, DateTime registeredOn, string role)
        {
            Id = id;
            Username = username;
            Email = email;
            RegisteredOn = registeredOn;
            Role = role;
        }
    }
}
