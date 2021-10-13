namespace _02.VaniPlanning
{
    public class Invoice
    {
        public Invoice(string number, string company, double subtotal, Department dep, DateTime issueDate, DateTime dueDate)
        {
            this.SerialNumber = number;
            this.CompanyName = company;
            this.Subtotal = subtotal;
            this.Department = dep;
            this.IssueDate = issueDate;
            this.DueDate = dueDate;
        }

        public string SerialNumber { get; set; }

        public string CompanyName { get; set; }

        public double Subtotal { get; set; }

        public Department Department { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DueDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var element = (Invoice)obj;
            var equals = Equals(this.SerialNumber, element.SerialNumber);
            return equals;
        }

        public override int GetHashCode()
        {
            return this.SerialNumber.GetHashCode();
        }
    }
}
