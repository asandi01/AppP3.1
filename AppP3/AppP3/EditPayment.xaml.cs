using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPayment : ContentPage {
        public EditPayment(int idSelected) {
            InitializeComponent();
            id.Text = idSelected.ToString();
        }
    }
}