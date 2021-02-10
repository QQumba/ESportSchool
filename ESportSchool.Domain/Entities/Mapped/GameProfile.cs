using ESportSchool.Domain.Constants;

namespace ESportSchool.Domain.Entities.Mapped
{
    public class GameProfile : BaseEntity
    {
        public Game Game { get; set; }
        public string About { get; set; }
        public string Rank { get; set; }
        public string AdditionalRankInfo { get; set; }
    }
}