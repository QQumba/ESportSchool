using ESportSchool.Domain.Entities.GameProfiles.GameRanks;

namespace ESportSchool.Domain.Entities.GameProfiles
{
    public class CsgoProfile : BaseGameProfile
    {
        public override string Name { get; } = "CS:GO";
        public CsgoRank Rank { get; set; }   
    }
}