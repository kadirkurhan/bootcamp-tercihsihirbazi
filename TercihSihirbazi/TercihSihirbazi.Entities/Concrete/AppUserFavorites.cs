using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Concrete
{
    public class AppUserFavorites
    {
        public AppUser AppUser { get; set; }
        public DetailObject Favorited { get; set; }
        public int AppUserId { get; set; }
        public int DetailObjectId { get; set; }
    }
}
