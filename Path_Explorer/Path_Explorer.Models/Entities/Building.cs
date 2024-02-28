using Path_Explorer.Models.AbstractModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Path_Explorer.Models.Entities {
    public class Building : BaseAudit {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinate { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
