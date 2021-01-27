using System.Runtime.InteropServices;
using ESportSchool.Domain.Entities.NotMapped;

namespace ESportSchool.Domain.Entities.GameProfiles
{
    public abstract class BaseGameProfile : BaseEntity
    {
        public virtual string Name { get; }
        public string About { get; set; }
    }
}