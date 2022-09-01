using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Dtos.ProfileDtos
{
    public class ProfileFavoriteDto : IDto
    {
        public int DetailObjectId { get; set; }
        public int AppUserId { get; set; }
    }
}
