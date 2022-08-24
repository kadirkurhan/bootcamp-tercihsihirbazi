using System;
using System.Collections.Generic;
using System.Text;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Concrete
{
    public class Profile : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<DetailObject> FavoriteList { get; set; }

    }
}
