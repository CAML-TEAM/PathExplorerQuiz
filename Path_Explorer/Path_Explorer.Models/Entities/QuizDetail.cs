using Path_Explorer.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Path_Explorer.Models.Entities {
    public class QuizDetail : BaseAudit
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int OptionId { get; set; }
        public virtual QuestionOption Option { get; set; }
        public int Score { get; set; }
    }
}
