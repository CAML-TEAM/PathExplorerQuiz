
using Path_Explorer.DataTransferObject.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Path_Explorer.BusinessLogic.Responses {
    public class GetQuestionsVm : BaseAPICommonResponse
    {
        public List<QuestionDetail> Questions { get; set; }
        public string ActionCall { get; set; }
        public int TotalQuestions { get; set; } = 0;
    }
    public class QuestionDetail
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string Text { get; set; }
        public int BuildingId { get; set; }

        [Column(TypeName = "jsonb")]
        public List<object> Options { get; set; }
    }

    public class GetBuildingVm : BaseAPICommonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Coordinate { get; set; }
        public string ActionCall { get; set; }
    }
    public class CreateQuizVm : BaseAPICommonResponse
    {
        public string QuizReference { get; set; }
        public string Username { get; set; }
        public DateTime DateTime { get; set; }
        public string ActionCall { get; set; }
    }
    public class SubmitAnswerVm : BaseAPICommonResponse
    {
        public string QuizReference { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public int OptionScore { get; set; }
        public int CorrectOptionId { get; set; }
        public string ActionCall { get; set; }
    }
    public class QuizDetailVm : BaseAPICommonResponse
    {
        public int Id { get; set; }
        public string QuizReference { get; set; }
        public string Username { get; set; }
        public int TotalScore { get; set; }
        public int MaxScore { get; set; } = 100;
        public string Status { get; set; }
        public string Remark { get; set; }
        public List<AnsweredQuestionDetail> Questions { get; set; }
        public DateTime DateCreated { get; set; }
        public string ActionCall { get; set; }
    }

    public class AnsweredQuestionDetail
    {
        public int Id { get; set; }
        public int SelectedOptionId { get; set; }
        public int Score { get; set; }
        public int BuildingId { get; set; }

        [Column(TypeName = "jsonb")]
        public List<object> Options { get; set; }
    }

    public class QuizDataVm
    {
        public int Id { get; set; }
        public string QuizReference { get; set; }
        public int TotalScore { get; set; }
        public int MaxScore { get; set; } = 100;
        public string Status { get; set; }
        public string Remark { get; set; }
        public List<AnsweredQuestionDetail> Questions { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class GetQuizListVm : BaseAPICommonResponse
    {
        public string Username { get; set; }
        public List<QuizDataVm> Quizzes { get; set; }
        public int TotalQuizzes { get; set; }
        public string ActionCall { get; set; }
    }

    public class CommonExceptionResponse : BaseAPICommonResponse
    {
        public string ActionCall { get; set; }
    }

}
