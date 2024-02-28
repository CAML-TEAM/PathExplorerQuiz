using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using Path_Explorer.BusinessLogic.Abstractions;
using Path_Explorer.BusinessLogic.Responses;
using Path_Explorer.DAL.Context;
using Path_Explorer.Models.Entities;
using System.Collections.Generic;

namespace Path_Explorer.BusinessLogic.Concrete;

public class QuizManagementService : IQuizManagementService
{
    private readonly ILogger<QuizManagementService> _logger;
    private PathExplorerMssqlDbContext _pathExplorerMssqlDbContext { get; set; }
    private PathExplorerDbContext _pathExplorerDbContext { get; set; }
    public QuizManagementService(ILogger<QuizManagementService> logger, PathExplorerMssqlDbContext pathExplorerMssqlDbContext, PathExplorerDbContext pathExplorerDbContext)
    {
        _logger= logger;
        _pathExplorerMssqlDbContext = pathExplorerMssqlDbContext;
        _pathExplorerDbContext = pathExplorerDbContext;
    }
    public async Task<CreateQuizVm> CreateQuiz(string username)
    {
        var response = new CreateQuizVm();
        try
        {
            var quizRef = await GenerateReference();
            var new_record = new Quiz()
            {
                Reference = quizRef,
                Username = username,    
                QuizStatus = "STARTED"
            };
            await _pathExplorerMssqlDbContext.Quizzes.AddAsync(new_record);
            await _pathExplorerMssqlDbContext.SaveChangesAsync();

            response = new CreateQuizVm()
            {
                QuizReference = quizRef,
                DateTime = new_record.DateCreated,
                IsSuccess = true,
                ActionCall = "CREATE_QUIZ",
                Message = $"Quiz {quizRef} successfully created",
                Username = username
            };
            return response;
        }
        catch(Exception ex)
        {
            response = new CreateQuizVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "CREATE_QUIZ",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    public async Task<GetQuestionsVm> GetQuestions()
    {
        var response = new GetQuestionsVm();
        try
        {
            var questions = await _pathExplorerMssqlDbContext.Questions.OrderBy(c => Guid.NewGuid()).ToListAsync();
            var questionDetails = new List<QuestionDetail>();
            foreach (var question in questions)
            {
                var qDetail = new QuestionDetail()
                {
                    Id = question.Id,
                    BuildingName = question.Building.Name,
                    Text = question.Text,
                    BuildingId = question.BuildingId
                };

                var options = new List<object>();
                var questionOptions = question.Options.OrderBy(c => Guid.NewGuid());
                foreach (var option in questionOptions)
                {
                    var optionDetail = new Dictionary<string, object>();
                    options.Add(optionDetail);
                    optionDetail.Add("Id", option.Id);
                    optionDetail.Add("Text", option.Text);
                    optionDetail.Add("Score", option.score);
                    optionDetail.Add("IsAnswer", option.IsAnswer);
                }
                qDetail.Options = options;
                questionDetails.Add(qDetail);
            }
            response = new GetQuestionsVm()
            {
                IsSuccess = true,
                Questions = questionDetails,
                TotalQuestions = questionDetails.Count,
                ActionCall = "GET_QUESTIONS",
                Message = $"{questionDetails.Count} Quiz questions successfully fetched"
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new GetQuestionsVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "GET_QUESTIONS",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }
    public async Task<GetQuestionsVm> GetQuestionsByBuilding(int buidlingId)
    {
        var response = new GetQuestionsVm();
        try
        {
            var building = await _pathExplorerMssqlDbContext.Buildings.FirstOrDefaultAsync(x => x.Id.Equals(buidlingId));
            if (building is null)
            {
                return response = new GetQuestionsVm
                {
                    IsSuccess = false,
                    Message = "Invalid Building Id provided",
                    ActionCall = "GET_QUESTIONS_BY_BUILDING",
                };
            }

            var questions  = await _pathExplorerMssqlDbContext.Questions.Where(x=>x.BuildingId.Equals(buidlingId)).OrderBy(c => Guid.NewGuid()).ToListAsync();
           var questionDetails =  new List<QuestionDetail>();
            foreach(var question in questions)
            {
                var qDetail = new QuestionDetail()
                {
                    Id = question.Id,
                    BuildingName = question.Building.Name,
                    Text = question.Text,
                    BuildingId = question.BuildingId
                };

                var options = new List<object>();
                var questionOptions = question.Options.OrderBy(c => Guid.NewGuid());
                foreach (var option in questionOptions)
                {
                    var optionDetail = new Dictionary<string, object>();
                    options.Add(optionDetail);
                    optionDetail.Add("Id", option.Id);
                    optionDetail.Add("Text", option.Text);
                    optionDetail.Add("Score", option.score);
                    optionDetail.Add("IsAnswer", option.IsAnswer);
                }
                qDetail.Options = options;
                questionDetails.Add(qDetail);
            }
            response = new GetQuestionsVm()
            {
                IsSuccess = true,
                Questions = questionDetails,
                TotalQuestions = questionDetails.Count,
                ActionCall = "GET_QUESTIONS_BY_BUILDING",
                Message = $"{questionDetails.Count} Quiz questions successfully fetched"
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new GetQuestionsVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "GET_QUESTIONS_BY_BUILDING",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    public async Task<GetBuildingVm> GetBuildingDetails(int buidlingId)
    {
        var response = new GetBuildingVm();
        try
        {
            var building = await _pathExplorerMssqlDbContext.Buildings.FirstOrDefaultAsync(x => x.Id.Equals(buidlingId));
            if (building is null)
            {
                return response = new GetBuildingVm
                {
                    IsSuccess = false,
                    Message = "Invalid Building Id provided",
                    ActionCall = "GET_BUILDING_DETAILS",
                };
            }

            response = new GetBuildingVm()
            {
                IsSuccess = true,
                Name = building.Name,
                Id = building.Id,
                Coordinate = building.Coordinate,
                Description = building.Description,
                Message = "Building details fetched successfully",
                ActionCall = "GET_BUILDING_DETAILS",
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new GetBuildingVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "GET_BUILDING_DETAILS",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    public async Task<QuizDetailVm> GetQuizDetails(string QuizReference)
    {
        var response = new QuizDetailVm();
        try
        {
            var quiz = await _pathExplorerMssqlDbContext.Quizzes.FirstOrDefaultAsync(x=>x.Reference.Equals(QuizReference));
            if (quiz is null)
            {
                return response = new QuizDetailVm
                {
                    IsSuccess = false,
                    QuizReference = QuizReference,
                    Message = "Invalid Quiz Reference provided",
                    ActionCall = "GET_QUIZ_DETAILS",
                };
            }
            int totalScore = quiz.QuizDetails.Sum(x => x.Score);
            var remark = "";
            if (totalScore > 0 && totalScore < 40)
            {
                remark = "FAILED";
            }
            else if (totalScore >= 40 && totalScore < 60)
            {
                remark = "Fair, You can do better";
            }
            else if (totalScore >= 60 && totalScore < 80)
            {
                remark = "Bravo, You can still do better";
            }
            else if (totalScore >= 80 && totalScore < 90)
            {
                remark = "Wow, Fantastic";
            }
            else if (totalScore >= 90)
            {
                remark = "You are a Genius";
            }
            var questionDetails = new List<AnsweredQuestionDetail>();
            var quizDetails= quiz.QuizDetails.ToList();
            foreach (var item in quizDetails)
            {
                var qDetail = new AnsweredQuestionDetail()
                {
                    Id = item.QuestionId,
                    Score = item.Score,
                    SelectedOptionId = item.OptionId,
                    BuildingId = item.Question.BuildingId
                };

                var options = new List<object>();
                var questionOptions = item.Question.Options;
                foreach (var option in questionOptions)
                {
                    var optionDetail = new Dictionary<string, object>();
                    options.Add(optionDetail);
                    optionDetail.Add("Id", option.Id);
                    optionDetail.Add("Text", option.Text);
                    optionDetail.Add("Score", option.score);
                    optionDetail.Add("IsAnswer", option.IsAnswer);
                }
                qDetail.Options = options;
                questionDetails.Add(qDetail);
            }
            response = new QuizDetailVm()
            {
                IsSuccess = true,
                Status = quiz.QuizStatus,
                QuizReference=QuizReference,
                Username = quiz.Username, 
                TotalScore = totalScore,
                Remark  = remark,
                Questions = questionDetails,
                Message = $"Quiz details fetched successfully",
                Id = quiz.Id,
                DateCreated = quiz.DateCreated,
                ActionCall = "GET_QUIZ_DETAILS",
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new QuizDetailVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "GET_QUIZ_DETAILS",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    public async Task<GetQuizListVm> GetQuizList(string username)
    {
        var response = new GetQuizListVm();
        try
        {
            var quizList = await _pathExplorerMssqlDbContext.Quizzes.Where(x => x.Username.ToLower().Equals(username.ToLower())).ToListAsync();
            if (!quizList.Any())
            {
                return response = new GetQuizListVm
                {
                    IsSuccess = true,
                    Message = "User does not have any quiz record",
                    Username=username,
                    TotalQuizzes = 0,
                    ActionCall = "GET_QUIZ_LIST",
                };
            }
            var quizDetails = new List<QuizDataVm>();
            foreach(var quiz in quizList )
            {
                int totalScore = quiz.QuizDetails.Sum(x => x.Score);
                var remark = "";
                if (totalScore > 0 && totalScore < 40)
                {
                    remark = "FAILED";
                }
                else if (totalScore >= 40 && totalScore < 60)
                {
                    remark = "Fair, You can do better";
                }
                else if (totalScore >= 60 && totalScore < 80)
                {
                    remark = "Bravo, You can still do better";
                }
                else if (totalScore >= 80 && totalScore < 90)
                {
                    remark = "Wow, Fantastic";
                }
                else if (totalScore >= 90)
                {
                    remark = "You are a Genius";
                }
                var questionDetails = new List<AnsweredQuestionDetail>();
                var quizDetailList = quiz.QuizDetails.ToList();
                foreach (var item in quizDetailList)
                {
                    var qDetail = new AnsweredQuestionDetail()
                    {
                        Id = item.QuestionId,
                        Score = item.Score,
                        SelectedOptionId = item.OptionId,
                        BuildingId = item.Question.BuildingId
                    };

                    var options = new List<object>();
                    var questionOptions = item.Question.Options;
                    foreach (var option in questionOptions)
                    {
                        var optionDetail = new Dictionary<string, object>();
                        options.Add(optionDetail);
                        optionDetail.Add("Id", option.Id);
                        optionDetail.Add("Text", option.Text);
                        optionDetail.Add("Score", option.score);
                        optionDetail.Add("IsAnswer", option.IsAnswer);
                    }
                    qDetail.Options = options;
                    questionDetails.Add(qDetail);
                }
                var data = new QuizDataVm
                {
                    Id = quiz.Id,
                    DateCreated = quiz.DateCreated,
                    TotalScore = totalScore,
                    QuizReference = quiz.Reference,
                    Remark = remark,
                    Status = quiz.QuizStatus,
                    Questions = questionDetails

                };
                quizDetails.Add(data);
            }

            response = new GetQuizListVm()
            {
                IsSuccess = true,
                ActionCall = "GET_QUIZ_LIST",
                Quizzes = quizDetails, 
                TotalQuizzes = quizDetails.Count,
                Username = username,
                Message = $"Quiz List fetched successfully",
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new GetQuizListVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "GET_QUIZ_LIST",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    public async Task<SubmitAnswerVm> SubmitAnswer(string quizReference, int questionId, int optionId, string username)
    {
        var response = new SubmitAnswerVm();
        try
        {
            var questions = await _pathExplorerMssqlDbContext.Questions.FirstOrDefaultAsync(x=>x.Id.Equals(questionId));
            if (questions == null)
            {
                return response = new SubmitAnswerVm
                {
                    IsSuccess = false,
                    Message = "Invalid question id provided",
                    ActionCall = "SUBMIT_ANSWER",
                };
            }
            var corrrectOptionId = questions.Options.FirstOrDefault(x => x.IsAnswer);
            var quiz = await _pathExplorerMssqlDbContext.Quizzes.FirstOrDefaultAsync(x => x.Reference.Equals(quizReference));
            if (quiz == null)
            {
                return response = new SubmitAnswerVm
                {
                    IsSuccess = false,
                    Message = "Invalid quiz reference provided",
                    ActionCall = "SUBMIT_ANSWER",
                };
            }
            if(!quiz.Username.ToLower().Equals(username.ToLower()))
            {
                return response = new SubmitAnswerVm
                {
                    IsSuccess = false,
                    Message = "Quiz reference does not match username provided",
                    ActionCall = "SUBMIT_ANSWER",
                };
            }
            var questionOption = await _pathExplorerMssqlDbContext.QuestionOptions.FirstOrDefaultAsync(x => x.Id.Equals(optionId));
            if (questionOption == null)
            {
                return response = new SubmitAnswerVm
                {
                    IsSuccess = false,
                    Message = "Invalid option Id provided",
                    ActionCall = "SUBMIT_ANSWER",
                };
            }
            if(quiz.QuizDetails.Where(x=>x.QuestionId.Equals(questionId)).Any())
            {
                var record = quiz.QuizDetails.Where(x => x.QuestionId.Equals(questionId)).FirstOrDefault();
                return response = new SubmitAnswerVm
                {
                    IsSuccess = false,
                    OptionId = record.OptionId,
                    OptionScore = record.Option.score,
                    QuestionId = questionId,
                    QuizReference = quizReference,
                    CorrectOptionId = corrrectOptionId.Id,
                    Message = "You have already answered this question",
                    ActionCall = "SUBMIT_ANSWER",
                };
            }
            var new_record = new QuizDetail()
            {
                OptionId = optionId,
                QuestionId = questionId,
                Score = questionOption.score,
                QuizId = quiz.Id,
                
            };
            await _pathExplorerMssqlDbContext.AddAsync(new_record);
            quiz.QuizStatus = "INCOMPLETE";
            await _pathExplorerMssqlDbContext.SaveChangesAsync();
           
            response = new SubmitAnswerVm()
            {
                IsSuccess = true,
                OptionId = optionId,
                OptionScore = questionOption.score, 
                QuestionId = questionId,
                QuizReference = quizReference,
                CorrectOptionId = corrrectOptionId.Id,
                ActionCall = "SUBMIT_ANSWER",
                Message = $"Quiz answer submitted successfully"
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new SubmitAnswerVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "SUBMIT_ANSWER",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    public async Task<QuizDetailVm> SubmitQuiz(string QuizReference)
    {
        var response = new QuizDetailVm();
        try
        {
            var quiz = await _pathExplorerMssqlDbContext.Quizzes.FirstOrDefaultAsync(x => x.Reference.Equals(QuizReference));
            if (quiz is null)
            {
                return response = new QuizDetailVm
                {
                    IsSuccess = false,
                    QuizReference = QuizReference,
                    Message = "Invalid Quiz Reference provided",
                    ActionCall = "SUBMIT_QUIZ",
                };
            }
            quiz.QuizStatus = "COMPLETED";
            await _pathExplorerMssqlDbContext.SaveChangesAsync();

            int totalScore = quiz.QuizDetails.Sum(x => x.Score);
            var remark = "";
            if (totalScore > 0 && totalScore < 40)
            {
                remark = "FAILED";
            }
            else if (totalScore >= 40 && totalScore < 60)
            {
                remark = "Fair, You can do better";
            }
            else if (totalScore >= 60 && totalScore < 80)
            {
                remark = "Bravo, You can still do better";
            }
            else if (totalScore >= 80 && totalScore < 90)
            {
                remark = "Wow, Fantastic";
            }
            else if (totalScore >= 90)
            {
                remark = "You are a Genius";
            }
            response = new QuizDetailVm()
            {
                IsSuccess = true,
                Status = quiz.QuizStatus,
                QuizReference = QuizReference,
                Username = quiz.Username,
                TotalScore = totalScore,
                Remark = remark,
                Message = $"Quiz {QuizReference} submitted successfully",
                Id = quiz.Id,
                DateCreated = quiz.DateCreated,
                ActionCall = "SUBMIT_QUIZ",
            };
            return response;
        }
        catch (Exception ex)
        {
            response = new QuizDetailVm
            {
                IsSuccess = false,
                Message = $"Process could not be completed",
                StatusCode = "EXPT",
                ActionCall = "SUBMIT_QUIZ",
            };
            _logger.LogError($"Exception: {ex.Message} | Action: {response.ActionCall}");
            return response;
        }
    }

    private async Task<string> GenerateReference()
    {

        Random random = new Random();
        int randomNumber = random.Next(10000000, 99999999);

        // Fetch the quiz with ref generated
        var quiz = await _pathExplorerMssqlDbContext.Quizzes.Where(x => x.Reference.Equals(randomNumber.ToString())).FirstOrDefaultAsync();

        while (quiz != null)
        {
            randomNumber = random.Next(10000000, 99999999);
        }
       
        return randomNumber.ToString();

    }
}
