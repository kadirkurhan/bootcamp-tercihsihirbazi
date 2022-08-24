using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Dtos.ProfileDtos
{
    public class ProfileAddDto : IDto
    {
        public string Name { get; set; }
    }
}
