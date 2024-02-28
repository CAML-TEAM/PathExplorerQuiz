using Path_Explorer.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Path_Explorer.Models.Entities {
    public class QuestionOption : BaseAudit
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsAnswer { get; set; }
        public int score { get; set; } = 0;
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
