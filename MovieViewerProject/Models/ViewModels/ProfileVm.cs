using MovieViewerProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieViewerProject.Models.ViewModels
{
    public class ProfileVm
    {
        public int ProfileId { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required, MaxLength(12), Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Credit Card Expiration")]
        public DateTime CreditCardExp { get; set; }

        [Required, MaxLength(100), Display(Name = "Street Address")]
        public string StreetAddress { get; set; }


        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(2)]
        public string State { get; set; }


        [Required]
        public int Zip { get; set; }

        [NotMapped, Display(Name = "Total Money Spent")]
        public float MoneySpent { get; set; }

        [NotMapped, Display(Name = "Has Paid")]
        public bool HasPaid { get; set; }


        public ICollection<Watched> WatchedCollection { get; set; }

        public Profile GetProfileInstance()
        {
            return new Profile
            {
                ProfileId = 0,
                ApplicationUserId = User.Id,
                User = User,
                CreditCardNumber = this.CreditCardNumber,
                CreditCardExp = this.CreditCardExp,
                StreetAddress = this.StreetAddress,
                City = this.City,
                State = this.State,
                Zip = this.Zip,
                MoneySpent = this.MoneySpent,
                HasPaid = this.HasPaid,
                WatchedCollection = this.WatchedCollection
               
            };

        }







    }
}
