using Path_Explorer.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Path_Explorer.Models.Entities {
    public class Quiz : BaseAudit
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Username { get; set; }
        public string QuizStatus { get; set; }
        public virtual ICollection<QuizDetail> QuizDetails { get; set; }
    }
}
