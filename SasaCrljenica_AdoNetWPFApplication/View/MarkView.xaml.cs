using SasaCrljenica_AdoNetWPFApplication.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MarkView.xaml
    /// </summary>
    public partial class MarkView : Window
    {
        //for Dell-PC
        //static string connString = Properties.Settings.Default.TriTabeleConnectionString;

        //for HP Laptop
        //static string connString = Properties.Settings.Default.TriTabeleConnectionString1;

        //added 19.09.2019, universal connection string
        //static string computer = Environment.MachineName;
        //static string connString = @"Data Source=" + computer + @";Initial Catalog=TriTabele;Integrated Security=True";

        SqlConnection sqlConn = new SqlConnection(Connection.ConnectionString.connString);

        Student student = new Student();
        Subject subject = new Subject();
        Mark mark = new Mark();
        int no = 1;
        public MarkView()
        {
            InitializeComponent();
            SelectDataFromDatabase();
            FillDataGrid();
        }
        private List<Mark> listStudentMark = new List<Mark>();

        public void FillDataGrid()
        {
            try
            {
                dataGridMark.ItemsSource = listStudentMark;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #region CRUD operation
        private void SelectDataFromDatabase()
        {
            string query = "select tblMark.MarkID,tblStudent.StudentID,tblSubject.SubjectID,tblStudent.StudentName,tblStudent.SurName,tblSubject.SubjectName,tblMark.Mark FROM((tblMark INNER JOIN tblStudent ON tblMark.StudentID=tblStudent.StudentID) INNER JOIN tblSubject ON tblMark.SubjectID=tblSubject.SubjectID)";

            try
            {
                sqlConn.Open();
                SqlCommand sqlComm = new SqlCommand(query, sqlConn);
                SqlDataReader sqlDR = sqlComm.ExecuteReader();

                listStudentMark.Clear();

                while (sqlDR.Read())
                {
                    Mark objMark = new Mark();

                    objMark.Number = no;
                    objMark.MarkID = Convert.ToInt32(sqlDR["MarkID"]);
                    objMark.StudentID = Convert.ToInt32(sqlDR["StudentID"]);
                    objMark.SubjectID = Convert.ToInt32(sqlDR["SubjectID"]);
                    objMark.StudentName = sqlDR["StudentName"].ToString();
                    objMark.StudentSurname = sqlDR["SurName"].ToString();
                    objMark.SubjectName = sqlDR["SubjectName"].ToString();
                    objMark.MarkEvaluation = Convert.ToInt32(sqlDR["Mark"]);

                    listStudentMark.Add(objMark);

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

        private void btnAddMark_Click(object sender, RoutedEventArgs e)
        {
            //mark = (Mark)dataGridMark.SelectedItem;
            Mark objMark = new Mark();

            if (txtStudentName.Text != "" && txtStudentSurname.Text != "" && txtMarkEvaluation.Text != "" && txtSubjectName.Text != "")
            {
                // find StudentID from StudentName and StudentSurname
                string query1 = string.Format("select StudentID from tblStudent where StudentName='{0}' and SurName='{1}';", txtStudentName.Text, txtStudentSurname.Text);

                try
                {
                    sqlConn.Open();
                    SqlCommand sqlComm = new SqlCommand(query1, sqlConn);
                    SqlDataReader sqlDR = sqlComm.ExecuteReader();
                    if (sqlDR.Read())
                    {
                        objMark.StudentID = Convert.ToInt32(sqlDR["StudentID"]);
                    }
                    else
                    {
                        MessageBox.Show("This Student not exist! Please repeat operation!");
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

                // Find SubjectID from Subject Name
                string query2 = string.Format("select SubjectID from tblSubject where SubjectName='{0}';", txtSubjectName.Text);

                try
                {
                    sqlConn.Open();
                    SqlCommand sqlComm = new SqlCommand(query2, sqlConn);
                    SqlDataReader sqlDR = sqlComm.ExecuteReader();
                    if (sqlDR.Read())
                    {
                        objMark.SubjectID = Convert.ToInt32(sqlDR["SubjectID"]);
                    }
                    else
                    {
                        MessageBox.Show("This Subject not exist! Please repeat operation!");
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

                // insert into tblMark values Mark, StudentID and SubjectID
                string query3 = string.Format("insert into tblMark values ({0},{1},{2})", txtMarkEvaluation.Text, objMark.StudentID, objMark.SubjectID);
                try
                {
                    sqlConn.Open();
                    SqlCommand comm = new SqlCommand(query3, sqlConn);
                    comm.ExecuteNonQuery();
                    MessageBox.Show("Mark evaluation for Student succesfully added!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    sqlConn.Close();

                    MarkView markView = new MarkView();
                    this.Close();
                    markView.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please enter Mark evaluation, StudentName, StudentSurname and SubjectName before add data to database!");
            }

        }
        #endregion
    }
}

