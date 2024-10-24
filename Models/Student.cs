namespace Technical.Models
{
    public class Student
    {
        public int RollNo { get; set; }
        public string Name { get; set; }

        public string StudentUserName { get; set; }

         public string Password { get; set; }

        public string Role {  get; set; }   

        public Student() {

            this.RollNo = 0;
            this.Name = "Unknown";
            this.StudentUserName = "Unknown";
            this.Password = "Unknown";
            this.Role = "Unknown";
        
        }
       
    }
}
