namespace _02.VaniPlanning
{
    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> invoices;

        public Agency()
        {
            this.invoices = new Dictionary<string, Invoice>();
        }

        public void Create(Invoice invoice)
        {
            if (this.Contains(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            this.invoices.Add(invoice.SerialNumber, invoice);
        }

        public void ThrowInvoice(string number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            this.invoices.Remove(number);
        }

        public void ThrowPayed()
        {
            var payed = this.invoices.Values.Where(x => x.Subtotal == 0);

            foreach (var invoice in payed)
            {
                this.invoices.Remove(invoice.SerialNumber);
            }
        }

        public int Count()
        {
            return this.invoices.Count;
        }

        public bool Contains(string number)
        {
            return this.invoices.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            bool exist = false;

            foreach (var invoice in this.invoices.Values)
            {
                if (invoice.DueDate == due)
                {
                    invoice.Subtotal = 0;
                    exist = true;
                }
            }

            if (!exist)
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return this.invoices.Values
                .Where(x => x.IssueDate >= start && x.IssueDate <= end)
                .OrderBy(x => x.IssueDate)
                .ThenBy(x => x.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            IEnumerable<Invoice> result = this.invoices.Values
                .Where(x => x.SerialNumber.Contains(serialNumber))
                .OrderByDescending(x => x.SerialNumber);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }

            return result;
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var result = this.invoices.Values
                .Where(x => x.DueDate > start && x.DueDate < end);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in result)
            {
                this.invoices.Remove(invoice.SerialNumber);
            }

            return result;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return this.invoices.Values
                .Where(x => x.Department == department)
                .OrderByDescending(x => x.Subtotal)
                .ThenBy(x => x.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            return this.invoices.Values
                .Where(x => x.CompanyName == company)
                .OrderByDescending(x => x.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            bool exist = false;

            foreach (var invoice in this.invoices.Values)
            {
                if (invoice.DueDate == dueDate)
                {
                    invoice.DueDate = dueDate.AddDays(days);
                    exist = true;
                }
            }

            if (!exist)
            {
                throw new ArgumentException();
            }
        }
    }
}
