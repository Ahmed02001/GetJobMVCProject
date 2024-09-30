namespace Graduate_Project.Models
{
    public class StudentSkill
    {
        public int StudentId { get; set; }
        public GraduationStudent Student { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}