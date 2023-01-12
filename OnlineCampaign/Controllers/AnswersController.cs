using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCampaign.Enums;
using OnlineCampaign.Models;
using OnlineCampaign.Models.Ddtos;
using OnlineCampaign.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineCampaign.Controllers
{
    [ApiController]
    [Route("api/onlineCampaign/answer")]
    public class AnswersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Answer>> CreateData(CreateAnswerDto input)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var id = 0;
                    if (context.Answers.ToList().Count == 0)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = context.Answers.OrderByDescending(x => x.AnswerId).FirstOrDefault().AnswerId + 1;
                    }
                    context.Answers.Add(new Answer()
                    {
                        AnswerId = id,
                        AnswerValue = input.AnswerValue,
                        QuestionId = input.QuestionId,
                        OptionId = input.OptionId

                    });
                    context.SaveChanges();

                    var response = new AnswerResponseDto()
                    {
                        AnswerId = id,
                        AnswerValue = input.AnswerValue,
                        OptionId = input.OptionId,
                        QuestionId = input.QuestionId

                    };
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<Answer>>> GetListDataAsync()
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    IList<AnswerResponseDto> answerList = new List<AnswerResponseDto>();
                    answerList = context.Answers.ToList()
                         .Select(x => new AnswerResponseDto()
                         {
                             AnswerId = x.AnswerId,
                             AnswerValue = x.AnswerValue,
                             QuestionId = x.QuestionId,
                             OptionId = x.OptionId

                         }).ToList<AnswerResponseDto>();

                    return StatusCode(StatusCodes.Status200OK, answerList);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<ActionResult> GetDatabyAnswerId(int id)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var answers = context.Answers.Where(x => x.AnswerId == id)
                               .Select(x => new AnswerResponseDto()
                               {
                                   AnswerId = x.AnswerId,
                                   AnswerValue = x.AnswerValue,
                                   QuestionId = x.QuestionId,
                                   OptionId = x.OptionId

                               }).FirstOrDefault();
                    return StatusCode(StatusCodes.Status200OK, answers);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<UpdateAnswerDto>> UpdateData(int id, [FromBody] UpdateAnswerDto input)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var existingAnswer = context.Answers.Where(x => x.AnswerId == id)
                                                    .FirstOrDefault();
                    if (existingAnswer != null)
                    {
                        if (input.AnswerValue != existingAnswer.AnswerValue)
                        {
                            existingAnswer.AnswerValue = input.AnswerValue;
                        }
                        context.Answers.Update(existingAnswer);
                        context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, existingAnswer);
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
                    var existingAnswer = context.Answers.Where(x => x.AnswerId == id)
                                     .FirstOrDefault();
                    if (existingAnswer != null)
                    {
                        context.Answers.Remove(existingAnswer);
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


        // Relation On Both Tables Answers/ Options/ Questions

        //[HttpPost]
        //[Route("createlist")]
        //public async Task<ActionResult<List<Answer>>> CreateRelationData([FromBody] List<Answer> input)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            foreach (var data in input)
        //            {
        //                context.Answers.Add(new Answer()
        //                {
        //                    AnswerId = data.AnswerId,
        //                    AnswerValue = data.AnswerValue,
        //                    QuestionId = data.QuestionId,
        //                    OptionId = data.OptionId,

        //                    Option = new Option()
        //                    {
        //                        OptionId = data.OptionId,
        //                        QuestionId = answer.Option.QuestionId,
        //                        QuestionType = answer.Option.QuestionType,
        //                        OptionName = answer.Option.OptionName,
        //                        OptionValue = answer.Option.OptionValue,

        //                    },
        //                    Question = new Question()
        //                    {
        //                        Id = data.QuestionId,
        //                        Name = answer.Option.Question.Name,
        //                        QuestionType = answer.Option.Question.QuestionType,
        //                        IsPublished = answer.Option.Question.IsPublished
        //                    }
        //                });

        //                context.SaveChanges();

        //            }
        //            return StatusCode(StatusCodes.Status200OK);
        //        }
        //        catch (Exception ex)
        //        {

        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }
        //    }


        //}

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<ActionResult<Answer>> GetRelationDatabyAllList(int id)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            var dataByOptionId = (from answer in context.Answers.Where(answer => answer.AnswerId == id).ToList()
        //                                  join option in context.Options on answer.OptionId equals option.OptionId
        //                                  join question in context.Questions on answer.QuestionId equals question.Id
        //                                  select new
        //                                  {
        //                                      AnswerId = answer.AnswerId,
        //                                      AnswerValue = answer.AnswerValue,
        //                                      OptionId = answer.OptionId,
        //                                      QuestionId = answer.QuestionId,

        //                                      Option = new Option()
        //                                      {
        //                                          OptionId = answer.OptionId,
        //                                          QuestionId = answer.QuestionId,
        //                                          QuestionType = answer.QuestionType,
        //                                          OptionName = answer.OptionName,
        //                                          OptionValue = answer.Option.OptionValue,
        //                                      },
        //                                      Question = new Question()
        //                                      {
        //                                          Id = answer.Question.Id,
        //                                          Name = answer.Question.Name,
        //                                          QuestionType = answer.Question.QuestionType,
        //                                          IsPublished = answer.Question.IsPublished

        //                                      }
        //                                  }).ToList();
        //            return Ok(dataByOptionId);
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

        //        }


        //    }
        //}



        //[HttpDelete]
        //[Route("{id}")]

        //public async Task<ActionResult> DeleteRElationalData(int id)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            var existingAnswer = (from answer in context.Answers
        //                                  join option in context.Options
        //                                  on answer.OptionId equals option.OptionId
        //                                  where answer.OptionId == id
        //                                  join question in context.Questions
        //                                  on answer.QuestionId equals question.Id
        //                                  where answer.QuestionId == id
        //                                  select answer).ToList();
        //            if (existingAnswer != null)
        //            {
        //                context.Answers.RemoveRange(existingAnswer);
        //                context.SaveChanges();
        //                return StatusCode(StatusCodes.Status200OK, "Record Successfully Deleted");

        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }
        //    }

        //}








    }
}
