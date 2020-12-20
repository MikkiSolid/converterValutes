using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CurConv
{
    public partial class MainPage : ContentPage
    {
        MainViewModel mvm;
        public MainPage(MainViewModel MVM)
        {
            InitializeComponent();
            mvm = MVM;
            BindingContext = mvm;

        }

    }
}
