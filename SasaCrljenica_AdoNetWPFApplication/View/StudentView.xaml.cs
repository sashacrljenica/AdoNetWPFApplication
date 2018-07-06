using SasaCrljenica_AdoNetWPFApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace SasaCrljenica_AdoNetWPFApplication.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        static string connString = Properties.Settings.Default.TriTabeleConnectionString;
        SqlConnection sqlConn = new SqlConnection(connString);

        Student student = new Student();
        int no = 1;

        public StudentView()
        {
            InitializeComponent();
            SelectDataFromDatabase();
            FillDataGrid();
        }

        private List<Student> listStudent = new List<Student>();

        public void FillDataGrid()
        {
            try
            {
                dataGridStudent.ItemsSource = listStudent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #region CRUD operation

        private void SelectDataFromDatabase()
        {
            string query = "select * from tblStudent";

            try
            {
                SqlCommand sqlComm = new SqlCommand(query, sqlConn);
                sqlConn.Open();

                SqlDataReader sqlDR = sqlComm.ExecuteReader();

                listStudent.Clear();

                while (sqlDR.Read())
                {
                    Student obj = new Student();
                    obj.StudentID = Convert.ToInt32(sqlDR["StudentID"]);
                    obj.Name = sqlDR["StudentName"].ToString();
                    obj.Surname = sqlDR["SurName"].ToString();
                    obj.Number = no;

                    listStudent.Add(obj);

                    no++;
                }
                sqlDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && txtSurname.Text != "")
            {
                string query1 = string.Format("select * from tblStudent where StudentName='{0}' and SurName='{1}';", txtName.Text, txtSurname.Text);

                sqlConn.Open();

                SqlCommand comm1 = new SqlCommand(query1, sqlConn);
                SqlDataReader sqlReader = comm1.ExecuteReader();

                if (!sqlReader.HasRows)
                {
                    string query = string.Format("insert into tblStudent values ('{0}','{1}')", txtName.Text, txtSurname.Text);
                    try
                    {
                        sqlReader.Close();
                        SqlCommand comm = new SqlCommand(query, sqlConn);
                        comm.ExecuteNonQuery();
                        MessageBox.Show("Student succesfully added!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    finally
                    {
                        sqlConn.Close();

                        StudentView studentView = new StudentView();
                        this.Close();
                        studentView.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Particular Student already exist!");
                    sqlReader.Close();
                    sqlConn.Close();
                    txtName.Text = "";
                    txtSurname.Text = "";
                }

            }
            else
            {
                MessageBox.Show("Please write name and surname of Student before add to database!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            student = (Student)dataGridStudent.SelectedItem;

            try
            {
                sqlConn.Open();

                string query = string.Format("update tblStudent set StudentName='{0}', SurName='{1}' where StudentID='{2}';", txtName.Text, txtSurname.Text, student.StudentID);
                SqlCommand comm = new SqlCommand(query, sqlConn);
                if (student.StudentID != 0)
                {
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Student succesfully updated!");
                }
                else
                {
                    MessageBox.Show("Please select Student for updating!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlConn.Close();

                StudentView studentView = new StudentView();
                this.Close();
                studentView.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            student = (Student)dataGridStudent.SelectedItem;

            try
            {
                if (student != null)
                {
                    sqlConn.Open();

                    string query = string.Format("delete from tblStudent where StudentID='{0}';", student.StudentID);
                    SqlCommand comm = new SqlCommand(query, sqlConn);
                    if (student.StudentID != 0)
                    {
                        comm.ExecuteNonQuery();
                        MessageBox.Show("Student deleted succesfully!");
                    }
                    else
                    {
                        MessageBox.Show("Please select Student for deleting!");
                    }
                }
                else
                {
                    MessageBox.Show("Please select Student for delete!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlConn.Close();

                StudentView studentView = new StudentView();
                this.Close();
                studentView.ShowDialog();
            }
        }     
        #endregion
    }
}

