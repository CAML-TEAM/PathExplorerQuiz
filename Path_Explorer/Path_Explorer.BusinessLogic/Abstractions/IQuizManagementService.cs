
using Path_Explorer.BusinessLogic.Responses;
using System.Text.Json;

namespace Path_Explorer.BusinessLogic.Abstractions;

public interface IQuizManagementService {

    /// <summary>
    /// Get Question and Options
    /// </summary>
    /// <returns>GetQuestionsVm</returns>
    Task<GetQuestionsVm> GetQuestions();
    
    /// <summary>
    /// Get Question and Options by Building Id
    /// </summary>
    /// <returns>GetQuestionsVm</returns>
    Task<GetQuestionsVm> GetQuestionsByBuilding(int buildingId);

    /// <summary>
    /// Get Building Details
    /// </summary>
    /// <returns>GetBuildingVm</returns>
    Task<GetBuildingVm> GetBuildingDetails(int buildingId);

    /// <summary>
    /// Create Quiz
    /// </summary>
    /// <returns>CreateQuizVm</returns>
    Task<CreateQuizVm> CreateQuiz(string username);

    /// <summary>
    /// Submit Quiz Answer
    /// </summary>
    /// <returns>SubmitAnswerVm</returns>
    Task<SubmitAnswerVm> SubmitAnswer(string QuizReference, int questionId, int optionId, string username);
    
    /// <summary>
    /// Submit Quiz
    /// </summary>
    /// <returns>GetQuestionsVm</returns>
    Task<QuizDetailVm> SubmitQuiz(string QuizReference);
    
    /// <summary>
    /// Create Quiz
    /// </summary>
    /// <returns>GetQuestionsVm</returns>
    Task<GetQuizListVm> GetQuizList(string username);

    /// <summary>
    /// Get Quiz
    /// </summary>
    /// <returns>QuizDetailVm</returns>
    Task<QuizDetailVm> GetQuizDetails(string QuizReference);

}