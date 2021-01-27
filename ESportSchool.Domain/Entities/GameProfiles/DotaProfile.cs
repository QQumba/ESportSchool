using ESportSchool.Domain.Entities.GameProfiles.GameRanks;

namespace ESportSchool.Domain.Entities.GameProfiles
{
    public class DotaProfile : BaseGameProfile
    {
        public override string Name { get; } = "Dota 2";
        public DotaRank Rank { get; set; }
    }
}