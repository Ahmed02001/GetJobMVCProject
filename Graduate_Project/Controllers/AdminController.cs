using Graduate_Project.ViewModels;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Graduate_Project.Models;
using ClosedXML.Excel;
using Font = iTextSharp.text.Font;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Graduate_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        public readonly ApplicationDbContextProject _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, ApplicationDbContextProject context)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult index()

        {
            var users = _userManager.Users;

            int userCount = users.Count();

            ViewBag.UserCount = userCount;

            ViewBag.StudentCount = _context.Students.Count();

            return View("index");
        }


        public IActionResult UserReport()
        {
           
            var users = _userManager.Users
            .Select(user => new UserReportViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RegistrationDate = user.LockoutEnd.Value.Date.ToLocalTime(),
            }).ToList();



            return PartialView("_Users", users); // Pass the list to the view
        }


        public async Task<IActionResult> ExportUserReportPDF()
        {
            // Fetch all users from UserManager
            var users = await _userManager.Users.ToListAsync();

            // Create a MemoryStream to store the PDF document
            using (var memoryStream = new MemoryStream())
            {
                // Create a PDF document with margins
                var document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, memoryStream);

                // Open the document to enable writing to it
                document.Open();

                // Add a title to the PDF
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                document.Add(new Paragraph("Users Report", titleFont));
                document.Add(new Paragraph("\n"));

                // Regular font for student details
                var regularFont = FontFactory.GetFont("Arial", 12);

                // Loop through each user and add details as a list
                foreach (var user in users)
                {
                    // Add user details in paragraph format
                    document.Add(new Paragraph($"User ID: {user.Id}", regularFont));
                    document.Add(new Paragraph($"Email: {user.Email}", regularFont));
                    document.Add(new Paragraph($"Username: {user.UserName}", regularFont));

                    // Assuming you have a property for registration date (for example, created date or lockout end)
                    var registrationDate = user.LockoutEnd?.ToString("yyyy-MM-dd") ?? "N/A";
                    document.Add(new Paragraph($"Registration Date: {registrationDate}", regularFont));

                    // Add space between users for better readability
                    document.Add(new Paragraph("\n"));
                }

                // Add the current date and time at the bottom of the PDF
                var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var dateFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                var dateTimeParagraph = new Paragraph($"Generated on: {dateTime}", dateFont);
                dateTimeParagraph.Alignment = Element.ALIGN_CENTER;

                // Add space before the date
                document.Add(new Paragraph("\n\n\n"));
                document.Add(dateTimeParagraph);

                // Close the document
                document.Close();

                // Return the PDF document as a download
                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", "UsersReport.pdf");
            }
        }


        public ActionResult ExportUserReportEXCEL()
        {
            var users = _userManager.Users.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("User Report");

                worksheet.Cell(1, 1).Value = "#";
                worksheet.Cell(1, 2).Value = "UserId";
                worksheet.Cell(1, 3).Value = "UserName";
                worksheet.Cell(1, 4).Value = "Email Address";
                worksheet.Cell(1, 5).Value = "Registration Date";
                

                int row = 2;
                foreach (var user in users)
                {
                    worksheet.Cell(row, 1).Value = row - 1;
                    worksheet.Cell(row, 2).Value = user.Id;
                    worksheet.Cell(row, 3).Value = user.UserName;
                    worksheet.Cell(row, 4).Value = user.Email;
                    worksheet.Cell(row, 5).Value = user.LockoutEnd.HasValue ? user.LockoutEnd.Value.DateTime.ToString("yyyy-MM-dd") : string.Empty;


                    row++;
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "reports", "UserReport.xlsx");
                Directory.CreateDirectory(Path.GetDirectoryName(path)); // Ensure the directory exists
                workbook.SaveAs(path);

                // Generate the file for download
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserReport.xlsx");
                }
            }
        }

        public IActionResult DeleteStudent(int graduationStudentId)
        {
            // Fetch the student record by GraduationStudentId
            var studentToDelete = _context.Students
                .FirstOrDefault(s => s.GraduationStudentId == graduationStudentId);

            if (studentToDelete == null)
            {
                // If no student is found, return a "Not Found" view or message
                return NotFound($"Student with ID {graduationStudentId} not found.");
            }

            // Fetch the related StudentSkills based on GraduationStudentId (StudentId)
            var studentSkillsToDelete = _context.StudentSkills
                .Where(ss => ss.StudentId == graduationStudentId)
                .ToList();

            // Remove the matching StudentSkills records (if any)
            if (studentSkillsToDelete.Any())
            {
                _context.StudentSkills.RemoveRange(studentSkillsToDelete);
            }

            // Remove the student record itself
            _context.Students.Remove(studentToDelete);

            // Save the changes to the database
            _context.SaveChanges();

            // Optionally, redirect to a list or success page after deletion
            return RedirectToAction("StudentDetails"); // Redirect to the index or a list of students after deletion
        }

        public async Task<IActionResult> Delete(string userId)
        {
            // Fetch the user by userId
            var userToDelete = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (userToDelete == null)
            {
                // If the user is not found, return a "Not Found" view or message
                return NotFound($"User with ID {userId} not found.");
            }

            // Fetch the students associated with the user
            var studentsToDelete = _context.Students
                .Where(s => s.UserId == User.Identity.Name) // Assuming there's a UserId field in Students table
                .ToList();

            // Fetch all student skills related to the students
            var studentIds = studentsToDelete.Select(s => s.GraduationStudentId).ToList();
            var studentSkillsToDelete = _context.StudentSkills
                .Where(ss => studentIds.Contains(ss.StudentId))
                .ToList();

            // Remove the matching StudentSkills records (if any)
            if (studentSkillsToDelete.Any())
            {
                _context.StudentSkills.RemoveRange(studentSkillsToDelete);
            }

            // Remove the students associated with the user
            if (studentsToDelete.Any())
            {
                _context.Students.RemoveRange(studentsToDelete);
            }

            // Remove the user record itself
            var result = await _userManager.DeleteAsync(userToDelete);

            if (!result.Succeeded)
            {
                // If user deletion fails, return an error view with the details
                return View("Error", result.Errors);
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Redirect to a list or success page after deletion
            return RedirectToAction("UserReport", "Admin"); // Redirect to the index or user list after deletion
        }




        public async Task<IActionResult> StudentDetails()
        {
            var query = await(from student in _context.Students
                              join studentSkill in _context.StudentSkills
                                  on student.GraduationStudentId equals studentSkill.StudentId
                              join skill in _context.Skills
                                  on studentSkill.SkillId equals skill.Id
                              select new
                              {
                                  student.GraduationStudentId,
                                  student.Name,
                                  student.Email,
                                  student.Phone,
                                  student.University,
                                  student.College,
                                  student.Specialization,
                                  student.Degree,
                                  student.UserId,
                                  skill.SkillName
                              }).ToListAsync();

            return View(query);
        }


        public IActionResult ReportAllStudent()
        {
            // Fetch students data from the database
            var students = (from student in _context.Students
                            join studentSkill in _context.StudentSkills
                                on student.GraduationStudentId equals studentSkill.StudentId
                            join skill in _context.Skills
                                on studentSkill.SkillId equals skill.Id
                            select new
                            {
                                student.GraduationStudentId,
                                student.Name,
                                student.Email,
                                student.Phone,
                                student.University,
                                student.College,
                                student.Specialization,
                                student.Degree,
                                student.UserId,
                                skill.SkillName
                            }).ToList();

            // Group students by UserId
            var groupedStudents = students
                .GroupBy(s => new { s.UserId, s.Name })
                .ToList();

            // Create a MemoryStream to store the PDF document
            using (var memoryStream = new MemoryStream())
            {
                // Create a PDF document with margins
                var document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(document, memoryStream);

                // Open the document to enable writing to it
                document.Open();

                // Add a title to the PDF
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                document.Add(new Paragraph("Student Report", titleFont));
                document.Add(new Paragraph("\n"));

                // Regular font for student details
                var regularFont = FontFactory.GetFont("Arial", 12);

                // Loop through each group and add details
                foreach (var group in groupedStudents)
                {
                    var userId = group.Key.UserId;
                    var studentName = group.Key.Name;

                    // Get all skills for the current user
                    var skills = group.Select(s => s.SkillName).Distinct().ToList();
                    var skillsList = string.Join(", ", skills);

                    // Print student details
                    document.Add(new Paragraph($"User Name: {userId}", regularFont));
                    document.Add(new Paragraph($"Name: {studentName}", regularFont));
                    document.Add(new Paragraph($"Graduation ID: {group.First().GraduationStudentId}", regularFont));
                    document.Add(new Paragraph($"Email: {group.First().Email}", regularFont));
                    document.Add(new Paragraph($"Phone: {group.First().Phone}", regularFont));
                    document.Add(new Paragraph($"University: {group.First().University}", regularFont));
                    document.Add(new Paragraph($"College: {group.First().College}", regularFont));
                    document.Add(new Paragraph($"Specialization: {group.First().Specialization}", regularFont));
                    document.Add(new Paragraph($"Degree: {group.First().Degree}", regularFont));
                    document.Add(new Paragraph($"Skills: {skillsList}", regularFont));
                    document.Add(new Paragraph("\n")); // Add space for readability
                    
                }

                // Add the current date and time at the bottom of the PDF
                var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var dateFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                var dateTimeParagraph = new Paragraph($"Generated on: {dateTime}", dateFont);
                dateTimeParagraph.Alignment = Element.ALIGN_CENTER;

                // Add space before the date
                document.Add(new Paragraph("\n\n\n"));
                document.Add(dateTimeParagraph);

                // Close the document
                document.Close();

                // Return the PDF document as a download
                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", "StudentReport.pdf");
            }
        }

        public ActionResult ReportAllStudentEXCEL()
        {
            var Results = (from student in _context.Students
                           join studentSkill in _context.StudentSkills
                               on student.GraduationStudentId equals studentSkill.StudentId
                           join skill in _context.Skills
                               on studentSkill.SkillId equals skill.Id
                           select new
                           {
                               student.GraduationStudentId,
                               student.Name,
                               student.Email,
                               student.Phone,
                               student.University,
                               student.College,
                               student.Specialization,
                               student.Degree,
                               student.UserId,
                               skill.SkillName
                           }).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Student Report");

                worksheet.Cell(1, 1).Value = "#";
                worksheet.Cell(1, 2).Value = "StudentId";
                worksheet.Cell(1, 3).Value = "Full Name";
                worksheet.Cell(1, 4).Value = "Email Address";
                worksheet.Cell(1, 5).Value = "Phone Number";
                worksheet.Cell(1, 6).Value = "University";
                worksheet.Cell(1, 7).Value = "College";
                worksheet.Cell(1, 8).Value = "Specialization";
                worksheet.Cell(1, 9).Value = "Degree";
                worksheet.Cell(1, 10).Value = "UserName";
                worksheet.Cell(1, 11).Value = "Skill";

                int row = 2;
                foreach (var student in Results)
                {
                    worksheet.Cell(row, 1).Value = row - 1;
                    worksheet.Cell(row, 2).Value = student.GraduationStudentId;
                    worksheet.Cell(row, 3).Value = student.Name;
                    worksheet.Cell(row, 4).Value = student.Email;
                    worksheet.Cell(row, 5).Value = student.Phone;
                    worksheet.Cell(row, 6).Value = student.University;
                    worksheet.Cell(row, 7).Value = student.College;
                    worksheet.Cell(row, 8).Value = student.Specialization;
                    worksheet.Cell(row, 9).Value = student.Degree;
                    worksheet.Cell(row, 10).Value = student.UserId;
                    worksheet.Cell(row, 11).Value = student.SkillName;
                    row++;
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "reports", "AllStudentReport.xlsx");
                Directory.CreateDirectory(Path.GetDirectoryName(path)); // Ensure the directory exists
                workbook.SaveAs(path);

                // Generate the file for download
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllStudentReport.xlsx");
                }
            }
        }

        public IActionResult SearchByUserName()
        {
            var searchStudent = (from student in _context.Students
                              join studentSkill in _context.StudentSkills
                                  on student.GraduationStudentId equals studentSkill.StudentId
                              join skill in _context.Skills
                                  on studentSkill.SkillId equals skill.Id
                              select new
                              {
                                  student.GraduationStudentId,
                                  student.Name,
                                  student.Email,
                                  student.Phone,
                                  student.University,
                                  student.College,
                                  student.Specialization,
                                  student.Degree,
                                  student.UserId,
                                  skill.SkillName
                              }).ToList();


            
            return View(searchStudent);
        }

        
    }
}
