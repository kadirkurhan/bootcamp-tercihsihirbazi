using TercihSihirbazi.Entities.Concrete;
using TercihSihirbazi.Entities.Interfaces;

namespace TercihSihirbazi.Entities.Dtos.ProfileDtos
{
    public class ProfileUserFavoritesDto : IDto
    {
        public string Username { get; set; }
        public List<DetailObject> Favorites { get; set; }
    }
}