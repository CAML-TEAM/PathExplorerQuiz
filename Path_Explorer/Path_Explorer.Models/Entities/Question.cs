using Path_Explorer.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Path_Explorer.Models.Entities {
    public class Question : BaseAudit {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int BuildingId { get; set; }
        public virtual Building Building { get; set; }
        public virtual ICollection<QuestionOption> Options { get; set; }
    }
}
