using ESportSchool.Domain.Entities.GameProfiles.GameRanks;

namespace ESportSchool.Domain.Entities.GameProfiles
{
    public class LolProfile : BaseGameProfile
    {
        public override string Name { get; } = "League of Legends";
        public LolRank Rank { get; set; }
    }
}