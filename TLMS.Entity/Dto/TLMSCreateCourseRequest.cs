namespace TLMS.ServiceManager.Controller
{
    public class TLMSCreateCourseRequest
    {

        public string CourseCd { get; set; }
        public string CourseTitle { get; set; }
        public string School { get; set; }
        public string ProgrammeLevel { get; set; }
        public string Programme { get; set; }
        public string CourseType { get; set; }
        public string CourseArea { get; set; }
        public decimal CourseUnit { get; set; }
        public string Remarks { get; set; }
    }

    public class TLMSCreateCourseResponse
    {
        public string AllocationSub { get; set; }
        public string CourseCd { get; set; }
        public string CourseTitle { get; set; }
        public string School { get; set; }
        public string ProgrammeLevel { get; set; }
        public string Programme { get; set; }
        public string CourseType { get; set; }
        public string CourseArea { get; set; }
        public decimal CourseUnit { get; set; }
        public string Remarks { get; set; }
    }
}