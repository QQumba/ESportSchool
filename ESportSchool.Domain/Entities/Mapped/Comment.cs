namespace ESportSchool.Domain.Entities.Mapped
{
    public class Comment : BaseEntity
    {
        public string Value { get; set; }
        public bool Edited { get; set; }
        //relations
        public virtual Coach Coach { get; set; }
        public virtual User User { get; set; }
    }
}