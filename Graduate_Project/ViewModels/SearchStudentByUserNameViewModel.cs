namespace Graduate_Project.ViewModels
{
    public class SearchStudentByUserNameViewModel
    {
        public int GraduationStudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string University { get; set; }
        public string College { get; set; }
        public string Specialization { get; set; }
        public string Degree { get; set; }
        public string UserId { get; set; }


        // List of skills for each student
        public List<string> Skills { get; set; } = new List<string>();
    }
}
