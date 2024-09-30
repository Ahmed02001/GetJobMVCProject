using ClosedXML.Excel;
using Graduate_Project.Models;
using Graduate_Project.ViewModels;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;


namespace Graduate_Project.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {

        private readonly ApplicationDbContextProject _context;

        private readonly JobPredictionService _jobPredictionService;

        private readonly IWebHostEnvironment _webHostEnvironment;



        public StudentController(ApplicationDbContextProject context, IWebHostEnvironment webHostEnvironment, JobPredictionService jobPredictionService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _jobPredictionService = jobPredictionService;
        }

        [BindProperty]
        public StudentSkillsViewModel Input { get; set; }

        public void OnGet(int studentId)
        {
            Input = new StudentSkillsViewModel
            {
                StudentId = studentId
            };
        }
        //ApplicationDbContextProject context = new ApplicationDbContextProject();


        [HttpGet]
        public IActionResult New()
        {

            var model = new StudentSkillsViewModel
            {
                

                UniversityOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "جامعة الملك سعود King Saud University", Text = "جامعة الملك سعود King Saud University" },
                    new SelectListItem { Value = "جامعة الملك عبد العزيز King Abdulaziz University", Text = "جامعة الملك عبد العزيز King Abdulaziz University" },
                    new SelectListItem { Value = "جامعة الملك فهد للبترول والمعادن King Fahd University of Petroleum and Minerals", Text = "جامعة الملك فهد للبترول والمعادن King Fahd University of Petroleum and Minerals" },
                    new SelectListItem { Value = "جامعة الملك فيصل King Faisal University", Text = "جامعة الملك فيصل King Faisal University" },
                    new SelectListItem { Value = "جامعة الملك خالد King Khalid University", Text = "جامعة الملك خالد King Khalid University" },
                    new SelectListItem { Value = "جامعة الإمام محمد بن سعود الإسلامية Imam Muhammad bin Saud Islamic University", Text = "جامعة الإمام محمد بن سعود الإسلامية Imam Muhammad bin Saud Islamic University" },
                    new SelectListItem { Value = "جامعة الأميرة نورة بنت عبد الرحمن Princess Nourah bint Abdulrahman University", Text = "جامعة الأميرة نورة بنت عبد الرحمن Princess Nourah bint Abdulrahman University" },
                    new SelectListItem { Value = "جامعة القصيم Qassim University", Text = "جامعة القصيم Qassim University" },
                    new SelectListItem { Value = "جامعة طيبة Taibah University", Text = "جامعة طيبة Taibah University" },
                    new SelectListItem { Value = "جامعة الطائف Taif University", Text = "جامعة الطائف Taif University" },
                    new SelectListItem { Value = "جامعة حائل Hail University", Text = "جامعة حائل Hail University" },
                    new SelectListItem { Value = "جامعة تبوك Tabuk University", Text = "جامعة تبوك Tabuk University" },
                    new SelectListItem { Value = "جامعة جازان Jazan University", Text = "جامعة جازان Jazan University" },
                    new SelectListItem { Value = "جامعة نجران Najran University", Text = "جامعة نجران Najran University" },
                    new SelectListItem { Value = "جامعة الحدود الشمالية Northern Borders University", Text = "جامعة الحدود الشمالية Northern Borders University" },
                    new SelectListItem { Value = "جامعة بيشة Bisha University", Text = "جامعة بيشة Bisha University" },
                    new SelectListItem { Value = "جامعة شقراء Shaqra University", Text = "جامعة شقراء Shaqra University" },
                    new SelectListItem { Value = "جامعة المجمعة AlMajma'ah University", Text = "جامعة المجمعة AlMajma'ah University" },
                    new SelectListItem { Value = "جامعة الجوف Al-Jouf University", Text = "جامعة الجوف Al-Jouf University" },
                    new SelectListItem { Value = "جامعة الأمير سطام بن عبد العزيز Prince Sattam bin Abdulaziz University", Text = "جامعة الأمير سطام بن عبد العزيز Prince Sattam bin Abdulaziz University" },
                    new SelectListItem { Value = "جامعة الإمام عبد الرحمن بن فيصل Imam Abdulrahman bin Faisal University", Text = "جامعة الإمام عبد الرحمن بن فيصل Imam Abdulrahman bin Faisal University" },
                    new SelectListItem { Value = "جامعة الباحة Al-Baha University", Text = "جامعة الباحة Al-Baha University" },
                    new SelectListItem { Value = "جامعة جدة Jeddah University", Text = "جامعة جدة Jeddah University" },
                    new SelectListItem { Value = "جامعة حفر الباطن Hafr Al-Batin University", Text = "جامعة حفر الباطن Hafr Al-Batin University" },
                    new SelectListItem { Value = "جامعة الملك سعود بن عبد العزيز للعلوم الصحية King Saud bin Abdulaziz University for Health Sciences", Text = "جامعة الملك سعود بن عبد العزيز للعلوم الصحية King Saud bin Abdulaziz University for Health Sciences" },
                    new SelectListItem { Value = "جامعة الملك عبد الله للعلوم والتقنية King Abdullah University of Science and Technology", Text = "جامعة الملك عبد الله للعلوم والتقنية King Abdullah University of Science and Technology" },
                    new SelectListItem { Value = "الجامعة السعودية الإلكترونية Saudi Electronic University", Text = "الجامعة السعودية الإلكترونية Saudi Electronic University" },
                    new SelectListItem { Value = "جامعة المعرفة Knowledge University", Text = "جامعة المعرفة Knowledge University" },
                    new SelectListItem { Value = "جامعة الإمام محمد بن سعود الإسلامية (فرع المدينة المنورة) Imam Muhammad bin Saud Islamic University (Madinah Branch)", Text = "جامعة الإمام محمد بن سعود الإسلامية (فرع المدينة المنورة) Imam Muhammad bin Saud Islamic University (Madinah Branch)" },


                    new SelectListItem { Value = "جامعة الأمير محمد بن فهد Prince Mohammed bin Fahd University", Text = "جامعة الأمير محمد بن فهد Prince Mohammed bin Fahd University" },
                    new SelectListItem { Value = "جامعة الأمير سلطان Prince Sultan University", Text = "جامعة الأمير سلطان Prince Sultan University" },
                    new SelectListItem { Value = "جامعة اليمامة Al-Yamamah University", Text = "جامعة اليمامة Al-Yamamah University" },
                    new SelectListItem { Value = "جامعة عفت Effat University", Text = "جامعة عفت Effat University" },
                    new SelectListItem { Value = "جامعة دار العلوم Dar Al Uloom University", Text = "جامعة دار العلوم Dar Al Uloom University" },
                    new SelectListItem { Value = "جامعة الفيصل Al-Faisal University", Text = "جامعة الفيصل Al-Faisal University" },
                    new SelectListItem { Value = "جامعة رياض العلم Riyadh Elm University", Text = "جامعة رياض العلم Riyadh Elm University" },
                    new SelectListItem { Value = "جامعة سليمان الراجحي Sulaiman Al Rajhi University", Text = "جامعة سليمان الراجحي Sulaiman Al Rajhi University" },
                    new SelectListItem { Value = "جامعة المستقبل Future University", Text = "جامعة المستقبل Future University" },
                    new SelectListItem { Value = "جامعة دار الحكمة Dar Al-Hekma University", Text = "جامعة دار الحكمة Dar Al-Hekma University" },
                    new SelectListItem { Value = "جامعة الأعمال والتكنولوجيا University of Business and Technology", Text = "جامعة الأعمال والتكنولوجيا University of Business and Technology" },
                    new SelectListItem { Value = "كلية الأمير سلطان للإدارة Prince Sultan College for Business", Text = "كلية الأمير سلطان للإدارة Prince Sultan College for Business" },
                    new SelectListItem { Value = "الجامعة العربية المفتوحة Arab Open University", Text = "الجامعة العربية المفتوحة Arab Open University" },
                    new SelectListItem { Value = "جامعة فهد بن سلطان Fahd bin Sultan University", Text = "جامعة فهد بن سلطان Fahd bin Sultan University" },
                    new SelectListItem { Value = "جامعة محمد بن عبد الله Muhammad bin Abdullah University", Text = "جامعة محمد بن عبد الله Muhammad bin Abdullah University" },
                    new SelectListItem { Value = "جامعة الأمير فواز Prince Fawaz University", Text = "جامعة الأمير فواز Prince Fawaz University" },



                    // Add more universities as needed
                },

                CollegeOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "كلية الهندسة    Faculty of Engineering", Text = "كلية الهندسة   Faculty of Engineering" },
                    new SelectListItem { Value = "كلية العلوم    Faculty of Science", Text = "كلية العلوم   Faculty of Science" },
                    new SelectListItem { Value = "كلية الحاسبات والمعلومات    Faculty of Computer and Information Sciences", Text = "كلية الحاسبات والمعلومات   Faculty of Computer and Information Sciences" },
                    new SelectListItem { Value = "كلية الآداب    Faculty of Arts", Text = "كلية الآداب   Faculty of Arts" },
                    new SelectListItem { Value = "كلية الحقوق    Faculty of Law", Text = "كلية الحقوق   Faculty of Law" },
                    new SelectListItem { Value = "كلية طب الأسنان    Faculty of Dentistry", Text = "كلية طب الأسنان   Faculty of Dentistry" },
                    new SelectListItem { Value = "كلية الصيدلة    Faculty of Pharmacy", Text = "كلية الصيدلة   Faculty of Pharmacy" },
                    new SelectListItem { Value = "كلية الطب    Faculty of Medicine", Text = "كلية الطب   Faculty of Medicine" },
                    new SelectListItem { Value = "كلية الزراعة    Faculty of Agricultural", Text = "كلية الزراعة   Faculty of Agricultural" },
                    new SelectListItem { Value = "كلية إدارة الأعمال    Faculty of Business", Text = "كلية إدارة الأعمال   Faculty of Business" },
                    new SelectListItem { Value = "كلية التجارة    Faculty of Commerce", Text = "كلية التجارة   Faculty of Commerce" },
                    new SelectListItem { Value = "كلية العلاج الطبيعي    Faculty of Physical Therapy", Text = "كلية العلاج الطبيعي   Faculty of Physical Therapy" },
                    new SelectListItem { Value = "كلية الألسن    Faculty of Al-Alsun", Text = "كلية الألسن   Faculty of Al-Alsun" },
                    new SelectListItem { Value = "كلية الذكاء الاصطناعي    Faculty of Artificial Intelligence", Text = "كلية الذكاء الاصطناعي   Faculty of Artificial Intelligence" }


                },

                SpecializationOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "الهندسة المدنية Civil Engineering", Text = "الهندسة المدنية Civil Engineering" },
                    new SelectListItem { Value = "الهندسة الميكانيكية Mechanical Engineering", Text = "الهندسة الميكانيكية Mechanical Engineering" },
                    new SelectListItem { Value = "الهندسة الكهربائية Electrical Engineering", Text = "الهندسة الكهربائية Electrical Engineering" },
                    new SelectListItem { Value = "الهندسة المعمارية Architecture Engineering", Text = "الهندسة المعمارية Architecture Engineering" },
                    new SelectListItem { Value = "هندسة الكمبيوتر Computer Engineering", Text = "هندسة الكمبيوتر Computer Engineering" },
                    new SelectListItem { Value = "الهندسة الكيميائية Chemical Engineering", Text = "الهندسة الكيميائية Chemical Engineering" },
                    new SelectListItem { Value = "الهندسة الطبية الحيوية Biomedical Engineering", Text = "الهندسة الطبية الحيوية Biomedical Engineering" },
                    new SelectListItem { Value = "الهندسة الصناعية Industrial Engineering", Text = "الهندسة الصناعية Industrial Engineering" },
                    new SelectListItem { Value = "الهندسة البيئية Environmental Engineering", Text = "الهندسة البيئية Environmental Engineering" },
                    new SelectListItem { Value = "هندسة الميكاترونكس Mechatronics Engineering", Text = "هندسة الميكاترونكس Mechatronics Engineering" },
                    new SelectListItem { Value = "هندسة البترول Petroleum Engineering", Text = "هندسة البترول Petroleum Engineering" },
                    new SelectListItem { Value = "هندسة التعدين Mining Engineering", Text = "هندسة التعدين Mining Engineering" },
                    new SelectListItem { Value = "هندسة الطيران Aerospace Engineering", Text = "هندسة الطيران Aerospace Engineering" },
                    new SelectListItem { Value = "هندسة الاتصالات Telecommunications Engineering", Text = "هندسة الاتصالات Telecommunications Engineering" },
                    new SelectListItem { Value = "هندسة المواد Materials Engineering", Text = "هندسة المواد Materials Engineering" },
                    new SelectListItem { Value = "الهندسة الإنشائية Structural Engineering", Text = "الهندسة الإنشائية Structural Engineering" },
                    new SelectListItem { Value = "هندسة السيارات Automotive Engineering", Text = "هندسة السيارات Automotive Engineering" },
                    new SelectListItem { Value = "الهندسة النووية Nuclear Engineering", Text = "الهندسة النووية Nuclear Engineering" },

                    new SelectListItem { Value = "التشريح الآدمي وعلم الأجنة Human Anatomy and Embryology", Text = "التشريح الآدمي وعلم الأجنة Human Anatomy and Embryology" },
                    new SelectListItem { Value = "الكيمياء الحيوية الطبية Medical Biochemistry", Text = "الكيمياء الحيوية الطبية Medical Biochemistry" },
                    new SelectListItem { Value = "الميكروبيولوجيا الطبية والمناعة Medical Microbiology and Immunology", Text = "الميكروبيولوجيا الطبية والمناعة Medical Microbiology and Immunology" },
                    new SelectListItem { Value = "(الباثولوجيا (علم الأمراض Pathology", Text = "(الباثولوجيا (علم الأمراض Pathology" },
                    new SelectListItem { Value = "الطفيليات الطبية Medical Parasitology", Text = "الطفيليات الطبية Medical Parasitology" },
                    new SelectListItem { Value = "طب المجتمع Community Medicine", Text = "طب المجتمع Community Medicine" },
                    new SelectListItem { Value = "طب الأسرة Family Medicine", Text = "طب الأسرة Family Medicine" },
                    new SelectListItem { Value = "الأشعة التشخيصية Radiology", Text = "الأشعة التشخيصية Radiology" },
                    new SelectListItem { Value = "التحاليل الطبية Clinical Pathology", Text = "التحاليل الطبية Clinical Pathology" },
                    new SelectListItem { Value = "الأمراض الجلدية والتناسلية Dermatology, Andrology & STDs", Text = "الأمراض الجلدية والتناسلية Dermatology, Andrology & STDs" },
                    new SelectListItem { Value = "الجراحة العامة Surgery", Text = "الجراحة العامة Surgery" },
                    new SelectListItem { Value = "جراحة المخ والأعصاب Neurosurgery", Text = "جراحة المخ والأعصاب Neurosurgery" },
                    new SelectListItem { Value = "جراحة التجميل Plastic Surgery", Text = "جراحة التجميل Plastic Surgery" },
                    new SelectListItem { Value = "جراحة القلب والصدر Cardiothoracic Surgery", Text = "جراحة القلب والصدر Cardiothoracic Surgery" },
                    new SelectListItem { Value = "الأمراض النفسية والعصبية Neurology & Psychiatry", Text = "الأمراض النفسية والعصبية Neurology & Psychiatry" },
                    new SelectListItem { Value = "الأمراض الصدرية Chest Medicine", Text = "الأمراض الصدرية Chest Medicine" },
                    new SelectListItem { Value = "طب الأطفال Pediatric Medicine", Text = "طب الأطفال Pediatric Medicine" },
                    new SelectListItem { Value = "أمراض النساء والتوليد Obstetrics & Gynaecology", Text = "أمراض النساء والتوليد Obstetrics & Gynaecology" },
                    new SelectListItem { Value = "الأنف و الأذن و الحنجرة Otorhinolaryngology", Text = "الأنف و الأذن و الحنجرة Otorhinolaryngology" },
                    new SelectListItem { Value = "جراحة المسالك البولية والتناسلية Urology", Text = "جراحة المسالك البولية والتناسلية Urology" },
                    new SelectListItem { Value = "جراحة العظام Orthopedic Surgery", Text = "جراحة العظام Orthopedic Surgery" },

                    new SelectListItem { Value = "طب الأسنان العام General Dentistry", Text = "طب الأسنان العام General Dentistry" },
                    new SelectListItem { Value = "تقويم الأسنان Orthodontics", Text = "تقويم الأسنان Orthodontics" },
                    new SelectListItem { Value = "جراحة الفم Oral Surgery", Text = "جراحة الفم Oral Surgery" },
                    new SelectListItem { Value = "أمراض اللثة Periodontology", Text = "أمراض اللثة Periodontology" },
                    new SelectListItem { Value = "طب أسنان الأطفال Pediatric Dentistry", Text = "طب أسنان الأطفال Pediatric Dentistry" },
                    new SelectListItem { Value = "التعويضات السنية Prosthodontics", Text = "التعويضات السنية Prosthodontics" },
                    new SelectListItem { Value = "علاج جذور الأسنان Endodontics", Text = "علاج جذور الأسنان Endodontics" },
                    new SelectListItem { Value = "علم أمراض الفم Oral Pathology", Text = "علم أمراض الفم Oral Pathology" },
                    new SelectListItem { Value = "المواد السنية Dental Materials", Text = "المواد السنية Dental Materials" },
                    new SelectListItem { Value = "طب الأسنان الوقائي Public Health Dentistry", Text = "طب الأسنان الوقائي Public Health Dentistry" },
                    new SelectListItem { Value = "طب الأسنان لكبار السن Geriatric Dentistry", Text = "طب الأسنان لكبار السن Geriatric Dentistry" },

                    new SelectListItem { Value = "علم الأحياء Biology", Text = "علم الأحياء Biology" },
                    new SelectListItem { Value = "الكيمياء Chemistry", Text = "الكيمياء Chemistry" },
                    new SelectListItem { Value = "الفيزياء Physics", Text = "الفيزياء Physics" },
                    new SelectListItem { Value = "الرياضيات Mathematics", Text = "الرياضيات Mathematics" },
                    new SelectListItem { Value = "الإحصاء Statistics", Text = "الإحصاء Statistics" },
                    new SelectListItem { Value = "الجيولوجيا Geology", Text = "الجيولوجيا Geology" },
                    new SelectListItem { Value = "العلوم البيئية Environmental Science", Text = "العلوم البيئية Environmental Science" },
                    new SelectListItem { Value = "علوم الكمبيوتر Computer Science", Text = "علوم الكمبيوتر Computer Science" },
                    new SelectListItem { Value = "الكيمياء الحيوية Biochemistry", Text = "الكيمياء الحيوية Biochemistry" },
                    new SelectListItem { Value = "الميكروبيولوجيا Microbiology", Text = "الميكروبيولوجيا Microbiology" },
                    new SelectListItem { Value = "علم النبات Botany", Text = "علم النبات Botany" },
                    new SelectListItem { Value = "علم الحيوان Zoology", Text = "علم الحيوان Zoology" },
                    new SelectListItem { Value = "علم الفلك Astronomy", Text = "علم الفلك Astronomy" },
                    new SelectListItem { Value = "الكيمياء الفيزيائية Physical Chemistry", Text = "الكيمياء الفيزيائية Physical Chemistry" },
                    new SelectListItem { Value = "الكيمياء العضوية Organic Chemistry", Text = "الكيمياء العضوية Organic Chemistry" },
                    new SelectListItem { Value = "الكيمياء غير العضوية Inorganic Chemistry", Text = "الكيمياء غير العضوية Inorganic Chemistry" },

                    new SelectListItem { Value = "الهندسة الزراعية Agricultural Engineering", Text = "الهندسة الزراعية Agricultural Engineering" },
                    new SelectListItem { Value = "إنتاج النبات Plant Production", Text = "إنتاج النبات Plant Production" },
                    new SelectListItem { Value = "إنتاج الحيوان Animal Production", Text = "إنتاج الحيوان Animal Production" },
                    new SelectListItem { Value = "علم التربة Soil Science", Text = "علم التربة Soil Science" },
                    new SelectListItem { Value = "البساتين Horticulture", Text = "البساتين Horticulture" },
                    new SelectListItem { Value = "أمراض النبات Plant Pathology", Text = "أمراض النبات Plant Pathology" },
                    new SelectListItem { Value = "علم الحشرات Entomology", Text = "علم الحشرات Entomology" },
                    new SelectListItem { Value = "التكنولوجيا الحيوية الزراعية Agricultural Biotechnology", Text = "التكنولوجيا الحيوية الزراعية Agricultural Biotechnology" },
                    new SelectListItem { Value = "الاقتصاد الزراعي Agricultural Economics", Text = "الاقتصاد الزراعي Agricultural Economics" },
                    new SelectListItem { Value = "البستنة Horticulture", Text = "البستنة Horticulture" },
                    new SelectListItem { Value = "علوم وتكنولوجيا الغذاء Food Science and Technology", Text = "علوم وتكنولوجيا الغذاء Food Science and Technology" },
                    new SelectListItem { Value = "حماية النبات Plant Protection", Text = "حماية النبات Plant Protection" },
                    new SelectListItem { Value = "الإرشاد الزراعي Agricultural Extension", Text = "الإرشاد الزراعي Agricultural Extension" },
                    new SelectListItem { Value = "تربية الأحياء المائية Aquaculture", Text = "تربية الأحياء المائية Aquaculture" },
                    new SelectListItem { Value = "الغابات Forestry", Text = "الغابات Forestry" },
                    new SelectListItem { Value = "التكنولوجيا الحيوية الزراعية Agricultural Biotechnology", Text = "التكنولوجيا الحيوية الزراعية Agricultural Biotechnology" },

                    new SelectListItem { Value = "اللغة العربية Arabic Language", Text = "اللغة العربية Arabic Language" },
                    new SelectListItem { Value = "اللغة الإنجليزية English Language", Text = "اللغة الإنجليزية English Language" },
                    new SelectListItem { Value = "التاريخ History", Text = "التاريخ History" },
                    new SelectListItem { Value = "الفلسفة Philosophy", Text = "الفلسفة Philosophy" },
                    new SelectListItem { Value = "علم الاجتماع Sociology", Text = "علم الاجتماع Sociology" },
                    new SelectListItem { Value = "علم النفس Psychology", Text = "علم النفس Psychology" },
                    new SelectListItem { Value = "علم اللغة Linguistics", Text = "علم اللغة Linguistics" },
                    new SelectListItem { Value = "الأنثروبولوجيا Anthropology", Text = "الأنثروبولوجيا Anthropology" },
                    new SelectListItem { Value = "العلوم السياسية Political Science", Text = "العلوم السياسية Political Science" },
                    new SelectListItem { Value = "تاريخ الفن Art History", Text = "تاريخ الفن Art History" },
                    new SelectListItem { Value = "دراسات المسرح Theater Studies", Text = "دراسات المسرح Theater Studies" },
                    new SelectListItem { Value = "دراسات الموسيقى Musicology", Text = "دراسات الموسيقى Musicology" },
                    new SelectListItem { Value = "دراسات الإعلام Media Studies", Text = "دراسات الإعلام Media Studies" },
                    new SelectListItem { Value = "الدراسات الثقافية Cultural Studies", Text = "الدراسات الثقافية Cultural Studies" },
                    new SelectListItem { Value = "الكتابة الإبداعية Creative Writing", Text = "الكتابة الإبداعية Creative Writing" },

                    new SelectListItem { Value = "المحاسبة Accounting", Text = "المحاسبة Accounting" },
                    new SelectListItem { Value = "إدارة الأعمال Business Administration", Text = "إدارة الأعمال Business Administration" },
                    new SelectListItem { Value = "التمويل Finance", Text = "التمويل Finance" },
                    new SelectListItem { Value = "التسويق Marketing", Text = "التسويق Marketing" },
                    new SelectListItem { Value = "إدارة الموارد البشرية Human Resources", Text = "إدارة الموارد البشرية Human Resources" },
                    new SelectListItem { Value = "الأعمال الدولية International Business", Text = "الأعمال الدولية International Business" },
                    new SelectListItem { Value = "الاقتصاد Economics", Text = "الاقتصاد Economics" },
                    new SelectListItem { Value = "نظم المعلومات الإدارية Management Information Systems", Text = "نظم المعلومات الإدارية Management Information Systems" },
                    new SelectListItem { Value = "التأمين Insurance", Text = "التأمين Insurance" },
                    new SelectListItem { Value = "إدارة العقارات Real Estate Management", Text = "إدارة العقارات Real Estate Management" },
                    new SelectListItem { Value = "إدارة اللوجستيات وسلسلة الإمداد Logistics and Supply Chain Management", Text = "إدارة اللوجستيات وسلسلة الإمداد Logistics and Supply Chain Management" },
                    new SelectListItem { Value = "التجارة الإلكترونية E-commerce", Text = "التجارة الإلكترونية E-commerce" },

                    new SelectListItem { Value = "اللغة العربية Arabic Language", Text = "اللغة العربية Arabic Language" },
                    new SelectListItem { Value = "اللغة الإنجليزية English Language", Text = "اللغة الإنجليزية English Language" },
                    new SelectListItem { Value = "اللغة الفرنسية French Language", Text = "اللغة الفرنسية French Language" },
                    new SelectListItem { Value = "اللغة الألمانية German Language", Text = "اللغة الألمانية German Language" },
                    new SelectListItem { Value = "اللغة الإيطالية Italian Language", Text = "اللغة الإيطالية Italian Language" },
                    new SelectListItem { Value = "اللغة الإسبانية Spanish Language", Text = "اللغة الإسبانية Spanish Language" },
                    new SelectListItem { Value = "الترجمة Translation", Text = "الترجمة Translation" },
                    new SelectListItem { Value = "علم اللغة Linguistics", Text = "علم اللغة Linguistics" },
                    new SelectListItem { Value = "الأدب Literature", Text = "الأدب Literature" },
                    new SelectListItem { Value = "دراسات ثقافية Cultural Studies", Text = "دراسات ثقافية Cultural Studies" },
                    new SelectListItem { Value = "الأدب المقارن Comparative Literature", Text = "الأدب المقارن Comparative Literature" },



                },

                DegreeOptions = new List<SelectListItem>()
                {
                    new SelectListItem { Value = "ممتاز Excellent", Text = "ممتاز Excellent" },
                    new SelectListItem { Value = "جيد جدا Very Good", Text = "جيد جدا Very Good" },
                    new SelectListItem { Value = "جيد Good", Text = "جيد Good" },
                    new SelectListItem { Value = "مقبول Fair", Text = "مقبول Fair" },

                }
            };

            return View(model);
        }


        public JsonResult SearchSkills(string term)
        {
            var skills = _context.Skills
                                 .Where(s => s.SkillName.StartsWith(term))
                                 .Select(s => new SkillViewModel { Id = s.Id, SkillName = s.SkillName })
                                 .ToList();

            return Json(skills);
        }


        [HttpPost]
        public IActionResult New(StudentSkillsViewModel viewModel)
        {
            var student = _context.Students.Include(s => s.StudentSkills).FirstOrDefault(s => s.GraduationStudentId == viewModel.StudentId);

            if (student == null) return NotFound();

            // Clear existing skills (optional)
            student.StudentSkills.Clear();

            // Add selected skills
            foreach (var skill in viewModel.SelectedSkills)
            {
                student.StudentSkills.Add(new StudentSkill { SkillId = skill.Id });
            }

            _context.SaveChanges();
            return RedirectToAction("Details", "Student");
        }


        [HttpPost]
        public async Task<IActionResult> SubmitStudentData(StudentSkillsViewModel studentSkills)
        {
            // Find the student by ID or create a new student
            int skillCount = studentSkills.SelectedSkills.Count;
            if (skillCount < 4)
            {
                // Handle case where less than 4 skills are selected
                TempData["PredictedJob"] = $"الوظيفة المتوقعة لك هى :\n\n {studentSkills.Specialization}";

                return RedirectToAction("JobRecommendation");
            }

            if(skillCount > 4)
            {
                return Content("من فضلك ادخل 4 مهارات فقط");
            }

            if (skillCount == 4)
            {
                var student = _context.Students.Include(s => s.StudentSkills).FirstOrDefault(s => s.GraduationStudentId == studentSkills.StudentId);
                if (student == null)
                {
                    student = new GraduationStudent
                    {
                        Name = studentSkills.Name,
                        Email = studentSkills.Email,
                        Phone = studentSkills.Phone,
                        University = studentSkills.University,
                        College = studentSkills.College,
                        Specialization = studentSkills.Specialization,
                        Degree = studentSkills.Degree,
                        GraduationYear = studentSkills.GraduationYear,
                        UserId = User.Identity!.Name
                    };
                    _context.Students.Add(student);
                }
                else
                {
                    // Update existing student details
                    student.Name = studentSkills.Name;
                    student.Email = studentSkills.Email;
                    student.Phone = studentSkills.Phone;
                    student.University = studentSkills.University;
                    student.College = studentSkills.College;
                    student.Specialization = studentSkills.Specialization;
                    student.Degree = studentSkills.Degree;
                    student.GraduationYear = studentSkills.GraduationYear;

                    // Clear existing skills (optional)
                    student.StudentSkills.Clear();
                }

                if (student.StudentSkills == null)
                {
                    student.StudentSkills = new List<StudentSkill>();
                }

                // Add selected skills
                foreach (var skill in studentSkills.SelectedSkills)
                {
                    student.StudentSkills.Add(new StudentSkill { SkillId = skill.Id });
                }

                _context.SaveChanges();

                // Call the AI model using the selected skills
                var skillNames = studentSkills.SelectedSkills.Select(s => s.SkillName).ToList();
                string jobPrediction = await _jobPredictionService.GetJobPredictionAsync(skillNames);

                TempData["PredictedJob"] = $"{jobPrediction}";

                return RedirectToAction("JobRecommendation");
            }
            return View("");
        }


        public IActionResult JobRecommendation()
        {
            ViewBag.PredictedJob = TempData["PredictedJob"];
            return View();
        }

        public async Task<IActionResult> Details()
        {
            var query = await (from student in _context.Students
                               join studentSkill in _context.StudentSkills
                                   on student.GraduationStudentId equals studentSkill.StudentId
                               join skill in _context.Skills
                                   on studentSkill.SkillId equals skill.Id
                               where student.UserId == User.Identity!.Name
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

        public IActionResult Delete(int graduationStudentId)
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
            return RedirectToAction("Details"); // Redirect to the index or a list of students after deletion
        }


        public ActionResult ExportStudentReportEXCEL()
        {
            var Results = (from student in _context.Students
                           join studentSkill in _context.StudentSkills
                               on student.GraduationStudentId equals studentSkill.StudentId
                           join skill in _context.Skills
                               on studentSkill.SkillId equals skill.Id
                           where student.UserId == User.Identity!.Name
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

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "reports", "StudentReport.xlsx");
                Directory.CreateDirectory(Path.GetDirectoryName(path)); // Ensure the directory exists
                workbook.SaveAs(path);

                // Generate the file for download
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentReport.xlsx");
                }
            }
        }

        public IActionResult ExportStudentReportPDF()
        {
            // Fetch students data from the database
            var students = (from student in _context.Students
                            join studentSkill in _context.StudentSkills
                                on student.GraduationStudentId equals studentSkill.StudentId
                            join skill in _context.Skills
                                on studentSkill.SkillId equals skill.Id
                            where student.UserId == User.Identity!.Name
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


    }
}
