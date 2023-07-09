namespace StudentInformation.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public double Result { get; set; }
        public bool IsContestant { get; set; }
    }
}
