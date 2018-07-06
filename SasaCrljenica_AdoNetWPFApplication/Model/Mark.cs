using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SasaCrljenica_AdoNetWPFApplication.Model
{
    class Mark
    {
        public int MarkID { get; set; }
        public int Number { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string SubjectName { get; set; }
        public int MarkEvaluation { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
    }
}
