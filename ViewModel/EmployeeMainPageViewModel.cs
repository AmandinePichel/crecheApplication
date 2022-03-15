using CrecheApplication.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrecheApplication.ViewModel
{
    class EmployeeMainPageViewModel
    {
        public ValidationInput validation { get; set; }
        public EmployeeMainPageViewModel()
        {
            validation = new ValidationInput();
        }
    }
}
