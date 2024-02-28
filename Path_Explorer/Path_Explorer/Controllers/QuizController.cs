using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Path_Explorer.BusinessLogic.Abstractions;
using Path_Explorer.BusinessLogic.Requests;
using Path_Explorer.BusinessLogic.Responses;

namespace Path_Explorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizManagementService _quizManagementService;
        private readonly ILogger<QuizController> _logger;
        public QuizController(IQuizManagementService quizManagementService, ILogger<QuizController> logger)
        {
            _quizManagementService = quizManagementService;
            _logger = logger;   
        }

        /// <summary>
        /// CreateQuiz.
        /// </summary>
        /// <param name="request">The CreateQuizRequestVm.</param>
        /// <returns>ActionResult.</returns>
        // POST: api/quiz/CreateQuiz
        [HttpPost("CreateQuiz")]
        public async Task<ActionResult<string>> CreateQuiz(CreateQuizRequestVm request)
        {
            try
            {
                var response = await _quizManagementService.CreateQuiz(request.username);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "CreateQuiz" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: CreateQuiz");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "CreateQuiz" });
            }
        }

        /// <summary>
        /// GetQuestions.
        /// </summary>
        /// <returns>ActionResult.</returns>
        // GET: api/quiz/GetQuestions
        [HttpGet("GetQuestions")]
        public async Task<ActionResult<string>> GetQuestions()
        {
            try
            {
                var response = await _quizManagementService.GetQuestions();
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "GetQuestions" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: GetQuestions");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "GetQuestions" });
            }
        }

        /// <summary>
        /// GetQuestionsByBuilding.
        /// </summary>
        /// <returns>ActionResult.</returns>
        // GET: api/quiz/GetQuestionsByBuilding
        [HttpGet("GetQuestionsByBuilding")]
        public async Task<ActionResult<string>> GetQuestionsByBuilding(int buildingId)
        {
            try
            {
                var response = await _quizManagementService.GetQuestionsByBuilding(buildingId);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "GetQuestionsByBuilding" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: GetQuestionsByBuilding");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "GetQuestionsByBuilding" });
            }
        }

        /// <summary>
        /// GetBuildingDetails.
        /// </summary>
        /// <returns>ActionResult.</returns>
        // GET: api/quiz/GetQuestionsByBuilding
        [HttpGet("GetBuildingDetails")]
        public async Task<ActionResult<string>> GetBuildingDetails(int buildingId)
        {
            try
            {
                var response = await _quizManagementService.GetBuildingDetails(buildingId);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "GetBuildingDetails" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: GetBuildingDetails");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "GetBuildingDetails" });
            }
        }

        /// <summary>
        /// GetQuizDetails.
        /// </summary>
        /// <returns>ActionResult.</returns>
        // GET: api/quiz/GetQuizDetails
        [HttpGet("GetQuizDetails")]
        public async Task<ActionResult<string>> GetQuizDetails(string QuizReference)
        {
            try
            {
                var response = await _quizManagementService.GetQuizDetails(QuizReference);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "GetQuizDetails" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: GetQuizDetails");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "GetQuizDetails" });
            }
        }

        /// <summary>
        /// GetQuizList.
        /// </summary>
        /// <returns>ActionResult.</returns>
        // GET: api/quiz/GetQuizListByUsername
        [HttpGet("GetQuizListByUsername")]
        public async Task<ActionResult<string>> GetQuizList(string username)
        {
            try
            {
                var response = await _quizManagementService.GetQuizList(username);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "GetQuizListByUsername" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: GetQuizListByUsername");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "GetQuizListByUsername" });
            }
        }

        /// <summary>
        /// SubmitAnswer.
        /// </summary>
        /// <param name="request">The SubmitAnswerRequestVm.</param>
        /// <returns>ActionResult.</returns>
        // POST: api/quiz/SubmitAnswer
        [HttpPost("SubmitAnswer")]
        public async Task<ActionResult<string>> SubmitAnswer(SubmitAnswerRequestVm request)
        {
            try
            {
                var response = await _quizManagementService.SubmitAnswer(request.quizReference,request.questionId, request.optionId, request.username);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "SubmitAnswer" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: SubmitAnswer");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "SubmitAnswer" });
            }
        }

        /// <summary>
        /// SubmitQuiz.
        /// </summary>
        /// <param name="request">The SubmitQuizRequestVm.</param>
        /// <returns>ActionResult.</returns>
        // POST: api/quiz/SubmitQuiz
        [HttpPost("SubmitQuiz")]
        public async Task<ActionResult<string>> SubmitQuiz(SubmitQuizRequestVm request)
        {
            try
            {
                var response = await _quizManagementService.SubmitQuiz(request.quizReference);
                if (response == null)
                {
                    return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EMPTY_RES", Message = "Empty Response", ActionCall = "SubmitQuiz" });
                }
                if (!response.IsSuccess)
                {
                    return Ok(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message} | Action: SubmitQuiz");
                return Ok(new CommonExceptionResponse { IsSuccess = false, StatusCode = "EXPT", Message = "Request not fulfilled, Contact Administrator", ActionCall = "SubmitQuiz" });
            }
        }
    }
}
