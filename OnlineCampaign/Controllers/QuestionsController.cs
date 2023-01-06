using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCampaign.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Options;
using OnlineCampaign.Enums;
using OnlineCampaign.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace OnlineCampaign.Controllers
{
    [ApiController]
    [Route("api/onlineCampaign/question")]
    public class QuestionsController : ControllerBase
    {

        [HttpPost]

        public async Task<ActionResult<Question>> CreateData(CreateQuestionDto input)
        {
            using (var context = new OnlineCampaignContext())

                try
                {
                    var id = 0;
                    if (context.Questions.ToList().Count == 0)
                    {
                        id = 1;

                    }
                    else
                    {
                        id = context.Questions.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
                    }
                    context.Questions.Add(new Question()
                    {
                        Id = id,
                        Name = input.Name,
                        QuestionType = input.QuestionType,
                        IsPublished = input.IsPublished

                    });
                    context.SaveChanges();

                    var response = new QuestionResponseDto()
                    {
                        Id = id,
                        Name = input.Name,
                        QuestionType = input.QuestionType,
                        IsPublished = input.IsPublished
                    };
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
        }
        [HttpGet]
        public async Task<ActionResult<List<Question>>> GetListDataAsync()
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    IList<QuestionResponseDto> questionList = new List<QuestionResponseDto>();
                    questionList = context.Questions.ToList()
                         .Select(x => new QuestionResponseDto()
                         {
                             Id = x.Id,
                             Name = x.Name,
                             QuestionType = x.QuestionType,
                             IsPublished = x.IsPublished

                         }).ToList<QuestionResponseDto>();
                    //var listData = context.Questions.ToList();
                    //foreach (var item in listData)
                    //{
                    //    var data = new QuestionResponseDto()
                    //    {
                    //        Id = item.Id,
                    //        Name = item.Name,
                    //        QuestionType = item.QuestionType,
                    //        IsPublished = item.IsPublished
                    //    };
                    //    questionList.Add(data);
                    //}
                    return StatusCode(StatusCodes.Status200OK, questionList);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }
        }
        [HttpGet]
        [Route("{id}")]

        public async Task<ActionResult> GetDatabyQuestionId(int id)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var questions = context.Questions.Where(x => x.Id == id)
                               .Select(x => new QuestionResponseDto()
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   QuestionType = x.QuestionType,
                                   IsPublished = x.IsPublished
                               }).FirstOrDefault();
                    return StatusCode(StatusCodes.Status200OK, questions);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
        [HttpPut]
        public async Task<ActionResult<UpdateQuestionDto>> UpdateData(int id,[FromBody] UpdateQuestionDto input)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var existingQuestion = context.Questions.Where(x => x.Id == id)
                                                    .FirstOrDefault();
                    if (existingQuestion != null)
                    {
                        if (input.Name != existingQuestion.Name)
                        {
                            existingQuestion.Name = input.Name;
                        }
                        if (input.QuestionType != existingQuestion.QuestionType)
                        {
                            existingQuestion.QuestionType = input.QuestionType;
                        }
                        if (input.IsPublished != existingQuestion.IsPublished)
                        {
                            existingQuestion.IsPublished = input.IsPublished;
                        }
                        context.Questions.Update(existingQuestion);
                        context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, existingQuestion);

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
                    }
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }

        }
        [HttpDelete]
        [Route("{id}")]

        public async Task<ActionResult> DeleteData(int id)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var existingQuestion = context.Questions.Where(x => x.Id == id)
                                     .FirstOrDefault();
                    if (existingQuestion != null)
                    {
                        context.Questions.Remove(existingQuestion);
                        context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, "Record Successfully Deleted");

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
                    }
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }
    }

}




