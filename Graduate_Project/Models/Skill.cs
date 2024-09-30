

namespace Graduate_Project.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillName { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<StudentSkill> StudentSkills { get; set; }
    }
}
