using OnlineArasan.Models;

namespace OnlineArasan.ViewModels.Popups
{
    public class ContactInfoViewModel : BaseViewModel
    {
        private Customer _Customer;
        public Customer Customer
        {
            get { return _Customer; }
            set { SetValue(ref _Customer, value); }
        }
        public int Result { get; set; }
       public ContactInfoViewModel()
        {
            Customer = new Customer();
        }
    }
}
