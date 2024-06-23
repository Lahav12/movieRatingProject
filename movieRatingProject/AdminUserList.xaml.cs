using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using ViewModel;


namespace movieRatingProject
{
    /// <summary>
    /// Interaction logic for AdminUserList.xaml
    /// </summary>
    public partial class AdminUserList : Page
    {
        public AdminUserList()
        {
            InitializeComponent();

            UserDB userDB = new UserDB();
            UserList users = userDB.SelectAll();

            UserListGrid.ItemsSource = users;
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserDB userDB = new UserDB();

            for (int i = 0; i < UserListGrid.SelectedItems.Count; i++)
            {
                if (UserListGrid.SelectedItems[i] is User selectedUser)
                {
                    userDB.RemoveUser(selectedUser);
                }
            }
        }
    }
}
