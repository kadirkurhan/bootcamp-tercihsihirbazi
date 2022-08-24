using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Dtos.ProfileDtos
{
    public class ProfileUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
