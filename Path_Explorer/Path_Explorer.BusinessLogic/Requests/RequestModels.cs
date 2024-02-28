using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_Explorer.BusinessLogic.Requests
{
    internal class RequestModels
    {

    }

    public class CreateQuizRequestVm
    {
        [Required]
       public string username {  get; set; }
    }
    public class SubmitQuizRequestVm
    {
        [Required]
       public string quizReference {  get; set; }
    }

    public class SubmitAnswerRequestVm
    {
        [Required]
        public string quizReference { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public int questionId { get; set; }
        [Required]
        public int optionId { get; set; }
    }
}
